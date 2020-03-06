namespace Forms.Core.Models.InFlight.Physical
{
    public abstract class PhysicalFormNode : FormNode
    {
        public abstract PhysicalFormNodeType PhysicalFormNodeType { get; }
        public override bool IsDecisionNode => false;
    }
}