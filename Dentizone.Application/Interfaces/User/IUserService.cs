using Dentizone.Application.DTOs.User;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using System.Linq.Expressions;

namespace Dentizone.Application.Interfaces.User
{
    public interface IUserService
    {
        Task<UserView> CreateAsync(CreateAppUser userDto);
        Task<UserView> GetByIdAsync(string id);

        Task<ICollection<UserView>> GetAllAsync(int page, string? searchByName = null,
                                                Expression<Func<AppUser, bool>>? filterExpression = null);

        Task<UserView> UpdateAsync(string id, UserDto appUser);
        Task<UserView> DeleteAsync(string id);
        Task SetKycStatusAsync(string userId, KycStatusDTO kycStatusDto);
        Task SetUserStateAsync(string userId, UserStateDTO userStateDto);
        Task<UserView> UpdateKyc(string userId, KycStatus status);
    }
}