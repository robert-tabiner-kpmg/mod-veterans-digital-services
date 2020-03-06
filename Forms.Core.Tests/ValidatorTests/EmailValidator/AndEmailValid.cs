using Forms.Core.Models.InFlight;
using Forms.Core.Models.Validation;
using Forms.Core.Tests.Builders.Form;
using Xunit;

namespace Forms.Core.Tests.ValidatorTests.EmailValidator
{
    public class AndEmailValid : WhenTestingEmailValidator
    {
        [Fact]
        public void WhenTheEmailIsValid__ThenNoErrorsAreReturned()
        {
            // arrange
            var answerItem = AnswerBuilder.Build.WithDefaultValue("name@example.com").AnAnswer();
            TestService = new EmailValidation(new EmailValidationProperties());
            InFlightQuestion = new InFlightQuestion
            {
                Answer = answerItem
            };
            
            // act
            var result = Act();

            // assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void WhenTheEmailIsNotRequiredAndAnswerEmpty__ThenNoErrorsAreReturned()
        {
            // arrange
            TestService = new EmailValidation(new EmailValidationProperties
            {
                IsRequired = false
            });
            InFlightQuestion = new InFlightQuestion
            {
                Answer = new Answer()
            };
            
            // act
            var result = Act();

            // assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }
    }
}