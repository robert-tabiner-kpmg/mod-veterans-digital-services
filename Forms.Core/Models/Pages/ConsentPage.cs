using System.Collections.Generic;
using Forms.Core.Models.Questions;

namespace Forms.Core.Models.Pages
{
    public class ConsentPage : PostTaskPage
    {
        public List<BaseQuestion> Questions { get; set; } = new List<BaseQuestion>();
    }
}