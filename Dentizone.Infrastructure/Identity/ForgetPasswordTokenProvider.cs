using Microsoft.AspNetCore.Identity;

namespace Dentizone.Infrastructure.Identity
{
    public class UuidPasswordResetTokenProvider<TUser> : IUserTwoFactorTokenProvider<TUser>
        where TUser : class
    {
        private readonly IResetTokenStore _store;

        public UuidPasswordResetTokenProvider(IResetTokenStore store)
        {
            _store = store;
        }

        public async Task<string> GenerateAsync(string purpose, UserManager<TUser> manager, TUser user)
        {
            var token = Guid.NewGuid().ToString();
            var userId = await manager.GetUserIdAsync(user);
            await _store.StoreAsync(userId, token, TimeSpan.FromMinutes(15));
            return token;
        }

        public async Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser> manager, TUser user)
        {
            var userId = await manager.GetUserIdAsync(user);
            return await _store.ValidateAsync(userId, token);
        }

        public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
            => Task.FromResult(true);
    }
}