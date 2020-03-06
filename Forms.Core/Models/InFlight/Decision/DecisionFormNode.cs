namespace Forms.Core.Models.InFlight.Decision
{
    public abstract class DecisionFormNode : FormNode
    {
        public abstract DecisionFormNodeType DecisionFormNodeType { get; }
        public override bool IsDecisionNode => true;
    }
}