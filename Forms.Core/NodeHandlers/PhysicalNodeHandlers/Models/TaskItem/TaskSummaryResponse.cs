using System.Collections.Generic;
using Forms.Core.Models.Display;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Static;

namespace Forms.Core.NodeHandlers.PhysicalNodeHandlers.Models.TaskItem
{
    public class TaskSummaryResponse : PhysicalResponse
    {
        public string NextNodeId { get; set; }
        public List<TaskGrouping> TaskGroupings { get; set; }
        public SummaryPage Page { get; set; }
    }
}