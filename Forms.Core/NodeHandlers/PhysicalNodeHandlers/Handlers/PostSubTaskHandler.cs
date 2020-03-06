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
using Forms.Core.NodeHandlers.PhysicalNodeHandlers.Models.PostTask;
using Forms.Core.Services.Interfaces;
using Graph;
using Graph.Models;

namespace Forms.Core.NodeHandlers.PhysicalNodeHandlers.Handlers
{
    public class PostSubTaskHandler : IPhysicalNodeHandler
    {
        public PhysicalFormNodeType PhysicalFormNodeType => PhysicalFormNodeType.PostSubTask;
        private readonly IStaticFormProvider _staticFormProvider;

        public PostSubTaskHandler(IStaticFormProvider staticFormProvider)
        {
            _staticFormProvider = staticFormProvider;
        }

        public Task<PhysicalResponse> Handle(FormType formType, string formKey, GraphNode<Key, FormNode> node)
        {
            var postSubTaskFormNode = node.AssertType<PostSubTaskFormNode>();

            var itemRouter = node.Neighbors.First(GraphNodePredicates.IsSubTaskRouterNode);
            
            var postTaskPage = _staticFormProvider.GetPage(formType,
                    StaticKey.ForPostSubTaskPage(postSubTaskFormNode.TaskId, postSubTaskFormNode.SubTaskId)) as
                RepeatTaskPage;
            
            var taskItemNodeIds = node.Neighbors.Where(GraphNodePredicates.IsSubTaskItemRouterNode).Select(x => x.Key.Value);

            // Next node can be any node except for the a SubTaskItem or the SubTaskRouter of the current SubTask
            var nextNode = node.Neighbors.First(x => !x.IsSubTaskItemRouterNode() && !(x.IsSubTaskRouterNode(out var str) && str.SubTaskId == postSubTaskFormNode.SubTaskId));
            
            return Task.FromResult(new RepeatSubTaskResponse
            {
                NextNodeId = nextNode.Key.Value,
                Page = postTaskPage,
                TaskItemNodeIds = taskItemNodeIds,
                SubTaskRouterNodeId = itemRouter.Key.Value
            } as PhysicalResponse);
        }
    }
}