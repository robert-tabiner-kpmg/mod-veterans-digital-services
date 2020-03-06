using System.Collections.Generic;

namespace Forms.Core.Models.Questions
{
    public class RadioQuestion : BaseQuestion
    {
        public List<string> Options { get; set; }
        public bool Inline { get; set; }
    }
}