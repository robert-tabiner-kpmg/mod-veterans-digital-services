using FluentValidation.Results;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.Validation;

namespace Forms.Core.Tests.ValidatorTests.EmailValidator
{
    public class WhenTestingEmailValidator
    {
        public EmailValidation TestService { get; set; }
        public InFlightQuestion InFlightQuestion { get; set; }
        

        public ValidationResult Act()
        {
            return TestService.Validate(InFlightQuestion);
        }
    }
}