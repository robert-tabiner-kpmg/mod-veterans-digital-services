using System.Collections;
using System.Collections.Generic;
using Forms.Core.Models.Pages;

namespace Forms.Core.NodeHandlers.PhysicalNodeHandlers.Models.PostTask
{
    public class RepeatTaskResponse : PostTaskResponse<RepeatTaskPage>
    {
        public IEnumerable<string> TaskItemNodeIds { get; set; }
        public IEnumerable<string> TaskItemSummaryNodeIds { get; set; }
        public string TaskId { get; set; }
        public string TaskRouterNodeId { get; set; }
        public override bool ShowBackButton => false;
    }
}