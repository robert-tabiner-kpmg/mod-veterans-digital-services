using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Newtonsoft.Json;
using StackExchange.Redis;
using Xunit;

namespace Forms.Infrastructure.Tests.ServiceTests.RedisCacheFrameworkTests.GetTests.CheckingExists.GettingData
{
    public class AndGettingResourceSucceeds : AndTheKeyExists
    {
        private List<RedisValue> RedisResources { get; set; } = new List<RedisValue>();

        public AndGettingResourceSucceeds()
        {
            var fixture = new Fixture();

            Resource = fixture.Create<ResourceToCache>();

            RedisResources.Add(JsonConvert.SerializeObject(Resource));

            MockRedisDatabase.Setup(x => x.SetMembersAsync(CacheKey, CommandFlags.None)).ReturnsAsync(RedisResources.ToArray);
        }

        [Fact]
        public async Task WhenGettingItemFromCache__TheExpectedResultIsReturned()
        {
            await Act();
            Assert.Equal(System.Text.Json.JsonSerializer.Serialize(Resource), System.Text.Json.JsonSerializer.Serialize(Result));
        }
    }
}
