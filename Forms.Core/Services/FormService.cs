
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
using Forms.Core.Models.Static;
using Forms.Core.Repositories.Interfaces;
using Forms.Core.Services.Interfaces;
using Graph.Analysis.PathFinding;
using Graph.Analysis.Traversal;
using Graph.Models;
using Task = System.Threading.Tasks.Task;

namespace Forms.Core.Services
{
    public class FormService : IFormService
    {
        private readonly IFormRepository _formRepository;
        private readonly IStaticFormProvider _staticFormProvider;

        public FormService(IFormRepository formRepository, IStaticFormProvider staticFormProvider)
        {
            _formRepository = formRepository;
            _staticFormProvider = staticFormProvider;
        }

        public async Task<Graph<Key, FormNode>> InitialiseForm(string formKey, FormType formType)
        {
            var form = _staticFormProvider.GetForm(formType);
            
            var graph = new Graph<Key, FormNode>();

            var formRouterNode = new GraphNode<Key, FormNode>(Key.ForDecisionFormRouter(), new FormRouterFormNode());
            graph.AddNode(formRouterNode);

            var taskListNode = form.TaskListPage is null
                ? new GraphNode<Key, FormNode>(Key.ForTaskList(), new TaskListGhost())
                : new GraphNode<Key, FormNode>(Key.ForTaskList(), new TaskListFormNode());

            graph.AddNode(taskListNode);
            graph.AddDirectedEdge(formRouterNode, taskListNode);

            foreach (var task in form.Tasks)
            {
                AddTask(graph, taskListNode, task, 0);
            }

            await _formRepository.SaveForm(formKey, graph);

            return graph;
        }

        private void AddTask(Graph<Key, FormNode> graph, GraphNode<Key, FormNode> taskListNode, Models.Static.Task task, int repeatIndex)
        {
            if (!taskListNode.IsAnyTaskListNode())
                throw new UnexpectedNodeTypeException<TaskListFormNode, TaskListGhost>(taskListNode.Value);
            
            var taskRouterNode = new GraphNode<Key, FormNode>(Key.ForDecisionTaskRouter(task.Id), new TaskRouterFormNode(task.Id));

            var preTaskNode = task.PreTaskPage is null
                ? new GraphNode<Key, FormNode>(Key.ForPreTaskPage(task.Id), new PreTaskGhost())
                : new GraphNode<Key, FormNode>(Key.ForPreTaskPage(task.Id), new PreTaskFormNode(task.Id));

            var postTaskNode = task.PostTaskPage is null
                ? new GraphNode<Key, FormNode>(Key.ForPostTaskPage(task.Id), new PostTaskGhost())
                : new GraphNode<Key, FormNode>(Key.ForPostTaskPage(task.Id), new PostTaskFormNode(task.Id));

            graph.AddNode(taskRouterNode);
            graph.AddNode(preTaskNode);
            graph.AddNode(postTaskNode);

            var allTaskNodes = AddTaskItem(graph, taskRouterNode, preTaskNode, postTaskNode, task, repeatIndex);

            foreach (var (_, (node, nextKey)) in allTaskNodes)
            {
                if (nextKey is null) continue;
                if (nextKey.Value == Key.EmptyKey().Value) continue;
                var (nextNode, _) = allTaskNodes[nextKey];
                graph.AddDirectedEdge(node, nextNode);
            }

            // Add the Task edges
            graph.AddDirectedEdge(taskListNode, taskRouterNode);
            graph.AddDirectedEdge(taskRouterNode, preTaskNode);
            graph.AddDirectedEdge(taskRouterNode, postTaskNode, EdgeCosts.TaskToPostTaskCost);
            graph.AddDirectedEdge(postTaskNode, taskListNode);
        }

