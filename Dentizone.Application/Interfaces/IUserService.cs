using Dentizone.Application.DTOs.User;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using System.Linq.Expressions;

namespace Dentizone.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserView> CreateAsync(CreateAppUser userDto);
        Task<DomainUserView> GetByIdAsync(string id);

        Task<ICollection<UserView>> GetAllAsync(int page, string? searchByName = null,
            Expression<Func<AppUser, bool>>? filterExpression = null);

        Task<UserView> DeleteAsync(string id);
        Task<UserView> SetKycStatusAsync(string userId, KycStatus status);
        Task SetUserStateAsync(string userId, UserStateDto userStateDto);
        Task<UserView> SetNationalId(string userId, string nationalId);

        Task<UserStatsView> GetUserStatsAsync();
    }
}