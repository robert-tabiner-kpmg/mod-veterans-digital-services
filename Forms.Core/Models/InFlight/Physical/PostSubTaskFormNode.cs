namespace Forms.Core.Models.InFlight.Physical
{
    public class PostSubTaskFormNode : PhysicalTaskFormNode
    {
        public PostSubTaskFormNode(string taskId, string subTaskId) : base(taskId)
        {
            SubTaskId = subTaskId;
        }

        public override PhysicalFormNodeType PhysicalFormNodeType => PhysicalFormNodeType.PostSubTask;
        public string SubTaskId { get; }
    }
}