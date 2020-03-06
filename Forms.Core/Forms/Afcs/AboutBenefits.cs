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
            GroupNameIndex = 3,
            SummaryPage = new SummaryPage(),
            TaskItems = new List<ITaskItem>
            {
                new TaskQuestionPage
                {
                    Id = "receiving-other-benefits",
                    Header = "Are you receiving any of the following?",
                    NextPageId = "receiving-industrial-benefit",
                    Questions = new List<BaseQuestion>
                    {
                        new CheckboxQuestion
                        {
                            Id = "question1",
                            Options = new List<string>
                            {
                                "Personal Independence Payment (PIP) or Disability Living Allowance (DLA)",
                                "Income support",
                                "Universal credit",
                                "Income-related Employment and Support Allowance (ESA)",
                                "Tax Credits paid to you or your family",
                                "Housing Benefit and Council Tax Benefit"
                            },
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "receiving-industrial-benefit",
                    Header = "Are you receiving Industrial Injuries Disablement Benefit (IIDB)?",
                    IntroText =
                        "You may receive an Industrial Injuries Disablement Benefit (IIDB) if you became ill or are disabled because of an accident or disease either at work or on an approved employment training scheme or course.",
                    NextPageId = "mesothelioma-payment",
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
                    Id = "mesothelioma-payment",
                    Header = "Have you received payment under the Diffuse Mesothelioma Payment Scheme (DMPS)?",
                    NextPageId = "mesothelioma-payment-2008",
                    IntroText =
                        "You may be claiming for this if you were diagnosed with diffuse mesothelioma on or after 25 July 2012.",
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
                    Id = "mesothelioma-payment-2008",
                    Header = "Have you received payment under the Diffuse Mesothelioma 2008 Scheme?",
                    NextPageId = "pneumoconisos-payment",
                    IntroText =
                        "You may be claiming for this if you were diagnosed with diffuse mesothelioma before 25 July 2012.",
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
                    Id = "pneumoconisos-payment",
                    Header = "Have you received payment under The Workers Compensation 1979 Pneumoconiosis Act?",
                    IntroText =
                        "The Pneumoconiosis etc. (Workers’ Compensation) Act 1979 provides lump sum payments to sufferers of certain dust related industrial diseases.",
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
            }
        };
    }
}