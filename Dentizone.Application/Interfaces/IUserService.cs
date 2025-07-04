using Dentizone.Application.DTOs.User;
using Dentizone.Domain.Enums;
using Dentizone.Application.DTOs;

namespace Dentizone.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserView> CreateAsync(CreateAppUser userDto);
        Task<DomainUserView> GetByIdAsync(string id);

        Task<PagedResultDto<UserTableView>> GetAllAsync(UserFilterDto filters);

        Task<UserView> DeleteAsync(string id);
        Task<UserView> SetKycStatusAsync(string userId, KycStatus status);
        Task SetUserStateAsync(string userId, UserStateDto userStateDto);
        Task<UserView> SetNationalId(string userId, string nationalId);

        Task<UserStatsView> GetUserStatsAsync();
    }
}