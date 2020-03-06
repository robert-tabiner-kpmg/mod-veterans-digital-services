using System;
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
    public class PreSubTaskHandler : IPhysicalNodeHandler
    {
        public PhysicalFormNodeType PhysicalFormNodeType => PhysicalFormNodeType.PreSubTask;
        
        private readonly IStaticFormProvider _staticFormProvider;

        public PreSubTaskHandler(IStaticFormProvider staticFormProvider)
        {
            _staticFormProvider = staticFormProvider;
        }
        public Task<PhysicalResponse> Handle(FormType formType, string formKey, GraphNode<Key, FormNode> node)
        {
            var preSubTaskFormNode = node.AssertType<PreSubTaskFormNode>();

            var preTaskPage = _staticFormProvider.GetPage(formType,
                StaticKey.ForPreSubTaskPage(preSubTaskFormNode.TaskId, preSubTaskFormNode.SubTaskId)) as PreTaskPage;

            var taskItemNode = node.Neighbors.First(GraphNodePredicates.IsSubTaskItemRouterNode);
            
            return Task.FromResult(new PreTaskResponse
            {
                NextNodeId = taskItemNode.Key.Value,
                PreTaskPage = preTaskPage
            } as PhysicalResponse);
        }
    }
}