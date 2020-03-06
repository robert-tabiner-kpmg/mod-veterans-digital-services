using System.Collections.Generic;
using System.Text;
using Forms.Core.Forms.Test.FormElements;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Static;

namespace Forms.Core.Forms.Test
{
    public static class Test
    {
        public static Form Form => new Form
        {
            Name = "Automated Testing Form",
            TaskListPage = new TaskListPage(),
            TaskGroups = new List<string>
            {
                "Example subtask and repeating",
                "Example path changing",
                "Form element tests"
            },
            Tasks = new List<Task>
            {
                ExampleSubTaskAndRepeating.Task,
                ExamplePathChanging.Task,
                DateInput.Task,
                RegularTextInput.Task,
                NumberTextInput.Task,
                TelephoneNumberTextInput.Task,
                EmailTextInput.Task,
                RegularRadioInput.Task,
                InlineRadioInput.Task,
            },
            StartPage = new ContentPage
            {
                Header = "Automated Testing Form",
                BodyText = new StringBuilder()
                    .Append("<p>Use this form when testing.</p>")
                    .ToString()
            },
            TermsAndConditions = new ContentPage
            {
                Header = "This is the Terms & Conditions page",
                BodyText = new StringBuilder()
                    .Append("<p><strong>Here is some text in bold explaining something.</strong></p>")
                    .Append("<p>Here is some additional text.</p>")
                    .ToString()
            }
        };
    }
}