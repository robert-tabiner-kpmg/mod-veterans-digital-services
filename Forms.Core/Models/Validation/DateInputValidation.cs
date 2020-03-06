using System;
using FluentValidation;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.Validation.CustomValidators;

namespace Forms.Core.Models.Validation
{
    public class DateInputValidation : AbstractValidator<InFlightQuestion>
    {
        public DateInputValidation(DateInputValidationProperties properties)
        {
            if (properties.IsRequired)
            {
                RuleFor(x => x.Answer.Values["day"])
                    .NotNull().WithMessage(properties.IsRequiredMessage)
                    .NotEmpty().WithMessage(properties.IsRequiredMessage);

                RuleFor(x => x.Answer.Values["month"])
                    .NotNull().WithMessage(properties.IsRequiredMessage)
                    .NotEmpty().WithMessage(properties.IsRequiredMessage);

                RuleFor(x => x.Answer.Values["year"])
                    .NotNull().WithMessage(properties.IsRequiredMessage)
                    .NotEmpty().WithMessage(properties.IsRequiredMessage);
            }

            RuleFor(x => x.Answer.Values["day"])
                .Length(1,2)
                .WithMessage(properties.InvalidFormatMessage);

            RuleFor(x => x.Answer.Values["month"])
                .Length(1,2)
                .WithMessage(properties.InvalidFormatMessage);

            RuleFor(x => x.Answer.Values["year"])
                .Length(4)
                .WithMessage(properties.InvalidFormatMessage);

            RuleFor(x => x.Answer.AnswerAsString).IsValidDate(properties.InvalidFormatMessage);

            if (properties.IsInFuture){
                RuleFor(x => x.Answer.AnswerAsString).IsInFuture(properties.InvalidFutureMessage);
            }
            if (properties.IsInPast){
                RuleFor(x => x.Answer.AnswerAsString).IsInPast(properties.InvalidPastMessage);
            }
        }
    }

    public class DateInputValidationProperties
    {
        public bool IsRequired { get; set; } = true;
        public string IsRequiredMessage { get; set; } = "Enter a date";
        public string InvalidFormatMessage { get; set; } = "Enter a valid date like 31 03 1980";
        public bool IsInFuture { get; set; }
        public string InvalidPastMessage { get; set; } = "Please enter a date in the past";
        public string InvalidFutureMessage { get; set; } = "Please enter a date in the future";
        public bool IsInPast { get; set; }
    }
}