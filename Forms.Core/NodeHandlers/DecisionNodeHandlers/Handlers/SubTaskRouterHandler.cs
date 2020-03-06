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
    public class SubTaskRouterHandler : IDecisionNodeHandler
    {
        public DecisionFormNodeType DecisionFormNodeType => DecisionFormNodeType.SubTaskRouter;
        
        public Task<GraphNode<Key, FormNode>> Handle(GraphNode<Key, FormNode> node)
        {
            var nodeAsRouter = node.AssertType<SubTaskRouterFormNode>();
                        
            var subTaskItems = node.Neighbors.Where(GraphNodePredicates.IsSubTaskItemRouterNode);

            var preSubTaskNode = node.Neighbors.First(GraphNodePredicates.IsPreSubTaskNode);
            
            var stopKey = Key.ForPostSubTaskPage(nodeAsRouter.TaskId, nodeAsRouter.SubTaskId, nodeAsRouter.RepeatIndices);

            var hasUnansweredItems = false;
            var hasAnsweredItems = false;
            
            var traversal = new GraphTraversalBuilder<Key, FormNode>(TraversalType.BreadthFirstSearch, false)
                .WithOnNodeDequeuedHandler((props, output) =>
                {
                    if (props.Node.Key.Equals(stopKey))
                    {
                        output.Skip = true;
                        return;
                    }

                    if (props.Node.Value.IsDecisionNode) return;

                    if (!props.Node.IsTaskQuestionPageNode(out var questionNode)) return;
                    
                    if (questionNode.PageStatus == PageStatus.Unanswered)
                    {
                        hasUnansweredItems = true;
                        output.IsComplete = true;
                        return;
                    }

                    hasAnsweredItems = true;
                }).Build();
            
            foreach (var subTask in subTaskItems)
            {
                hasUnansweredItems = false;
                hasAnsweredItems = false;
                
                traversal.Run(subTask);

                if (hasUnansweredItems)
                {
                    return Task.FromResult(hasAnsweredItems ? subTask : preSubTaskNode);
                }
            }
            
            return Task.FromResult(node.Neighbors.First(GraphNodePredicates.IsPostSubTaskNode));
        }
    }
}