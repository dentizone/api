using Dentizone.Domain.Interfaces;

namespace Dentizone.Infrastructure.Identity
{
    public interface IResetTokenStore
    {
        Task StoreAsync(string userId, string token, TimeSpan? expiry = null);
        Task<string?> GetAsync(string userId);
        Task<bool> ValidateAsync(string userId, string token);
        Task RemoveAsync(string userId);
    }
    public class RedisResetTokenStore(IRedisService redisService) : IResetTokenStore
    {
        private const string Prefix = "reset-token:";



        public Task StoreAsync(string userId, string token, TimeSpan? expiry = null)
        {
            return redisService.SetValue(Prefix + userId, token, expiry ?? TimeSpan.FromMinutes(15));
        }

        public async Task<string?> GetAsync(string userId)
        {
            return await redisService.GetValue(Prefix + userId);
        }

        public async Task<bool> ValidateAsync(string userId, string token)
        {
            var saved = await GetAsync(userId);
            return saved == token;
        }

        public Task RemoveAsync(string userId)
        {
            return redisService.InvalidateCache(Prefix + userId);
        }
    }

}
