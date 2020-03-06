using System.Collections.Generic;
using System.Linq;
using Forms.Core.EffectHandlers.Models;
using Forms.Core.Models.InFlight.Decision.Ghost;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Questions;
using Forms.Core.Models.Static;
using Forms.Core.Models.Validation;

namespace Forms.Core.Forms.Test
{
    public static class ExamplePathChanging
    {
        public static Task Task => new Task
        {
            Id = "seeking-employment-task",
            Name = "Your employment situation",
            GroupNameIndex = 1,
            SummaryPage = new SummaryPage
            {
                Header = "Check your answers for this section",
            },
            TaskItems = new List<ITaskItem>
            {
                new TaskQuestionPage
                {
                    Id = "seeking-employment",
                    Header = "Are you seeking new employment?",
                    NextPageId = "dream-job-title",
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Id = "question1",
                            Options = new List<string>
                            {
                                "Yes",
                                "No"
                            },
                            Validator = new RadioValidation(new RadioValidationProperties())
                        }
                    },
                    Effects = new List<Effect>
                    {
                        new PathChangeEffect(x =>
                            x.First().Answer.Values["default"] ==
                            "Yes"
                                ? "dream-job-title"
                                : "not-seeking-employment")
                    }
                },
                new TaskQuestionGhost("not-seeking-employment"),
                new TaskQuestionPage
                {
                    Id = "dream-job-title",
                    Header = "What would you dream job be?",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Id = "question1",
                            Type = "Text",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 50,
                            })
                        }
                    }
                }
            }
        };
    }
}