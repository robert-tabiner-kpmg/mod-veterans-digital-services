using AutoFixture;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.Validation;
using Forms.Core.Tests.Builders.Form;
using Xunit;

namespace Forms.Core.Tests.ValidatorTests.TextInputValidator
{
    public class AndInputIsValid : WhenTestingTextInputValidator
    {
        public Fixture Fx { get; set; }
        
        public AndInputIsValid()
        {
            Fx = new Fixture();    
        }

        [Theory]
        [InlineData("123456")]
        public void WhenTheFieldIsANumber__AndTheValidationRequiresANumber__ThenNoErrorIsReturned(string input)
        {
            // arrange
            var answerItem = AnswerBuilder.Build.WithDefaultValue(input).AnAnswer();
            
            var properties = new TextInputValidationProperties
            {
                IsNumber = true,
            };
            InFlightQuestion = new InFlightQuestion {Answer = answerItem};
            
            // act
            TestService = new TextInputValidation(properties);
            var result = Act();

            // assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void WhenTheFieldIsNotRequired__ThenNoErrorIsReturned(string input)
        {
            // arrange
            var answerItem = AnswerBuilder.Build.WithDefaultValue(input).AnAnswer();
            
            var properties = new TextInputValidationProperties
            {
                IsRequired = false
            };
            InFlightQuestion = new InFlightQuestion {Answer = answerItem};
            
            // act
            TestService = new TextInputValidation(properties);
            var result = Act();

            // assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }
    }
}