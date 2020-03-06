using System.Linq;
using System.Threading.Tasks;
using Forms.Core.Extensions;
using Forms.Core.Forms;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Decision;
using Forms.Core.Models.InFlight.Physical;
using Forms.Core.Models.Pages;
using Forms.Core.NodeHandlers.PhysicalNodeHandlers.Interfaces;
using Forms.Core.NodeHandlers.PhysicalNodeHandlers.Models;
using Forms.Core.NodeHandlers.PhysicalNodeHandlers.Models.PreTask;
using Forms.Core.Services.Interfaces;
using Graph;
using Graph.Models;

namespace Forms.Core.NodeHandlers.PhysicalNodeHandlers.Handlers
{
    public class PreTaskHandler : IPhysicalNodeHandler
    {
        public PhysicalFormNodeType PhysicalFormNodeType => PhysicalFormNodeType.PreTask;
        private readonly IStaticFormProvider _staticFormProvider;

        public PreTaskHandler(IStaticFormProvider staticFormProvider)
        {
            _staticFormProvider = staticFormProvider;
        }
        
        public Task<PhysicalResponse> Handle(FormType formType, string formKey, GraphNode<Key, FormNode> node)
        {
            var preTaskFromNode = node.AssertType<PreTaskFormNode>();
            var preTaskPage = _staticFormProvider.GetPage(formType, StaticKey.ForPreTaskPage(preTaskFromNode.TaskId)) as PreTaskPage;

            var taskItemNode = node.Neighbors.First(GraphNodePredicates.IsTaskItemRouterNode);
            
            return Task.FromResult(new PreTaskResponse
            {
                NextNodeId = taskItemNode.Key.Value,
                PreTaskPage = preTaskPage
            } as PhysicalResponse);
        }
    }
}