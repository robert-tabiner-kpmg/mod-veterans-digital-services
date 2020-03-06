using System.Collections.Generic;

namespace Forms.Core.Models.InFlight.Decision
{
    public class TaskRouterFormNode : DecisionFormNode
    {
        public TaskRouterFormNode(string taskId)
        {
            TaskId = taskId;
            TaskItemIds = new HashSet<int> { 0 };
        }

        public string TaskId { get; }
        public HashSet<int> TaskItemIds { get; set; }
        public int LastAddedItemId { get; set; } = 0;
        public int AddTaskItem()
        {
            LastAddedItemId += 1;
            TaskItemIds.Add(LastAddedItemId);
            return LastAddedItemId;
        }

        public void RemoveTaskItem(int id)
        {
            TaskItemIds.Remove(id);
        }
        public override DecisionFormNodeType DecisionFormNodeType => DecisionFormNodeType.TaskRouter;
    }
}