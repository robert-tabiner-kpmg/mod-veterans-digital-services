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
            Name = "Claim Details",
            GroupNameIndex = 3,
            SummaryPage = new SummaryPage(),
            PostTaskPage = new RepeatTaskPage
            {
                Header = "Claim Details",
                RepeatLinkText = "Add another claim",
                SummaryTableText = "Claim"
            },
            PreTaskPage = new PreTaskPage
            {
                Header = "Claim Details",
                Body = new StringBuilder()
                    .Append("<p>This form allows you to make multiple claims for individual injuries, illnesses or medical conditions that have occurred at different points in time as a result of your service.</p>")
                    .Append("<p>For a specific accident or incident, you can include all of the injuries and conditions sustained within a single claim.</p>")
                    .ToString(),
                BeginLinkText = "Add a claim"
            },
            TaskItems = new List<ITaskItem>
            {
                new TaskQuestionPage
                {
                    Id = "claim-illness",
                    Header = "What type of medical condition, injury or illness you are claiming for?",
                    NextPageId = "claim-illness-condition", 
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Hint = "Please select the option that applies to your claim:",
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
                                ? "claim-accident-condition"
                                //: "claim-time-date")
                                : "claim-illness-condition")
                    }

                },
                //new question added as per reuqirment document version 0.2

                new TaskQuestionPage
                {
                    Id = "claim-illness-condition",
                    Header = "What medical condition are you claiming for?",
                    IntroText = "Where you have a specific medical diagnosis, please include this here",
                    //NextPageId = "claim-accident-journey-reason",//"claim-accident-location",
                   NextPageId = "claim-illness-surgery-address",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Id = "question1",
                            //Hint = "Please provide your rank at the time",
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
                            Id = "claim-illness-surgery-address",
                            NextPageId = "claim-illness-date",//"claim-other-treatment",
                            Header = "Which Medical Practioner gave you the diagnosis (if known)?",
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
                    Id = "claim-illness-date",
                    Header = "What was the date your condition started?",
                    NextPageId = "claim-illness-condition-related",
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
                                "Tick if this date is approximate"
                            }
                        }
                    }
                },

                  new TaskQuestionPage
                {
                    Id = "claim-illness-condition-related",
                    Header = "Is your Illness/Condition related to:",
                    NextPageId = "claim-illness-condition-dueto",
                    Questions = new List<BaseQuestion>
                    {
                        new CheckboxQuestion
                        {
                            Id = "question1",
                            Hint = "Select all that apply",
                            Options = new List<string>
                            {
                                "Duties - Operations overseas",
                                "Duties - Operations UK",
                                "Trade",
                                "Training",
                                "Misconduct by others",
                                "Consequential to another medical condition"
                            }
                        }
                    }
                },

                   new TaskQuestionPage
                {
                    Id = "claim-illness-condition-dueto",
                    Header = "Is your condition due to exposure to?",
                    NextPageId ="claim-illness-first-medical-attention-date",
                    Questions = new List<BaseQuestion>
                    {
                        new CheckboxQuestion
                        {
                            Id = "question1",
                            Hint = "Select all that apply",
                            Options = new List<string>
                            {
                                "Cold",
                                "Heat",
                                "Noise",
                                "Vibration",
                                "Chemical exposure"
                            }, 
                        }
                    },
                    Effects = new List<Effect>
                    {

                        new PathChangeEffect(x =>
                            x.First().Answer.Values.ContainsValue("Chemical exposure")
                                ? "claim-illness-condition-chemical-exposure"
                                : "claim-illness-first-medical-attention-date")
                    }
                },


                    new TaskQuestionPage
                {
                    Id = "claim-illness-condition-chemical-exposure",
                    Header = "Please tell us about the chemical exposure?",
                    NextPageId = "claim-illness-first-medical-attention-date",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Label = "What substances?",
                            Id = "question1",
                            Type = "Text",
                            Width=20,
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 50
                            })
                        },
                        new DateInputQuestion
                        {
                            Id = "question2",
                            Label="Date you were first exposed to these?",
                            Hint = "If unknown give approx date. For example, 27 03 2007",
                            Validator = new DateInputValidation(new DateInputValidationProperties {IsInPast = true})
                        },
                        new TextInputQuestion
                        {
                            Label = "Length of exposure",
                            Id = "question3",
                            Type = "Text",
                            Width=20,
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
                    Id = "claim-illness-first-medical-attention-date",
                    Header = "When did you first seek medical attention for the condition(s)?",
                    NextPageId = "claim-illness-downgraded",
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
                    Id = "claim-illness-downgraded",
                    NextPageId = "claim-illness-downgraded-dates",
                    Header = "Were you downgraded for any of the conditions on this claim?",
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
                                ? "claim-illness-downgraded-dates"
                                : "claim-illness-note")
                    }
                },

                    new TaskQuestionPage
                {
                    Id = "claim-illness-downgraded-dates",
                    Header = "When were you downgraded?",
                    NextPageId = "claim-illness-note",
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
                        },
                        new TextInputQuestion
                        {
                            Label = "From Medical Category",
                            Id = "question3",
                            Type = "Text",
                            Width=20,
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 20
                            })
                        },

                        new TextInputQuestion
                        {
                            Label = "To Medical Category",
                            Id = "question4",
                            Type = "Text",
                            Width=20,
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
                    Id = "claim-illness-note",
                    Header = "Why is your condition related to your armed forces service?",
                    IntroText="Tell us in your own words why you feel your claimed medical condition or injury is caused or  made worse by your service in the Armed Forces. " +
                        "Include information you think is relevant but do not include details of operations.  " +
                        "If you are claiming for a Road Traffic Accident and you were not on a direct route between your starting point and destination, " +
                        "please tell us why here.<br><br>" +

                        "Note: You MUST NOT include information classified as Secret or above.  " +
                        "If you need to tell us information classified as Secret or above, please write &#8220;Classified  Information&#8220; here and " +
                        "we will contact you after we receive your claim.<br><br>"+

                        "If you have served or are serving (whether directly or in a support role) with the United Kingdom Special Forces (UKSF),"+
                        "you must seek advice from the MOD A Block Disclosure Cell BEFORE completing this section. " +
                        "The Disclosure Cell can be contacted by emailing MAB-J1-Disclosures-ISA-Mailbox@mod.gov.uk .",

                    //NextPageId = "claim-accident-journey-reason",//"claim-accident-location",
                   //NextPageId = "claim-accident-location",
                    Questions = new List<BaseQuestion>
                    {
                        new TextareaQuestion
                        {
                            Id = "question1",
                            Rows = 8,
                            //Hint = "What were the chemical, biological or hazardous substances you were exposed to?",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 1500
                            })
                        }

                        //new TextInputQuestion
                        //{
                        //    //Label = "From Medical Category",
                        //    Id = "question1",
                        //    Type = "Text",
                        //    Width=20,
                            
                        //    Validator = new TextInputValidation(new TextInputValidationProperties
                        //    {
                        //        IsRequired = true,
                        //        MaxLength = 20
                        //    })
                        //},
                    }
                },

                    new TaskQuestionPage
                {
                    Id = "claim-accident-condition",
                    Header = "Was the Incident/Accident related to Sporting/Adventure Training/Physical Training?",
                    NextPageId = "claim-accident-non-sporting-medical-condition",
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Hint = "Please select the option:",
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
                                ? "claim-accident-sporting-medical-condition"
                                //: "claim-time-date")
                                : "claim-accident-non-sporting-medical-condition")
                    }

                },

                    new TaskQuestionPage
                {
                    Id = "claim-accident-non-sporting-medical-condition",
                    NextPageId = "claim-accident-non-sporting-surgery-address",
                    Header = "What medical condition(s) are you claiming for?  " , 
                    IntroText =    "Where you have any specific medical diagnosis, please include them here",
                    Questions = new List<BaseQuestion>
                    {
                        new TextareaQuestion
                        {
                            Id = "question1",
                            Hint = "Please include all claimed medical conditions you think are linked to the incident, even if they developed afterwards. " +
                            "Tell us which side of the body is affected where needed (e.g. left arm)",
                            Rows = 5,
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 250
                            })
                        }
                    }
                },

                     new TaskQuestionPage
                        {
                            Id = "claim-accident-non-sporting-surgery-address",
                            NextPageId = "claim-accident-non-sporting-date",//"claim-other-treatment",
                            Header = "Which Medical Practioner gave you the diagnosis (if known)?",
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
                                    Id = "question3",
                                    Label="",
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
                    Id = "claim-accident-non-sporting-date",
                    Header = "What was the date of injury/ incident?",
                    NextPageId = "claim-accident-non-sporting-duty",
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
                    Id = "claim-accident-non-sporting-duty",
                    Header = "Were you on duty at the time of incident?",
                    NextPageId = "claim-accident-non-sporting-report-to",
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
                            x.First().Answer.Values["default"] ==
                                "Yes"
                                ? "claim-accident-non-sporting-report-to"
                                //: "claim-time-date")
                                : "claim-accident-non-sporting-form")
                    }
                },

                 new TaskQuestionPage
                {
                    Id = "claim-accident-non-sporting-report-to",
                    Header = "Who did you report the incident to?",
                    NextPageId = "claim-accident-non-sporting-form",
                    Questions = new List<BaseQuestion>
                    {
                        new CheckboxQuestion
                        {
                            Id = "question1",
                            Hint = "Select all that apply",
                            Options = new List<string>
                            {
                                "Unit medic",
                                "Hospital",
                                "Chain of command",
                                "Colleague",
                                "Other person",
                                "I didn't report the incident"
                            }
                        }
                    }
                },

                  new TaskQuestionPage
                {
                    Id = "claim-accident-non-sporting-form",
                    Header = "Was an accident form completed?",
                    NextPageId = "claim-accident-non-sporting-location",
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Id = "question1",
                            Options = new List<string>
                            {
                                "Yes [Please send us a copy if you have one or you can upload a copy later in this application]", "No"
                            },
                            Validator = new RadioValidation(new RadioValidationProperties())
                        }
                    },
                    Effects = new List<Effect>
                    {
                        new PathChangeEffect(x =>
                            x.First().Answer.Values["default"] ==
                                "Yes"
                                ? "claim-accident-non-sporting-location"
                                //: "claim-time-date")
                                : "claim-accident-non-sporting-location")
                    }
                },

                  new TaskQuestionPage
                {
                    Id = "claim-accident-non-sporting-location",
                    Header = "Where were you when the incident happened?",
                    NextPageId = "claim-accident-non-sporting-activity",
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Id = "question1",
                            Options = new List<string>
                            {
                                "Operations location overseas",
                                "Operations location UK",
                                "Home base",
                                "Accomodation whilst on Operations",
                                "Accomodation on home base",
                                "An off-duty location"
                            },
                            Validator = new RadioValidation(new RadioValidationProperties())
                        }
                    }

                },

                   new TaskQuestionPage
                {
                    Id = "claim-accident-non-sporting-activity",
                    Header = "What were you doing at the time the incident occured?",
                    NextPageId = "claim-accident-non-sporting-road-traffic",
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Id = "question1",
                            Options = new List<string>
                            {
                                "Operations Duties overseas",
                                "Operations Duties UK",
                                "Home base duties",
                                "Training Excercise",
                                "Travelling"
                            },
                            Validator = new RadioValidation(new RadioValidationProperties())
                        }
                    }
                   },

                   new TaskQuestionPage
                {
                    Id = "claim-accident-non-sporting-road-traffic",
                    Header = "Was the incident a Road Traffic Accident?",
                    NextPageId = "claim-accident-non-sporting-journey-reason",
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
                            x.First().Answer.Values["default"] ==
                                "Yes"
                                ? "claim-accident-non-sporting-journey-reason"
                                //: "claim-time-date")
                                : "claim-accident-non-sporting-reported-to")
                    }
                },

                   new TaskQuestionPage
                {
                    Id = "claim-accident-non-sporting-journey-reason",
                    Header = "What was the reason for your journey?",
                    NextPageId = "claim-accident-non-sporting-journey-from",
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Id = "question1",
                            Options = new List<string>
                            {
                                "Duties - Operations",
                                "Duties - Trade",
                                "Duties - Training",
                                "Personal (non-duty/off-duty)"
                            },
                            Validator = new RadioValidation(new RadioValidationProperties())
                        }
                    }
                   },

                    new TaskQuestionPage
                {
                    Id = "claim-accident-non-sporting-journey-from",
                    Header = "Where did your journey start?",
                    NextPageId = "claim-accident-non-sporting-journey-to",
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Id = "question1",
                            Options = new List<string>
                            {
                                "Operations location overseas",
                                "Operations location - UK",
                                "Accomodation - field",
                                "Accomodation - base",
                                "Home base",
                                "Your home",
                                "An off-duty location"

                            },
                            Validator = new RadioValidation(new RadioValidationProperties())
                        }
                    }
                   },

                     new TaskQuestionPage
                {
                    Id = "claim-accident-non-sporting-journey-to",
                    Header = "Where were you travelling to?",
                    NextPageId = "claim-accident-non-sporting-direct-route",
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Id = "question1",
                            Options = new List<string>
                            {
                                "Operations location overseas",
                                "Operations location - UK",
                                "Accomodation - Operations",
                                "Accomodation - base",
                                "Home base",
                                "Your home",
                                "An off-duty location"
                            },
                            Validator = new RadioValidation(new RadioValidationProperties())
                        }
                    }
                   },

                     new TaskQuestionPage
                {
                    Id = "claim-accident-non-sporting-direct-route",
                    Header = "Were you on a direct route?",
                    IntroText = "A direct route means you took a reasonable route from start to end and did not divert for personal reasons, e.g. to visit a friend.",
                    NextPageId = "claim-accident-non-sporting-reported-to",
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
                            x.First().Answer.Values["default"] ==
                                "Yes"
                                ? "claim-accident-non-sporting-reported-to"
                                //: "claim-time-date")
                                : "claim-accident-non-sporting-reported-to")
                    }
                },

                      new TaskQuestionPage
                {
                    Id = "claim-accident-non-sporting-reported-to",
                    Header = "Was the incident reported to the civilian or military police?",
                    NextPageId = "claim-accident-police-ref",
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
                            x.First().Answer.Values["default"] ==
                                "Yes"
                                ? "claim-accident-police-ref"
                                //: "claim-time-date")
                                : "claim-accident-non-sporting-leave")
                    }
                },

                      new TaskQuestionPage
                {
                    Id = "claim-accident-police-ref",
                    NextPageId = "claim-accident-non-sporting-leave",
                    Header = "Please tell us the Police reference number (if known)",
                    //WarningText = "EPAW applies for UKSF",
                    Questions = new List<BaseQuestion>
                    {
                       

                            new TextInputQuestion
                                {
                                    Label = "Civilian - case ref:",
                                    Id = "question1",
                                    Type = "Text"
                                },
                                new TextInputQuestion
                                {
                                    Label = "Military - case ref:",
                                    Id = "question2",
                                    Type = "Text"
                                },

                                new CheckboxQuestion
                                {
                                    Id = "question3",
                                    //Label = "Select all that apply",
                                    Options = new List<string>
                                    {
                                        "I don't know"
                                    }
                                }
                    }

                },

                 new TaskQuestionPage
                {
                    Id = "claim-accident-non-sporting-leave",
                    Header = "Were you on authorised leave at the time of the accident?",
                    NextPageId = "claim-accident-witness",
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
                    Id = "claim-accident-witness",
                    Header = "Were there any witness?",
                    NextPageId = "claim-accident-first-aid",
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
                    Id = "claim-accident-first-aid",
                    Header = "Did you receive first aid treatment at the time?",
                    NextPageId = "claim-accident-hospital-facility",
                    IntroText="Please only tell us about treatment you received for the injury/condition that you are claiming for",
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
                    Id = "claim-accident-hospital-facility",
                    Header = "Did you go to, or were you taken to, a hospital or medical facility?",
                    NextPageId = "claim-accident-hospital-address",
                    IntroText="Please only tell us about treatment you received for the injury/condition that you are claiming for",
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
                            x.First().Answer.Values["default"] ==
                                "Yes"
                                ? "claim-accident-hospital-address"
                                //: "claim-time-date")
                                : "claim-accident-downgraded")
                    }
                },
                 new TaskQuestionPage
                        {
                            Id = "claim-accident-hospital-address",
                            NextPageId = "claim-accident-downgraded",//"claim-other-treatment",
                            Header = "Which hospital or medical facility were you taken to?",
                            IntroText="Please only tell us about treatment you received for the injury/condition that you are claiming for",
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
                                    Id = "question3",
                                    Label = "",
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
                    Id = "claim-accident-downgraded",
                    NextPageId = "claim-accident-downgraded-dates",
                    Header = "Were you downgraded for any of the conditions on this claim?",
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
                                ? "claim-accident-downgraded-dates"
                                : "claim-accident-note")
                    }
                },

                    new TaskQuestionPage
                {
                    Id = "claim-accident-downgraded-dates",
                    Header = "When were you downgraded?",
                    NextPageId = "claim-accident-note",//"claim-accident-location",//
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
                        },
                        new TextInputQuestion
                        {
                            Label = "From Medical Category",
                            Id = "question3",
                            Type = "Text",
                            Width=20,
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 20
                            })
                        },

                        new TextInputQuestion
                        {
                            Label = "To Medical Category",
                            Id = "question4",
                            Type = "Text",
                            Width=20,
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
                    Id = "claim-accident-note",
                    Header = "Why is your condition related to your armed forces service?",
                    IntroText="Tell us in your own words why you feel your claimed medical condition or injury is caused or  made worse by your service in the Armed Forces. " +
                        "Include information you think is relevant but do not include details of operations.  " +
                        "If you are claiming for a Road Traffic Accident and you were not on a direct route between your starting point and destination, " +
                        "please tell us why here.<br><br>" +

                        "Note: You MUST NOT include information classified as Secret or above.  " +
                        "If you need to tell us information classified as Secret or above, please write &#8220;Classified  Information&#8220; here and " +
                        "we will contact you after we receive your claim.<br><br>"+

                        "If you have served or are serving (whether directly or in a support role) with the United Kingdom Special Forces (UKSF),"+
                        "you must seek advice from the MOD A Block Disclosure Cell BEFORE completing this section. " +
                        "The Disclosure Cell can be contacted by emailing MAB-J1-Disclosures-ISA-Mailbox@mod.gov.uk .",

                    //NextPageId = "claim-accident-journey-reason",//"claim-accident-location",
                   //NextPageId = "claim-accident-location",
                    Questions = new List<BaseQuestion>
                    {
                        new TextareaQuestion
                        {
                            Id = "question1",
                            Rows = 8,
                            //Hint = "What were the chemical, biological or hazardous substances you were exposed to?",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 1500
                            })
                        }
                    }
                },

                new TaskQuestionPage
                {
                    Id = "claim-accident-sporting-medical-condition",
                    NextPageId = "claim-accident-sporting-surgery-address",
                    Header = "What medical condition(s) are you claiming for?  " ,
                     IntroText =   "Where you have any specific medical diagnosis, please include them here",
                    Questions = new List<BaseQuestion>
                    {
                        new TextareaQuestion
                        {
                            Id = "question1",
                            Hint = "Please include all claimed medical conditions you think are linked to the incident, even if they developed afterwards. " +
                            "Tell us which side of the body is affected where needed (e.g. left arm)",
                            Rows = 5,
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 250
                            })
                        }
                    }
                },

                     new TaskQuestionPage
                        {
                            Id = "claim-accident-sporting-surgery-address",
                            NextPageId = "claim-accident-sporting-date",
                            Header = "Which Medical Practioner gave you the diagnosis (if known)?",
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
                                    Id = "question3",
                                    Type = "Text",
                                    Label = ""
                                },
                                new TextInputQuestion
                                {
                                    Id = "question4",
                                    //Hint = "Town or city",
                                    Type = "Text",
                                    Width = 12,
                                    Label = "Town or city"
                                },
                                new TextInputQuestion
                                {
                                    Id = "question5",
                                    //Hint = "County",
                                    Type = "Text",
                                    Width = 12,
                                    Label = "County"
                                },
                                new TextInputQuestion
                                {
                                    Id = "question6",
                                   // Hint = "Postcode",
                                    Type = "Text",
                                    Width = 12,
                                    Label = "Postcode"
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
                    Id = "claim-accident-sporting-date",
                    Header = "What was the date of injury/ incident?",
                    NextPageId = "claim-accident-sporting-activity",
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
                    Id = "claim-accident-sporting-activity",
                    NextPageId = "claim-accident-sporting-authorize",
                    Header = "What was the activity?",
                    IntroText = "(E.G. skiing/football/diving)",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Id = "question1",
                            //Rows = 3,
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
                    Id = "claim-accident-sporting-authorize",
                    NextPageId = "claim-accident-sporting-related",
                    Header = "Were you representing your Unit?",
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Id = "question1",
                            Options = new List<string>
                            {
                                "Yes [Please send us copies of part 1 orders/admin istructions/authorisation. You can " +
                                "upload a copy later in the application]",
                                "No"
                            },
                            Validator = new RadioValidation(new RadioValidationProperties())
                        }
                    }
                },

                     new TaskQuestionPage
                {
                    Id = "claim-accident-sporting-related",
                    NextPageId = "claim-accident-sporting-witness",
                    Header = "Is your illness/condition related to",
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Id = "question1",
                            Options = new List<string>
                            {
                                "Duties - Operations overseas",
                                "Duties - Operations UK",
                                "Trade",
                                "Misconduct by others",
                                "Consequential to another medical condition"
                            },
                            Validator = new RadioValidation(new RadioValidationProperties())
                        }
                    }
                },

                      new TaskQuestionPage
                {
                    Id = "claim-accident-sporting-witness",
                    Header = "Were there any witness?",
                    NextPageId = "claim-accident-sporting-first-aid",
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
                    Id = "claim-accident-sporting-first-aid",
                    Header = "Did you receive first aid treatment at the time?",
                    NextPageId = "claim-accident-sporting-hospital-facility",
                    IntroText="Please only tell us about treatment you received for the injury/condition that you are claiming for",
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
                    Id = "claim-accident-sporting-hospital-facility",
                    Header = "Did you go to, or were you taken to, a hospital or medical facility?",
                    NextPageId = "claim-accident-sporting-hospital-address",
                    IntroText="Please only tell us about treatment you received for the injury/condition that you are claiming for",
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
                            x.First().Answer.Values["default"] ==
                                "Yes"
                                ? "claim-accident-sporting-hospital-address"
                                //: "claim-time-date")
                                : "claim-accident-sporting-downgraded")
                    }
                },
                 new TaskQuestionPage
                        {
                            Id = "claim-accident-sporting-hospital-address",
                            NextPageId = "claim-accident-sporting-downgraded",//"claim-other-treatment",
                            Header = "Which hospital or medical facility were you taken to?",
                            IntroText="Please only tell us about treatment you received for the injury/condition that you are claiming for",
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
                                    Id = "question3",
                                    Type = "Text",
                                    Label = ""
                                },
                                new TextInputQuestion
                                {
                                    Id = "question4",                                   
                                    Type = "Text",
                                    Width = 12,
                                    Label = "Town or city"
                                },
                                new TextInputQuestion
                                {
                                    Id = "question5",                                   
                                    Type = "Text",
                                    Width = 12,
                                    Label = "County"
                                },
                                new TextInputQuestion
                                {
                                    Id = "question6",
                                    Type = "Text",
                                    Width = 12,
                                    Label = "Postcode"
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
                    Id = "claim-accident-sporting-downgraded",
                    NextPageId = "claim-accident-sporting-downgraded-dates",
                    Header = "Were you downgraded for any of the conditions on this claim?",
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
                                ? "claim-accident-sporting-downgraded-dates"
                                : "claim-accident-sporting-note")
                    }
                },

                    new TaskQuestionPage
                {
                    Id = "claim-accident-sporting-downgraded-dates",
                    Header = "When were you downgraded?",
                    NextPageId = "claim-accident-sporting-note",
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
                        },
                        new TextInputQuestion
                        {
                            Label = "From Medical Category",
                            Id = "question3",
                            Type = "Text",
                            Width=20,
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 20
                            })
                        },

                        new TextInputQuestion
                        {
                            Label = "To Medical Category",
                            Id = "question4",
                            Type = "Text",
                            Width=20,
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
                    Id = "claim-accident-sporting-note",
                    Header = "Why is your condition related to your armed forces service?",
                    IntroText="Tell us in your own words why you feel your claimed medical condition or injury is caused or  made worse by your service in the Armed Forces. " +
                        "Include information you think is relevant but do not include details of operations.  " +
                        "If you are claiming for a Road Traffic Accident and you were not on a direct route between your starting point and destination, " +
                        "please tell us why here.<br><br>" +

                        "Note: You MUST NOT include information classified as Secret or above.  " +
                        "If you need to tell us information classified as Secret or above, please write &#8220;Classified  Information&#8220; here and " +
                        "we will contact you after we receive your claim.<br><br>"+

                        "If you have served or are serving (whether directly or in a support role) with the United Kingdom Special Forces (UKSF),"+
                        "you must seek advice from the MOD A Block Disclosure Cell BEFORE completing this section. " +
                        "The Disclosure Cell can be contacted by emailing MAB-J1-Disclosures-ISA-Mailbox@mod.gov.uk .",
                    Questions = new List<BaseQuestion>
                    {
                        new TextareaQuestion
                        {
                            Id = "question1",
                            Rows = 8,
                            //Hint = "What were the chemical, biological or hazardous substances you were exposed to?",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 1500
                            })
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
