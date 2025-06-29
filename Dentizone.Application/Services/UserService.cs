using System.Linq.Expressions;
using AutoMapper;
using Dentizone.Application.DTOs.User;
using Dentizone.Application.Interfaces.User;
using Dentizone.Application.Services.Payment;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;
using Dentizone.Infrastructure;

namespace Dentizone.Application.Services
{
    public class UserService(
        IUserRepository userRepository,
        IMapper mapper,
        IWalletService walletService,
        AppDbContext dbContext)
        : IUserService
    {
        public async Task<UserView> CreateAsync(CreateAppUser userDto)
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var userEntity = mapper.Map<AppUser>(userDto);
                var createdUser = await userRepository.CreateAsync(userEntity);
                await walletService.CreateWallet(userEntity.Id);
                await transaction.CommitAsync();
                return mapper.Map<UserView>(createdUser);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task<UserView> DeleteAsync(string id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User with id {id} not found.");
            }

            var deletedUser = await userRepository.DeleteAsync(id);
            return mapper.Map<UserView>(deletedUser);
        }

        public async Task<ICollection<UserView>> GetAllAsync(int page, string? searchByName = null,
            Expression<Func<AppUser, bool>>? filterExpression = null)
        {
            var users = await userRepository.GetAllAsync(page, filterExpression);
            if (users == null)
            {
                throw new NotFoundException("No users found.");
            }

            if (!string.IsNullOrEmpty(searchByName))
            {
                users = users.Where(u => u.FullName.Contains(searchByName, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return mapper.Map<ICollection<UserView>>(users);
        }

        public async Task<DomainUserView> GetByIdAsync(string id)
        {
            var user = await userRepository.FindBy(u => u.Id == id,
                [
                    u => u.University
                ]
            );
            if (user == null)
            {
                throw new NotFoundException($"User with id {id} not found.");
            }

            return mapper.Map<DomainUserView>(user);
        }


        public async Task SetUserStateAsync(string userId, UserStateDTO userStateDto)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with id {userId} not found.");
            }

            user.Status = userStateDto.Status;
            await userRepository.Update(user);
        }


        public async Task<UserView> SetKycStatusAsync(string userId, KycStatus status)
        {
            var user = await userRepository.GetByIdAsync(userId);
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
            var updatedUser = await userRepository.Update(user);
            return mapper.Map<UserView>(updatedUser);
        }

        public async Task<UserView> SetNationalId(string userId, string nationalId)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with id {userId} not found.");
            }

            user.NationalId = long.Parse(nationalId);
            var updated = await userRepository.Update(user);

            return mapper.Map<UserView>(updated);
        }
    }
}