        private Dictionary<Key, (GraphNode<Key, FormNode>, Key)> AddTaskItem(Graph<Key, FormNode> graph, GraphNode<Key, FormNode> taskRouterNode,
            GraphNode<Key, FormNode> preTaskNode, GraphNode<Key, FormNode> postTaskNode, Models.Static.Task task,
            int repeatIndex)
        {
            taskRouterNode.AssertType<TaskRouterFormNode>();
            
            var nodes = new Dictionary<Key, (GraphNode<Key, FormNode>, Key)>();

            var taskItemRouter = new GraphNode<Key, FormNode>(Key.ForDecisionTaskItemRouter(task.Id, repeatIndex), new TaskItemRouterFormNode(task.Id, repeatIndex));
            var summaryNode = task.SummaryPage is null
                ? new GraphNode<Key, FormNode>(Key.ForTaskSummary(task.Id, repeatIndex), new TaskSummaryGhost())
                : new GraphNode<Key, FormNode>(Key.ForTaskSummary(task.Id, repeatIndex), new TaskSummaryFormNode(task.Id));

            graph.AddNode(summaryNode);
            graph.AddNode(taskItemRouter);
            graph.AddDirectedEdge(taskRouterNode, taskItemRouter);
            graph.AddDirectedEdge(summaryNode, taskItemRouter);

            foreach (var taskItem in task.TaskItems)
            {
                if (taskItem is TaskQuestionPage || taskItem is TaskQuestionGhost)
                {
                    var (newNode, nextKey) = AddTaskQuestion(graph, task.Id, taskItem, new[] { repeatIndex });
                    nodes.Add(newNode.Key, (newNode, nextKey));
                    if (nextKey is null)
                    {
                        if (nodes.Remove(newNode.Key))
                        {
                            nodes.Add(newNode.Key, (newNode, Key.EmptyKey()));
                        }
                        graph.AddDirectedEdge(newNode, summaryNode);
                    }
                }

                if (taskItem is SubTask subTask)
                {
                    var newNodes = AddSubTask(graph, task.Id, subTask, new[] { repeatIndex });
                    nodes.AddRange(newNodes);
                    foreach (var (_, (newNode, newNodeNextKey)) in newNodes)
                    {
                        if (newNodeNextKey is null)
                        {
                            if (nodes.Remove(newNode.Key))
                            {
                                nodes.Add(newNode.Key, (newNode, Key.EmptyKey()));
                            }
                            graph.AddDirectedEdge(newNode, summaryNode);
                        }
                    }
                }
            }

            var (firstTaskItemNode, _) = nodes[Key.ForTaskItemPage(task.Id, task.TaskItems.First().Id, new[] { repeatIndex })];
            graph.AddDirectedEdge(taskItemRouter, firstTaskItemNode);
            graph.AddDirectedEdge(preTaskNode, taskItemRouter);

            graph.AddDirectedEdge(postTaskNode, summaryNode);
            graph.AddDirectedEdge(summaryNode, postTaskNode);

            nodes.Add(taskItemRouter.Key, (taskItemRouter, null));

            return nodes;
        }

        private (GraphNode<Key, FormNode>, Key) AddTaskQuestion(Graph<Key, FormNode> graph, string taskId, ITaskItem taskQuestionPage, IEnumerable<int> repeatIndices)
        {
            var isTaskQuestionGhost = taskQuestionPage.GetType() == typeof(TaskQuestionGhost);
            
            var key = Key.ForTaskItemPage(taskId, taskQuestionPage.Id, repeatIndices);

            var node = isTaskQuestionGhost ? 
                new GraphNode<Key, FormNode>(key, new TaskQuestionGhost(taskQuestionPage.Id)) : 
                new GraphNode<Key, FormNode>(key, new TaskQuestionPageFormNode(taskId, taskQuestionPage.Id, repeatIndices));
            
            graph.AddNode(node);
            var nextKey = taskQuestionPage.NextPageId != null ? Key.ForTaskItemPage(taskId, taskQuestionPage.NextPageId, repeatIndices) : null;

            return (node, nextKey);
        }

