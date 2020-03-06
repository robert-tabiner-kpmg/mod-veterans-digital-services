namespace Forms.Core.Models.InFlight.Physical
{
    public class PreSubTaskFormNode : PhysicalTaskFormNode
    {
        public PreSubTaskFormNode(string taskId, string subTaskId) : base(taskId)
        {
            SubTaskId = subTaskId;
        }

        public override PhysicalFormNodeType PhysicalFormNodeType => PhysicalFormNodeType.PreSubTask;
        public string SubTaskId { get; }
    }
}