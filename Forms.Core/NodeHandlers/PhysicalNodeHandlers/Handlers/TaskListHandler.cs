using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forms.Core.Exceptions;
using Forms.Core.Extensions;
using Forms.Core.Forms;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Decision;
using Forms.Core.Models.InFlight.Decision.Ghost;
using Forms.Core.Models.InFlight.Physical;
using Forms.Core.Models.Pages;
using Forms.Core.NodeHandlers.PhysicalNodeHandlers.Interfaces;
using Forms.Core.NodeHandlers.PhysicalNodeHandlers.Models;
using Forms.Core.Services.Interfaces;
using Graph;
using Graph.Analysis.Traversal;
using Graph.Models;

namespace Forms.Core.NodeHandlers.PhysicalNodeHandlers.Handlers
{
    public class TaskListHandler : IPhysicalNodeHandler
    {
        public PhysicalFormNodeType PhysicalFormNodeType => PhysicalFormNodeType.TaskList;
        private readonly IStaticFormProvider _staticFormProvider;

        public TaskListHandler(IStaticFormProvider staticFormProvider)
        {
            _staticFormProvider = staticFormProvider;
        }


        public Task<PhysicalResponse> Handle(FormType formType, string formKey, GraphNode<Key, FormNode> node)
        {
            node.AssertType<TaskListFormNode>();
            
            var taskRouters = node.Neighbors.Where(GraphNodePredicates.IsTaskRouterNode);

            var tasks = new List<TaskListTask>();
            
            foreach (var taskRouter in taskRouters)
            {
                var taskNode = taskRouter.AssertType<TaskRouterFormNode>();

                var staticTask = _staticFormProvider.GetTask(formType, StaticKey.ForTaskNode(taskNode.TaskId));

                var taskListTask = new TaskListTask
                {
                    Name = staticTask.Name,
                    TaskRouterNodeKey = taskRouter.Key.Value,
                    TaskId = staticTask.Id,
                    GroupNameIndex = staticTask.GroupNameIndex,
                    IsComplete = IsTaskComplete(taskRouter),
                };
                
                tasks.Add(taskListTask);
            }

            var isTaskCompleteList = tasks.Select(x => (x.TaskId, x.IsComplete)).ToList();
            foreach (var task in tasks)
            {
                var staticTask = _staticFormProvider.GetTask(formType, StaticKey.ForTaskNode(task.TaskId));
                task.IsHidden = staticTask.HiddenWhen?.Invoke(isTaskCompleteList) ?? false;
            }
            
            return Task.FromResult(new TaskListResponse
            {
                FormName = _staticFormProvider.GetFormName(formType),
                Tasks = tasks,
                TaskGroupNames = _staticFormProvider.GetTaskGroups(formType)
            } as PhysicalResponse);
        }

        private bool IsTaskComplete(GraphNode<Key, FormNode> taskRouter)
        {
            taskRouter.AssertType<TaskRouterFormNode>();

            // We assume all of our pages are complete and traverse each task item until we find a page which is incomplete
            var hasUnansweredItem = false;
            
            var traversal = new GraphTraversalBuilder<Key, FormNode>(TraversalType.BreadthFirstSearch, false)
                .WithOnNodeDequeuedHandler((node, output) =>
                {
                    if (!node.Node.IsTaskQuestionPageNode(out var taskQuestionPageFormNode)) return;
                    if (taskQuestionPageFormNode.PageStatus == PageStatus.Answered) return;
                    
                    // If we found an unanswered page then we're finished
                    hasUnansweredItem = true;
                    output.IsComplete = true;
                })
                .WithOnNodeDiscoveredHandler((props, output) =>
                {
                    // We don't need to queue past the summary
                    if (props.Node.IsAnyTaskSummaryNode())
                    {
                        output.ShouldQueue = false;
                    }
                })
                .Build();

            var taskItemRouters = taskRouter.Neighbors.Where(GraphNodePredicates.IsTaskItemRouterNode);
            
            foreach (var taskItem in taskItemRouters)
            {
                traversal.Run(taskItem);
                if (hasUnansweredItem) break;
            }

            return !hasUnansweredItem;
        }
    }
}