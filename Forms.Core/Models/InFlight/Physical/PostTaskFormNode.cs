namespace Forms.Core.Models.InFlight.Physical
{
    public class PostTaskFormNode : PhysicalTaskFormNode
    {
        public PostTaskFormNode(string taskId) : base(taskId)
        {
        }

        public override PhysicalFormNodeType PhysicalFormNodeType => PhysicalFormNodeType.PostTask;
    }
}