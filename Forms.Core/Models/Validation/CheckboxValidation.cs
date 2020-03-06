using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Forms.Core.Models.InFlight;

namespace Forms.Core.Models.Validation
{
    public class CheckboxValidation : AbstractValidator<InFlightQuestion>
    {
        public CheckboxValidation(CheckboxValidationProperties validationProps)
        {
            RuleFor(x => x.Answer.AnswerAsString).NotNull().WithMessage(validationProps.RequiredMessage);

            if (validationProps.ValidOptions != null)
            {
                RuleFor(x => x.Answer.AnswerAsString).Custom((answer, context) =>
                {
                    if (!validationProps.ValidOptions.Contains(answer))
                    {
                        context.AddFailure(validationProps.ValidOptionsMessage);
                    }
                });
            }
        }
    }

    public class CheckboxValidationProperties
    {
        public string RequiredMessage { get; set; } = "An option is required";
        
        public IEnumerable<string> ValidOptions { get; set; }
        public string ValidOptionsMessage { get; set; }
    }
}