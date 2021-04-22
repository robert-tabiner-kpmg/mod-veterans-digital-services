using System.Collections.Generic;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Questions;
using Forms.Core.Models.Static;
using Forms.Core.Models.Validation;
using Forms.Core.EffectHandlers.Models;
using System.Linq;
using Forms.Core.Models.InFlight.Decision.Ghost;

namespace Forms.Core.Forms.Afcs
{
    public static class AboutBenefits
    {
        public static Task Task => new Task
        {
            Id = "about-benefits-task",
            Name = "Other benefits, allowances or entitlement",
            GroupNameIndex = 4,
            SummaryPage = new SummaryPage(),
            TaskItems = new List<ITaskItem>
            {
                new TaskQuestionPage
                {
                    Id = "receiving-other-benefits",
                    Header = "Other benefits, allowances or entitlements you receive.",
                    IntroText ="Payments from the Armed Forces Compensation Scheme and War Pension Scheme MAY affect " +
                    "related benefits from the Department for Work and Pensions or other authorities.  " +
                    "It is your responsibility to inform the relevant Benefit Office, local authority or " +
                    "Tax Credit Office if you receive payments under one of their schemes.",
                    NextPageId = "receiving-other-payments",
                    Questions = new List<BaseQuestion>
                    {
                        new CheckboxQuestion
                        {
                            Id = "question1",
                            Label = "Are you receiving any of the following?",
                            Hint ="Please tick any that apply",
                            Options = new List<string>
                            {
                                "Tax credits paid to you or your family",
                                "Housing Benefit or Council Tax Benefit",
                                "Industrial Injuries Disablement Benefit",
                                "None"
                            },
                        }
                    }
                },

                new TaskQuestionPage
                {
                    Id = "receiving-other-payments",
                    Header = "Have you ever been paid any of the following?",
                    IntroText ="These schemes make payments for certain illnesses caused by exposure to asbestos and dust."+
                    "<ul><li>Diffuse Mesothelioma 2014 Scheme</li><li>Diffuse Mesothelioma 2008 Scheme</li>" +
                    "<li>The Workers Compensation 1979 Pneumoconiosis Act</li></ul>",
                    NextPageId = "other-payment-details",
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
                                ? "other-payment-details"
                                : "no-payments-received")
                    }
                },
                new TaskQuestionGhost("no-payments-received"),
                new TaskQuestionPage
                {
                    Id = "other-payment-details",
                    Header = "Please tell us the date you received the payment(s) and the amount you received.",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Id = "question1",
                            Label ="Diffuse Mesothelioma 2014 Scheme",
                            Hint="Date payment received and amount: ",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = false,
                                MaxLength = 100
                            })
                        },
                         new TextInputQuestion
                        {
                            Id = "question2",
                            Label ="Diffuse Mesothelioma 2008 Scheme",
                            Hint="Date payment received and amount: ",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = false,
                                MaxLength = 100
                            })
                        },
                          new TextInputQuestion
                        {
                            Id = "question3",
                            Label ="The Workers Compensation 1979 Pneumoconiosis Act",
                            Hint="Date payment received and amount: ",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = false,
                                MaxLength = 100
                            })
                        },
                    }
                },

            }
        };
    }
}