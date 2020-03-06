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
    public static class NominateRepresentative
    {
        public static Task Task => new Task
        {
            Id = "nominate-representative-task",
            SummaryPage = new SummaryPage(),
            Name = "Nominate a representative",
            GroupNameIndex = 5,
            TaskItems = new List<ITaskItem>
            {
                new TaskQuestionPage
                {
                    Id = "nominate-someone-1",
                    NextPageId = "nominee-details",
                    Header = "Nominate a representative",
                    IntroText = new StringBuilder()
                        .Append(
                            "<p>If you wish to nominate a representative to hep you with your claim, please provide their details below.</p>")
                        .Append(
                            "<p>You can nominate a representative who may ask us how your claim is progressing. We can only give them this information if we have your written agreement.</p>")
                        .Append(
                            "<p>When a decision on your claim is made, we will with your agreement, send a copy of the notification to your nominated representative.</p>")
                        .Append(
                            "<p>Please be aware that the decision notification contains personal information. This may include details of your bank or building society account and any medical conditions that we have considered as part of your claim. It may also show how we have calculated your award.</p>")
                        .ToString(),
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Label = "Would you like to nominate a representative?",
                            Id = "question1",
                            Options = new List<string> {"No", "Yes"},
                            Validator = new RadioValidation(new RadioValidationProperties())
                        }
                    },
                    Effects = new List<Effect>
                    {
                        new PathChangeEffect(x =>
                            x.First().Answer.Values["default"] == "Yes" ? "nominee-details" : "no-representative")
                    }
                },
                new TaskQuestionGhost("no-representative"),
                new TaskQuestionPage
                {
                    Id = "nominee-details",
                    NextPageId = "nomination-release-details",
                    Header = "Please provide contact details for you nominated representative",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Label = "Full name",
                            Id = "question1",
                            Type = "Text",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 30
                            })
                        },
                        new TextInputQuestion
                        {
                            Label = "Email address",
                            Id = "question2",
                            Type = "Text",
                            Validator = new EmailValidation(new EmailValidationProperties())
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
                        new TextInputQuestion
                        {
                        Id = "question8",
                        Label = "Telephone number",
                        Type = "Number",
                        Width = 12,
                        Validator = new TelephoneValidation(new TelephoneValidationProperties())
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "nomination-release-details",
                    Header =
                        "Please tick the boxes below to tell us what information we can release to your representative.",
                    Questions = new List<BaseQuestion>
                    {
                        new CheckboxQuestion
                        {
                            Label = "Current claim",
                            Id = "question1",
                            Options = new List<string> {"Information on the present position"}
                        },
                        new CheckboxQuestion
                        {
                            Label = "Future claims or reviews",
                            Id = "question2",
                            Options = new List<string> {"Any documents relating on the claim"}
                        },
                        new CheckboxQuestion
                        {
                            Label = "Decision notification",
                            Id = "question3",
                            Options = new List<string>
                            {
                                "A copy of the decision notification",
                                "A decision notification for any future claim/review"
                            }
                        }
                    }
                }
            }
        };
    }
}