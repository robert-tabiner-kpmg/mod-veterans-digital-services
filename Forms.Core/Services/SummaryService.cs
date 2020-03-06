using System.Collections.Generic;
using System.Linq;
using Forms.Core.Exceptions;
using Forms.Core.Extensions;
using Forms.Core.Forms;
using Forms.Core.Models.Display;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Decision;
using Forms.Core.Models.InFlight.Decision.Ghost;
using Forms.Core.Models.InFlight.Physical;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Static;
using Forms.Core.Services.Interfaces;
using Graph.Models;

namespace Forms.Core.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly IStaticFormProvider _staticFormProvider;

        public SummaryService(IStaticFormProvider staticFormProvider)
        {
            _staticFormProvider = staticFormProvider;
        }

        public IDictionary<string, IList<TaskGrouping>> GetFormSummary(Graph<Key, FormNode> graph, FormType formType)
        {
            var taskList = graph.Nodes.FindByKey(Key.ForTaskList());

            var form = _staticFormProvider.GetForm(formType);

            var result = new Dictionary<string, IList<TaskGrouping>>();
            
            foreach (var task in form.Tasks)
            {
                var taskNode = taskList.Neighbors.FindByKey(Key.ForDecisionTaskRouter(task.Id));
                result.Add(task.Id, GetTaskSummary(taskNode, task));
            }

            return result;
        }

        public IList<TaskGrouping> GetTaskSummary(GraphNode<Key, FormNode> taskNode, Task task)
        {
            var taskRouter = taskNode.AssertType<TaskRouterFormNode>();

            var orderedItemIds = taskRouter.TaskItemIds.OrderBy(x => x).ToList();
            
            var taskItemRouters = taskNode.Neighbors.Where(GraphNodePredicates.IsTaskItemRouterNode);

            return taskItemRouters.Select(taskItem => GetTaskItemSummary(taskItem, task, orderedItemIds.Count == 1 ? (int?)null : orderedItemIds.IndexOf(taskItem.AssertType<TaskItemRouterFormNode>().RepeatIndex))).ToList();
        }

        public TaskGrouping GetTaskItemSummary(GraphNode<Key, FormNode> taskItemNode, Task task, int? repeatIndex = null)
        {
            taskItemNode.AssertType<TaskItemRouterFormNode>();
            
            var taskItemGroup = new TaskGrouping
            {
                Name = CreateDisplayName(task.Name, repeatIndex),
                NodeId = taskItemNode.Key.Value,
                Items = new List<BaseDisplayItem>()
            };

            var currentItem = taskItemNode.Neighbors.First(GraphNodePredicates.IsAnyTaskItemNode);

            while (currentItem != null)
            {
                AddTaskDisplayItem(taskItemGroup, task.TaskItems, currentItem);

                if (currentItem.IsSubTaskRouterNode(out var subTaskRouterFormNode))
                {
                    var postSubTask = currentItem.Neighbors.First(GraphNodePredicates.IsPostSubTaskNode);
                    currentItem = postSubTask.Neighbors.FirstOrDefault(x =>
                        x.IsAnyTaskItemNode() && !(x.IsSubTaskRouterNode(out var str) &&
                                                   str.SubTaskId == subTaskRouterFormNode.SubTaskId));
                }
                else
                {
                    currentItem = currentItem.Neighbors.FirstOrDefault(x => x.IsAnyTaskItemNode());
                }
            }

            return taskItemGroup;
        }

        private void AddTaskDisplayItem(TaskGrouping taskItemGroup, List<ITaskItem> taskItems, GraphNode<Key, FormNode> currentFormNode)
        {
            if (!currentFormNode.IsAnyTaskItemNode())
                throw new UnexpectedNodeTypeException<TaskQuestionPageFormNode, SubTaskRouterFormNode, TaskQuestionGhost>(currentFormNode.Value);

            if (currentFormNode.IsTaskQuestionGhost()) return;

            if (currentFormNode.IsTaskQuestionPageNode(out var taskQuestionPageFormNode))
            {
                var taskItem = (TaskQuestionPage)taskItems.First(x => x.Id == taskQuestionPageFormNode.PageId);

                foreach (var question in taskItem.Questions)
                {
                    taskItemGroup.Items.Add(new TaskDisplayItem
                    {
                        NodeId = currentFormNode.Key.Value,
                        Header = taskItem.Header,
                        QuestionText = question.Label,
                        Question = taskQuestionPageFormNode.Questions.FirstOrDefault(x => x.Id == question.Id)
                    });
                }
            }

            if (currentFormNode.IsSubTaskRouterNode(out var subTaskRouterFormNode))
            {
                var taskItem = (SubTask)taskItems.First(x => x.Id == subTaskRouterFormNode.SubTaskId);

                var subTaskItems = currentFormNode.Neighbors.Where(GraphNodePredicates.IsSubTaskItemRouterNode);
                var orderedItemIds = subTaskRouterFormNode.TaskItemIds.OrderBy(x => x).ToList();

                
                foreach (var subTaskItem in subTaskItems)
                {
                    var subTaskItemIndex = orderedItemIds.IndexOf(subTaskItem.AssertType<SubTaskItemRouterFormNode>().RepeatIndex);
                    
                    var subTaskGroup = new TaskGrouping
                    {
                        Name = CreateDisplayName(taskItem.DisplayName, orderedItemIds.Count == 1 ? (int?)null : subTaskItemIndex),
                        NodeId = currentFormNode.Key.Value,
                        Items = new List<BaseDisplayItem>()
                    };
                
                    taskItemGroup.Items.Add(subTaskGroup);
                    
                    var currentItem = subTaskItem.Neighbors.First(GraphNodePredicates.IsAnyTaskItemNode);

                    while (currentItem != null)
                    {
                        AddTaskDisplayItem(subTaskGroup, taskItem.TaskItems, currentItem);

                        if (currentItem.IsSubTaskRouterNode(out var childSubTaskRouterFormNode))
                        {
                            var postSubTask = currentItem.Neighbors.First(GraphNodePredicates.IsPostSubTaskNode);
                            currentItem = postSubTask.Neighbors.FirstOrDefault(x =>
                                x.IsAnyTaskItemNode() && !(x.IsSubTaskRouterNode(out var str) &&
                                                           str.SubTaskId == childSubTaskRouterFormNode.SubTaskId));
                        }
                        else
                        {
                            currentItem = currentItem.Neighbors.FirstOrDefault(x => x.IsAnyTaskItemNode());
                        }
                    } 
                }
            }
        }

        private string CreateDisplayName(string taskName, int? repeatIndex = null)
        {
            return string.Concat(taskName, repeatIndex != null ? $" {repeatIndex + 1}" : string.Empty);
        }
    }
}