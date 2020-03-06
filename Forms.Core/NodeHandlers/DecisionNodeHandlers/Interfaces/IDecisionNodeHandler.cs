using System.Threading.Tasks;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Decision;
using Graph;
using Graph.Models;

namespace Forms.Core.NodeHandlers.DecisionNodeHandlers.Interfaces
{
    public interface IDecisionNodeHandler
    {
        DecisionFormNodeType DecisionFormNodeType { get; }
        Task<GraphNode<Key, FormNode>> Handle(GraphNode<Key, FormNode> node);
    }
}