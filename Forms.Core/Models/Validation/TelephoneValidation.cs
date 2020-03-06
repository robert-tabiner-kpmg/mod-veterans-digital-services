using FluentValidation;
using Forms.Core.Models.InFlight;

namespace Forms.Core.Models.Validation
{
    public class TelephoneValidation : AbstractValidator<InFlightQuestion>
    {
        public TelephoneValidation(TelephoneValidationProperties properties)
        {   
            if (properties.IsRequired)
            {
                RuleFor(x => x.Answer.AnswerAsString)
                    .NotNull().WithMessage(properties.IsRequiredMessage)
                    .MinimumLength(1).WithMessage(properties.IsRequiredMessage);
            }
            
            RuleFor(x => x.Answer.AnswerAsString)
                .Matches(
                    @"^(((\+44\s?\d{4}|\(?0\d{4}\)?)\s?\d{3\s?}\s?\d{3\s?})|((\+44\s?\d{3}|\(?0\d{3}\)?)\s?\d{3}\s?\d{4})|((\+44\s?\d{2}|\(?0\d{2}\)?)\s?\d{4}\s?\d{4}))(\s?\#(\d{4}|\d{3}))?$")
                    .When(x => x.Answer.AnswerAsString != null)
                    .WithMessage(properties.InvalidFormatMessage);
        }
    }
    
    public class TelephoneValidationProperties
    {
        public bool IsRequired { get; set; } = true;
        public string IsRequiredMessage { get; set; } = "Please enter a valid UK phone number";

        public string InvalidFormatMessage { get; set; } = "Please enter a valid UK phone number";
    }
}