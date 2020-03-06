using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forms.Core.Extensions;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Decision;
using Forms.Core.Models.InFlight.Physical;
using Forms.Core.Models.Pages;
using Forms.Core.NodeHandlers.DecisionNodeHandlers.Interfaces;
using Graph;
using Graph.Analysis.Traversal;
using Graph.Analysis.Traversal.Event;
using Graph.Models;

namespace Forms.Core.NodeHandlers.DecisionNodeHandlers.Handlers
{
    public class TaskRouterHandler : IDecisionNodeHandler
    {
        public DecisionFormNodeType DecisionFormNodeType => DecisionFormNodeType.TaskRouter;

        public Task<GraphNode<Key, FormNode>> Handle(GraphNode<Key, FormNode> node)
        {
            node.AssertType<TaskRouterFormNode>();
            
            var preTaskNode = node.Neighbors.First(GraphNodePredicates.IsAnyPreTaskNode);
            var postTaskNode = node.Neighbors.First(GraphNodePredicates.IsAnyPostTaskNode);
            
            var taskItemRouters = node.Neighbors.Where(GraphNodePredicates.IsTaskItemRouterNode)
                .OrderBy(x => x.IsTaskItemRouterNode(out var taskItem) ? taskItem.RepeatIndex : 0).ToList();

            var hasAnsweredItems = false;
            GraphNode<Key, FormNode> summaryNode = null;
            
            var traversal = new GraphTraversalBuilder<Key, FormNode>(TraversalType.BreadthFirstSearch, true)
                .WithOnNodeDequeuedHandler((props, output) =>
                {
                    if (props.Node.Value.IsDecisionNode) return;

                    if (props.Node.IsAnyTaskSummaryNode())
                    {
                        summaryNode = props.Node;
                        output.Skip = true;
                        return;
                    }


                    if (!props.Node.IsTaskQuestionPageNode(out var taskPageNode)) return;
                    
                    if (taskPageNode.PageStatus == PageStatus.Unanswered)
                    {
                        output.IsComplete = true;
                        return;
                    }

                    hasAnsweredItems = true;
                })
                .WithOnEdgeDiscoveredHandler((props, output) =>
                {
                    if (props.Cost == 1) return;
                    
                    if (props.Cost == EdgeCosts.SubTaskToPostSubTaskCost)
                    {
                        if (props.FromNode.IsSubTaskRouterNode() && props.ToNode.IsPostSubTaskNode())
                        {
                            output.ShouldQueue = false;
                        }
                    }
                })
                .Build();
            
            
            
            // Run the traversal on each of our TaskItems
            foreach (var router in taskItemRouters)
            {
                hasAnsweredItems = false;
                summaryNode = null;

                var result = traversal.Run(router);

                // Found an unanswered node
                if (result.Result == null) continue;
                
                // We had no answered questions and only one repeat (the user has never visited the task)
                if (!hasAnsweredItems && taskItemRouters.Count == 1) return Task.FromResult(preTaskNode);

                // The user has answered some questions but not all
                return Task.FromResult(router);
            }
            
            // All items were answered
            return postTaskNode.Value.IsDecisionNode ? Task.FromResult(summaryNode) : Task.FromResult(postTaskNode);
        }
    }
}