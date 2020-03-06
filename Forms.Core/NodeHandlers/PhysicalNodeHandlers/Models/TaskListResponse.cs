using System.Collections.Generic;
using Forms.Core.Models.Pages;

namespace Forms.Core.NodeHandlers.PhysicalNodeHandlers.Models
{
    public class TaskListResponse : PhysicalResponse
    {
        public string FormName { get; set; }
        public List<TaskListTask> Tasks { get; set; }
        public List<string> TaskGroupNames { get; set; }
        public override bool ShowBackButton => false;
    }
}