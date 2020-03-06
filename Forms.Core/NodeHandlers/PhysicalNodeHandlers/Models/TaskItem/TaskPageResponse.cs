using System.Collections.Generic;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.Pages;

namespace Forms.Core.NodeHandlers.PhysicalNodeHandlers.Models.TaskItem
{
    public class TaskPageResponse : PhysicalResponse
    {
        public string NodeId { get; set; }
        public Dictionary<string, Answer> Answers { get; set; }
        public TaskQuestionPage TaskQuestionPage { get; set; }
        public int QuestionNumber { get; set; }
        public int QuestionCount { get; set; }
    }
}