using Forms.Core.Models.Pages;

namespace Forms.Core.NodeHandlers.PhysicalNodeHandlers.Models.PreTask
{
    public class PreTaskResponse : PhysicalResponse
    {
        public string NextNodeId { get; set; }
        public PreTaskPage PreTaskPage { get; set; }
    }
}