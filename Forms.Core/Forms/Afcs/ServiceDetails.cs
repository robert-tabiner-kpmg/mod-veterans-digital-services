using System.Collections.Generic;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Questions;
using Forms.Core.Models.Static;
using Forms.Core.Models.Validation;

namespace Forms.Core.Forms.Afcs
{
    public static class ServiceDetails
    {
        public static Task Task => new Task
        {
            Id = "service-details-task",
            Name = "Service details",
            GroupNameIndex = 2,
            PreTaskPage = new PreTaskPage()
            {
                Header = "Service details",
                Body = "<p>We need to know about each period of service you have had in HM Armed Forces.  " +
                "A period of service is defined as a term of service between enlistment and discharge within one service type.</p>" +
                "<p>If you have had more than one period of service," +
                "or changed branches services for example from Royal Navy to Army, " +
                "please tell us about each period separately. You will be able to add further periods of service at the end of this section.<p>",
                BeginLinkText = "Add a period of service"
            },
            PostTaskPage = new RepeatTaskPage
            {
                Header = "Service details",
                SummaryTableText = "Service Period",
                Body = "You can add details of another period of service or click continue to go to the next section.",
                RepeatLinkText = "Add another period of service"
            },
            SummaryPage = new SummaryPage(),
            TaskItems = new List<ITaskItem>
            {
                new TaskQuestionPage
                {
                    Id = "service-name",
                    NextPageId = "service-number",
                    Header = "What was/is your full name during this period of service?",
                    IntroText = "<p>If you used more than one name, please include all names you used.</p>"+
                    "<P>If you do not wish to disclose a name you used, please write &#8216;contact me for details&#8217; and we will get in touch with you to discuss this further if we need to.</P>",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Id = "question1",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                IsRequiredMessage = "Enter the full name in service",
                                MaxLength = 50,
                                MaxLengthMessage = "Full name in service must be 50 characters or fewer"
                            })
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "service-number",
                    NextPageId = "service-branch",
                    Header = "What is your service number?",
                    IntroText = "<p>Please tell us the service number you had for this period of service.</p>",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Id = "question1",
                            Type = "Text",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                IsRequiredMessage = "Enter the service number",
                                MaxLength = 20,
                                MaxLengthMessage = "Service number must be 20 characters or fewer",
                            })
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "service-branch",
                    NextPageId = "service-type",
                    Header = "What was/is your service branch?",
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Id = "question1",
                            Options = new List<string>
                                {"Royal Navy", "Army", "Royal Air Force", "Royal Marines"},
                            Validator = new RadioValidation(new RadioValidationProperties
                            {
                                RequiredMessage = "Select your service branch",
                                ValidOptionsMessage = "Select your service branch",
                                ValidOptions = new[]
                                    {"Royal Navy", "Army", "Royal Air Force", "Royal Marines"}
                            })
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "service-type",
                    NextPageId = "service-rank",
                    Header = "What was/is your service type?",
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Id = "question1",
                            Options = new List<string> {"Regular", "Reserve (includes Full Time Reserve Service)"},
                            Validator = new RadioValidation(new RadioValidationProperties
                            {
                                RequiredMessage = "Select your service type",
                                ValidOptions = new[] {"Regular", "Reserve (includes Full Time Reserve Service)"},
                                ValidOptionsMessage = "Select your service type"
                            })
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "service-rank",
                    NextPageId = "service-trade",
                    Header = "What is your rank (if serving) or rank on discharge?",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Id = "question1",
                            Type = "Text",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                IsRequiredMessage = "Enter the service rank",
                                MaxLength = 30,
                                MaxLengthMessage = "Service rank must be 30 characters or fewer",
                            })
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "service-trade",
                    NextPageId = "service-enlistment-date",
                    Header = "What was/is your trade or specialism?",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Id = "question1",
                            Type = "Text",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                IsRequiredMessage = "Enter the service trade",
                                MaxLength = 30,
                                MaxLengthMessage = "Service trade must be 30 characters or fewer",
                            })
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "service-enlistment-date",
                    NextPageId = "service-discharge",
                    Header = "What was the date of your enlistment?",
                    IntroText = "Please tell us the date for this period of service.  If you can&#39;t remember exactly, include an estimated date even if this is only the year.",
                    Questions = new List<BaseQuestion>
                    {
                        new DateInputQuestion
                        {
                            
                            Id = "question1",
                            Validator = new DateInputValidation(new DateInputValidationProperties {IsInPast = true, IsRequired=false})
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "service-discharge",
                    NextPageId = "unit-address",
                    Header = "What was the date of and reason for your discharge?",
                    IntroText="Please tell us the date (if you are no longer serving) you left this period of service.  If you can&#39;t remember exactly, include an estimated date even if this is only the year.",
                    Questions = new List<BaseQuestion>
                    {
                        // TODO - validation here is a little tricky
                        new CheckboxQuestion()
                        {
                            Id = "question2",
                            Options = new List<string> {"I am still serving"}
                        },
                        new DateInputQuestion
                        {
                            Id = "question1",
                            Validator = new DateInputValidation(new DateInputValidationProperties {IsInPast = true, IsRequired=false})
                        },
                        new TextInputQuestion
                        {
                            Label = "Discharge reason",
                            Id = "question3",
                            Type = "Text",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                MaxLength = 40,
                            })
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "unit-address",
                    Header = "What was/is the address of your current/last service unit?",
                    IntroText = "Please include details to your best recollection for this period of service, even if the location has since closed down.",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Id = "question1",
                            Type = "Text",
                            Label = "Building name",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                MaxLength = 30
                            })
                        },
                        new TextInputQuestion
                        {
                            Id = "question2",
                            Type = "Text",
                            Label = "Street name",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                MaxLength = 30
                            })
                        },
                        new TextInputQuestion
                        {
                            Id = "question3",
                            Type = "Text",
                            Label = "Town or city",
                            Width = 15,
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                MaxLength = 30
                            })
                        },
                        new TextInputQuestion
                        {
                            Id = "question4",
                            Type = "Text",
                            Label = "County",
                            Width = 15,
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                MaxLength = 30
                            })
                        },
                        new TextInputQuestion
                        {
                            Id = "question5",
                            Type = "Text",
                            Label = "Country",
                            Width = 15,
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                MaxLength = 30, IsRequired = false
                            })
                        },
                        new TextInputQuestion
                        {
                            Id = "question5",
                            Type = "Text",
                            Label = "Postcode",
                            Width = 10,
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                MaxLength = 10
                            })
                        }
                    }
                }
            }
        };
    }
}