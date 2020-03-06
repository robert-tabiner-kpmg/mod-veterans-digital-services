using System.Collections;
using System.Collections.Generic;

namespace Forms.Core.Models.InFlight.Physical
{
    public class TaskQuestionPageFormNode : PhysicalTaskFormNode
    {
        public TaskQuestionPageFormNode(string taskId, string pageId, IEnumerable<int> repeatIndices) : base(taskId)
        {
            PageId = pageId;
            RepeatIndices = repeatIndices;
        }

        public string PageId { get; }
        
        public override PhysicalFormNodeType PhysicalFormNodeType => PhysicalFormNodeType.TaskQuestionPage;
        public List<InFlightQuestion> Questions { get; set; } = new List<InFlightQuestion>();
        public PageStatus PageStatus { get; set; } = PageStatus.Unanswered;
        public IEnumerable<int> RepeatIndices { get; }
    }
}