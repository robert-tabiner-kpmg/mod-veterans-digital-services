using FluentValidation;
using Forms.Core.Models.InFlight;

namespace Forms.Core.Models.Validation
{
    public class NationalInsuranceValidation : AbstractValidator<InFlightQuestion>
    {
        public NationalInsuranceValidation(int minLength = 0, int maxLength = 25)
        {
            if (minLength > 0)
            {
                RuleFor(x => x.Answer.AnswerAsString)
                    .NotNull().WithMessage("Field is required");
            }

            RuleFor(x => x.Answer.AnswerAsString)
                .MinimumLength(minLength).WithMessage($"Minimum length {minLength} characters")
                .MaximumLength(maxLength).WithMessage($"Maximum length {maxLength} characters")
                .Matches(@"^\s*[a-zA-Z]{2}(?:\s*\d\s*){6}[a-zA-Z]?\s*$")
                .WithMessage("Enter a national insurance number in the correct format");
        }
    }
}