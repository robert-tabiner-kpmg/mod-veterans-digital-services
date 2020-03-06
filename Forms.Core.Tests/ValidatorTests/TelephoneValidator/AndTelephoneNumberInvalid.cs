using System.Linq;
using AutoFixture;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.Validation;
using Forms.Core.Tests.Builders.Form;
using Xunit;

namespace Forms.Core.Tests.ValidatorTests.TelephoneValidator
{
    public class AndTelephoneNumberInvalid : WhenTestingTelephoneValidator
    {
        [Theory]
        [InlineData("abcd")]
        [InlineData("0777777777")] // 1 char short
        [InlineData("077777777777")] // 1 char long
        [InlineData("+44777777777")] // 1 char short
        [InlineData("+4477777777777")] // 1 char long
        public void WhenTheNumberIsInIncorrectFormat__ThenCorrectErrorMessageIsReturned(string number)
        {
            // arrange
            var fx = new Fixture();
            var answerItem = AnswerBuilder.Build.WithDefaultValue(number).AnAnswer();
            var properties = new TelephoneValidationProperties
            {
                IsRequired = true,
                IsRequiredMessage = fx.Create<string>(),
                InvalidFormatMessage = fx.Create<string>()
            };
            TestService = new TelephoneValidation(properties);
            InFlightQuestion = new InFlightQuestion
            {
                Answer = answerItem
            };
            
            // act
            var result = Act();
            
            // assert
            Assert.False(result.IsValid);
            Assert.Equal(properties.InvalidFormatMessage, result.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void WhenTheNumberIsRequiredButAnswerIsEmpty__ThenCorrectErrorMessageIsReturned(string email)
        {
            // arrange
            var fx = new Fixture();
            var answerItem = AnswerBuilder.Build.WithDefaultValue(email).AnAnswer();
            
            var properties = new TelephoneValidationProperties
            {
                IsRequired = true,
                IsRequiredMessage = fx.Create<string>(),
                InvalidFormatMessage = fx.Create<string>()
            };
            TestService = new TelephoneValidation(properties);
            InFlightQuestion = new InFlightQuestion
            {
                Answer = answerItem
            };
            
            // act
            var result = Act();
            
            // assert
            Assert.False(result.IsValid);
            Assert.Equal(properties.IsRequiredMessage, result.Errors.First().ErrorMessage);
        }
    }
}