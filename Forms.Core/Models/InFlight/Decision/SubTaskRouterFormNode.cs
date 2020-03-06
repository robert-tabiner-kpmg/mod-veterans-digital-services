using System.Collections.Generic;
using System.Linq;

namespace Forms.Core.Models.InFlight.Decision
{
    public class SubTaskRouterFormNode : DecisionFormNode
    {
        public SubTaskRouterFormNode(string taskId, string subTaskId, IEnumerable<int> repeatIndices)
        {
            TaskId = taskId;
            SubTaskId = subTaskId;
            RepeatIndices = repeatIndices;
            TaskItemIds = new HashSet<int> { 0 };
        }

        public override DecisionFormNodeType DecisionFormNodeType => DecisionFormNodeType.SubTaskRouter;
        
        public string TaskId { get; }
        public string SubTaskId { get; }
        public IEnumerable<int> RepeatIndices { get; }

        public HashSet<int> TaskItemIds { get; set; }
        public int LastAddedItemId { get; set; } = 0;
        public int AddSubTaskItem()
        {
            LastAddedItemId += 1;
            TaskItemIds.Add(LastAddedItemId);
            return LastAddedItemId;
        }

        public void RemoveSubTaskItem(int id)
        {
            TaskItemIds.Remove(id);
        }
    }
}