using System.Collections.Generic;
using Forms.Core.Models.Pages;

namespace Forms.Core.NodeHandlers.PhysicalNodeHandlers.Models.PostTask
{
    public class RepeatSubTaskResponse : PostTaskResponse<RepeatTaskPage>
    {
        public IEnumerable<string> TaskItemNodeIds { get; set; }
        public string SubTaskRouterNodeId { get; set; }
        public override bool ShowBackButton => false;
    }
}