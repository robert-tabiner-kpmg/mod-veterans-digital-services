using System.Collections.Generic;
using Forms.Core.Models.InFlight;

namespace Forms.Core.Models.Display
{
    public class TaskDisplayItem : BaseDisplayItem
    {
        public string QuestionText { get; set; }
        public string Header { get; set; }
        public InFlightQuestion Question { get; set; }
        public string NodeId { get; set; }
    }
}