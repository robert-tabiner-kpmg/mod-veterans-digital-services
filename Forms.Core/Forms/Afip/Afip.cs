using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forms.Core.EffectHandlers.Models;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Questions;
using Forms.Core.Models.Static;
using Forms.Core.Models.Validation;
using Graph;

namespace Forms.Core.Forms.Afip
{
    public static class Afip
    {
        public static Form Form => new Form
        {
            Name = "Afip claim form",
            TaskListPage = null,
            StartPage = new ContentPage
            {
                Header = "Armed Forces Independence Payment",
                BodyText = new StringBuilder()
                    .Append("<p>Use this form when claiming under the Armed Forces Independence Payment</p>")
                    .ToString()
            },
            TermsAndConditions = new ContentPage
            {
                Header = "Claim Form",
                BodyText = new StringBuilder()
                    .Append("<p><strong>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. " +
                            "Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. " +
                            "Praesent mauris. Fusce nec tellus sed augue semper porta. Mauris massa. Vestibulum lacinia arcu eget nulla. " +
                            "Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Curabitur sodales " +
                            "ligula in libero. Sed dignissim lacinia nunc.</strong></p>")
                    .Append("<p>Curabitur tortor. Pellentesque nibh. Aenean quam. In scelerisque sem at dolor. Maecenas mattis. Sed " +
                            "convallis tristique sem. Proin ut ligula vel nunc egestas porttitor. Morbi lectus risus, iaculis vel, suscipit " +
                            "quis, luctus non, massa. Fusce ac turpis quis ligula lacinia aliquet. Mauris ipsum. Nulla metus metus, ullamcorper " +
                            "vel, tincidunt sed, euismod in, nibh. Quisque volutpat condimentum velit.</p>")
                    .Append("<p>Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam nec ante. " +
                            "Sed lacinia, urna non tincidunt mattis, tortor neque adipiscing diam, a cursus ipsum ante quis turpis. " +
                            "Nulla facilisi. Ut fringilla. Suspendisse potenti. Nunc feugiat mi a tellus consequat imperdiet. Vestibulum sapien. " +
                            "Proin quam. Etiam ultrices. Suspendisse in justo eu magna luctus suscipit. Sed lectus. Integer euismod lacus luctus " +
                            "magna. Quisque cursus, metus vitae pharetra auctor, sem massa mattis sem, at interdum magna augue eget diam.</p>")
                    .ToString()
            },
            Tasks = new List<Task>
            {
                new Task
                {
                    Id = "afip-form",
                    Name = "Form Details",
                    SummaryPage = new SummaryPage(),
                    PostTaskPage = new ConsentPage
                    {
                        Header = "Submit Form",
                        Questions = new List<BaseQuestion>
                        {
                            new RadioQuestion
                            {
                                Label = "I authorise Veterans UK to correspond via email wherever possible",
                                Id = "question1",
                                Inline = true,
                                Validator = new RadioValidation(new RadioValidationProperties()),
                                Options = new List<string>
                                {
                                    "Yes", "No"
                                }
                            },
                            new RadioQuestion
                            {
                                Label =
                                    "I authorise DWP to use my details to see if I am entitled to any other benefits",
                                Id = "question2",
                                Inline = true,
                                Validator = new RadioValidation(new RadioValidationProperties()),
                                Options = new List<string>
                                {
                                    "Yes", "No"
                                }
                            }
                        }
                    },
                    TaskItems = new List<ITaskItem>
                    {
                        new TaskQuestionPage
                        {
                            Id = "member-number",
                            NextPageId = "national-insurance",
                            Header = "Member number",
                            Questions = new List<BaseQuestion>
                            {
                                new TextInputQuestion
                                {
                                    Id = "question1",
                                    Type = "Number",
                                    Hint =
                                        "If you know your member number, please provide it here before continuing:",
                                    Validator = new TextInputValidation(new TextInputValidationProperties
                                    {
                                        IsRequired = false,
                                        MaxLength = 12,
                                        MaxLengthMessage = "Member number must be 12 characters or fewer",
                                    })
                                }
                            }
                        },
                        new TaskQuestionPage
                        {
                            Id = "national-insurance",
                            NextPageId = "phone-number",
                            Header = "National insurance number",
                            Questions = new List<BaseQuestion>
                            {
                                new TextInputQuestion
                                {
                                    Id = "question1",
                                    Hint =
                                        "It's on your National Insurance card, benefit letter, payslip or p60. For example: 'QQ 12 34 56 C'",
                                    Type = "Text",
                                    Validator = new NationalInsuranceValidation()
                                }
                            }
                        },
                        new TaskQuestionPage
                        {
                            Id = "phone-number",
                            Header = "Contact telephone number",
                            NextPageId = "contact-email",
                            Questions = new List<BaseQuestion>
                            {
                                new TextInputQuestion()
                                {
                                    Id = "question1",
                                    Type = "tel",
                                    Autocomplete = "tel",
                                    Hint =
                                        "Provide a UK landline or UK mobile number that we can contact you on",
                                    Validator = new TelephoneValidation(new TelephoneValidationProperties())
                                }
                            }
                        },
                        new TaskQuestionPage
                        {
                            Id = "contact-email",
                            Header = "Email address",
                            NextPageId = "uk-or-overseas-bank",
                            Questions = new List<BaseQuestion>
                            {
                                new TextInputQuestion()
                                {
                                    Id = "question1",
                                    Type = "email",
                                    Autocomplete = "email",
                                    Hint = "We will send confirmation of your claim to this address",
                                    Width = 50,
                                    Validator = new EmailValidation(new EmailValidationProperties())
                                }
                            }
                        },
                        new TaskQuestionPage
                        {
                            Id = "uk-or-overseas-bank",
                            Header = "Where is your bank account located?",
                            NextPageId = "uk-bank",
                            Questions = new List<BaseQuestion>
                            {
                                new RadioQuestion()
                                {
                                    Id = "question1",
                                    Validator = new RadioValidation(new RadioValidationProperties()),
                                    Options = new List<string>
                                    {
                                        "Uk", "Overseas"
                                    }
                                }
                            },
                            Effects = new List<Effect>
                            {
                                new PathChangeEffect(x =>
                                    x.First().Answer.Values["default"] ==
                                    "Uk"
                                        ? "uk-bank"
                                        : "overseas-bank")
                            }
                        },
                        new TaskQuestionPage
                        {
                            Id = "uk-bank",
                            Header = "UK bank or building society account details",
                            NextPageId = null,
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
                                new TextInputQuestion()
                                {
                                    Id = "question2",
                                    Label = "Sort Code",
                                    Hint = "Must be 6 digits",
                                    Type = "Integer",
                                    Validator = new SortCodeValidation(new SortCodeValidationProperties()),
                                    Width = 5,
                                    InputMode = "number"
                                },
                                new TextInputQuestion
                                {
                                    Id = "question3",
                                    Label = "Account Number",
                                    Hint = "Must be between 6 to 8 digits long",
                                    Type = "Integer",
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
                                    Width = 10,
                                    InputMode = "number"
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
                            Id = "overseas-bank",
                            NextPageId = null,
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
                                    Hint = "can be up to 10 digits",
                                    Type = "Integer",
                                    Validator = new SortCodeValidation(new SortCodeValidationProperties{IsOverseasSortCode = true}),
                                    Width = 10,
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
                                        IsNumberMessage = "Enter a valid Account Number",
                                        MaxLength = 18,
                                        MaxLengthMessage = "Account Number must be 18 characters or fewer",
                                        MinLength = 1,
                                        MinLengthMessage = "Enter a valid Account Number",
                                        IsRequired = true,
                                        IsRequiredMessage = "Enter the Account Number"
                                    }),
                                },
                                new TextInputQuestion
                                {
                                    Id = "question4",
                                    Label = "International Bank Account Number (IBAN)",
                                    Type = "Integer",
                                    InputMode = "number",
                                    Validator = new TextInputValidation(new TextInputValidationProperties
                                    {
                                        IsNumber = true,
                                        IsNumberMessage = "Enter a valid International Bank Account Number",
                                        MaxLength = 20,
                                        MaxLengthMessage =
                                            "International Bank Account Number must be 20 characters or fewer",
                                        MinLength = 1,
                                        MinLengthMessage = "Enter a valid International Bank Account Number",
                                        IsRequired = true,
                                        IsRequiredMessage = "Enter the International Bank Account Number"
                                    }),
                                },
                                new TextInputQuestion
                                {
                                    Id = "question5",
                                    Label = "Bank Identifier Code (BIC)",
                                    Type = "Text",
                                    Validator = new TextInputValidation(new TextInputValidationProperties
                                    {
                                        IsNumber = true,
                                        IsNumberMessage = "Enter a valid Bank Identifier Code",
                                        MaxLength = 18,
                                        MaxLengthMessage =
                                            "Bank Identifier Code must be 18 characters or fewer",
                                        MinLength = 1,
                                        MinLengthMessage = "Enter a valid Bank Identifier Code",
                                        IsRequired = true,
                                        IsRequiredMessage = "Enter the Bank Identifier Code"
                                    }),
                                    Width = 20
                                }
                            }
                        },
                    }
                }
            },
        };
    }
};
            