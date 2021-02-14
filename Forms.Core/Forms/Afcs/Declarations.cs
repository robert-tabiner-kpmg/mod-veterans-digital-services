using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forms.Core.EffectHandlers.Models;
using Forms.Core.Models.InFlight.Decision.Ghost;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Questions;
using Forms.Core.Models.Static;
using Forms.Core.Models.Validation;

namespace Forms.Core.Forms.Afcs
{
    public static class Declarations
    {
        public static Task Task => new Task
        {
            Id = "declarations-task",
            HiddenWhen = (tasks) =>
            {
                return false; 
                // return tasks.Any(x => x.taskId != "declarations-task" && !x.isTaskComplete);
            },
            SummaryPage = new SummaryPage(),
            Name = "Declaration and submission",
            GroupNameIndex = 6,
            TaskItems = new List<ITaskItem>
            {
                new TaskQuestionPage
                {
                    Id = "is-welfare-service-agent",
                    Header = "Is this claim being completed by a Veterans Welfare Service (VWS) or Authorised Agent?",
                    NextPageId = "welfare-service-agent",
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
                            x.First().Answer.Values["default"] == "Yes"
                                ? "welfare-service-agent"
                                : "self-completed")
                    }
                },
                new TaskQuestionGhost("self-completed"),
                new TaskQuestionPage
                {
                    Id = "welfare-service-agent",
                    Header = "For completion by Veterans Welfare Service (VWS) or Authorised Agent only",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Label = "Reference number",
                            Id = "question1",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                MaxLength = 30
                            })
                        },
                        new TextInputQuestion
                        {
                            Label = "Name of department or organisation",
                            Id = "question2",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                MaxLength = 30
                            })
                        },
                        new TextInputQuestion
                        {
                            Label = "Building and street",
                            Id = "question3",
                            Type = "Text",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 30
                            })                        
                        },
                        new TextInputQuestion
                        {
                            Id = "question4",
                            Type = "Text",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                MaxLength = 30
                            })
                        },
                        new TextInputQuestion
                        {
                            Id = "question5",
                            Label = "Town or city",
                            Type = "Text",
                            Width = 12,
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 20
                            })
                        },
                        new TextInputQuestion
                        {
                            Id = "question6",
                            Label = "County",
                            Type = "Text",
                            Width = 12,
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 20
                            })
                        },
                        new TextInputQuestion
                        {
                            Id = "question7",
                            Label = "Postcode",
                            Type = "Text",
                            Width = 12,
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 10
                            })
                        },
                        new DateInputQuestion
                        {
                            Id = "question8",
                            Label = "When did the claimant first contact the VWS or 'Authorised Agent' about this claim?",
                            Hint = "For example, 27 03 2007",
                            Validator = new DateInputValidation(new DateInputValidationProperties
                            {
                                IsInPast = true
                            })
                        }
                    }
                }
            },
            PostTaskPage = new ConsentPage
            {
                Header = "Consent page",
                Questions = new List<BaseQuestion>
                {
                    new RadioQuestion
                    {
                        Id = "question1",
                        Label = "I agree to repay any sum paid as a result of this claim in the event that an overpayment is made for any reason",
                        Options = new List<string>
                        {
                            "I agree"
                        },
                        Validator = new RadioValidation(new RadioValidationProperties())
                    }
                }
            },
        };
    }
}