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
            Name = "Do you want to nominate a representative?",
            GroupNameIndex = 1,
            TaskItems = new List<ITaskItem>
            {
                new TaskQuestionPage
                {
                    Id = "nominate-someone-1",
                    NextPageId = "nominee-details",
                    Header = "Do you want to nominate a representative?",
                    IntroText = new StringBuilder()
                        .Append(
                            "<p>If someone is helping you make your claim, e.g. a charity welfare officer or a solicitor, you can nominate them as a representative to ask us how your claim is progressing or receive copies of our enquiries and decision letters.</p>")
                        .Append(
                            "<p>We can only give them this information if we have your written agreement. When a decision on your claim is made, we will, with your agreement, send a copy of the notification to your nominated representative.</p>")
                        .Append(
                            "<p><b>Please be aware</b> that the decision notification contains personal information. This may include details of your bank or building society account and any medical conditions that we have considered as part of your claim. It may also show how we have calculated your award.</p>")
                        .Append(
                            "<p>You can nominate 1 representative here but you can add further representatives or change their details by writing to us at any time.</p>")
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
                    NextPageId = "nomination-other-details",
                    Header = "Please provide contact details for your nominated representative",
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
                            Label = "",
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
                        },
                        new TextInputQuestion
                        {
                            Label = "Email address",
                            Id = "question2",
                            Type = "Text",
                            Validator = new EmailValidation(new EmailValidationProperties())
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "nomination-other-details",
                    Header =
                        "What is your representative&#39s role?",
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Id = "question1",
                            Options = new List<string> { "Veterans UK welfare manager", "Charity welfare manager","Solicitor","Friend or relative","Other"},
                            Validator = new RadioValidation(new RadioValidationProperties())
                        }
                    },
                    Effects = new List<Effect>
                    {
                        new PathChangeEffect(x =>
                            x.First().Answer.Values["default"] == "Yes" ? "nominee-details" : "no-representative")
                    }
                    }
                
            }
        };
    }
}