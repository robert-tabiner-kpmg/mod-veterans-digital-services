using System.Collections.Generic;
using Forms.Core.Models.Pages;

namespace Forms.Core.Models.Static
{
    public class Form
    {
        public string Name { get; set; }
        public TaskListPage TaskListPage { get; set; }
        public List<Task> Tasks { get; set; }
        public List<string> TaskGroups { get; set; }
        public ContentPage StartPage { get; set; }
        public ContentPage TermsAndConditions { get; set; }
    }
}