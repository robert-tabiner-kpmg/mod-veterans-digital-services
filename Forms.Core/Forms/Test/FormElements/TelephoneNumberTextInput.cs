using System.Collections.Generic;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Questions;
using Forms.Core.Models.Static;
using Forms.Core.Models.Validation;

namespace Forms.Core.Forms.Test.FormElements
{
    public class TelephoneNumberTextInput
    {
        public static Task Task => new Task
        {
            Id = "telephone-input-task",
            Name = "Text input (telephone)",
            GroupNameIndex = 2,
            TaskItems = new List<ITaskItem>
            {
                new TaskQuestionPage
                {
                    Id = "telephone-input-page",
                    Header = "Enter a telephone number",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Id = "question1",
                            Label = "Label goes here",
                            Hint = "Hint goes here",
                            Type = "tel",
                            Autocomplete = "tel",
                            Validator = new TelephoneValidation(new TelephoneValidationProperties())
                        }
                    },
                }
            }
        };
    }
}