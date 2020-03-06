using FluentValidation;
using Forms.Core.Models.InFlight;

namespace Forms.Core.Models.Validation
{
    public class EmailValidation: AbstractValidator<InFlightQuestion>
    {
        public EmailValidation(EmailValidationProperties properties)
        {
            if (properties.IsRequired)
            {
                RuleFor(x => x.Answer.AnswerAsString)
                    .NotNull().WithMessage(properties.MissingEmailValidationMessage);
            }
            
            RuleFor(x => x.Answer.AnswerAsString)
                .MaximumLength(properties.MaxLength).WithMessage($"Email must be {properties.MaxLength} characters or fewer")
                .EmailAddress().WithMessage(properties.InvalidEmailFormatValidationMessage);
        }
    }

    public class EmailValidationProperties
    {
        public bool IsRequired { get; set; } = true;
        public int MaxLength { get; set; } = 50;

        public string InvalidEmailFormatValidationMessage { get; set; } = "Enter an email address in the correct format, like name@example.com";
        public string MissingEmailValidationMessage { get; set; } = "Enter an email address in the correct format, like name@example.com";
    }
}