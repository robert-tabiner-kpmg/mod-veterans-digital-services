using Forms.Core.Models.Pages;

namespace Forms.Core.Models.Static
{
    public interface ITaskItem
    {
        string Id { get; set; }
        string NextPageId { get; set; }
    }
}