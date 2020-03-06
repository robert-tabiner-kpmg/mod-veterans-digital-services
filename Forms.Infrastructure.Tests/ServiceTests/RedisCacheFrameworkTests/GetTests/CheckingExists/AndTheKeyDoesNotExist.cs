using System.Threading.Tasks;
using Moq;
using StackExchange.Redis;
using Xunit;

namespace Forms.Infrastructure.Tests.ServiceTests.RedisCacheFrameworkTests.GetTests.CheckingExists
{
    public class AndTheKeyDoesNotExist : WhenCallingGet
    {
        public AndTheKeyDoesNotExist()
        {
            MockRedisDatabase.Setup(x => x.KeyExistsAsync(CacheKey, CommandFlags.None)).ReturnsAsync(false);
        }

        [Fact]
        public async Task WhenGettingFromCacheWithInvalidKey__ThenNullIsReturned()
        {
            await Act();

            Assert.Null(Result);
        }
    }
}
