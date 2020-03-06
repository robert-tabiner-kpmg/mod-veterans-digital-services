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
using Graph.Models;

namespace Forms.Core.NodeHandlers.PhysicalNodeHandlers.Handlers
{
    public class PostTaskHandler : IPhysicalNodeHandler
    {
        public PhysicalFormNodeType PhysicalFormNodeType => PhysicalFormNodeType.PostTask;
        private readonly IStaticFormProvider _staticFormProvider;

        public PostTaskHandler(IStaticFormProvider staticFormProvider)
        {
            _staticFormProvider = staticFormProvider;
        }
        
        public Task<PhysicalResponse> Handle(FormType formType, string formKey, GraphNode<Key, FormNode> node)
        {
            var postTaskFormNode = node.AssertType<PostTaskFormNode>();
            
            var taskListNode = node.Neighbors.First(GraphNodePredicates.IsAnyTaskListNode);
            var taskSummaryNodes = node.Neighbors.Where(GraphNodePredicates.IsAnyTaskSummaryNode);
            var taskItemNodes = taskSummaryNodes.Select(x => x.Neighbors.First(GraphNodePredicates.IsTaskItemRouterNode));

            var taskRouterKey = Key.ForDecisionTaskRouter(postTaskFormNode.TaskId);

            var postTaskPage = _staticFormProvider.GetPage(formType, StaticKey.ForPostTaskPage(postTaskFormNode.TaskId)) as PostTaskPage;

            return Task.FromResult(postTaskPage switch
            {
                ConsentPage consentPage => new ConsentPageResponse
                {
                    Page = consentPage,
                    NextNodeId = null,
                } as PhysicalResponse,
                RepeatTaskPage repeatTaskPage => new RepeatTaskResponse
                {
                    Page = repeatTaskPage,
                    NextNodeId = taskListNode.Key.Value,
                    TaskItemSummaryNodeIds = taskSummaryNodes.Select(x => x.Key.Value),
                    TaskItemNodeIds = taskItemNodes.Select(x => x.Key.Value),
                    TaskId = postTaskFormNode.TaskId,
                    TaskRouterNodeId = taskRouterKey.Value
                },
                _ => throw new NotImplementedException()
            });
        }
    }
}