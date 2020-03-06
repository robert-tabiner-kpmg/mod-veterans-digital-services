using System;
using System.Collections.Generic;
using Forms.Core.Models.Pages;

namespace Forms.Core.Models.Static
{
    public class Task
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int GroupNameIndex { get; set; }
        public SummaryPage SummaryPage { get; set; }
        public PostTaskPage PostTaskPage { get; set; }
        public PreTaskPage PreTaskPage { get; set; }
        public List<ITaskItem> TaskItems { get; set; }
        public Func<List<(string taskId, bool isTaskComplete)>, bool> HiddenWhen { get; set; }
    }
}