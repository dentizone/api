using Dentizone.Domain.Interfaces;
using Dentizone.Domain.Interfaces.Secret;
using StackExchange.Redis;

namespace Dentizone.Infrastructure.Cache
{
    public class RedisService : IRedisService
    {
        private readonly ConnectionMultiplexer redis;
        private readonly IDatabase database;
        private readonly ISecretService _secretService;

        public RedisService(ISecretService secretService)
        {
            _secretService = secretService;

            redis = ConnectionMultiplexer.Connect(
                                                  new ConfigurationOptions
                                                  {
                                                      EndPoints =
                                                      {
                                                          {
                                                              _secretService.GetSecret("RedisServer"),
                                                              int.Parse(_secretService.GetSecret("RedisPort"))
                                                          }
                                                      },
                                                      User = "default",
                                                      Password =
                                                          _secretService
                                                              .GetSecret("RedisPassword")
                                                  }
                                                 );
            database = redis.GetDatabase();
        }

        public async Task SetValue(string key, string value)
        {
            await database.StringSetAsync(key, value);
        }

        public async Task SetValue(string key, string value, TimeSpan expireTime)
        {
            await database.StringSetAsync(key, value, expireTime);
        }

        public async Task<string?> GetValue(string key)
        {
            var value = await database.StringGetAsync(key);
            return value.IsNullOrEmpty ? null : value.ToString();
        }
    }
}