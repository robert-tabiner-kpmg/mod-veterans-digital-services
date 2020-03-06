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
    public class SubTaskItemRouterHandler : IDecisionNodeHandler
    {
        public DecisionFormNodeType DecisionFormNodeType => DecisionFormNodeType.SubTaskItemRouter;
        
        public Task<GraphNode<Key, FormNode>> Handle(GraphNode<Key, FormNode> node)
        {
            node.AssertType<SubTaskItemRouterFormNode>();
            return Task.FromResult(node.Neighbors.First(GraphNodePredicates.IsAnyTaskItemNode));
        }
    }
}