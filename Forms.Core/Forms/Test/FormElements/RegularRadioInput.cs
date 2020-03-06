using System.Collections.Generic;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Questions;
using Forms.Core.Models.Static;
using Forms.Core.Models.Validation;

namespace Forms.Core.Forms.Test.FormElements
{
    public class RegularRadioInput
    {
        public static Task Task => new Task
        {
            Id = "radio-input-task",
            Name = "Radio input (regular)",
            GroupNameIndex = 2,
            TaskItems = new List<ITaskItem>
            {
                new TaskQuestionPage
                {
                    Id = "radio-input-page",
                    Header = "Select an option",
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Id = "question1",
                            Label = "Label goes here",
                            Hint = "Hint goes here",
                            Inline = false,
                            Options = new List<string>
                            {
                                "Yes", "No", "Maybe"
                            },
                            Validator = new RadioValidation(new RadioValidationProperties())
                        }
                    },
                }
            }
        };
    }
}