using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Forms.Core.Models.InFlight;

namespace Forms.Core.Models.Validation
{
    public class RadioValidation : AbstractValidator<InFlightQuestion>
    {
        public RadioValidation(RadioValidationProperties validationProps)
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

    public class RadioValidationProperties
    {
        public string RequiredMessage { get; set; } = "An option is required";
        
        public IEnumerable<string> ValidOptions { get; set; }
        public string ValidOptionsMessage { get; set; }
    }
}