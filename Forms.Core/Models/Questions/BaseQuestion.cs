using FluentValidation;
using Forms.Core.Models.InFlight;

namespace Forms.Core.Models.Questions
{
    public abstract class BaseQuestion
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Hint { get; set; }
        public string Value { get; set; } = string.Empty;
        public AbstractValidator<InFlightQuestion> Validator { get; set; }
    }
}