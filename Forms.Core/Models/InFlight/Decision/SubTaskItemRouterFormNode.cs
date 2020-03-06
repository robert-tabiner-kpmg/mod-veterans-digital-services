namespace Forms.Core.Models.InFlight.Decision
{
    public class SubTaskItemRouterFormNode : DecisionFormNode
    {
        public SubTaskItemRouterFormNode(string taskId, string subTaskId, int repeatIndex)
        {
            TaskId = taskId;
            SubTaskId = subTaskId;
            RepeatIndex = repeatIndex;
        }

        public override DecisionFormNodeType DecisionFormNodeType => DecisionFormNodeType.SubTaskItemRouter;
        
        public string TaskId { get; }
        public string SubTaskId { get; }
        public int RepeatIndex { get; }
    }
}