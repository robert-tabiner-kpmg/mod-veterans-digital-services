using System.Threading.Tasks;
using Forms.Core.Forms;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Physical;
using Forms.Core.NodeHandlers.PhysicalNodeHandlers.Models;
using Graph;
using Graph.Models;

namespace Forms.Core.NodeHandlers.PhysicalNodeHandlers.Interfaces
{
    public interface IPhysicalNodeHandler
    {
        PhysicalFormNodeType PhysicalFormNodeType { get; }
        Task<PhysicalResponse> Handle(FormType formType, string formKey, GraphNode<Key, FormNode> node);
    }
}