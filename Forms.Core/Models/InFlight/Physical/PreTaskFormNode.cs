namespace Forms.Core.Models.InFlight.Physical
{
    public class PreTaskFormNode : PhysicalTaskFormNode
    {
        public PreTaskFormNode(string taskId) : base(taskId)
        {
        }

        public override PhysicalFormNodeType PhysicalFormNodeType => PhysicalFormNodeType.PreTask;
    }
}