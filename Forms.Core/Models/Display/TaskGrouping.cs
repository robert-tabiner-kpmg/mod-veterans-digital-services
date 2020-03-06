using System.Collections.Generic;

namespace Forms.Core.Models.Display
{
    public class TaskGrouping : BaseDisplayItem
    {
        public string Name { get; set; }
        public string NodeId { get; set; }
        public List<BaseDisplayItem> Items { get; set; }
    }
}