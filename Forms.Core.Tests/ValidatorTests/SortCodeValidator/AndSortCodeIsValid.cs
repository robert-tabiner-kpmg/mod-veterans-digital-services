using Forms.Core.Models.InFlight;
using Forms.Core.Models.Validation;
using Forms.Core.Tests.Builders.Form;
using Xunit;

namespace Forms.Core.Tests.ValidatorTests.SortCodeValidator
{
    public class AndSortCodeIsValid : WhenTestingSortCodeValidator
    {
        [Theory]
        [InlineData("121212")]
        [InlineData("12-34-56")]
        public void WhenTheNumberIsValid__ThenNoErrorsAreReturned(string number)
        {
            // arrange
            var answerItem = AnswerBuilder.Build.WithDefaultValue(number).AnAnswer();
            
            TestService = new SortCodeValidation(new SortCodeValidationProperties
            {
                IsRequired = true
            });
            
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
            TestService = new SortCodeValidation(new SortCodeValidationProperties()
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