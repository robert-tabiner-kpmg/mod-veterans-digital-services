using System.Collections.Generic;
using Forms.Core.EffectHandlers.Models;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Questions;
using Forms.Core.Models.Static;
using Forms.Core.Models.Validation;
using System.Linq;

namespace Forms.Core.Forms.Afcs
{
    public static class PersonalDetails
    {
        public static Task Task =>
            new Task
            {
                Id = "personal-details-task",
                Name = "Personal details",
                GroupNameIndex = 2,
                SummaryPage = new SummaryPage(),
                PostTaskPage = null,
                PreTaskPage = null,
                TaskItems = new List<ITaskItem>
                {
                    new TaskQuestionPage
                    {
                        Id = "name",
                        NextPageId = "contact-address",
                        Header = "What is your name?",
                        Questions = new List<BaseQuestion>
                        {
                            new TextInputQuestion
                            {
                                Id = "question1",
                                Type = "Text",
                                Label =
                                    "Surname or family name",
                                Validator = new TextInputValidation(new TextInputValidationProperties
                                {
                                    IsRequired = true,
                                    MaxLength = 40
                                })
                            },
                            new TextInputQuestion
                            {
                                Id = "question2",
                                Type = "Text",
                                Label =
                                    "All other names in full",
                                Validator = new TextInputValidation(new TextInputValidationProperties
                                {
                                    IsRequired = true,
                                    MaxLength = 40
                                })
                            }
                        }
                    },
                    new TaskQuestionPage
                    {
                        Id = "contact-address",
                        NextPageId = "date-of-birth",
                        Header = "What is your your address?",
                        IntroText = "We will send any postal correspondence to this address.",
                        Questions = new List<BaseQuestion>
                        {
                            new TextInputQuestion
                            {
                                Id = "question1",
                                Type = "Text",
                                Label = "Building and street",
                                Hint = "Base name for military establishments",
                                Validator = new TextInputValidation(new TextInputValidationProperties
                                {
                                    IsRequired = true,
                                    MaxLength = 30
                                })
                            },
                            new TextInputQuestion
                            {
                                Id = "question2",
                                Type = "Text",
                                Label = "",
                                Validator = new TextInputValidation(new TextInputValidationProperties
                                {
                                    IsRequired = true,
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
                                    IsRequired = true,
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
                                    IsRequired = true,
                                    MaxLength = 30
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
                                    IsRequired = true,
                                    MaxLength = 10
                                })
                            },
                            new TextInputQuestion
                            {
                                Id = "question6",
                                Type = "Text",
                                Label = "Country (if overseas)",
                                Width = 15,
                                Validator = new TextInputValidation(new TextInputValidationProperties
                                {
                                    IsRequired = true,
                                    MaxLength = 30
                                })
                            }
                        }
                    },
                    new TaskQuestionPage
                    {
                        Id = "date-of-birth",
                        NextPageId = "contact-number",
                        Header = "What is your date of birth?",
                        Questions = new List<BaseQuestion>
                        {
                            new DateInputQuestion
                            {
                                Id = "question1",
                                Hint = "For example: 31 03 1980",
                                Validator = new DateInputValidation(new DateInputValidationProperties {IsInPast = true})
                            }
                        }
                    },
                    new TaskQuestionPage
                    {
                        Id = "contact-number",
                        NextPageId = "contact-email",
                        Header = "What is your telephone number?",
                        IntroText = "We will use this to contact you if we have any questions about your claim.",
                        Questions = new List<BaseQuestion>
                        {
                            new TextInputQuestion
                            {
                                Label = "Daytime Telephone Number",
                                Id = "question1",
                                Type = "Text",
                                Hint = "For overseas numbers include the country code",
                                Validator = new TelephoneValidation(new TelephoneValidationProperties())
                            },
                            new TextInputQuestion
                            {
                                Id = "question2",
                                Type = "Text",
                                Label =
                                    "Mobile Telephone Number",
                                 Hint = "For overseas numbers include the country code",
                                Validator = new TelephoneValidation(new TelephoneValidationProperties())
                            }
                        }
                    },
                    new TaskQuestionPage
                    {
                        Id = "contact-email",
                        Header = "What is your email address?",
                        NextPageId = "national-insurance",
                        Questions = new List<BaseQuestion>
                        {
                            new TextInputQuestion()
                            {
                                Id = "question1",
                                Type = "email",
                                Autocomplete = "email",
                                Hint = "Please tell us the email address you would prefer us to contact you at. " +
                                "We will only use this to get in touch about your claim.",
                                Width = 50,
                                Validator = new EmailValidation(new EmailValidationProperties())
                            }
                        }
                    },
                    new TaskQuestionPage
                    {
                        Id = "national-insurance",
                        NextPageId = "pension-scheme",
                        Header = "What is your National Insurance number?",
                        IntroText =
                            "It's on your National Insurance card, benefit letter, payslip or p60. For example: 'QQ 12 34 56 C'.",
                        Questions = new List<BaseQuestion>
                        {
                            new TextInputQuestion
                            {
                                Id = "question1",
                                Type = "Text",
                                Validator = new NationalInsuranceValidation(0, 13)
                            }
                        }
                    },
                    new TaskQuestionPage
                    {
                        Id = "pension-scheme",
                        NextPageId = "previous-claim",
                        Header = "Which armed forces pension scheme are you a member of?",
                        Questions = new List<BaseQuestion>
                        {
                            new RadioQuestion
                            {
                                Hint = "Select one option.",
                                Id = "question1",
                                Options = new List<string>
                                {
                                    "1975", "2005", "2015", "None", "Other", "Don't know"
                                }, //Please give details if selecting "Other"
                                Validator = new RadioValidation(new RadioValidationProperties())
                            }
                        }
                    },
                    new TaskQuestionPage
                    {
                        Id = "previous-claim",
                        NextPageId = "gp-details",
                        Header = "Have you made a war pension or armed forces compensation scheme claim previously?",
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
                                x.First().Answer.Values["default"] == "Yes"
                                    ? "afcs-claim-reference"
                                    : "gp-details")
                        }
                    },
                    new TaskQuestionPage
                    {
                        Id = "afcs-claim-reference",
                        NextPageId = "gp-details",
                        Header = "What is the claim reference number (if known)?",
                        Questions = new List<BaseQuestion>
                        {
                            new TextInputQuestion
                            {
                                Id = "question1",
                                Type = "Text",
                                Label = "",
                                Validator = new TextInputValidation(new TextInputValidationProperties
                                {
                                    MaxLength = 50
                                })
                            }
                        }
                    },
                    new TaskQuestionPage
                    {
                        Id = "gp-details",
                        Header = "What is the name and address of your current Medical Officer or GP?",
                        Questions = new List<BaseQuestion>
                        {
                            new TextInputQuestion
                            {
                                Id = "question1",
                                Type = "Text",
                                Label = "Name",
                                Validator = new TextInputValidation(new TextInputValidationProperties
                                {
                                    IsRequired = true,
                                    MaxLength = 30
                                })
                            },
                            new TextInputQuestion
                            {
                                Id = "question2",
                                Type = "Text",
                                Label = "Building and street",
                                Validator = new TextInputValidation(new TextInputValidationProperties
                                {
                                    IsRequired = true,
                                    MaxLength = 30
                                })
                            },
                            new TextInputQuestion
                            {
                                Id = "question3",
                                Type = "Text",
                                Label = "",
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
                                Label = "Town or city",
                                Width = 15,
                                Validator = new TextInputValidation(new TextInputValidationProperties
                                {
                                    IsRequired = true,
                                    MaxLength = 30
                                })
                            },
                            new TextInputQuestion
                            {
                                Id = "question5",
                                Type = "Text",
                                Label = "County",
                                Width = 15,
                                Validator = new TextInputValidation(new TextInputValidationProperties
                                {
                                    IsRequired = true,
                                    MaxLength = 30
                                })
                            },
                            new TextInputQuestion
                            {
                                Id = "question6",
                                Type = "Text",
                                Label = "Postcode",
                                Width = 10,
                                Validator = new TextInputValidation(new TextInputValidationProperties
                                {
                                    IsRequired = true,
                                    MaxLength = 10
                                })
                            },
                             new TextInputQuestion
                            {
                                Id = "question7",
                                Type = "Text",
                                Label = "Country (if overseas)",
                                Width = 15,
                                Validator = new TextInputValidation(new TextInputValidationProperties
                                {
                                    IsRequired = true,
                                    MaxLength = 30
                                })
                            },
                            new TextInputQuestion
                            {
                                Id = "question8",
                                Type = "Text",
                                Label = "Telephone number",
                                Width = 10,
                                Validator = new TelephoneValidation(new TelephoneValidationProperties())
                            }
                        }
                    },
                }
            };
    }
}