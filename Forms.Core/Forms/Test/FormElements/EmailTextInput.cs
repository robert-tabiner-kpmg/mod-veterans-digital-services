using System.Collections.Generic;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Questions;
using Forms.Core.Models.Static;
using Forms.Core.Models.Validation;

namespace Forms.Core.Forms.Test.FormElements
{
    public class EmailTextInput
    {
        public static Task Task => new Task
        {
            Id = "email-input-task",
            Name = "Text input (email)",
            GroupNameIndex = 2,
            TaskItems = new List<ITaskItem>
            {
                new TaskQuestionPage
                {
                    Id = "email-input-page",
                    Header = "Enter an email address",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Id = "question1",
                            Label = "Label goes here",
                            Hint = "Hint goes here",
                            Type = "email",
                            Autocomplete = "email",
                            Validator = new EmailValidation(new EmailValidationProperties())
                        }
                    },
                }
            }
        };
    }
}