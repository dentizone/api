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

        public void SetValue(string key, string value)
        {
            database.StringSetAsync(key, value);
        }

        public void SetValue(string key, string value, TimeSpan expireTime)
        {
            database.StringSetAsync(key, value, expireTime);
        }

        public string? GetValue(string key)
        {
            var value = database.StringGet(key);
            return value.IsNullOrEmpty ? null : value.ToString();
        }
    }
}