using System.Threading.Tasks;
using Forms.Core.EffectHandlers.Models;
using Forms.Core.Models.InFlight;
using Graph.Models;

namespace Forms.Core.EffectHandlers.Interfaces
{
    public interface IEffectHandler
    {
        EffectTypes EffectTypes { get; }
        Task Handle(Effect effect, Graph<Key, FormNode> form, GraphNode<Key, FormNode> currentPageNode);
    }
}