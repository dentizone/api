using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<CreatedUserDTO> CreateAsync(UserDto userDto)
        {
            var userEntity = _mapper.Map<AppUser>(userDto);
            var createdUser = await _userRepository.CreateAsync(userEntity);
            return _mapper.Map<CreatedUserDTO>(createdUser);
        }

        public async Task<UserDto> DeleteAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User with id {id} not found.");
            }

            var deletedUser = await _userRepository.DeleteAsync(id);
            return _mapper.Map<UserDto>(deletedUser);
        }

        public async Task<ICollection<UserDto>> GetAllAsync(int page, string? searchByName = null,
            Expression<Func<AppUser, bool>>? filterExpression = null)
        {
            var users = await _userRepository.GetAllAsync(page, filterExpression);
            if (users == null)
            {
                throw new NotFoundException("No users found.");
            }

            if (!string.IsNullOrEmpty(searchByName))
            {
                users = users.Where(u => u.FullName.Contains(searchByName, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return _mapper.Map<ICollection<UserDto>>(users);
        }

        public async Task<UserDto?> GetByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User with id {id} not found.");
            }

            return _mapper.Map<UserDto>(user);
        }

        // TO BE REVIEWED CAREFULLY and TESTED
        public async Task<UserDto> UpdateAsync(string id, UserDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User with id {id} not found.");
            }

            var userEntity = _mapper.Map<AppUser>(userDto);
            userEntity.Id = id;
            var updatedUser = await _userRepository.Update(userEntity);
            if (updatedUser == null)
            {
                throw new NotFoundException($"User with id {id} not found.");
            }

            return _mapper.Map<UserDto>(updatedUser);
        }

        public async Task SetKycStatusAsync(string userId, KycStatusDTO kycStatusDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with id {userId} not found.");
            }

            user.KycStatus = kycStatusDto.KycStatus;
            await _userRepository.Update(user);
        }

        public async Task SetUserStateAsync(string userId, UserStateDTO userStateDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with id {userId} not found.");
            }

            user.Status = userStateDto.Status;
            await _userRepository.Update(user);
        }
    }
}