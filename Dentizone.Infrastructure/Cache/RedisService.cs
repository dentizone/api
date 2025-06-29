using Dentizone.Domain.Interfaces;
using Dentizone.Domain.Interfaces.Secret;
using StackExchange.Redis;

namespace Dentizone.Infrastructure.Cache
{
    public class RedisService : IRedisService, IDisposable, IAsyncDisposable
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        private readonly ISecretService _secretService;

        public RedisService(ISecretService secretService)
        {
            _secretService = secretService;

            _redis = ConnectionMultiplexer.Connect(
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
            _database = _redis.GetDatabase();
        }

        public async Task SetValue(string key, string value)
        {
            await _database.StringSetAsync(key, value);
        }

        public async Task SetValue(string key, string value, TimeSpan expireTime)
        {
            await _database.StringSetAsync(key, value, expireTime);
        }

        public async Task<string?> GetValue(string key)
        {
            var value = await _database.StringGetAsync(key);
            return value.IsNullOrEmpty ? null : value.ToString();
        }


        public void Dispose()
        {
            _redis.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await _redis.DisposeAsync();
        }

        public async Task InvalidateCache(string key)
        {
            await _database.KeyDeleteAsync(key);
        }
    }
}