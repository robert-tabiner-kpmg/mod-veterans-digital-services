namespace Forms.Core.Models.Pages
{
    public class TaskListTask
    {
        public string TaskRouterNodeKey { get; set; }
        public string TaskId { get; set; }
        public int GroupNameIndex { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public bool IsHidden { get; set; }
    }
}