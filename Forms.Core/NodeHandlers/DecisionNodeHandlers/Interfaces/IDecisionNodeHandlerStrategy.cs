using System.Threading.Tasks;
using Forms.Core.Models.InFlight;
using Graph;
using Graph.Models;

namespace Forms.Core.NodeHandlers.DecisionNodeHandlers.Interfaces
{
    public interface IDecisionNodeHandlerStrategy
    {
        Task<GraphNode<Key, FormNode>> Handle(GraphNode<Key, FormNode> node);
    }
}