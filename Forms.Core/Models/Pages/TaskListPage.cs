using System.Collections.Generic;

namespace Forms.Core.Models.Pages
{
    public class TaskListPage : Page
    {
        public string Title { get; set; }
        public List<TaskListTask> Tasks { get; set; }
        public List<string> TaskGroupNames { get; set; }
    }
}