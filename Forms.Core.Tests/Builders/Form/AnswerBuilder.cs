using System.Collections.Generic;
using AutoFixture;
using Forms.Core.Models.InFlight;

namespace Forms.Core.Tests.Builders.Form
{
    public class AnswerBuilder
    {
        private Answer Answer { get; }
        public static AnswerBuilder Build => new AnswerBuilder();

        private AnswerBuilder()
        {
            var fixture = new Fixture();
            Answer = fixture.Build<Answer>().Without(fx => fx.Values).Create();
            WithDefaultValue("this is the default value");
        }

        public AnswerBuilder WithDefaultValue(string value)
        {
            Answer.Values.Remove("default");
            Answer.Values.Add("default", value);
            return this;
        }
        
        public AnswerBuilder WithType(AnswerType answerType)
        {
            Answer.AnswerType = answerType;
            return this;
        }
        
        public AnswerBuilder WithSpecificKeyValues(Dictionary<string, string> keyValues)
        {
            Answer.Values.Clear();

            foreach (var (key, value) in keyValues)
            {
                Answer.Values.Add(key, value);
            }
            
            return this;
        }

        public Answer AnAnswer()
        {
            return Answer;
        }
    }
}