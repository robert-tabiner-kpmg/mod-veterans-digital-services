using FluentValidation.Results;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.Validation;

namespace Forms.Core.Tests.ValidatorTests.SortCodeValidator
{
    public class WhenTestingSortCodeValidator
    {
        public SortCodeValidation TestService { get; set; }
        public InFlightQuestion InFlightQuestion { get; set; }
        

        public ValidationResult Act()
        {
            return TestService.Validate(InFlightQuestion);
        }
    }
}