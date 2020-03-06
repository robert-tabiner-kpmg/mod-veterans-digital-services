using System.Linq;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.Validation;
using Forms.Core.Tests.Builders.Form;
using Xunit;

namespace Forms.Core.Tests.ValidatorTests.EmailValidator
{
    public class AndEmailInvalid : WhenTestingEmailValidator
    {
        [Theory]
        [InlineData("")]
        [InlineData("abcd")]
        [InlineData("name@example")]
        public void WhenTheEmailIsInIncorrectFormat__ThenCorrectErrorMessageIsReturned(string email)
        {
            // arrange
            var answerItem = AnswerBuilder.Build.WithDefaultValue(email).AnAnswer();
            var properties = new EmailValidationProperties();
            TestService = new EmailValidation(properties);
            InFlightQuestion = new InFlightQuestion
            {
                Answer = answerItem
            };
            
            // act
            var result = Act();
            
            // assert
            Assert.False(result.IsValid);
            Assert.Equal(properties.InvalidEmailFormatValidationMessage, result.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void WhenTheEmailIsRequiredButAnswerIsEmpty__ThenCorrectErrorMessageIsReturned(string email)
        {
            // arrange
            var answerItem = AnswerBuilder.Build.WithDefaultValue(email).AnAnswer();
            var properties = new EmailValidationProperties();
            TestService = new EmailValidation(properties);
            InFlightQuestion = new InFlightQuestion
            {
                Answer = answerItem
            };
            
            // act
            var result = Act();
            
            // assert
            Assert.False(result.IsValid);
            Assert.Equal(properties.MissingEmailValidationMessage, result.Errors.First().ErrorMessage);
        }

        [Fact]
        public void WhenTheEmailIsLongerThanTheMaxLimit__ThenCorrectErrorMessageIsReturned()
        {
            // arrange
            var maxLength = 100;
            var properties = new EmailValidationProperties
            {
                MaxLength = maxLength
            };
            
            var answerItem = AnswerBuilder.Build.WithDefaultValue(new string('a', maxLength + 1) + "@example.com").AnAnswer();
            TestService = new EmailValidation(properties);
            InFlightQuestion = new InFlightQuestion
            {
                Answer = answerItem
            };
            
            // act
            var result = Act();
            
            // assert
            Assert.False(result.IsValid);
            Assert.Equal($"Email must be {maxLength} characters or fewer", result.Errors.First().ErrorMessage);
        }
    }
}