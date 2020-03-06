using System;
using System.Linq;
using System.Threading.Tasks;
using Forms.Core.Extensions;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Decision;
using Forms.Core.Models.InFlight.Physical;
using Forms.Core.NodeHandlers.DecisionNodeHandlers.Interfaces;
using Graph;
using Graph.Models;

namespace Forms.Core.NodeHandlers.DecisionNodeHandlers.Handlers
{
    public class TaskItemRouterHandler : IDecisionNodeHandler
    {
        public DecisionFormNodeType DecisionFormNodeType => DecisionFormNodeType.TaskItemRouter;
        public Task<GraphNode<Key, FormNode>> Handle(GraphNode<Key, FormNode> node)
        {
            node.AssertType<TaskItemRouterFormNode>();
            return Task.FromResult(node.Neighbors.First(GraphNodePredicates.IsAnyTaskItemNode));
        }
    }
}