        private Dictionary<Key, (GraphNode<Key, FormNode>, Key)> AddSubTask(Graph<Key, FormNode> graph, string taskId, SubTask subTask, IEnumerable<int> repeatIndices)
        {
            var nodes = new Dictionary<Key, (GraphNode<Key, FormNode>, Key)>();

            var routerFormNode = new GraphNode<Key, FormNode>(Key.ForTaskItemPage(taskId, subTask.Id, repeatIndices), new SubTaskRouterFormNode(taskId, subTask.Id, repeatIndices));
            var preSubTaskNode = new GraphNode<Key, FormNode>(Key.ForPreSubTaskPage(taskId, subTask.Id, repeatIndices), new PreSubTaskFormNode(taskId, subTask.Id));
            var postSubTaskNode = new GraphNode<Key, FormNode>(Key.ForPostSubTaskPage(taskId, subTask.Id, repeatIndices), new PostSubTaskFormNode(taskId, subTask.Id));

            graph.AddNode(routerFormNode);
            graph.AddNode(preSubTaskNode);
            graph.AddNode(postSubTaskNode);

            var nextNodeKey = subTask.NextPageId != null ? Key.ForTaskItemPage(taskId, subTask.NextPageId, repeatIndices) : null;
            nodes.Add(postSubTaskNode.Key, (postSubTaskNode, nextNodeKey));

            var newNodes = AddSubTaskItem(graph, taskId, subTask, repeatIndices, 0, routerFormNode, preSubTaskNode,
                postSubTaskNode);

            nodes.AddRange(newNodes);

            graph.AddDirectedEdge(routerFormNode, preSubTaskNode);
            graph.AddDirectedEdge(routerFormNode, postSubTaskNode, EdgeCosts.SubTaskToPostSubTaskCost);
            graph.AddDirectedEdge(postSubTaskNode, routerFormNode);

            nodes.Add(routerFormNode.Key, (routerFormNode, Key.EmptyKey()));

            return nodes;
        }

        private Dictionary<Key, (GraphNode<Key, FormNode>, Key)> AddSubTaskItem(Graph<Key, FormNode> graph, string taskId, SubTask subTask, IEnumerable<int> repeatIndices, int itemRepeatIndex, GraphNode<Key, FormNode> parentNode, GraphNode<Key, FormNode> parentPreNode, GraphNode<Key, FormNode> parentPostNode)
        {
            parentNode.AssertType<SubTaskRouterFormNode>();
            parentPreNode.AssertType<PreSubTaskFormNode>();
            parentPostNode.AssertType<PostSubTaskFormNode>();
            
            var nodes = new Dictionary<Key, (GraphNode<Key, FormNode>, Key)>();

            var fullIndices = repeatIndices.ToList();
            fullIndices.Add(itemRepeatIndex);

            var itemRouterFormNode = new GraphNode<Key, FormNode>(Key.ForSubTaskItemRouter(taskId, subTask.Id, fullIndices), new SubTaskItemRouterFormNode(taskId, subTask.Id, itemRepeatIndex));
            graph.AddNode(itemRouterFormNode);

            foreach (var item in subTask.TaskItems)
            {
                if (item is TaskQuestionPage || item is TaskQuestionGhost)
                {
                    var (node, nextKey) = AddTaskQuestion(graph, taskId, item, fullIndices);
                    nodes.Add(node.Key, (node, nextKey));
                    if (nextKey is null)
                    {
                        if (nodes.Remove(node.Key))
                        {
                            nodes.Add(node.Key, (node, Key.EmptyKey()));
                        }
                        graph.AddDirectedEdge(node, parentPostNode);
                    }
                }

                if (item is SubTask itemSubTask)
                {
                    var newNodes = AddSubTask(graph, taskId, itemSubTask, fullIndices);
                    nodes.AddRange(newNodes);
                    foreach (var (_, (newNode, newNodeNextKey)) in newNodes)
                    {
                        if (newNodeNextKey is null)
                        {
                            if (nodes.Remove(newNode.Key))
                            {
                                nodes.Add(newNode.Key, (newNode, Key.EmptyKey()));
                            }
                            graph.AddDirectedEdge(newNode, parentPostNode);
                        }
                    }
                }
            }

            var (firstTaskItemNode, _) = nodes[Key.ForTaskItemPage(taskId, subTask.TaskItems.First().Id, fullIndices)];
            graph.AddDirectedEdge(itemRouterFormNode, firstTaskItemNode);

            graph.AddDirectedEdge(parentPreNode, itemRouterFormNode);
            graph.AddDirectedEdge(parentPostNode, itemRouterFormNode);
            graph.AddDirectedEdge(parentNode, itemRouterFormNode);

            return nodes;
        }

