using Forms.Core.Models.Pages;

namespace Forms.Core.NodeHandlers.PhysicalNodeHandlers.Models.PostTask
{
    public abstract class PostTaskResponse<T> : PhysicalResponse where T : PostTaskPage
    {
        public T Page { get; set; }
        public string NextNodeId { get; set; }
    }
}