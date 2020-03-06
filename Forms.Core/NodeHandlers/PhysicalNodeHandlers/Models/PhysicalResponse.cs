namespace Forms.Core.NodeHandlers.PhysicalNodeHandlers.Models
{
    public abstract class PhysicalResponse
    {
        public virtual bool ShowBackButton => true;
        public string CurrentNodeId { get; set; } 
    }
}