        public async Task SavePage(string formKey, InFlightPage page)
        {
            var form = await _formRepository.GetForm(formKey);
            
            var node = (TaskQuestionPageFormNode) form.Nodes.FindByKey(new Key(page.NodeId)).Value;
            node.Questions = page.Questions;
            node.PageStatus = PageStatus.Answered;

            await _formRepository.SaveForm(formKey, form);
        }

        public async Task<string> RepeatTask(FormType formType, string formKey, string taskId)
        {
            var staticForm = _staticFormProvider.GetForm(formType);
            var staticFormTask = staticForm.Tasks.First(t => t.Id == taskId);

            var form = await _formRepository.GetForm(formKey);
            var taskList = form.Nodes.FindByKey(Key.ForTaskList());

            var taskRouterNode = taskList.Neighbors.First(x =>
                x.Value.IsDecisionNode && x.Value is TaskRouterFormNode trfn && trfn.TaskId == taskId);

            var preTaskNode = taskRouterNode.Neighbors.First(GraphNodePredicates.IsAnyPreTaskNode);
            var postTaskNode = taskRouterNode.Neighbors.First(GraphNodePredicates.IsAnyPostTaskNode);

            var taskRouterFormNodeValue = taskRouterNode.AssertType<TaskRouterFormNode>();

            var nextItemId = taskRouterFormNodeValue.AddTaskItem();
            
            var allTaskNodes = AddTaskItem(form, taskRouterNode, preTaskNode, postTaskNode, staticFormTask,  nextItemId);
            foreach (var (_, (node, nextKey)) in allTaskNodes)
            {
                if (nextKey is null) continue;
                if (nextKey.Value == Key.EmptyKey().Value) continue;
                var (nextNode, _) = allTaskNodes[nextKey];
                form.AddDirectedEdge(node, nextNode);
            }
            
            var newTaskListItemKey = Key.ForDecisionTaskItemRouter(taskId, nextItemId).Value;

            await _formRepository.SaveForm(formKey, form);

            return newTaskListItemKey;
        }

        public async Task<string> RepeatSubTask(FormType formType, string formKey, string nodeId)
        {
            var staticForm = _staticFormProvider.GetForm(formType);

            var form = await _formRepository.GetForm(formKey);

            var subTaskRouterNode = form.Nodes.FindByKey(new Key(nodeId));
            var subTaskRouter = subTaskRouterNode.AssertType<SubTaskRouterFormNode>();

            var preSubTaskNode = subTaskRouterNode.Neighbors.First(GraphNodePredicates.IsPreSubTaskNode);
            
            var postSubTaskNode = subTaskRouterNode.Neighbors.First(GraphNodePredicates.IsPostSubTaskNode);

            var staticTask = staticForm.Tasks.First(t => t.Id == subTaskRouter.TaskId);
            var staticSubTask = (SubTask)staticTask.FindTaskItem(subTaskRouter.SubTaskId);

            var nextSubTaskItemId = subTaskRouter.AddSubTaskItem();
            
            var newNodes = AddSubTaskItem(form, subTaskRouter.TaskId, staticSubTask, subTaskRouter.RepeatIndices,
                nextSubTaskItemId, subTaskRouterNode, preSubTaskNode, postSubTaskNode);
            
            foreach (var (_, (node, nextKey)) in newNodes)
            {
                if (nextKey is null) continue;
                if (nextKey.Value == Key.EmptyKey().Value) continue;
                var (nextNode, _) = newNodes[nextKey];
                form.AddDirectedEdge(node, nextNode);
            }

            var newSubTaskItemKey = Key.ForSubTaskItemRouter(subTaskRouter.TaskId, subTaskRouter.SubTaskId, subTaskRouter.RepeatIndices.Concat(new []{ nextSubTaskItemId }));

            await _formRepository.SaveForm(formKey, form);

            return newSubTaskItemKey.Value;
        }

