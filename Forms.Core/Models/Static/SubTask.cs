using System.Collections.Generic;
using Forms.Core.Models.Pages;

namespace Forms.Core.Models.Static
{
    public class SubTask : ITaskItem
    {
        public string Id { get; set; }
        public string NextPageId { get; set; }
        public PostTaskPage PostTaskPage { get; set; }
        public PreTaskPage PreTaskPage { get; set; }
        public List<ITaskItem> TaskItems { get; set; }
        public string DisplayName { get; set; }
    }
}