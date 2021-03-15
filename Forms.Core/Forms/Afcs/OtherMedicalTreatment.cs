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
    public static class OtherMedicalTreatment
    {
        public static Task Task => new Task
        {
            Id = "other-medical-reatment-task",
            Name = "Other Medical Treatment",
            GroupNameIndex = 3,
            SummaryPage = new SummaryPage(),
            PostTaskPage = new RepeatTaskPage
            {
                Header = "Other Medical Treatment",
                RepeatLinkText = "Add another Medical Treatment",
                SummaryTableText = "Hospital"
            },
            PreTaskPage = new PreTaskPage
            {
                Header = "Other Medical Treatment",
                Body = new StringBuilder()
                    .Append("<p>We need to know about any hospitals or medical facilities that have provided you with further treatment for the conditions you are claiming for, or if you are on a waiting list for treatment.</p>")
                    .Append("<p>If you have visited the same hospital or facility several times, you only need to provide the details in this section once.</p>")
                    .ToString(),
                BeginLinkText = "Add a Medical Treatment",


                //new TaskQuestionPage
                //{
                //    Id = "other-medical-treatment-status",
                //    Header = "Have you had any further hospital or medical treatment?",
                //    //NextPageId = "other-medical-treatment-address",
                //    Questions = new List<BaseQuestion>
                //    {
                //        new RadioQuestion
                //        {
                //           // Hint = "We need to know:",
                //            Id = "question1",
                //            Options = new List<string>
                //            {
                //                "Yes",
                //                "No - I have not received any further medical treatment."
                //            },
                //            Validator = new RadioValidation(new RadioValidationProperties())
                //        }
                //    },
                //    Effects = new List<Effect>
                //    {
                //        new PathChangeEffect(x =>
                //            x.First().Answer.Values["default"] ==
                //            "Yes"
                //                ? "other-medical-treatment-address"
                //                //: "claim-time-date")
                //                : "")
                //    }

                //}
            },
            TaskItems = new List<ITaskItem>
            {
                //new TaskQuestionPage
                //{
                //    Id = "other-medical-treatment-status",
                //    Header = "Have you had any further hospital or medical treatment?",
                //    //NextPageId = "other-medical-treatment-address",
                //    Questions = new List<BaseQuestion>
                //    {
                //        new RadioQuestion
                //        {
                //           // Hint = "We need to know:",
                //            Id = "question1",
                //            Options = new List<string>
                //            {
                //                "Yes",
                //                "No - I have not received any further medical treatment."
                //            },
                //            Validator = new RadioValidation(new RadioValidationProperties())
                //        }
                //    },
                //    Effects = new List<Effect>
                //    {
                //        new PathChangeEffect(x =>
                //            x.First().Answer.Values["default"] ==
                //            "Yes"
                //                ? "other-medical-treatment-address"
                //                //: "claim-time-date")
                //                : null)
                //    }

                //},
                 new TaskQuestionPage
                        {
                            Id = "other-medical-treatment-address",
                            NextPageId = "other-medical-treatment-start-date",
                            Header = "Please tell us the name and address of the hospital or facility providing the treatment?",
                            Questions = new List<BaseQuestion>
                            {
                                new TextInputQuestion
                                {
                                    Label = "Name",
                                    Id = "question1",
                                    Type = "Text"
                                },

                                new TextInputQuestion
                                {
                                    Label = "Address",
                                    Hint = "Building and street",
                                    Id = "question2",
                                    Type = "Text"
                                },
                                new TextInputQuestion
                                {
                                    Label = "",
                                    Id = "question3",
                                    Type = "Text"
                                },
                                new TextInputQuestion
                                {
                                    Id = "question4",
                                    Label = "Town or city",
                                    Type = "Text",
                                    Width = 12
                                },
                                new TextInputQuestion
                                {
                                    Id = "question5",
                                    Label = "County",
                                    Type = "Text",
                                    Width = 12
                                },
                                new TextInputQuestion
                                {
                                    Id = "question6",
                                    Label = "Postcode",
                                    Type = "Text",
                                    Width = 12
                                },

                                new TextInputQuestion
                                {
                                    Id = "question7",
                                    Label = "Telephone number"
                                },

                                new TextInputQuestion()
                                {
                                    Id = "question8",
                                    Label = "Email",
                                    Type = "email",
                                    Autocomplete = "email",
                                    Width = 50,
                                    Validator = new EmailValidation(new EmailValidationProperties()
                                        {
                                            IsRequired = false
                                        })
                                }

                            }
                        },

                           new TaskQuestionPage
                        {
                            Id = "other-medical-treatment-start-date",
                            Header = "When did this treatment start?",
                            IntroText="If you have received treatment at this hospistal medical facility on more than one occasion, please tell us the first time you visited",
                            NextPageId = "other-medical-treatment-end-date",
                            Questions = new List<BaseQuestion>
                            {
                                new DateInputQuestion
                                {
                                    Id = "question1",
                                    Hint = "For example 27 3 2007",
                                    Validator = new DateInputValidation(new DateInputValidationProperties {IsInPast = true})
                                },

                                new CheckboxQuestion
                                {
                                    Id = "question2",
                                    Options = new List<string>
                                    {
                                        "This date is approximate"
                                    }
                                },
                                 new CheckboxQuestion
                                {
                                    Id = "question2",
                                    Options = new List<string>
                                    {
                                        "I am still on a waiting list to attend"
                                    }
                                }
                            }
                        },

                           new TaskQuestionPage
                        {
                            Id = "other-medical-treatment-end-date",
                            Header = "When did this treatment end?",
                            NextPageId = "other-medical-treatment-type",
                            Questions = new List<BaseQuestion>
                            {
                                new DateInputQuestion
                                {
                                    Id = "question1",
                                    Hint = "For example 27 3 2007",
                                    Validator = new DateInputValidation(new DateInputValidationProperties {IsInPast = true})
                                },

                                new CheckboxQuestion
                                {
                                    Id = "question2",
                                    Options = new List<string>
                                    {
                                        "This date is approximate"
                                    }
                                },
                                 new CheckboxQuestion
                                {
                                    Id = "question2",
                                    Options = new List<string>
                                    {
                                        "This treatment has not yet ended"
                                    }
                                }
                            }
                        },

                new TaskQuestionPage
                {
                    Id = "other-medical-treatment-type",
                    Header = "What type of medical treatment did you receive?",
                    IntroText = "E.g. Surgery, specialist consultation,tests, physiotherapy",
                   NextPageId = "other-medical-treatment-condition",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Id = "question1",
                            //Hint = "Please provide your rank at the time",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 100
                            })
                        }
                    }
                },

                 new TaskQuestionPage
                {
                    Id = "other-medical-treatment-condition",
                    Header = "What conditions(s) was this treatment for?",
                   
                   //NextPageId = "claim-illness-surgery-address",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Id = "question1",
                            //Hint = "Please provide your rank at the time",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 100
                            })
                        }
                    }
                },

 





 

   

   

      


                new SubTask
                {
                    Id = "claim-illness-subtask",
                    NextPageId = null,
                    DisplayName = "Claim Illnesses",
                    PreTaskPage = new PreTaskPage
                    {
                        Header = "Details of condition, injuries and illnesses",
                        Body = new StringBuilder()
                            .Append("<p>Please add the conditions, injuries and illnesses related to the claim details that you have just provided.</p>")
                            .ToString(),
                        BeginLinkText = "Add a condition, injury or illness"
                    },
                    PostTaskPage = new RepeatTaskPage
                    {
                        Header = "Details of condition, injuries and illnesses",
                        RepeatLinkText = "Add another condition, injury or illness",
                        SummaryTableText = "Illness or injury"
                    },
                    TaskItems = new List<ITaskItem>
                    {
                        new TaskQuestionPage
                        {
                            Id = "claim-illness-name",
                            NextPageId = "claim-illness-medical-attention",
                            Header = "What is the condition, injury or illness you are claiming for?",
                            Questions = new List<BaseQuestion>
                            {
                                new TextInputQuestion
                                {
                                    Id = "question1",
                                    Label = "Condition, injury or illness"
                                }
                            }
                        },
                        new TaskQuestionPage
                        {
                            Id = "claim-illness-medical-attention",
                            Header = "Where did you first seek medical attention?",
                            NextPageId = "claim-illness-medical-diagnosis",
                            Questions = new List<BaseQuestion>
                            {
                                new TextInputQuestion
                                {
                                    Label = "Name (if known)",
                                    Id = "question1",
                                    Type = "Text"
                                },
                                new TextInputQuestion
                                {
                                    Label = "Address",
                                    Hint = "Building and street",
                                    Id = "question3",
                                    Type = "Text"
                                },
                                new TextInputQuestion
                                {
                                    Id = "question4",
                                    Type = "Text"
                                },
                                new TextInputQuestion
                                {
                                    Id = "question5",
                                    Hint = "Town of city",
                                    Type = "Text",
                                    Width = 12
                                },
                                new TextInputQuestion
                                {
                                    Id = "question6",
                                    Hint = "County",
                                    Type = "Text",
                                    Width = 12
                                },
                                new TextInputQuestion
                                {
                                    Id = "question7",
                                    Hint = "Postcode",
                                    Type = "Text",
                                    Width = 12
                                },
                                new TextInputQuestion
                                {
                                    Id = "question 8",
                                    Label = "Telephone number"
                                }
                            }
                        },
                        new TaskQuestionPage
                        {
                            Id = "claim-illness-medical-diagnosis",
                            NextPageId = "claim-hospital",
                            Header = "Medical diagnosis details",
                            Questions = new List<BaseQuestion>
                            {
                                new TextInputQuestion
                                {
                                    Id = "question1",
                                    Hint = "Medical diagnosis (if known)",
                                    Label = "Which specific medical diagnosis have you been given?"
                                },
                                new TextInputQuestion
                                {
                                    Label = "Which medical practitioner gave this diagnosis?",
                                    Id = "question2",
                                    Type = "Text"
                                },
                                new TextInputQuestion
                                {
                                    Label = "Address",
                                    Hint = "Building and street",
                                    Id = "question3",
                                    Type = "Text"
                                },
                                new TextInputQuestion
                                {
                                    Id = "question4",
                                    Type = "Text"
                                },
                                new TextInputQuestion
                                {
                                    Id = "question5",
                                    Hint = "Town or city",
                                    Type = "Text",
                                    Width = 12
                                },
                                new TextInputQuestion
                                {
                                    Id = "question6",
                                    Hint = "County",
                                    Type = "Text",
                                    Width = 12
                                },
                                new TextInputQuestion
                                {
                                    Id = "question7",
                                    Hint = "Postcode",
                                    Type = "Text",
                                    Width = 12
                                },
                                new TextInputQuestion
                                {
                                    Id = "question 8",
                                    Label = "Telephone number"
                                }
                            }
                        },
                        new TaskQuestionPage
                        {
                            Id = "claim-hospital",
                            Header = "Did you receive any hospital treatment for the injury, illness or condition?",
                            NextPageId = "hospital-visit-subtask",
                            Questions = new List<BaseQuestion>
                            {
                                new RadioQuestion
                                {
                                    Id = "question1",
                                    Options = new List<string>
                                    {
                                        "Yes", "No"
                                    },
                                    Validator = new RadioValidation(new RadioValidationProperties())
                                }
                            },
                            Effects = new List<Effect>
                            {
                                new PathChangeEffect(x =>
                                    x.First().Answer.Values["default"] == "Yes"
                                        ? "hospital-visit-subtask"
                                        : "claim-surgery-waiting")
                            }
                        },
                        new SubTask
                        {
                            Id = "hospital-visit-subtask",
                            NextPageId = "claim-surgery-waiting",
                            DisplayName = "Hospital visits",
                            PreTaskPage = new PreTaskPage
                            {
                                Header = "Hospital treatments",
                                Body = new StringBuilder()
                                    .Append("<p>These are the hospital treatments you have added about this injury, illness or condition so far. You can add more using the button below. Treatments in this context could be one stay in hospital (e.g. for an operation) or could be ongoing treatment over a number of hospital visits (e.g. a course of chemotherapy).</p>")
                                    .ToString(),
                                BeginLinkText = "Add a hospital treatment"
                            },
                            PostTaskPage = new RepeatTaskPage
                            {
                                Header = "Hospital treatments",
                                RepeatLinkText = "Add another hospital treatment",
                                SummaryTableText = "Hospital treatment"
                            },
                            TaskItems = new List<ITaskItem>()
                            {
                                new TaskQuestionPage
                                {
                                    Id = "claim-hospital-address",
                                    Header = "What were the details of this hospital treatment?",
                                    NextPageId = "claim-hospital-record",
                                    Questions = new List<BaseQuestion>
                                    {
                                        new TextInputQuestion
                                        {
                                            Id = "question1",
                                            Label = "Name of consultant or clinic (if known)"
                                        },
                                        new TextInputQuestion
                                        {
                                            Label = "Address",
                                            Hint = "Building and street",
                                            Id = "question3",
                                            Type = "Text"
                                        },
                                        new TextInputQuestion
                                        {
                                            Id = "question4",
                                            Type = "Text"
                                        },
                                        new TextInputQuestion
                                        {
                                            Id = "question5",
                                            Hint = "Town or city",
                                            Type = "Text",
                                            Width = 12
                                        },
                                        new TextInputQuestion
                                        {
                                            Id = "question6",
                                            Hint = "County",
                                            Type = "Text",
                                            Width = 12
                                        },
                                        new TextInputQuestion
                                        {
                                            Id = "question7",
                                            Hint = "Postcode",
                                            Type = "Text",
                                            Width = 12
                                        }
                                    }
                                },
                                new TaskQuestionPage
                                {
                                    Id = "claim-hospital-record",
                                    NextPageId = "claim-hospital-visit-dates",
                                    Header = "What is the hospital record number?",
                                    Questions = new List<BaseQuestion>
                                    {
                                        new TextInputQuestion
                                        {
                                            Id = "question1",
                                            Label = "Hospital record number"
                                        }
                                    }
                                },
                                new SubTask
                                {
                                    Id = "claim-hospital-visit-dates",
                                    NextPageId = null,
                                    DisplayName = "Hospital visit dates",
                                    PostTaskPage = new RepeatTaskPage
                                    {
                                        Header = "Hospital treatment dates",
                                        RepeatLinkText = "Add another treatment date",
                                        SummaryTableText = "Hospital visit"
                                    },
                                    PreTaskPage = new PreTaskPage
                                    {
                                        Header = "Hospital treatment dates",
                                        Body = new StringBuilder()
                                            .Append("<p>These treatment dates should refer exclusively to the date(s) on which you received the hospital treatment that you have just outlined. You will have the opportunity to add further hospital treatments on the next page.</p>")
                                            .Append("<p>Please provide the dates that you were treated at this hospital.</p>")
                                            .ToString(),
                                        BeginLinkText = "Add a treatment date"
                                    },
                                    TaskItems = new List<ITaskItem>
                                    {
                                        new TaskQuestionPage
                                        {
                                            Id =
                                                "claim-hospital-treatment-dates",
                                            NextPageId = null,
                                            Header = "What are the hospital treatment dates?",
                                            Questions = new List<BaseQuestion>
                                            {
                                                new DateInputQuestion
                                                {
                                                    Id = "question1",
                                                    Label = "Start date",
                                                    Validator = new DateInputValidation(
                                                        new DateInputValidationProperties {IsInPast = true})
                                                },
                                                new DateInputQuestion
                                                {
                                                    Id = "question2",
                                                    Label = "End date",
                                                    Validator = new DateInputValidation(
                                                        new DateInputValidationProperties())
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        new TaskQuestionPage
                        {
                            Id = "claim-surgery-waiting",
                            NextPageId = "claim-surgery-treatment-date", //or "claim-other-treatment",
                            Header = "Are you on the waiting list for surgery for this condition/illness?",
                            Questions = new List<BaseQuestion>
                            {
                                new RadioQuestion
                                {
                                    Id = "question1",
                                    Options = new List<string>
                                    {
                                        "Yes", "No"
                                    },
                                    Validator = new RadioValidation(new RadioValidationProperties())
                                }
                            },
                            Effects = new List<Effect>
                            {
                                new PathChangeEffect(x =>
                                    x.First().Answer.Values["default"] == "Yes"
                                        ? "claim-surgery-treatment-date"
                                        : "claim-other-treatment")
                            }
                        },
                        new TaskQuestionPage
                        {
                            Id = "claim-surgery-treatment-date",
                            NextPageId = "claim-surgery-address",
                            Header = "When is this surgery due to take place?",
                            Questions = new List<BaseQuestion>
                            {
                                new DateInputQuestion
                                {
                                    Id = "question1",
                                    Label = "Start date",
                                    Validator = new DateInputValidation(new DateInputValidationProperties
                                        {IsInFuture = true})
                                }
                            }
                        },
                        new TaskQuestionPage
                        {
                            Id = "claim-surgery-address",
                            NextPageId = "claim-other-treatment",
                            Header = "What is the name and address of the hospital where this is due to take place?",
                            Questions = new List<BaseQuestion>
                            {
                                new TextInputQuestion
                                {
                                    Label = "Address",
                                    Hint = "Building and street",
                                    Id = "question1",
                                    Type = "Text"
                                },
                                new TextInputQuestion
                                {
                                    Id = "question2",
                                    Type = "Text"
                                },
                                new TextInputQuestion
                                {
                                    Id = "question3",
                                    Hint = "Town or city",
                                    Type = "Text",
                                    Width = 12
                                },
                                new TextInputQuestion
                                {
                                    Id = "question4",
                                    Hint = "County",
                                    Type = "Text",
                                    Width = 12
                                },
                                new TextInputQuestion
                                {
                                    Id = "question5",
                                    Hint = "Postcode",
                                    Type = "Text",
                                    Width = 12
                                }
                            }
                        },
                        new TaskQuestionPage
                        {
                            Id = "claim-other-treatment",
                            NextPageId = "claim-treatment-type", //or end section
                            Header =
                                "Are you waiting for or have you received any other type of treatment for this claim, condition or illness?",
                            Questions = new List<BaseQuestion>
                            {
                                new RadioQuestion
                                {
                                    Id = "question1",
                                    Options = new List<string>
                                    {
                                        "Yes", "No"
                                    },
                                    Validator = new RadioValidation(new RadioValidationProperties())
                                }
                            },
                            Effects = new List<Effect>
                            {
                                new PathChangeEffect(x =>
                                    x.First().Answer.Values["default"] == "Yes" ? "claim-treatment-type" : "no-treatment-received")
                            }
                        },
                        new TaskQuestionGhost("no-treatment-received"),
                        new TaskQuestionPage
                        {
                            Id = "claim-treatment-type",
                            NextPageId = "claim-treatment-address",
                            Header = "What was/is the type of treatment?",
                            Questions = new List<BaseQuestion>
                            {
                                new TextareaQuestion
                                {
                                    Id = "question1",
                                    Rows = 5
                                }
                            }
                        },
                        new TaskQuestionPage
                        {
                            Id = "claim-treatment-address",
                            Header = "What is the full address of where you had/will have this treatment?",
                            NextPageId = null,
                            Questions = new List<BaseQuestion>
                            {
                                new TextInputQuestion
                                {
                                    Label = "Address",
                                    Hint = "Building and street",
                                    Id = "question1",
                                    Type = "Text"
                                },
                                new TextInputQuestion
                                {
                                    Id = "question2",
                                    Type = "Text"
                                },
                                new TextInputQuestion
                                {
                                    Id = "question3",
                                    Hint = "Town or city",
                                    Type = "Text",
                                    Width = 12
                                },
                                new TextInputQuestion
                                {
                                    Id = "question4",
                                    Hint = "County",
                                    Type = "Text",
                                    Width = 12
                                },
                                new TextInputQuestion
                                {
                                    Id = "question5",
                                    Hint = "Postcode",
                                    Type = "Text",
                                    Width = 12
                                }
                            }
                        }
                    }
                }
            }
        };
    }
}
