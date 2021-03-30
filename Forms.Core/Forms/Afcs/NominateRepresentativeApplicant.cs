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
    public static class NominateRepresentativeApplicant
    {
        public static Task Task => new Task
        {
            Id = "nominate-representative-Applicant-task",
            SummaryPage = new SummaryPage(),
            Name = "Who is making this application?",
            GroupNameIndex = 1,
            TaskItems = new List<ITaskItem>
            {
                new TaskQuestionPage
                {
                    Id = "nominate-someone-1",
                    NextPageId = "nominee-details",
                    Header = "Who is making this application?",
                    IntroText = new StringBuilder()
                        .Append(
                            "<p>The person named in this application must be the person who completes the declaration and final submission when all sections are completed.</p>")
                        .Append(
                            "<p>A claim may only be submitted on behalf of someone else if you have a Power of Attorney or other legal authority to act on their behalf.</p>")
                        
                        .ToString(),
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            //Label = "Would you like to nominate a representative?",
                            Id = "question1",
                            Options = new List<string> { "The person named on this claim is making the application.", 
                                "I am making an application on behalf of the person named claim on this and I have legal authority to act on their behalf."},
                            Validator = new RadioValidation(new RadioValidationProperties() )
                        }
                    },
                    Effects = new List<Effect>
                    {
                        new PathChangeEffect(x =>
                            x.First().Answer.Values["default"] == "The person named on this claim is making the application." ? "no-representative" : "nominee-details")
                    }
                },
                new TaskQuestionGhost("no-representative"),
                new TaskQuestionPage
                {
                    Id = "nominee-details",
                    NextPageId = "nomination-other-details",
                    Header = "What are your own details?",
                    IntroText = "You have told us you are making this claim on behalf of someone else.  Please provide your own details and tell us why you are applying on their behalf.",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Label = "Full name",
                            Id = "question1",
                            Type = "Text",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 30
                            })
                        },
                        new TextInputQuestion
                        {
                            Label = "Building and street",
                            Id = "question3",
                            Type = "Text",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 30
                            })                        
                        },
                        new TextInputQuestion
                        {
                            Id = "question4",
                            Label = "",
                            Type = "Text",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                MaxLength = 30
                            })
                        },
                        new TextInputQuestion
                        {
                            Id = "question5",
                            Label = "Town or city",
                            Type = "Text",
                            Width = 12,
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 20
                            })
                        },
                        new TextInputQuestion
                        {
                            Id = "question6",
                            Label = "County",
                            Type = "Text",
                            Width = 12,
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 20
                            })
                        },
                        new TextInputQuestion
                        {
                            Id = "question7",
                            Label = "Postcode",
                            Type = "Text",
                            Width = 12,
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                MaxLength = 10
                            })
                        },
                        new TextInputQuestion
                        {
                        Id = "question8",
                        Label = "Telephone number",
                        Type = "Number",
                        Width = 12,
                        Validator = new TelephoneValidation(new TelephoneValidationProperties())
                        },
                        new TextInputQuestion
                        {
                            Label = "Email address",
                            Id = "question2",
                            Type = "Text",
                            Validator = new EmailValidation(new EmailValidationProperties())
                        },
                    }
                },
                new TaskQuestionPage
                {
                    Id = "nomination-other-details",
                    Header =
                        "What legal authority do you have to make a claim on behalf of the person named on?",
                    IntroText = 
                     new StringBuilder()
                        .Append(
                            "<p>E.g. Power of Attorney held.</p>")
                        .Append(
                            "<p>Please upload a copy of the legal authority document you hold in the ‘Upload Documents’ section later in this application.</p>")
                        .ToString(),
                   
                    Questions = new List<BaseQuestion>
                    {
                         new TextInputQuestion
                        {
                            Id = "question2",
                            Type = "Text",
                             Validator = new TextInputValidation(new TextInputValidationProperties
                                {
                                    MaxLength = 100,
                                    IsRequired = true
                                })
                        },
                        
                    }
                }
            }
        };
    }
}