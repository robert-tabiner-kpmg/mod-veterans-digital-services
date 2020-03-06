using System.Collections.Generic;
using System.Linq;
using Forms.Core.EffectHandlers.Models;
using Forms.Core.Models.InFlight.Decision.Ghost;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Questions;
using Forms.Core.Models.Static;
using Forms.Core.Models.Validation;

namespace Forms.Core.Forms.Afcs
{
    public static class OtherCompensation
    {
        public static Task Task => new Task
        {
            Id = "other-compensation-task",
            Name = "Other compensation",
            GroupNameIndex = 3,
            SummaryPage = new SummaryPage(),
            TaskItems = new List<ITaskItem>
            {
                new TaskQuestionPage
                {
                    Id = "received-compensation",
                    NextPageId = "compensation-condition",
                    Header = "Are you claiming for or have you received compensation?",
                    IntroText = "This includes any compensation from MOD for criminal injuries or for civil negligence, or compensation from civil authorities in Great Britain and Northern Ireland for criminal injuries.",
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Id = "question1",
                            Options = new List<string> {"Yes", "No"},
                            Validator = new RadioValidation(new RadioValidationProperties())
                        }
                    },
                    Effects = new List<Effect>
                    {
                        new PathChangeEffect(x =>
                            x.First().Answer.Values["default"] == "Yes" ? "compensation-condition" : "no-compensation-received")
                    }
                },
                new TaskQuestionGhost("no-compensation-received"),
                new TaskQuestionPage
                {
                    Id = "compensation-condition",
                    NextPageId = "claim-outcome",
                    Header = "What condition(s) are you claiming or have you claimed compensation for?",
                    Questions = new List<BaseQuestion>
                    {
                        new TextareaQuestion
                        {
                            Id = "question1",
                            Rows = 5,
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 500
                            })
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "claim-outcome",
                    NextPageId = "other-payment-received",
                    Header = "Claim outcome",
                    IntroText = "Please include any reference numbers and details of the person or organisation.",
                    Questions = new List<BaseQuestion>
                    {
                        new TextareaQuestion
                        {
                            Id = "question1",
                            Label = "What is the status or what was the outcome of the claim?",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 500
                            })
                        },
                        new RadioQuestion
                        {
                            Label = "Did you receive a payment as a result of this claim?",
                            Id = "question2",
                            Options = new List<string> {"Yes", "No"},
                            Validator = new RadioValidation(new RadioValidationProperties())
                        }
                    },
                    Effects = new List<Effect>
                    {
                        new PathChangeEffect(x =>
                            x.First(q => q.Id == "question2").Answer.Values["default"] == "Yes"
                                ? "other-payment-received"
                                : "claim-solicitor-help")
                    }

                },
                new TaskQuestionPage
                {
                    Id = "other-payment-received",
                    NextPageId = "claim-payment-type",
                    Header = "Please tell us the amount of any payment you received?",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Id = "question1",
                            Label = "Amount paid",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 20
                            })
                        },
                    }
                },
                new TaskQuestionPage
                {
                    Id = "claim-payment-type",
                    NextPageId = "claim-payment-date",
                    Header = "What type of payment was this?",
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Id = "question1",
                            Options = new List<string> {"Interim settlement", "Final settlement"},
                            Validator = new RadioValidation(new RadioValidationProperties())
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "claim-payment-date",
                    NextPageId = "claim-solicitor-help",
                    Header = "When did you receive this payment?",
                    Questions = new List<BaseQuestion>
                    {
                        new DateInputQuestion
                        {
                            Id = "question1",
                            Hint = "For example, 27 03 2007",
                            Validator = new DateInputValidation(new DateInputValidationProperties {IsInPast = true})
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "claim-solicitor-help",
                    Header = "Did a solicitor help you with your claim for other compensation?",
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Id = "question1",
                            Options = new List<string> {"Yes", "No"},
                            Validator = new RadioValidation(new RadioValidationProperties())
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "claim-solicitor-details",
                    Header = "What are the contact details of the solicitor that helped you with your claim?",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Label = "Name",
                            Id = "question1",
                            Type = "Text",
                            Validator = new TextInputValidation(new TextInputValidationProperties())
                        },
                        new TextInputQuestion
                        {
                            Label = "Address",
                            Hint = "Building and street",
                            Id = "question2",
                            Type = "Text",
                            Validator = new TextInputValidation(new TextInputValidationProperties())
                        },
                        new TextInputQuestion
                        {
                            Id = "question3",
                            Type = "Text"
                        },
                        new TextInputQuestion
                        {
                            Id = "question4",
                            Hint = "Town of city",
                            Type = "Text",
                            Width = 12,
                            Validator = new TextInputValidation(new TextInputValidationProperties())
                        },
                        new TextInputQuestion
                        {
                            Id = "question5",
                            Hint = "County",
                            Type = "Text",
                            Width = 12,
                            Validator = new TextInputValidation(new TextInputValidationProperties())
                        },
                        new TextInputQuestion
                        {
                            Id = "question6",
                            Hint = "Postcode",
                            Type = "Text",
                            Width = 12,
                            Validator = new TextInputValidation(new TextInputValidationProperties())
                        },
                        new TextInputQuestion
                        {
                            Id = "question7",
                            Label = "Telephone number",
                            Type = "Text",
                            Width = 12,
                            Validator = new TelephoneValidation(new TelephoneValidationProperties())
                        }
                    }
                },
            }
        };
    }
}