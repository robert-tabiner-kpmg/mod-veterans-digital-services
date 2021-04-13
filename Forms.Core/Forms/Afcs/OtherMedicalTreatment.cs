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
            GroupNameIndex = 4,
            SummaryPage = new SummaryPage(),
            PostTaskPage = new RepeatTaskPage
            {
                Header = "Other Medical Treatment",
                RepeatLinkText = "Add another Medical Treatment",
                SummaryTableText = "Hospital"
            },
            //PreTaskPage = new PreTaskPage
            //{
            //    Header = "Other Medical Treatment",
            //    Body = new StringBuilder()
            //        .Append("<p>We need to know about any hospitals or medical facilities that have provided you with further treatment for the conditions you are claiming for, or if you are on a waiting list for treatment.</p>")
            //        .Append("<p>If you have visited the same hospital or facility several times, you only need to provide the details in this section once.</p>")
            //        .ToString(),
            //    BeginLinkText = "Add a Medical Treatment",



            //    //new TaskQuestionPage
            //    //{
            //    //    Id = "other-medical-treatment-status",
            //    //    Header = "Have you had any further hospital or medical treatment?",
            //    //    //NextPageId = "other-medical-treatment-address",
            //    //    Questions = new List<BaseQuestion>
            //    //    {
            //    //        new RadioQuestion
            //    //        {
            //    //           // Hint = "We need to know:",
            //    //            Id = "question1",
            //    //            Options = new List<string>
            //    //            {
            //    //                "Yes",
            //    //                "No - I have not received any further medical treatment."
            //    //            },
            //    //            Validator = new RadioValidation(new RadioValidationProperties())
            //    //        }
            //    //    },
            //    //    Effects = new List<Effect>
            //    //    {
            //    //        new PathChangeEffect(x =>
            //    //            x.First().Answer.Values["default"] ==
            //    //            "Yes"
            //    //                ? "other-medical-treatment-address"
            //    //                //: "claim-time-date")
            //    //                : "")
            //    //    }

            //    //}
            //},
            TaskItems = new List<ITaskItem>
            {
                new TaskQuestionPage
                {
                    Id = "other-medical-treatment-status",
                    Header = "Have you had any further hospital or medical treatment?",
                    //NextPageId = "other-medical-treatment-address",
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                           // Hint = "We need to know:",
                            Id = "question1",
                            Options = new List<string>
                            {
                                "Yes",
                                "No - I have not received any further medical treatment."
                            },
                            Validator = new RadioValidation(new RadioValidationProperties())
                        }
                    },
                    Effects = new List<Effect>
                    {
                        new PathChangeEffect(x =>
                            x.First().Answer.Values["default"] ==
                            "Yes"
                                ? "medical-treatment-subtask"
                                : "no-medical-treatment")
                    }

                },
                new TaskQuestionGhost("no-medical-treatment"),
                // new TaskQuestionPage
                //        {
                //            Id = "other-medical-treatment-address",
                //            NextPageId = "other-medical-treatment-start-date",
                //            Header = "Please tell us the name and address of the hospital or facility providing the treatment?",
                //            Questions = new List<BaseQuestion>
                //            {
                //                new TextInputQuestion
                //                {
                //                    Label = "Name",
                //                    Id = "question1",
                //                    Type = "Text"
                //                },

                //                new TextInputQuestion
                //                {
                //                    Label = "Address",
                //                    Hint = "Building and street",
                //                    Id = "question2",
                //                    Type = "Text"
                //                },
                //                new TextInputQuestion
                //                {
                //                    Label = "",
                //                    Id = "question3",
                //                    Type = "Text"
                //                },
                //                new TextInputQuestion
                //                {
                //                    Id = "question4",
                //                    Label = "Town or city",
                //                    Type = "Text",
                //                    Width = 12
                //                },
                //                new TextInputQuestion
                //                {
                //                    Id = "question5",
                //                    Label = "County",
                //                    Type = "Text",
                //                    Width = 12
                //                },
                //                new TextInputQuestion
                //                {
                //                    Id = "question6",
                //                    Label = "Postcode",
                //                    Type = "Text",
                //                    Width = 12
                //                },

                //                new TextInputQuestion
                //                {
                //                    Id = "question7",
                //                    Label = "Telephone number"
                //                },

                //                new TextInputQuestion()
                //                {
                //                    Id = "question8",
                //                    Label = "Email",
                //                    Type = "email",
                //                    Autocomplete = "email",
                //                    Width = 50,
                //                    Validator = new EmailValidation(new EmailValidationProperties()
                //                        {
                //                            IsRequired = false
                //                        })
                //                }

                //            }
                //        },

                //           new TaskQuestionPage
                //        {
                //            Id = "other-medical-treatment-start-date",
                //            Header = "When did this treatment start?",
                //            IntroText="If you have received treatment at this hospistal medical facility on more than one occasion, please tell us the first time you visited",
                //            NextPageId = "other-medical-treatment-end-date",
                //            Questions = new List<BaseQuestion>
                //            {
                //                new DateInputQuestion
                //                {
                //                    Id = "question1",
                //                    Hint = "For example 27 3 2007",
                //                    Validator = new DateInputValidation(new DateInputValidationProperties {IsInPast = true})
                //                },

                //                new CheckboxQuestion
                //                {
                //                    Id = "question2",
                //                    Options = new List<string>
                //                    {
                //                        "This date is approximate"
                //                    }
                //                },
                //                 new CheckboxQuestion
                //                {
                //                    Id = "question2",
                //                    Options = new List<string>
                //                    {
                //                        "I am still on a waiting list to attend"
                //                    }
                //                }
                //            }
                //        },

                //           new TaskQuestionPage
                //        {
                //            Id = "other-medical-treatment-end-date",
                //            Header = "When did this treatment end?",
                //            NextPageId = "other-medical-treatment-type",
                //            Questions = new List<BaseQuestion>
                //            {
                //                new DateInputQuestion
                //                {
                //                    Id = "question1",
                //                    Hint = "For example 27 3 2007",
                //                    Validator = new DateInputValidation(new DateInputValidationProperties {IsInPast = true})
                //                },

                //                new CheckboxQuestion
                //                {
                //                    Id = "question2",
                //                    Options = new List<string>
                //                    {
                //                        "This date is approximate"
                //                    }
                //                },
                //                 new CheckboxQuestion
                //                {
                //                    Id = "question2",
                //                    Options = new List<string>
                //                    {
                //                        "This treatment has not yet ended"
                //                    }
                //                }
                //            }
                //        },

                //new TaskQuestionPage
                //{
                //    Id = "other-medical-treatment-type",
                //    Header = "What type of medical treatment did you receive?",
                //    IntroText = "E.g. Surgery, specialist consultation,tests, physiotherapy",
                //   NextPageId = "other-medical-treatment-condition",
                //    Questions = new List<BaseQuestion>
                //    {
                //        new TextInputQuestion
                //        {
                //            Id = "question1",
                //            //Hint = "Please provide your rank at the time",
                //            Validator = new TextInputValidation(new TextInputValidationProperties
                //            {
                //                IsRequired = true,
                //                MaxLength = 100
                //            })
                //        }
                //    }
                //},

                // new TaskQuestionPage
                //{
                //    Id = "other-medical-treatment-condition",
                //    Header = "What conditions(s) was this treatment for?",
                   
                //   //NextPageId = "claim-illness-surgery-address",
                //    Questions = new List<BaseQuestion>
                //    {
                //        new TextInputQuestion
                //        {
                //            Id = "question1",
                //            //Hint = "Please provide your rank at the time",
                //            Validator = new TextInputValidation(new TextInputValidationProperties
                //            {
                //                IsRequired = true,
                //                MaxLength = 100
                //            })
                //        }
                //    }
                //},

 





 

   

   

      


                new SubTask
                {
                    Id = "medical-treatment-subtask",
                    NextPageId = null,
                    DisplayName = "Other Medical Treatment",
                    PreTaskPage = new PreTaskPage
                    {
                        Header = "Other Medical Treatment",
                        Body = new StringBuilder()
                            .Append("<p>We need to know about any hospitals or medical facilities that have provided you with further treatment for the conditions you are claiming for, or if you are on a waiting list for treatment.</p>")
                            .Append("<p>If you have visited the same hospital or facility several times, you only need to provide the details in this section once.</p>")
                            .ToString(),
                        BeginLinkText = "Add a Medical Treatment",

                    },
                    PostTaskPage = new RepeatTaskPage
                    {
                       Header = "Other Medical Treatment",
                        RepeatLinkText = "Add another Medical Treatment",
                        SummaryTableText = "Hospital"

                    },
                    TaskItems = new List<ITaskItem>
                    {
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
                }
                    }
                }
            }
        };
    }
}
