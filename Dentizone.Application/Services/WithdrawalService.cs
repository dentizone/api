
using AutoMapper;
using Dentizone.Application.DTOs.Withdrawal;
using Dentizone.Application.Interfaces;
using Dentizone.Application.Services.Payment;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces.Repositories;


namespace Dentizone.Application.Services
{
    public class WithdrawalService(IWithdrawalService withdrawalService, IWalletService walletService, IWalletRepository walletRepo, IWithdrawalRequestRepository withdrawalRepo,  IMapper mapper) : IWithdrawalService
    {
        public async Task<WithdrawalRequestView> CreateWithdrawalRequestAsync(string userId, WithdrawalRequestDto withdrawalRequestDto )
        {
            var wallet = await walletService.GetWalletByUserIdAsync(userId);
            if (wallet == null)
                throw new InvalidOperationException("Wallet not found.");
            if (wallet.Balance < withdrawalRequestDto.Amount)
                throw new InvalidOperationException("Insufficient balance.");

            wallet.Balance -= withdrawalRequestDto.Amount;
            await walletRepo.UpdateAsync(wallet);


            var request = new WithdrawalRequest
            {
                Id = Guid.NewGuid().ToString(),
                WalletId = wallet.Id,
                Amount = withdrawalRequestDto.Amount,
                Status = WithdrawalRequestStatus.Pending
            };
            var createdRequest = await withdrawalRepo.CreateAsync(request);
            if (createdRequest == null)
            {
                throw new InvalidOperationException("Failed to create withdrawal request.");
            }

            var withdrawalView = mapper.Map<WithdrawalRequestView>(createdRequest);

            return withdrawalView;
        }
    }
}
