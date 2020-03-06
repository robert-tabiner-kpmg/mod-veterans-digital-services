using FluentValidation;
using Forms.Core.Models.InFlight;

namespace Forms.Core.Models.Validation{
    public class SortCodeValidation : AbstractValidator<InFlightQuestion>
    {
        public SortCodeValidation(SortCodeValidationProperties properties)
        {
            if (properties.IsRequired)
            {
                RuleFor(x => x.Answer.AnswerAsString)
                    .NotNull().WithMessage(properties.IsRequiredMessage)
                    .NotEmpty().WithMessage(properties.IsRequiredMessage);
            }

            if (!properties.IsOverseasSortCode)
            {
                RuleFor(x => x.Answer.AnswerAsString)
                    .MinimumLength(6)
                    .When(x => x.Answer.AnswerAsString != null)
                    .WithMessage(properties.InvalidFormatMessage)
                    .MaximumLength(8)
                    .When(x => x.Answer.AnswerAsString != null)
                    .WithMessage(properties.InvalidFormatMessage)
                    .Matches(@"\b[0-9]{2}-?[0-9]{2}-?[0-9]{2}\b")
                    .When(x => x.Answer.AnswerAsString != null)
                    .WithMessage(properties.InvalidFormatMessage);
            }
            else
            {
                RuleFor(x => x.Answer.AnswerAsString)
                    .MinimumLength(6)
                    .When(x => x.Answer.AnswerAsString != null)
                    .WithMessage(properties.InvalidFormatMessage)
                    .MaximumLength(8)
                    .When(x => x.Answer.AnswerAsString != null)
                    .WithMessage(properties.InvalidFormatMessage)
                    .Matches(@"\b[0-9]{2}-?[0-9]{2}-?[0-9]{2}-?{[0-9]{2}-?[0-9]{2}}?\b")
                    .When(x => x.Answer.AnswerAsString != null)
                    .WithMessage(properties.InvalidFormatMessage);
            }
        }
    }

    public class SortCodeValidationProperties
    {
        public bool IsRequired { get; set; } = true;
        public string IsRequiredMessage { get; set; } = "Enter a sort code";
        public string InvalidFormatMessage { get; set; } = "Enter a valid sort code like 309430";
        public bool IsOverseasSortCode { get; set; } 
    }
}