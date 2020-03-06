using Forms.Core.Models.Static;

namespace Forms.Core.Models.InFlight.Decision.Ghost
{
    public class TaskQuestionGhost : DecisionFormNode, ITaskItem
    {
        public TaskQuestionGhost(string id)
        {
            Id = id;
        }
        
        public string Id { get; set; }
        public string NextPageId { get; set; }
        public override DecisionFormNodeType DecisionFormNodeType => DecisionFormNodeType.TaskQuestionGhost;
    }
}