using System;
using System.Linq;
using System.Threading.Tasks;
using Common.ExternalFrameworks;
using Forms.Infrastructure.Cache.ConnectionFactory;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Forms.Infrastructure.Cache
{
    public class RedisCacheFramework : ICacheFramework
    {
        private readonly IDatabase _redisDatabase;
        
        public RedisCacheFramework(IRedisConnectionFactory redisConnectionFactory)
        {
            _redisDatabase = redisConnectionFactory.GetDatabase();
        }
        
        public async Task<T> Get<T>(string key) where T : class
        {
            if (!await _redisDatabase.KeyExistsAsync(key))
            {
                return null;
            }

            var redisData = await _redisDatabase.SetMembersAsync(key);

            var formattedData = JsonConvert.DeserializeObject<T>(redisData.FirstOrDefault(), GetJsonSettings());;

            return formattedData;        
        }

        public async Task Save<T>(string key, T item)
        {
            await _redisDatabase.KeyDeleteAsync(key);
            await _redisDatabase.SetAddAsync(key, JsonConvert.SerializeObject(item, GetJsonSettings()));
            await _redisDatabase.KeyExpireAsync(key, new TimeSpan(15, 0, 0, 0));
        }

        private JsonSerializerSettings GetJsonSettings()
        {
            return new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            };
        }
    }
}