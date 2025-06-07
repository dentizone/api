using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dentizone.Application.DTOs.User;
using Dentizone.Application.Interfaces.User;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;

namespace Dentizone.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<CreatedUserDTO> CreateAsync(UserDTO userDTO)
        {
            var userEntity = _mapper.Map<AppUser>(userDTO);
            var createdUser = await _userRepository.CreateAsync(userEntity);
            return _mapper.Map<CreatedUserDTO>(createdUser);
        }

        public async Task<UserDTO> DeleteAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User with id {id} not found.");
            }
            var deletedUser = await _userRepository.DeleteAsync(id);
            return _mapper.Map<UserDTO>(deletedUser);
        }

        public async Task<ICollection<UserDTO>> GetAllAsync(int page, string? search = null)
        {
            var users = await _userRepository.GetAllAsync(page);
            if (users == null)
            {
                throw new NotFoundException("No users found.");
            }
            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(u => u.FullName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return _mapper.Map<ICollection<UserDTO>>(users);
        }

        public async Task<UserDTO?> GetByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User with id {id} not found.");
            }
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> UpdateAsync(string id, UserDTO userDTO)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User with id {id} not found.");
            }
            var userEntity = _mapper.Map<AppUser>(userDTO);
            userEntity.Id = id; 
            var updatedUser = await _userRepository.Update(userEntity);
            if (updatedUser == null)
            {
                throw new NotFoundException($"User with id {id} not found.");
            }
            return _mapper.Map<UserDTO>(updatedUser);
        }
        public async Task SetKycStatusAsync(string userId, KycStatusDTO kycStatusDTO)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with id {userId} not found.");
            }
            user.KycStatus = kycStatusDTO.KycStatus;
            await _userRepository.Update(user);
        }
        public async Task SetUserStateAsync(string userId, UserStateDTO userStateDTO)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with id {userId} not found.");
            }
            user.Status = userStateDTO.Status;
            await _userRepository.Update(user);
        }
}
