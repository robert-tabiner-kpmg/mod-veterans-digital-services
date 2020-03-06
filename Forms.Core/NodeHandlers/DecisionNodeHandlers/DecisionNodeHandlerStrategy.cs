using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forms.Core.Extensions;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Decision;
using Forms.Core.NodeHandlers.DecisionNodeHandlers.Interfaces;
using Graph;
using Graph.Models;

namespace Forms.Core.NodeHandlers.DecisionNodeHandlers
{
    public class DecisionNodeHandlerStrategy : IDecisionNodeHandlerStrategy
    {
        private readonly IEnumerable<IDecisionNodeHandler> _nodeHandlers;
        private const int ConsecutiveDecisionNodeLimit = 10;
        public DecisionNodeHandlerStrategy(IEnumerable<IDecisionNodeHandler> nodeHandlers)
        {
            _nodeHandlers = nodeHandlers;
        }

        public async Task<GraphNode<Key, FormNode>> Handle(GraphNode<Key, FormNode> node)
        {
            node.AssertType<DecisionFormNode>();

            var count = 0;
            var currentNode = node;
            while (count < ConsecutiveDecisionNodeLimit)
            {
                var nodeAsDecisionNode = (DecisionFormNode) currentNode.Value;
                
                var result = await _nodeHandlers.First(x => x.DecisionFormNodeType == nodeAsDecisionNode.DecisionFormNodeType).Handle(currentNode);

                if (!result.Value.IsDecisionNode) return result;

                currentNode = result;

                count++;
            }
            
            throw new Exception($"Expected to reach a physical form node in {ConsecutiveDecisionNodeLimit} iterations. Possible circular graph path found?");
        }
    }
}