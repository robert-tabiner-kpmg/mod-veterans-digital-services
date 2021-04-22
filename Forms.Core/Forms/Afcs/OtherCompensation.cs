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
            GroupNameIndex = 4,
            SummaryPage = new SummaryPage(),
            TaskItems = new List<ITaskItem>
            {
                new TaskQuestionPage
                {
                    Id = "received-compensation",
                    NextPageId = "compensation-condition",
                    Header = "Are you claiming for or have you received compensation payments from other sources?",
                    IntroText = "You only need to tell us about compensation for the medical conditions you are claiming for on this application." +
                    "<p>Compensation includes any payments from MOD for criminal injuries; civil negligence payments received via the courts; " +
                    "compensation from civil authorities in Great Britain and Northern Ireland for criminal injuries or any other compensation payments received for the medical conditions you are claiming for.</p>",
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
                    Header = "What medical condition(s) have you received (or are you claiming) other compensation for?",
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
                    IntroText = "Please include any reference numbers you have.",
                    Questions = new List<BaseQuestion>
                    {
                        new TextareaQuestion
                        {
                            Id = "question1",
                            Label = "Who did you claim from and what was the outcome of the claim?",
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
                    NextPageId = "claim-solicitor-details",
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
                            x.First().Answer.Values["default"] == "Yes" ? "claim-solicitor-details" : "no-solicitor")
                    }
                },
                 new TaskQuestionGhost("no-solicitor"),
                new TaskQuestionPage
                {
                    Id = "claim-solicitor-details",
                    Header = "What was the name and address of the solicitor who helped you?",
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
                            Label = "",
                            Type = "Text"
                        },
                        new TextInputQuestion
                        {
                            Id = "question4",
                            Label = "Town of city",
                            Type = "Text",
                            Width = 12,
                            Validator = new TextInputValidation(new TextInputValidationProperties())
                        },
                        new TextInputQuestion
                        {
                            Id = "question5",
                            Label = "County",
                            Type = "Text",
                            Width = 12,
                            Validator = new TextInputValidation(new TextInputValidationProperties())
                        },
                        new TextInputQuestion
                        {
                            Id = "question6",
                            Label = "Postcode",
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

                           // Validator = new TelephoneValidation(new TelephoneValidationProperties())
                        }
                    }
                },
            }
        };
    }
}