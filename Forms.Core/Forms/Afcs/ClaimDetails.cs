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
    public static class ClaimDetails
    {
        public static Task Task => new Task
        {
            Id = "claims-details-task",
            Name = "Claim and medical details",
            GroupNameIndex = 2,
            SummaryPage = new SummaryPage(),
            PostTaskPage = new RepeatTaskPage
            {
                Header = "Claim and medical details",
                RepeatLinkText = "Add another claim",
                SummaryTableText = "Claim"
            },
            PreTaskPage = new PreTaskPage
            {
                Header = "Claim and medical details",
                Body = new StringBuilder()
                    .Append("<p>This form allows you to make multiple claims based on individual injuries, illnesses or conditions that have occured at different points in time as a result of your service.</p>")
                    .Append("<p>For a specific accident or incident you can add all of the injuries and conditions sustained in a single claim.</p>")
                    .ToString(),
                BeginLinkText = "Add a claim"
            },
            TaskItems = new List<ITaskItem>
            {
                new TaskQuestionPage
                {
                    Id = "claim-illness",
                    Header = "What type of medical condition, injury or illness you are claiming for?",
                    NextPageId = "claim-accident-date", 
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Label = "Please select the option that applies to your claim:",
                            Id = "question1",
                            Options = new List<string>
                            {
                                "A condition, injury or illness that is the result of a specific accident or incident",
                                "A condition, injury or illness that started over a period of time and is not related to a specific incident or accident"
                            },
                            Validator = new RadioValidation(new RadioValidationProperties())
                        }
                    },
                    Effects = new List<Effect>
                    {
                        new PathChangeEffect(x =>
                            x.First().Answer.Values["default"] ==
                            "A condition, injury or illness that is the result of a specific accident or incident"
                                ? "claim-accident-date"
                                //: "claim-time-date")
                                : "claim-illness-condition")
                    }

                },
                //new question added as per reuqirment document
                new TaskQuestionPage
                {
                    Id = "claim-illness-condition",
                    Header = "What medical condition are you claiming for?",
                    NextPageId = "claim-accident-location",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Id = "question1",
                            //Hint = "Please provide your rank at the time",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = false,
                                MaxLength = 30
                            })
                        }
                    }
                },

                new TaskQuestionPage
                {
                    Id = "claim-accident-date",
                    Header = "What was the date of the incident/accident?",
                    NextPageId = "claim-accident-location",
                    Questions = new List<BaseQuestion>
                    {
                        new DateInputQuestion
                        {
                            Id = "question1",
                            Hint = "For example 27 3 2007",
                            Validator = new DateInputValidation(new DateInputValidationProperties {IsInPast = true})
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "claim-accident-location",
                    NextPageId = "claim-accident-result-of",
                    Header = "Where were you when you were injured?",
                    WarningText = "EPAW applies for UKSF",
                    Questions = new List<BaseQuestion>
                    {
                        new TextareaQuestion
                        {
                            Id = "question1",
                            Hint = "Please give details of the location you were in when your injury took place.",
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
                    Id = "claim-accident-result-of",
                    Header = "Was this incident or accident the result of:",
                    NextPageId = "claim-accident-journey-reason", 
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Label = "Please select the option that applies to your claim:",
                            Id = "question1",
                            Options = new List<string>
                            {
                                "A road accident",
                                "A sporting activity, adventure training or physical training",
                                "Other"
                            },
                            Validator = new RadioValidation(new RadioValidationProperties())

                        }
                    },
                    Effects = new List<Effect>
                    {
                        new PathChangeEffect(x =>
                        {
                            switch (x.First().Answer.Values["default"])
                            {
                                case "A road accident": return "claim-accident-journey-reason";
                                case "A sporting activity, adventure training or physical training":
                                    return "claim-sport-activity";
                                default: return "claim-other-doing";
                            }
                        })
                    }
                },
                new TaskQuestionPage
                {
                    Id = "claim-accident-journey-reason",
                    Header = "What was the reason for your journey?",
                    IntroText = "Please tell us why you were travelling.",
                    NextPageId = "claim-accident-journey-route",
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
                    Id = "claim-accident-journey-route",
                    Header = "What was the route you took from the start of your journey to the final destination?",
                    IntroText = "Please describe your route.",
                    NextPageId = "claim-accident-journey-authorised",
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
                    Id = "claim-accident-journey-authorised",
                    Header = "Were you on authorised leave at the time?",
                    NextPageId = "claim-accident-police-involved",
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
                    }
                },
                new TaskQuestionPage
                {
                    Id = "claim-accident-police-involved",
                    Header = "Were the police involved?",
                    NextPageId = "claim-accident-witness-involved",
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
                        },
                        new TextareaQuestion
                        {
                            Id = "question2",
                            Hint = "Please provide details about any police involvement.",
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
                    Id = "claim-accident-witness-involved",
                    Header = "Were any witnesses or passengers involved?",
                    NextPageId = "claim-accident-report-injury",
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
                        },
                        new TextareaQuestion
                        {
                            Id = "question2",
                            Hint = "Please provide details about any witnesses or passengers involved.",
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
                    Id = "claim-sport-activity",
                    NextPageId = "claim-sport-organised",
                    Header = "What was the activity?",
                    IntroText = "Please tell us what you were doing at the time when you sustained the injury/illness or condition.",
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
                    Id = "claim-sport-organised",
                    Header = "Was the activity organised by the armed forces?",
                    NextPageId = "claim-sport-representing",
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
                    }
                },
                new TaskQuestionPage
                {
                    Id = "claim-sport-representing",
                    Header = "Were you representing your unit at the time?",
                    NextPageId = "claim-sport-witnesses",
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
                    }
                },
                new TaskQuestionPage
                {
                    Id = "claim-sport-witnesses",
                    Header = "Were any witnesses involved?",
                    NextPageId = "claim-sport-treatment",
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
                        },
                        new TextareaQuestion
                        {
                            Id = "question2",
                            Hint = "Please provide details about any witnesses involved.",
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
                    Id = "claim-sport-treatment",
                    Header = "Were you given any treatment at the time of injury?",
                    NextPageId = "claim-accident-report-injury",
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
                        },
                        new TextareaQuestion
                        {
                            Id = "question2",
                            Hint = "Please provide details about the treatment you were given.",
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
                    Id = "claim-other-doing",
                    Header = "What were you doing at the time?",
                    NextPageId = "claim-accident-report-injury",
                    WarningText = "EPAW applies for UKSF",
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
                    Id = "claim-accident-report-injury",
                    Header = "Did you report the injury, illness or condition at the time?",
                    NextPageId = "incident-report",
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
                        },
                        new TextInputQuestion
                        {
                            Id = "question2",
                            Hint = "Who did you report it to?",
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
                    Id = "incident-report",
                    NextPageId = "acting-rank",
                    Header = "Was an incident report form completed?",
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
                },
                new TaskQuestionPage
                {
                    Id = "acting-rank",
                    NextPageId = "claim-downgraded",
                    Header = "Were you in an acting rank at the time?",
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
                        },
                        new TextInputQuestion
                        {
                            Id = "question2",
                            Hint = "Please provide your rank at the time",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = false,
                                MaxLength = 30
                            })
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "claim-time-date",
                    Header = "When did you start experiencing the injury, illness or condition?",
                    NextPageId = "claim-time-due-to",
                    Questions = new List<BaseQuestion>
                    {
                        new DateInputQuestion
                        {
                            Id = "question1",
                            Hint = "If unknown give approx date. For example, 27 03 2007",
                            Validator = new DateInputValidation(new DateInputValidationProperties {IsInPast = true})
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "claim-time-due-to",
                    Header = "Do you think the injury/illness/condition was due to any of the following?",
                    NextPageId = "claim-time-exposed",
                    Questions = new List<BaseQuestion>
                    {
                        new CheckboxQuestion
                        {
                            Id = "question1",
                            Options = new List<string>
                            {
                                "Trade",
                                "Duties",
                                "Training",
                                "Cold",
                                "Heat",
                                "Noise",
                                "Vibration",
                                "Chemical, biological or hazardous substances"
                            }
                        },
                        new TextareaQuestion
                        {
                            Id = "question2",
                            Rows = 4,
                            Hint = "What were the chemical, biological or hazardous substances you were exposed to?",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                MaxLength = 200
                            })
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "claim-time-exposed",
                    Header = "When were you first exposed to this?",
                    NextPageId = "claim-time-exposed-duration",
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
                    Id = "claim-time-exposed-duration",
                    Header = "How long were you exposed to this?",
                    NextPageId = "claim-downgraded",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Id = "question1",
                            Label = "Exposure time", 
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 50
                            })
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "claim-downgraded",
                    NextPageId = "claim-downgraded-dates", 
                    Header = "Were you downgraded for this condition, illness, injury?",
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
                                ? "claim-downgraded-dates"
                                : "claim-illness-subtask")
                    }
                },
                new TaskQuestionPage
                {
                    Id = "claim-downgraded-dates",
                    Header = "When were you downgraded?",
                    NextPageId = "claim-downgraded-category",
                    Questions = new List<BaseQuestion>
                    {
                        new DateInputQuestion
                        {
                            Id = "question1",
                            Label = "Date from:",
                            Hint = "For example, 27 03 2007",
                            Validator = new DateInputValidation(new DateInputValidationProperties {IsInPast = true})
                        },
                        new DateInputQuestion
                        {
                            Id = "question2",
                            Label = "Date to:",
                            Hint = "For example, 27 03 2010",
                            Validator = new DateInputValidation(new DateInputValidationProperties {IsInPast = true})
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "claim-downgraded-category",
                    NextPageId = "claim-illness-subtask",
                    Header = "What category were you downgraded to?",
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Id = "question1",
                            Options = new List<string>
                            {
                                "Category A", "Category B", "Category C", "Category D", "Category E"
                            },
                            Validator = new RadioValidation(new RadioValidationProperties())
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
