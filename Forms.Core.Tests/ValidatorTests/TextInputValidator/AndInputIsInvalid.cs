using System.Linq;
using AutoFixture;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.Validation;
using Forms.Core.Tests.Builders.Form;
using Xunit;

namespace Forms.Core.Tests.ValidatorTests.TextInputValidator
{
    public class AndInputIsInvalid : WhenTestingTextInputValidator
    {
        public Fixture Fx { get; set; }
        
        public AndInputIsInvalid()
        {
            Fx = new Fixture();    
        }
        
        [Theory]
        [InlineData("abcd")]
        [InlineData("...1")]
        public void WhenTheValidationRequiresANumber__AndTheInputIsNotANumber__ThenCorrectErrorMessageIsReturned(string input)
        {
            // arrange
            var answerItem = AnswerBuilder.Build.WithDefaultValue(input).AnAnswer(); 
            
            var properties = new TextInputValidationProperties
            {
                IsNumber = true,
                IsNumberMessage = Fx.Create<string>()
            };

            InFlightQuestion = new InFlightQuestion {Answer = answerItem};

            // act
            TestService = new TextInputValidation(properties);
            var result = Act();

            // assert
            Assert.False(result.IsValid);
            Assert.Contains(properties.IsNumberMessage, result.Errors.Select(x => x.ErrorMessage));
        }
        
        [Theory]
        [InlineData("aaaaaa")]
        public void WhenTheFieldIsGreaterThanTheMaxLength__ThenTheCorrectErrorMessageIsReturned(string input)
        {
            // arrange
            var answerItem = AnswerBuilder.Build.WithDefaultValue(input).AnAnswer();
            var properties = new TextInputValidationProperties
            {
                MaxLength = 5,
                MaxLengthMessage = Fx.Create<string>()
            };

            InFlightQuestion = new InFlightQuestion {Answer = answerItem};

            // act
            TestService = new TextInputValidation(properties);
            var result = Act();

            // assert
            Assert.False(result.IsValid);
            Assert.Contains(properties.MaxLengthMessage, result.Errors.Select(x => x.ErrorMessage));
        }

        [Theory]
        [InlineData("aa")]
        public void WhenTheFieldIsSmallerThanTheMinLength__ThenTheCorrectErrorMessageIsReturned(string input)
        {
            // arrange
            var answerItem = AnswerBuilder.Build.WithDefaultValue(input).AnAnswer();
            var properties = new TextInputValidationProperties
            {
                MinLength = 3,
                MinLengthMessage = Fx.Create<string>()
            };

            InFlightQuestion = new InFlightQuestion {Answer = answerItem};

            // act
            TestService = new TextInputValidation(properties);
            var result = Act();

            // assert
            Assert.False(result.IsValid);
            Assert.Contains(properties.MinLengthMessage, result.Errors.Select(x => x.ErrorMessage));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void WhenTheAnswerIsRequired__AndTheFieldIsNull__ThenTheCorrectErrorMessageIsReturned(string input)
        {
            // arrange
            var answerItem = AnswerBuilder.Build.WithDefaultValue(input).AnAnswer();
            var properties = new TextInputValidationProperties
            {
                IsRequired = true,
                IsRequiredMessage = Fx.Create<string>()
            };

            InFlightQuestion = new InFlightQuestion {Answer = answerItem};

            // act
            TestService = new TextInputValidation(properties);
            var result = Act();

            // assert
            Assert.False(result.IsValid);
            Assert.Contains(properties.IsRequiredMessage, result.Errors.Select(x => x.ErrorMessage));
        }
    }
}