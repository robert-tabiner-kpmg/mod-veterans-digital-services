using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Constants;
using Forms.Core.EffectHandlers.Interfaces;
using Forms.Core.Forms;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Physical;
using Forms.Core.NodeHandlers.DecisionNodeHandlers.Interfaces;
using Forms.Core.NodeHandlers.PhysicalNodeHandlers.Interfaces;
using Forms.Core.NodeHandlers.PhysicalNodeHandlers.Models;
using Forms.Core.NodeHandlers.PhysicalNodeHandlers.Models.PostTask;
using Forms.Core.NodeHandlers.PhysicalNodeHandlers.Models.PreTask;
using Forms.Core.NodeHandlers.PhysicalNodeHandlers.Models.TaskItem;
using Forms.Core.Services.Interfaces;
using Forms.Core.SubmissionHandlers.Interfaces;
using Forms.Web.Exceptions;
using Forms.Web.Models.ViewModels;
using Graph.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forms.Web.Controllers
{
    [Authorize]
    public class FormController : BaseController
    {
        private readonly IFormService _formService;
        private readonly IPhysicalNodeHandlerStrategy _physicalNodeHandlerStrategy;
        private readonly IDecisionNodeHandlerStrategy _decisionNodeHandlerStrategy;
        private readonly IEffectHandlerStrategy _effectHandlerStrategy;
        private readonly ISubmissionHandlerStrategy _submissionHandlerStrategy;
        private readonly IStaticFormProvider _staticFormProvider;

        public FormController(IFormService formService,
            IPhysicalNodeHandlerStrategy physicalNodeHandlerStrategy,
            IDecisionNodeHandlerStrategy decisionNodeHandlerStrategy,
            IEffectHandlerStrategy effectHandlerStrategy,
            ISubmissionHandlerStrategy submissionHandlerStrategy,
            IStaticFormProvider staticFormProvider)
        {
            _formService = formService;
            _physicalNodeHandlerStrategy = physicalNodeHandlerStrategy;
            _decisionNodeHandlerStrategy = decisionNodeHandlerStrategy;
            _effectHandlerStrategy = effectHandlerStrategy;
            _submissionHandlerStrategy = submissionHandlerStrategy;
            _staticFormProvider = staticFormProvider;
        }

        [HttpGet("[controller]/{nodeId}")]
        public async Task<IActionResult> Node(string nodeId, bool returnToSummary = false)
        {
            var node = await _formService.GetFormNode(GetFormId(), nodeId);

            var viewNode = node.Value.IsDecisionNode ? await _decisionNodeHandlerStrategy.Handle(node) : node;

            return await HandlePhysicalViewNode(viewNode, returnToSummary);
        }

        [HttpPost("[controller]/{currentNodeId}")]
        public async Task<IActionResult> Save(string currentNodeId, InFlightPage model)
        {
            var formId = GetFormId();
            var formType = GetFormTypeFromFormId();

           var (isValid, errors) = await _formService.ValidateQuestionPage(formType, formId, model);
           if (!isValid)
           {
               var viewNode = await _formService.GetFormNode(formId, model.NodeId);
               var currentAnswers = model.Questions.ToDictionary(x => x.Id, x => x.Answer);
               return await HandlePhysicalViewNode(viewNode, model.ReturnToSummary, currentAnswers, errors);
           }
            
            await _formService.SavePage(formId, model);
            await _effectHandlerStrategy.Handle(formType, formId, model.NodeId);
            
            var node = await _formService.GetFormNode(formId, model.NodeId);

            if (model.ReturnToSummary)
            {
                var nodeAsQuestionPage = (TaskQuestionPageFormNode) node.Value;
                return RedirectToAction("Node", new { nodeId = Key.ForTaskSummary(nodeAsQuestionPage.TaskId, nodeAsQuestionPage.RepeatIndices.First()).Value});
            }

            var nextNode = node.Neighbors.First();

            return RedirectToAction("Node", new {nodeId = nextNode.Key.Value});
        }

        private async Task<IActionResult> HandlePhysicalViewNode(GraphNode<Key, FormNode> viewNode,
            bool returnToSummary = false,
            IDictionary<string, Answer> inFlightAnswers = null,
            IDictionary<string, string> inFlightErrors = null)
        {
            var viewData = await _physicalNodeHandlerStrategy.Handle(GetFormTypeFromFormId(), GetFormId(), viewNode);
            viewData.CurrentNodeId = viewNode.Key.Value;
            ViewData[FormConstants.TempDataErrors] = inFlightErrors;
            ViewData[FormConstants.TempDataAnswers] = inFlightAnswers;
            ViewData[FormConstants.TempDataReturnToSummary] = returnToSummary;
            ViewData[FormConstants.BackButtonProps] = new BackButtonProps(viewNode.Key.Value, viewData.ShowBackButton);
            ViewData[FormConstants.ViewDataFormTitle] = _staticFormProvider.GetFormName(GetFormTypeFromFormId());

            return viewData switch
            {
                TaskListResponse taskListResponse => View("TaskList", taskListResponse),
                TaskPageResponse taskQuestionResponse => View("TaskQuestionPage", taskQuestionResponse),
                TaskSummaryResponse taskSummaryResponse => View("TaskSummary", taskSummaryResponse),
                RepeatTaskResponse repeatTaskResponse => View("RepeatTask", repeatTaskResponse),
                ConsentPageResponse consentPageResponse => View("ConsentPage", consentPageResponse),
                PreTaskResponse preTaskResponse => View("PreTask", preTaskResponse),
                RepeatSubTaskResponse repeatSubTaskResponse => View("RepeatSubTask", repeatSubTaskResponse),
                _ => throw new NotImplementedException()
            };
        }

        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> RepeatTask(string taskId)
        {
            var newRepeatedSectionId = await _formService.RepeatTask(GetFormTypeFromFormId(), GetFormId(), taskId);
            return RedirectToAction("Node", new {nodeId = newRepeatedSectionId });
        }
        
        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> RepeatSubTask(string nodeId)
        {
            var newRepeatedSectionId = await _formService.RepeatSubTask(GetFormTypeFromFormId(), GetFormId(), nodeId);
            return RedirectToAction("Node", new {nodeId = newRepeatedSectionId });
        }
        
        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> DeleteTask(string taskNodeId, string taskItemNodeId)
        {
            var nextNodeId = await _formService.DeleteTaskItem(GetFormTypeFromFormId(), GetFormId(), taskNodeId, taskItemNodeId);
            return RedirectToAction("Node", new {nodeId = nextNodeId });
        }
        
        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> DeleteSubTask(string taskNodeId, string taskItemNodeId)
        {
            var nextNodeId = await _formService.DeleteSubTaskItem(GetFormTypeFromFormId(), GetFormId(), taskNodeId, taskItemNodeId);
            return RedirectToAction("Node", new {nodeId = nextNodeId });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GoBack(string nodeId)
        {
            var nextNodeId = await _formService.GetPreviousNode(GetFormId(), nodeId);
            return RedirectToAction("Node", new {nodeId = nextNodeId });
        }

        [HttpPost("[action]/{currentNodeId}")]
        public async Task<IActionResult> SubmitForm(string currentNodeId, InFlightPage model)
        {
            var formType = GetFormTypeFromFormId();
            var formId = GetFormId();
            
            var (isValid, errors) = await _formService.ValidateQuestionPage(formType, formId, model);
            if (!isValid)
            {
                var viewNode = await _formService.GetFormNode(formId, model.NodeId);
                var currentAnswers = model.Questions.ToDictionary(x => x.Id, x => x.Answer);
                return await HandlePhysicalViewNode(viewNode, false, currentAnswers, errors);
            }
            
            var response = await _submissionHandlerStrategy.Handle(GetFormTypeFromFormId(), GetFormId(), model);

            return View("Confirmation", response);
        }
        
        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult TermsAndConditions()
        {
            var formType = GetFormTypeFromFormId();
            var termsAndConditions = _staticFormProvider.GetTermsAndConditions(formType);
            ViewData[FormConstants.ViewDataFormTitle] = _staticFormProvider.GetFormName(GetFormTypeFromFormId());

            return View("TermsAndConditions", termsAndConditions);
        }

        private string GetFormId()
        {
            return GetCookie(FormConstants.InFlightFormCookieName);
        }

        private FormType GetFormTypeFromFormId()
        {
            var formId = GetFormId() ?? GetCookie(FormConstants.RequestedFormCookieName);
            if (formId is null) return FormType.Afcs;
            var formTypeString = formId.Split("-").LastOrDefault();
            return GetFormType(formTypeString);
        }

        private FormType GetFormType(string formType)
        {
            if (!Enum.TryParse(formType, true, out FormType type))
                throw new FormNotRecognisedException(formType);

            return type;
        }
    }
}