using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forms.Core.Exceptions;
using Forms.Core.Extensions;
using Forms.Core.Forms;
using Forms.Core.Models.Display;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Decision;
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
    public class TaskSummaryHandler : IPhysicalNodeHandler
    {
        public PhysicalFormNodeType PhysicalFormNodeType => PhysicalFormNodeType.TaskSummary;
        private readonly IStaticFormProvider _staticFormProvider;
        private readonly ISummaryService _summaryService;

        public TaskSummaryHandler(IStaticFormProvider staticFormProvider, ISummaryService summaryService)
        {
            _staticFormProvider = staticFormProvider;
            _summaryService = summaryService;
        }
        
        public Task<PhysicalResponse> Handle(FormType formType, string formKey, GraphNode<Key, FormNode> node)
        {
            var summaryFormNode = node.AssertType<TaskSummaryFormNode>();

            var postTaskNode = node.Neighbors.FindByKey(Key.ForPostTaskPage(summaryFormNode.TaskId));

            var taskList = postTaskNode.Neighbors.FindByKey(Key.ForTaskList());
            var taskRouter = taskList.Neighbors.FindByKey(Key.ForDecisionTaskRouter(summaryFormNode.TaskId)).AssertType<TaskRouterFormNode>();
            
            var taskItemRouter = node.Neighbors.First(GraphNodePredicates.IsTaskItemRouterNode);
            var taskItemIds = taskRouter.TaskItemIds.OrderBy(x => x).ToList();
            var taskItemRouterValue = taskItemRouter.AssertType<TaskItemRouterFormNode>();
            var repeatIndex = taskItemIds.IndexOf(taskItemRouterValue.RepeatIndex);
            
            var staticSummaryPage = _staticFormProvider.GetPage(formType, StaticKey.ForTaskSummary(summaryFormNode.TaskId)) as SummaryPage;

            var task = _staticFormProvider.GetTask(formType, StaticKey.ForTaskNode(summaryFormNode.TaskId));
            
            var taskItemGroup = _summaryService.GetTaskItemSummary(taskItemRouter, task);
            var flattenedTaskGroupings = taskItemGroup.FlattenTaskGroupings();
            
            return Task.FromResult(new TaskSummaryResponse
            {
                Page = staticSummaryPage,
                NextNodeId = postTaskNode.Key.Value,
                TaskGroupings = flattenedTaskGroupings,
            } as PhysicalResponse);
        }
    }
}