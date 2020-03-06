using System;
using System.Globalization;
using FluentValidation;

namespace Forms.Core.Models.Validation.CustomValidators
{
    public static class CustomDateValidators {
        public static IRuleBuilderOptions<T, string> IsValidDate<T>(this IRuleBuilder<T, string> ruleBuilder, string validationMessage) {
            return ruleBuilder.Must(x =>
            {
                if (string.IsNullOrEmpty(x)) return true;
                return DateTime.TryParse(x,
                    CultureInfo.GetCultureInfo("en-GB"),
                    DateTimeStyles.None,
                    out _);
            }).WithMessage(validationMessage);
        }

        public static IRuleBuilderOptions<T, string> IsInFuture<T>(this IRuleBuilder<T, string> ruleBuilder, string validationMessage) {
            return ruleBuilder.Must(x =>
            {
                if (string.IsNullOrEmpty(x)) return true;
                if (DateTime.TryParse(x, CultureInfo.GetCultureInfo("en-GB"), DateTimeStyles.None, out var userDate)){
                    return (DateTime.Compare(userDate, DateTime.Today) != -1);
                }
                return true;

            }).WithMessage(validationMessage);
        }
        public static IRuleBuilderOptions<T, string> IsInPast<T>(this IRuleBuilder<T, string> ruleBuilder, string validationMessage) {
            return ruleBuilder.Must(x =>
            {
                if (string.IsNullOrEmpty(x)) return true;
                if (DateTime.TryParse(x, CultureInfo.GetCultureInfo("en-GB"), DateTimeStyles.None, out var userDate)){
                    return (DateTime.Compare(userDate, DateTime.Today) != 1);
                }
                return true;

            }).WithMessage(validationMessage);
        }
    }
}