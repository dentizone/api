using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.DTOs.User;

namespace Dentizone.Application.Interfaces.User
{
    public interface IUserService
    {
        Task<CreatedUserDTO>CreateAsync(UserDTO userDTO);
        Task<UserDTO?> GetByIdAsync(string id);
        Task<ICollection<UserDTO>> GetAllAsync(int page, string? search = null);
        Task<UserDTO> UpdateAsync(string id, UserDTO userDTO);
        Task<UserDTO> DeleteAsync(string id);
        Task SetKycStatusAsync(string userId, KycStatusDTO kycStatusDTO);
        Task SetUserStateAsync(string userId, UserStateDTO userStateDTO);

    }
}
