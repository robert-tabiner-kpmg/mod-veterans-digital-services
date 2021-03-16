using System.Collections.Generic;
using System.Linq;
using Forms.Core.EffectHandlers.Models;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Questions;
using Forms.Core.Models.Static;
using Forms.Core.Models.Validation;

namespace Forms.Core.Forms.Afcs
{
    public static class PaymentDetails
    {
        public static Task Task => new Task
        {
            Id = "payment-details-task",
            Name = "Payment details",
            GroupNameIndex = 5,
            SummaryPage = new SummaryPage(),
            TaskItems = new List<ITaskItem>
            {
                new TaskQuestionPage
                {
                    Id = "claim-illness",
                    Header = "Where is your bank account located?",
                    NextPageId = "uk-bank-account-details",
                    Questions = new List<BaseQuestion>
                    {
                        new RadioQuestion
                        {
                            Id = "question1",
                            Options = new List<string>
                            {
                                "In the United Kingdom",
                                "Overseas"
                            },
                            Validator = new RadioValidation(new RadioValidationProperties())
                        }
                    },
                    Effects = new List<Effect>
                    {
                        new PathChangeEffect(x =>
                            x.First().Answer.Values["default"] ==
                            "Overseas"
                                ? "overseas-bank-details"
                                : "uk-bank-account-details")
                    }
                },
                new TaskQuestionPage
                {
                    Id = "uk-bank-account-details",
                    Header = "UK bank or building society account details",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Id = "question1",
                            Label = "Name on the account",
                            Type = "Text",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                IsRequiredMessage = "Enter the name on the account",
                                MaxLength = 50,
                                MaxLengthMessage = "Name must be 50 characters or fewer",
                                MinLength = 1,
                                MinLengthMessage = "Enter the name on the account"
                            })
                        },
                        new TextInputQuestion
                        {
                            Id = "question2",
                            Label = "Sort Code",
                            Hint = "Must be 6 digits",
                            Type = "Integer",
                            InputMode = "number",
                            Validator = new SortCodeValidation(new SortCodeValidationProperties()),
                            Width = 5
                        },
                        new TextInputQuestion
                        {
                            Id = "question3",
                            Label = "Account Number",
                            Hint = "Must be between 6 to 8 digits long",
                            Type = "Integer",
                            InputMode = "number",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsNumber = true,
                                IsNumberMessage = "Enter a valid account number like 00733445",
                                IsRequired = true,
                                IsRequiredMessage = "Enter an account number",
                                MaxLength = 8,
                                MaxLengthMessage = "Account number must be between 6 and 8 digits",
                                MinLength = 6,
                                MinLengthMessage = "Account number must be between 6 and 8 digits"
                            }),
                            Width = 10
                        },
                        new TextInputQuestion
                        {
                            Id = "question4",
                            Label = "Building society roll number (if you have one)",
                            Hint = "You can find it on your card, statement or passbook.",
                            Type = "Text",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = false,
                                MaxLength = 20,
                                MaxLengthMessage =
                                    "Building society roll number must be between 1 and 20 characters",
                                MinLength = 1,
                                MinLengthMessage =
                                    "Building society roll number must be between 1 and 20 characters"
                            }),
                            Width = 20
                        }
                    }
                },
                new TaskQuestionPage
                {
                    Id = "overseas-bank-details",
                    Header = "Overseas bank account details",
                    Questions = new List<BaseQuestion>
                    {
                        new TextInputQuestion
                        {
                            Id = "question1",
                            Label = "Name on the account",
                            Type = "Text",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = true,
                                IsRequiredMessage = "Enter the name on the account",
                                MaxLength = 50,
                                MaxLengthMessage = "Name must be 50 characters or fewer",
                                MinLength = 1,
                                MinLengthMessage = "Enter the name on the account"
                            })
                        },
                        new TextInputQuestion
                        {
                            Id = "question2",
                            Label = "Sort Code",
                            Hint = "Can be up to 10 digits",
                            Type = "Integer",
                            Validator = new SortCodeValidation(new SortCodeValidationProperties{IsOverseasSortCode = true}),
                            Width = 5,
                            InputMode = "number"
                        },
                        new TextInputQuestion
                        {
                            Id = "question3",
                            Label = "Account Number",
                            Hint = "Can be up to 18 digits long",
                            Type = "Integer",
                            InputMode = "number",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsNumber = true,
                                IsRequired = true,
                                MaxLength = 18,
                                MinLength = 1,
                            }),
                            Width = 10
                        },
                        new TextInputQuestion
                        {
                            Id = "question4",
                            Label = "International Bank Account Number (IBAN)",
                            Type = "Text",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = false,
                                MaxLength = 20,
                                MinLength = 1,
                            }),
                            Width = 20
                        },
                        new TextInputQuestion
                        {
                            Id = "question5",
                            Label = "Bank Identifier Code (BIC)",
                            Type = "Text",
                            Validator = new TextInputValidation(new TextInputValidationProperties
                            {
                                IsRequired = false,
                                MaxLength = 20,
                                MinLength = 1,
                            }),
                            Width = 20
                        }
                    }
                },
            }
        };
    }
}