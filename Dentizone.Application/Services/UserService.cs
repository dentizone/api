using AutoMapper;
using Dentizone.Application.DTOs.User;
using Dentizone.Application.Interfaces.User;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

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

        public async Task<UserView> CreateAsync(CreateAppUser userDto)
        {
            var userEntity = _mapper.Map<AppUser>(userDto);
            var createdUser = await _userRepository.CreateAsync(userEntity);
            return _mapper.Map<UserView>(createdUser);
        }

        public async Task<UserView> UpdateAsync(string id, UserDto appUser)
        {
            throw new NotImplementedException();
        }

        public async Task<UserView> DeleteAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User with id {id} not found.");
            }

            var deletedUser = await _userRepository.DeleteAsync(id);
            return _mapper.Map<UserView>(deletedUser);
        }

        public async Task<ICollection<UserView>> GetAllAsync(int page, string? searchByName = null,
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

            return _mapper.Map<ICollection<UserView>>(users);
        }

        public async Task<UserView> GetByIdAsync(string id)
        {
            var user = await _userRepository.FindBy(u => u.Id == id,
                                                    [
                                                        u => u.University
                                                    ]
                                                   );
            if (user == null)
            {
                throw new NotFoundException($"User with id {id} not found.");
            }

            return _mapper.Map<UserView>(user);
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


        public async Task<UserView> SetKycStatusAsync(string userId, KycStatus status)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with id {userId} not found.");
            }

            user.Status = status switch
                          {
                              KycStatus.APPROVED => UserState.Active,
                              KycStatus.REJECTED => UserState.Banned,
                              KycStatus.NOT_SUBMITTED => UserState.PendingVerification,
                              _ => user.Status
                          };

            user.KycStatus = status;
            var updatedUser = await _userRepository.Update(user);
            return _mapper.Map<UserView>(updatedUser);
        }
    }
}