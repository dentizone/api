
using AutoMapper;
using Dentizone.Application.DTOs.Withdrawal;
using Dentizone.Application.Interfaces;
using Dentizone.Application.Services.Payment;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Mail;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Dentizone.Application.Services
{
    public class WithdrawalService(IWithdrawalService withdrawalService, IWalletService walletService, IWalletRepository walletRepo, IWithdrawalRequestRepository withdrawalRepo,IMailService mailService,  IMapper mapper) : IWithdrawalService
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
                throw new NotFoundException("Failed to create withdrawal request.");
            }

            var withdrawalView = mapper.Map<WithdrawalRequestView>(createdRequest);

            return withdrawalView;
        }

        public async Task<List<WithdrawalRequestView>> GetWithdrawalHistoryAsync(string userId, int page)
        {
            var wallet = await walletService.GetWalletByUserIdAsync(userId);
            if (wallet == null)
                throw new NotFoundException("Wallet not found.");

            var withdrawalRequests = await withdrawalRepo.GetAllAsync(page, w => w.WalletId == wallet.Id);
            if (!withdrawalRequests.Any())
            {
                throw new NotFoundException("No withdrawal requests found for this user.");
            }
            var withdrawalView = mapper.Map<List<WithdrawalRequestView>>(withdrawalRequests);
            return withdrawalView;
        }

        public async Task<WithdrawalRequestView> ApproveWithdrawalAsync(string id, string? adminNote)
        {
            var request = await withdrawalRepo.GetByIdAsync(id);
            if (request == null)
                throw new NotFoundException("Withdrawal request not found.");

            if (request.Status != WithdrawalRequestStatus.Pending)
                throw new InvalidOperationException("Only pending requests can be approved.");

            request.Status = WithdrawalRequestStatus.Approved;  
            if (string.IsNullOrEmpty(adminNote))
                request.AdminNotes = "No additional notes provided.";
            request.AdminNotes = adminNote;


            var updatedRequest = await withdrawalRepo.UpdateAsync(request);

            if (updatedRequest == null)
                throw new NotFoundException("Failed to update withdrawal request.");

            var UserId = updatedRequest.Wallet.UserId;

            var subject = "Withdrawal Approved";
            var body = $"Your withdrawal request of {request.Amount:C} has been approved. Note:{adminNote}";
            await mailService.Send(UserId, subject, body); 
            
            return mapper.Map<WithdrawalRequestView>(updatedRequest);
        }

        public async Task<WithdrawalRequestView> RejectWithdrawalAsync(string id, string? adminNote)
        {
            var request = await withdrawalRepo.GetByIdAsync(id);
            if (request == null)
                throw new NotFoundException("Withdrawal request not found.");

            if (request.Status != WithdrawalRequestStatus.Pending)
                throw new InvalidOperationException("Only pending requests can be rejected.");

            request.Status = WithdrawalRequestStatus.Rejected;

            if (string.IsNullOrEmpty(adminNote))
                adminNote = "No reason provided for rejection.";

            request.AdminNotes = adminNote;
            var updatedRequest = await withdrawalRepo.UpdateAsync(request);
            if (updatedRequest == null)
                throw new NotFoundException("Failed to update withdrawal request.");

            var UserId = updatedRequest.Wallet.UserId;
            var subject = "Withdrawal Rejected";
            var body = $"Your withdrawal request of {request.Amount:C} has been rejected. Reason: {adminNote}";
            await mailService.Send(UserId, subject, body);

            return mapper.Map<WithdrawalRequestView>(updatedRequest);
        }
    }
}
