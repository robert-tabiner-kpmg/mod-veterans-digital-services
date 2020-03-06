using System;
using System.Threading.Tasks;
using Moq;
using StackExchange.Redis;
using Xunit;

namespace Forms.Infrastructure.Tests.ServiceTests.RedisCacheFrameworkTests.SaveTests
{
    public class AndTheConnectionThrowsAnError : WhenCallingSaveOnCache
    {
        private Exception ThrownException { get; set; }

        public AndTheConnectionThrowsAnError()
        {
            ThrownException = new Exception("An error in the Redis Database");

            MockRedisDatabase.Setup(x => x.KeyDeleteAsync(CacheKey, CommandFlags.None)).ThrowsAsync(ThrownException);
        }

        [Fact]
        public async Task WhenSavingItemAndRedisConnectionFactoryThrows__TheExceptionBubblesUp()
        {
            var caughtException = await Assert.ThrowsAsync<Exception>(Act);

            Assert.Equal(ThrownException, caughtException);
        }
    }
}
