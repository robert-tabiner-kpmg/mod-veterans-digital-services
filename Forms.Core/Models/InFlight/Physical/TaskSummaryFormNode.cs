namespace Forms.Core.Models.InFlight.Physical
{
    public class TaskSummaryFormNode : PhysicalTaskFormNode
    {
        public TaskSummaryFormNode(string taskId) : base(taskId)
        {
        }

        public override PhysicalFormNodeType PhysicalFormNodeType => PhysicalFormNodeType.TaskSummary;
    }
}