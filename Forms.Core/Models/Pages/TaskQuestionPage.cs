using System.Collections.Generic;
using Forms.Core.EffectHandlers.Models;
using Forms.Core.Models.Questions;
using Forms.Core.Models.Static;

namespace Forms.Core.Models.Pages
{
    public class TaskQuestionPage : Page, ITaskItem
    {
        public string Id { get; set; }
        public string NextPageId { get; set; }
        public List<BaseQuestion> Questions { get; set; } = new List<BaseQuestion>();
        public string Header { get; set; }
        public List<Effect> Effects { get; set; } = new List<Effect>();
        public string IntroText { get; set; }
        public string WarningText { get; set; }
        public bool IncludeQuestionNumber { get; set; } = true;
    }
}