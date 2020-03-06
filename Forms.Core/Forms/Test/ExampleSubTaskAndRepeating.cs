using System.Collections.Generic;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Questions;
using Forms.Core.Models.Static;
using Forms.Core.Models.Validation;

namespace Forms.Core.Forms.Test
{
    public static class ExampleSubTaskAndRepeating
    {
        public static Task Task => new Task
        {
            Id = "experience-task",
            Name = "Previous work experience",
            GroupNameIndex = 0,
            PostTaskPage = new RepeatTaskPage
            {
                Header = "Companies",
                Body = "You have worked at the following companies.",
                RepeatLinkText = "Add another company"
            },
            PreTaskPage = new PreTaskPage
            {
                Header = "Companies",
                Body = "Here you can add companies that have held a position with in the past.",
                BeginLinkText = "Add a company"
            },
            SummaryPage = new SummaryPage
            {
                Header = "Check your answers for this section",
            },
            TaskItems = new List<ITaskItem>
            {
                new TaskQuestionPage
                {
                    Id = "company-name",
                    NextPageId = "company-dates",
                    Header = "What is the name of the company?",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Id = "question1",
                            Hint = "The name of the company",
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
                    Id = "company-dates",
                    NextPageId = "role-subtask",
                    Header = "When did you work there?",
                    Questions = new List<BaseQuestion>
                    {
                        new DateInputQuestion
                        {
                            Id = "question1",
                            Hint = "The date you started",
                            Validator = new DateInputValidation(new DateInputValidationProperties
                            {
                                IsRequired = true,
                            })
                        },
                        new DateInputQuestion
                        {
                            Id = "question2",
                            Hint = "The date you finished (leave empty if you are still working there)",
                            Validator = new DateInputValidation(new DateInputValidationProperties
                            {
                                IsRequired = false,
                            })
                        }
                    }
                },
                new SubTask
                {
                    Id = "role-subtask",
                    DisplayName = "Role",
                    PostTaskPage = new RepeatTaskPage
                    {
                        Header = "Roles",
                        Body = "You have held the following roles.",
                        RepeatLinkText = "Add another role"
                    },
                    PreTaskPage = new PreTaskPage
                    {
                        Header = "Roles",
                        Body = "Here you can add roles that have held at this company.",
                        BeginLinkText = "Add a role"
                    },
                    TaskItems = new List<ITaskItem>
                    {
                        new TaskQuestionPage
                        {
                            Id = "role-title",
                            NextPageId = "project-subtask",
                            Header = "What is the title of the role?",
                            Questions = new List<BaseQuestion>
                            {
                                new TextInputQuestion
                                {
                                    Id = "question1",
                                    Validator = new TextInputValidation(new TextInputValidationProperties
                                    {
                                        IsRequired = true,
                                        MaxLength = 50
                                    })
                                }
                            }
                        },
                        new SubTask
                        {
                            Id = "project-subtask",
                            DisplayName = "Project",
                            PostTaskPage = new RepeatTaskPage
                            {
                                Header = "Projects",
                                Body = "You have been a involved in the following projects.",
                                RepeatLinkText = "Add another project"
                            },
                            PreTaskPage = new PreTaskPage
                            {
                                Header = "Projects",
                                Body = "Here you can add projects that have worked on.",
                                BeginLinkText = "Add a project"
                            },
                            TaskItems = new List<ITaskItem>
                            {
                                new TaskQuestionPage
                                {
                                    Id = "project-title",
                                    NextPageId = "technologies-subtask",
                                    Header = "What is the title of the project?",
                                    Questions = new List<BaseQuestion>
                                    {
                                        new TextInputQuestion
                                        {
                                            Id = "question1",
                                            Validator = new TextInputValidation(new TextInputValidationProperties
                                            {
                                                IsRequired = true,
                                                MaxLength = 50
                                            })
                                        }
                                    }
                                },
                                new SubTask
                                {
                                    Id = "technologies-subtask",
                                    DisplayName = "Technology",
                                    PostTaskPage = new RepeatTaskPage
                                    {
                                        Header = "Technologies used",
                                        Body = "You used the following technologies.",
                                        RepeatLinkText = "Add another technology"
                                    },
                                    PreTaskPage = new PreTaskPage
                                    {
                                        Header = "Technologies used",
                                        Body = "Here you can add technologies that you used during the project.",
                                        BeginLinkText = "Add a technology"
                                    },
                                    TaskItems = new List<ITaskItem>
                                    {
                                        new TaskQuestionPage
                                        {
                                            Id = "technology-name",
                                            Header = "What technology did you use?",
                                            Questions = new List<BaseQuestion>
                                            {
                                                new TextInputQuestion
                                                {
                                                    Id = "question1",
                                                    Validator = new TextInputValidation(new TextInputValidationProperties
                                                    {
                                                        IsRequired = true,
                                                        MaxLength = 50
                                                    })
                                                }
                                            }
                                        },
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };
    }
}