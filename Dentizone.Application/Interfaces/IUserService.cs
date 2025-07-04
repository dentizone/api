using Dentizone.Application.DTOs.User;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using System.Linq.Expressions;
using Dentizone.Application.DTOs;
using Dentizone.Domain.Interfaces;

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