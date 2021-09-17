using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeDelivery.Backend.Challenge.Application.Services.Caching;

namespace ZeDelivery.Backend.Challenge.Infrastructure.Services.Caching
{
    public class CacheService : ICacheService
    {
        private readonly ILogger<CacheService> logger;
        private readonly RedisConnector connection;
        public CacheService(ILogger<CacheService> logger, RedisConnector connection)
        {
            this.logger = logger;
            this.connection = connection;
        }
        public async Task<CacheEntry<T>> TryGetAsync<T>(string key)
        {
            try
            {
                var db = connection.Database;

                var output = await db.StringGetAsync(key);

                var deserializedObject = MsgPackSerialization.Deserialize<T>(output);

                return new CacheEntry<T>(deserializedObject, true);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Error retrieving the key: {key}");
                return new CacheEntry<T>(default, false);
            }
        }

        public async Task TrySetAsync<T>(string key, T value)
        {
            try
            {
                var db = connection.Database;

                var packedMessage = MsgPackSerialization.Serialize(value);
                
                var expiration = DateTime.UtcNow.AddMinutes(30);

                await db.StringSetAsync(key, packedMessage, TimeSpan.FromTicks(expiration.Ticks));
            }
            catch (Exception e)
            {
                logger.LogError($"Error setting the key: {key}");
            }
        }
    }
}
