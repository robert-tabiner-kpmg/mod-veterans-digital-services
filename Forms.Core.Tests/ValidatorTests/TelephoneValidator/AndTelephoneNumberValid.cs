using Forms.Core.Models.InFlight;
using Forms.Core.Models.Validation;
using Forms.Core.Tests.Builders.Form;
using Xunit;

namespace Forms.Core.Tests.ValidatorTests.TelephoneValidator
{
    public class AndTelephoneNumberValid : WhenTestingTelephoneValidator
    {
        [Theory]
        [InlineData("07777777777")]
        [InlineData("+447777777777")]
        public void WhenTheNumberIsValid__ThenNoErrorsAreReturned(string number)
        {
            // arrange
            var answerItem = AnswerBuilder.Build.WithDefaultValue(number).AnAnswer();
            TestService = new TelephoneValidation(new TelephoneValidationProperties());
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
        public void WhenTheNumberIsNotRequiredAndAnswerEmpty__ThenNoErrorsAreReturned()
        {
            // arrange
            TestService = new TelephoneValidation(new TelephoneValidationProperties
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