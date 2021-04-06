using System.Collections.Generic;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Questions;
using Forms.Core.Models.Static;
using Forms.Core.Models.Validation;

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
                    "Tax Credit Office if you receive payments under one of the schemes.",
                    NextPageId = "receiving-other-payments",
                    Questions = new List<BaseQuestion>
                    {
                        new CheckboxQuestion
                        {
                            Id = "question1",
                            Label = "Are you receiving any of the following?",
                            Options = new List<string>
                            {
                                "Tax credits paid to you or your family",
                                "Housing Benefit or Council Tax Benefit",
                                "Industrial Injuries Disablement Benefit"
                            },
                        }
                    }
                },

                new TaskQuestionPage
                {
                    Id = "receiving-other-payments",
                    Header = "Have you ever been paid any of the following?",
                    IntroText ="These schemes make payments for certain illnesses caused by exposure to asbestos and dust.",
                    NextPageId = "other-payment-details",
                    Questions = new List<BaseQuestion>
                    {
                        new CheckboxQuestion
                        {
                            Id = "question1",
                            Options = new List<string>
                            {
                                "Diffuse Mesothelioma 2014 Scheme",
                                "Diffuse Mesothelioma 2008 Scheme",
                                "The Workers Compensation 1979 Pneumoconiosis Act"
                            },
                        }

                    }
                },
                new TaskQuestionPage
                {
                    Id = "other-payment-details",
                    Header = "Please tell us the date you received the payment(s) and the amount you received.",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Id = "question1",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 100
                            })
                        },
                    }
                },

            }
        };
    }
}