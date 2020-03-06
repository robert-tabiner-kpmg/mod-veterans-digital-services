using System.Linq;
using System.Threading.Tasks;
using Forms.Core.Extensions;
using Forms.Core.Forms;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Physical;
using Forms.Core.Models.Pages;
using Forms.Core.NodeHandlers.PhysicalNodeHandlers.Interfaces;
using Forms.Core.NodeHandlers.PhysicalNodeHandlers.Models;
using Forms.Core.NodeHandlers.PhysicalNodeHandlers.Models.TaskItem;
using Forms.Core.Services.Interfaces;
using Graph;
using Graph.Models;

namespace Forms.Core.NodeHandlers.PhysicalNodeHandlers.Handlers
{
    public class TaskQuestionPageHandler : IPhysicalNodeHandler
    {
        public PhysicalFormNodeType PhysicalFormNodeType => PhysicalFormNodeType.TaskQuestionPage;
        private readonly IStaticFormProvider _staticFormProvider;
        private readonly IFormService _formService;

        public TaskQuestionPageHandler(IStaticFormProvider staticFormProvider, IFormService formService)
        {
            _staticFormProvider = staticFormProvider;
            _formService = formService;
        }

        public async Task<PhysicalResponse> Handle(FormType formType, string formKey, GraphNode<Key, FormNode> node)
        {
            var questionPageFormNode = node.AssertType<TaskQuestionPageFormNode>();
            
            var answersDictionary = questionPageFormNode.Questions.ToDictionary(x => x.Id, x => x.Answer);
            
            var questionPage = (TaskQuestionPage)_staticFormProvider.GetPage(formType, StaticKey.ForTaskQuestionPage(questionPageFormNode.TaskId, questionPageFormNode.PageId));

            var (questionNumber, totalQuestions) = await _formService.GetQuestionNumber(formKey, node.Key.Value);
            
            return new TaskPageResponse
            {
                NodeId = node.Key.Value,
                TaskQuestionPage = questionPage,
                Answers = answersDictionary,
                QuestionCount = totalQuestions,
                QuestionNumber = questionNumber
            };
        }
    }
}