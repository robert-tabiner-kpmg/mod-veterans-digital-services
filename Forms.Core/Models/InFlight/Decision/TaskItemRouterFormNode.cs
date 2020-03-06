namespace Forms.Core.Models.InFlight.Decision
{
    public class TaskItemRouterFormNode : DecisionFormNode
    {
        public TaskItemRouterFormNode(string taskId, int repeatIndex)
        {
            TaskId = taskId;
            RepeatIndex = repeatIndex;
        }

        public override DecisionFormNodeType DecisionFormNodeType => DecisionFormNodeType.TaskItemRouter;
        public string TaskId { get; }
        public int RepeatIndex { get; }
    }
}