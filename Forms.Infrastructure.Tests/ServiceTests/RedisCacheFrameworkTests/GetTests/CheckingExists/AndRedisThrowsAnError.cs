using System;
using System.Threading.Tasks;
using Moq;
using StackExchange.Redis;
using Xunit;

namespace Forms.Infrastructure.Tests.ServiceTests.RedisCacheFrameworkTests.GetTests.CheckingExists
{
    public class AndRedisThrowsAnError : WhenCallingGet
    {
        private Exception ThrownException { get; set; }

        public AndRedisThrowsAnError()
        {
            ThrownException = new Exception("An error in the Redis Database");

            MockRedisDatabase.Setup(x => x.KeyExistsAsync(CacheKey, CommandFlags.None)).ThrowsAsync(ThrownException);
        }

        [Fact]
        public async Task WhenGettingItemAndRedisConnectionFactoryThrows__TheExceptionBubblesUp()
        {
            var caughtException = await Assert.ThrowsAsync<Exception>(Act);

            Assert.Equal(ThrownException, caughtException);
        }
    }
}
