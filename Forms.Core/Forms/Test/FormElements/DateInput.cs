using System.Collections.Generic;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Questions;
using Forms.Core.Models.Static;
using Forms.Core.Models.Validation;

namespace Forms.Core.Forms.Test.FormElements
{
    public class DateInput
    {
        public static Task Task => new Task
        {
            Id = "date-input-task",
            Name = "Date input",
            GroupNameIndex = 2,
            TaskItems = new List<ITaskItem>
            {
                new TaskQuestionPage
                {
                    Id = "date-input-page",
                    Header = "Enter a date",
                    Questions = new List<BaseQuestion>
                    {
                        new DateInputQuestion
                        {
                            Id = "question1",
                            Label = "Label goes here",
                            Hint = "Hint goes here",
                            Validator = new DateInputValidation(new DateInputValidationProperties
                            {
                                IsRequired = true,
                                IsInPast = true,
                            })
                        }
                    },
                }
            }
        };
    }
}