        public async Task<string> DeleteTaskItem(FormType formType, string formKey, string taskNodeId, string taskItemNodeId)
        {
            var form = await _formRepository.GetForm(formKey);

            var taskRouterNode = form.Nodes.FindByKey(new Key(taskNodeId));
            var taskRouter = taskRouterNode.AssertType<TaskRouterFormNode>();

            var taskItemRouterNode = taskRouterNode.Neighbors.FindByKey(new Key(taskItemNodeId));
            var taskItemRouter = taskItemRouterNode.AssertType<TaskItemRouterFormNode>();

            var taskItemSummaryNodeKey = Key.ForTaskSummary(taskItemRouter.TaskId, taskItemRouter.RepeatIndex);
            var postTaskNode = taskRouterNode.Neighbors.FindByKey(Key.ForPostTaskPage(taskItemRouter.TaskId));

            var traversal = new GraphTraversalBuilder<Key, FormNode>(TraversalType.BreadthFirstSearch, true)
                .WithOnNodeDequeuedHandler((props, output) =>
                {
                    // Anything beyond the summary we do not need to delete so we skip visiting the node
                    if (props.Node.Key.Equals(taskItemSummaryNodeKey))
                        output.Skip = true;
                })
                .WithOnNodeDiscoveredHandler((props, output) =>
                {
                    // Prevent queuing the PostTaskNode as we do not want it included in the traversal
                    if (props.Node.Key.Equals(postTaskNode.Key))
                        output.ShouldQueue = false;
                })
                .Build();

            var traversalResult = traversal.Run(taskItemRouterNode);

            foreach (var (traversedNode, _) in traversalResult.TraversalOrder)
            {
                form.Remove(traversedNode);
            }
            
            taskRouter.RemoveTaskItem(taskItemRouter.RepeatIndex);
            
            var nextNodeId = postTaskNode.Key.Value;
            
            // If the last Task has been deleted we need to ensure an empty Item is added
            if (!taskRouter.TaskItemIds.Any())
            {
                var task = _staticFormProvider.GetTask(formType, StaticKey.ForTaskNode(taskRouter.TaskId));
                var nextId = taskRouter.AddTaskItem();
                var preTaskNode = taskRouterNode.Neighbors.First(x => x.Value is PreTaskFormNode || x.Value is PreTaskGhost);
                var newNodes = AddTaskItem(form, taskRouterNode, preTaskNode, postTaskNode, task, nextId);
                
                // Add any required edges for new nodes
                foreach (var (_, (node, nextKey)) in newNodes)
                {
                    if (nextKey is null) continue;
                    if (nextKey.Value == Key.EmptyKey().Value) continue;
                    var (nextNode, _) = newNodes[nextKey];
                    form.AddDirectedEdge(node, nextNode);
                }

                // Navigate the user to the new task item instead of the PostTask page
                nextNodeId = preTaskNode.Key.Value;
            }

            await _formRepository.SaveForm(formKey, form);

            return nextNodeId;
        }

