using System;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using StackExchange.Redis;
using Xunit;

namespace Forms.Infrastructure.Tests.ServiceTests.RedisCacheFrameworkTests.SaveTests
{
    public class AndSavingSucceeds : WhenCallingSaveOnCache
    {
        private string SerialisedResource { get; set; }

        public AndSavingSucceeds()
        {
            SerialisedResource = JsonConvert.SerializeObject(Resource);
        }

        [Fact]
        public async Task WhenCallingSaveOnCache__TheCurrentCacheAtKeyWasDeleted()
        {
            await Act();
            MockRedisDatabase.Verify(x => x.KeyDeleteAsync(CacheKey, CommandFlags.None), Times.Once);
        }


        [Fact]
        public async Task WhenCallingSaveOnCache__TheResourceWasSerialisedBeforeBeingCached()
        {
            await Act();
            MockRedisDatabase.Verify(x => x.SetAddAsync(CacheKey, SerialisedResource, CommandFlags.PreferMaster), Times.Once);
        }

        [Fact]
        public async Task WhenCallingSaveOnCache__TheExpiryWasSetToFifteenDays()        
        {
            await Act();
            MockRedisDatabase.Verify(x => x.KeyExpireAsync(CacheKey, new TimeSpan(15,0,0,0), CommandFlags.PreferMaster), Times.Once);
        }
    }
}
