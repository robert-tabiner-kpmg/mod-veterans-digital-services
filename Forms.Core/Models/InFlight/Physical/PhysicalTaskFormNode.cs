namespace Forms.Core.Models.InFlight.Physical
{
    public abstract class PhysicalTaskFormNode : PhysicalFormNode
    {
        protected PhysicalTaskFormNode(string taskId)
        {
            TaskId = taskId;
        }

        public string TaskId { get; }
    }
}