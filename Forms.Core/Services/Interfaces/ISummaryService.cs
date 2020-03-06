using System.Collections.Generic;
using Forms.Core.Forms;
using Forms.Core.Models.Display;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.Static;
using Graph.Models;

namespace Forms.Core.Services.Interfaces
{
    public interface ISummaryService
    {
        IDictionary<string, IList<TaskGrouping>> GetFormSummary(Graph<Key, FormNode> graph, FormType formType);
        IList<TaskGrouping> GetTaskSummary(GraphNode<Key, FormNode> taskNode, Task task);
        TaskGrouping GetTaskItemSummary(GraphNode<Key, FormNode> taskItemNode, Task task, int? repeatIndex = null);
    }
}