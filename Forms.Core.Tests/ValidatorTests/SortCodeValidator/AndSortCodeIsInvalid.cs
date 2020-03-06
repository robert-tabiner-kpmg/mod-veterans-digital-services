using System.Linq;
using AutoFixture;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.Validation;
using Forms.Core.Tests.Builders.Form;
using Xunit;

namespace Forms.Core.Tests.ValidatorTests.SortCodeValidator
{
    public class AndSortCodeIsInvalid : WhenTestingSortCodeValidator
    {
        [Theory]
        [InlineData("abcdef")]
        [InlineData("ab-cd-ef")]
        [InlineData("12-12-1")]
        [InlineData("12-12-121")]
        [InlineData("12-12-12-12")]
        [InlineData("12345678")]
        [InlineData("12345")]
        public void WhenTheNumberIsInIncorrectFormat__ThenCorrectErrorMessageIsReturned(string number)
        {
            // arrange
            var fx = new Fixture();
            var answerItem = AnswerBuilder.Build.WithDefaultValue(number).AnAnswer();
            var properties = new SortCodeValidationProperties()
            {
                IsRequired = true,
                IsRequiredMessage = fx.Create<string>(),
                InvalidFormatMessage = fx.Create<string>()
            };
            
            InFlightQuestion = new InFlightQuestion
            {
                Answer = answerItem
            };
            
            TestService = new SortCodeValidation(properties);
            
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
            var properties = new SortCodeValidationProperties()
            {
                IsRequired = true,
                IsRequiredMessage = fx.Create<string>(),
                InvalidFormatMessage = fx.Create<string>()
            };
            
            InFlightQuestion = new InFlightQuestion
            {
                Answer = answerItem
            };
            
            TestService = new SortCodeValidation(properties);
            
            // act
            var result = Act();
            
            // assert
            Assert.False(result.IsValid);
            Assert.Equal(properties.IsRequiredMessage, result.Errors.First().ErrorMessage);
        }
    }
}