        public async Task<string> DeleteSubTaskItem(FormType formType, string formKey, string subTaskNode, string subTaskItemNode)
        {
            var form = await _formRepository.GetForm(formKey);
            
            var subTaskRouterNode = form.Nodes.FindByKey(new Key(subTaskNode));
            var subTaskRouter = subTaskRouterNode.AssertType<SubTaskRouterFormNode>();
            
            var subTaskItemRouterNode = subTaskRouterNode.Neighbors.FindByKey(new Key(subTaskItemNode));
            var subTaskItemRouter = subTaskItemRouterNode.AssertType<SubTaskItemRouterFormNode>();
            
            var postSubTaskNode = subTaskRouterNode.Neighbors.FindByKey(Key.ForPostSubTaskPage(subTaskRouter.TaskId, subTaskRouter.SubTaskId, subTaskRouter.RepeatIndices));

            var traversal = new GraphTraversalBuilder<Key, FormNode>(TraversalType.BreadthFirstSearch, true)
                .WithOnNodeDiscoveredHandler((props, output) =>
                {
                    // We do not want the PostSubTaskNode for our sub task or anything beyond it to be deleted so we prevent queueing it
                    if (props.Node.Key.Equals(postSubTaskNode.Key))
                    {
                        output.ShouldQueue = false;
                    }
                })
                .Build();
            
            var traversalResult = traversal.Run(subTaskItemRouterNode);

            foreach (var (traversedNode, _) in traversalResult.TraversalOrder)
            {
                form.Remove(traversedNode);
            }

            subTaskRouter.RemoveSubTaskItem(subTaskItemRouter.RepeatIndex);

            var nextNodeId = postSubTaskNode.Key.Value;
            
            // If the last SubTask has been deleted we need to ensure an empty Item is added
            if (!subTaskRouter.TaskItemIds.Any())
            {
                var subTask = _staticFormProvider.GetSubTask(formType, StaticKey.ForSubTask(subTaskRouter.TaskId, subTaskRouter.SubTaskId));
                var nextId = subTaskRouter.AddSubTaskItem();
                var preSubTaskNode = subTaskRouterNode.Neighbors.First(x => x.Value is PreSubTaskFormNode);
                var newNodes = AddSubTaskItem(form, subTaskRouter.TaskId, subTask, subTaskRouter.RepeatIndices, nextId, subTaskRouterNode, preSubTaskNode, postSubTaskNode);
                
                // Add any required edges for new nodes
                foreach (var (_, (node, nextKey)) in newNodes)
                {
                    if (nextKey is null) continue;
                    if (nextKey.Value == Key.EmptyKey().Value) continue;
                    var (nextNode, _) = newNodes[nextKey];
                    form.AddDirectedEdge(node, nextNode);
                }

                // Navigate the user to the new sub task item instead of the PostSubTask page
                nextNodeId = preSubTaskNode.Key.Value;
            }

            await _formRepository.SaveForm(formKey, form);

            return nextNodeId;
        }

        public async Task<string> GetPreviousNode(string formKey, string currentNodeId)
        {
            var form = await _formRepository.GetForm(formKey);
            
            var currentNode = form.Nodes.FindByKey(new Key(currentNodeId));
            var taskFormNode = currentNode.AssertType<PhysicalTaskFormNode>();
            
            var taskRouterNode = form.Nodes.FindByKey(Key.ForDecisionTaskRouter(taskFormNode.TaskId));
            taskRouterNode.AssertType<TaskRouterFormNode>();

            // We prevent queueing from the task list so that we don't have to iterate over other tasks
            var stopKey = Key.ForTaskList();

            var pathFinder = new GraphPathFinderBuilder<Key, FormNode>(PathFinderType.PriorityPathFinder)
                .WithOnEdgeDiscoveredHandler((e, output) =>
                {
                    if (e.ToNode.Key.Equals(stopKey)) output.ShouldQueue = false;
                })
                .Build();

            var pathResult = pathFinder.FindPath(form, taskRouterNode, currentNode);

            // Skip the current node then find the first physical node
            var reversedPath = pathResult.Path.Reverse();
            var physicalNode = reversedPath.Skip(1).FirstOrDefault(x => !x.Value.IsDecisionNode);

            return physicalNode is null ? Key.ForTaskList().Value : physicalNode.Key.Value;
        }
        
