using System.Collections.Generic;

namespace Forms.Core.Models.InFlight
{
    public class InFlightPage
    {
        public string NodeId { get; set; }
        public List<InFlightQuestion> Questions { get; set; } = new List<InFlightQuestion>();
        public bool ReturnToSummary { get; set; }
    }
}