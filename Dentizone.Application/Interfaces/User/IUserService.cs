using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.DTOs.User;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces.User
{
    public interface IUserService
    {
        Task<CreatedUserDTO> CreateAsync(UserDto userDto);
        Task<UserDto?> GetByIdAsync(string id);

        Task<ICollection<UserDto>> GetAllAsync(int page, string? searchByName,
            Expression<Func<AppUser, bool>>? filterExpression);

        Task<UserDto> UpdateAsync(string id, UserDto userDto);
        Task<UserDto> DeleteAsync(string id);
        Task SetKycStatusAsync(string userId, KycStatusDTO kycStatusDto);
        Task SetUserStateAsync(string userId, UserStateDTO userStateDto);
    }
}