        public async Task<GraphNode<Key, FormNode>> GetFormNode(string formKey, string nodeId)
        {
            var form = await _formRepository.GetForm(formKey);
            return form.Nodes.FindByKey(new Key(nodeId));
        }

        public async Task<(bool isValid, IDictionary<string, string> errors)> ValidateQuestionPage(FormType formType, string formKey, InFlightPage inFlightPage)
        {
            var form = await _formRepository.GetForm(formKey);

            var node = form.Nodes.FindByKey(new Key(inFlightPage.NodeId));

            var questions = node.Value switch
            {
                TaskQuestionPageFormNode questionPageFormNode => ((TaskQuestionPage) _staticFormProvider.GetPage(
                    formType, StaticKey.ForTaskQuestionPage(questionPageFormNode.TaskId, questionPageFormNode.PageId))).Questions,
                PostTaskFormNode postTaskFormNode => ((ConsentPage) _staticFormProvider.GetPage(
                    formType, StaticKey.ForPostTaskPage(postTaskFormNode.TaskId))).Questions,
                _ => throw new UnexpectedNodeTypeException<TaskQuestionPageFormNode, PostTaskFormNode>(node.Value)
            };
            
            var errors = new Dictionary<string, string>();
            
            foreach (var question in questions)
            {
                var answer = inFlightPage.Questions[questions.IndexOf(question)];

                var result = question.Validator?.Validate(answer);

                if (result is null || result.IsValid) continue;
                
                errors.Add(question.Id, result.Errors.First().ErrorMessage);
            }

            return (isValid: !errors.Any(), errors: errors);
        }

        public async Task<(int questionNumber, int totalQuestions)> GetQuestionNumber(string formKey, string questionPageNodeId)
        {
            var form = await _formRepository.GetForm(formKey);

            var questionPageFormNode = form.Nodes.FindByKey(new Key(questionPageNodeId));
            var questionPageValue = questionPageFormNode.AssertType<TaskQuestionPageFormNode>();

            var taskListNode = form.Nodes.FindByKey(Key.ForTaskList());

            var taskRouterNode = taskListNode.Neighbors.First(x => x.IsTaskRouterNode(out var taskRouter)
                                                                   && taskRouter.TaskId == questionPageValue.TaskId);

            var taskItemRouterNode = taskRouterNode.Neighbors.First(x => x.IsTaskItemRouterNode(out var tir)
                                                                         && tir.RepeatIndex ==
                                                                         questionPageValue.RepeatIndices.First());

            var postTaskNode = taskRouterNode.Neighbors.First(GraphNodePredicates.IsAnyPostTaskNode);

            var pathFinder = new GraphPathFinderBuilder<Key, FormNode>(PathFinderType.PriorityPathFinder)
                .WithOnNodeDequeuedHandler((props, output) =>
                {
                    // We don't need to queue beyond the PostTask as all the question nodes lie between the item router and post task nodes
                    if ((props.Node.Value.IsDecisionNode && props.Node.Value is DecisionFormNode dfn && dfn.DecisionFormNodeType == DecisionFormNodeType.PostTaskGhost) ||
                        (!props.Node.Value.IsDecisionNode && props.Node.Value is PhysicalFormNode pfn && pfn.PhysicalFormNodeType == PhysicalFormNodeType.PostTask))
                    {
                        output.Skip = true;
                    }
                })
                .Build();
            
            var taskItemToQuestionPath = pathFinder.FindPath(form, taskItemRouterNode, questionPageFormNode);
            var questionToSummaryPath = pathFinder.FindPath(form, questionPageFormNode, postTaskNode);

            var questionNumber = taskItemToQuestionPath.Path.Count(GraphNodePredicates.IsTaskQuestionPageNode);
            
            var remainingQuestionCount = questionToSummaryPath.Path.Count(GraphNodePredicates.IsTaskQuestionPageNode) -1; // -1 so we don't include the current question twice

            return (questionNumber, questionNumber + remainingQuestionCount);
        }
    }
}