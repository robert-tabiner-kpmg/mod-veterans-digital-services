using FluentValidation;
using Forms.Core.Models.InFlight;

namespace Forms.Core.Models.Validation
{
    public class TextInputValidation : AbstractValidator<InFlightQuestion>
    {
        public TextInputValidation(TextInputValidationProperties properties)
        {
            if (properties.IsRequired)
            {
                RuleFor(x => x.Answer.AnswerAsString)
                    .NotNull().WithMessage(properties.IsRequiredMessage)
                    .NotEmpty().WithMessage(properties.IsRequiredMessage);
            }

            if (properties.IsNumber)
            {
                RuleFor(x => x.Answer.AnswerAsString)
                    .Matches("^[0-9]+$")
                    .When(x => !string.IsNullOrEmpty(x.Answer.AnswerAsString))
                    .WithMessage(properties.IsNumberMessage);
            }

            RuleFor(x => x.Answer.AnswerAsString)
                .MinimumLength(properties.MinLength)
                    .When(x => !string.IsNullOrEmpty(x.Answer.AnswerAsString))
                    .WithMessage(properties.MinLengthMessage)
                .MaximumLength(properties.MaxLength)
                    .When(x => !string.IsNullOrEmpty(x .Answer.AnswerAsString))
                    .WithMessage(properties.MaxLengthMessage);
        }
    }
    
    public class TextInputValidationProperties
    {
        public bool IsRequired { get; set; }
        public string IsRequiredMessage { get; set; } = "Field is required";
            
        public int MinLength { get; set; } = 1;
        public string MinLengthMessage { get; set; } = "Shorter than minimum length";

        public int MaxLength { get; set; } = 25;
        public string MaxLengthMessage { get; set; } = "Greater than maximum length";

        public bool IsNumber { get; set; } = false;
        public string IsNumberMessage = "Must only include numbers";
    }
}