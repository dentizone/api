using AutoMapper;
using Dentizone.Application.DTOs.Withdrawal;
using Dentizone.Application.Interfaces;
using Dentizone.Application.Services.Payment;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Mail;
using Dentizone.Domain.Interfaces.Repositories;


namespace Dentizone.Application.Services
{
    public class WithdrawalService(
        IWalletService walletService,
        IWithdrawalRequestRepository withdrawalRepo,
        IMailService mailService,
        IMapper mapper) : IWithdrawalService
    {
        public async Task<WithdrawalRequestView> CreateWithdrawalRequestAsync(
            string userId, WithdrawalRequestDto withdrawalRequestDto)
        {
            var wallet = await walletService.GetWalletByUserIdAsync(userId);
            if (wallet == null)
                throw new NotFoundException("Wallet not found.");
            if (wallet.Balance < withdrawalRequestDto.Amount)
                throw new BadActionException("Insufficient balance.");

            wallet.Balance -= withdrawalRequestDto.Amount;
            await walletService.UpdateWallet(wallet);


            var request = new WithdrawalRequest
            {
                WalletId = wallet.Id,
                Amount = withdrawalRequestDto.Amount,
                Status = WithdrawalRequestStatus.Pending
            };
            var createdRequest = await withdrawalRepo.CreateAsync(request);
            if (createdRequest == null)
            {
                throw new BadActionException("Failed to create withdrawal request.");
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
                return [];
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
                throw new BadActionException("Only pending requests can be approved.");

            request.Status = WithdrawalRequestStatus.Approved;
            if (string.IsNullOrEmpty(adminNote))
                request.AdminNotes = "No additional notes provided.";
            request.AdminNotes = adminNote;


            await withdrawalRepo.UpdateAsync(request);


            //  var UserId = request.Wallet.User.Email ;

            var subject = "Withdrawal Approved";
            var body = $"Your withdrawal request of {request.Amount:C} has been approved. Note:{adminNote}";
            //  await mailService.Send(UserId, subject, body);

            return mapper.Map<WithdrawalRequestView>(request);
        }

        public async Task<WithdrawalRequestView> RejectWithdrawalAsync(string id, string? adminNote)
        {
            var request = await withdrawalRepo.GetByIdAsync(id);
            if (request == null)
                throw new NotFoundException("Withdrawal request not found.");

            if (request.Status != WithdrawalRequestStatus.Pending)
                throw new BadActionException("Only pending requests can be rejected.");

            request.Status = WithdrawalRequestStatus.Rejected;

            if (string.IsNullOrEmpty(adminNote))
                adminNote = "No reason provided for rejection.";

            request.AdminNotes = adminNote;
            var updatedRequest = await withdrawalRepo.UpdateAsync(request);
            if (updatedRequest == null)
                throw new NotFoundException("Failed to update withdrawal request.");

            var userId = updatedRequest.Wallet.UserId;
            var subject = "Withdrawal Rejected";
            var body = $"Your withdrawal request of {request.Amount:C} has been rejected. Reason: {adminNote}";
            await mailService.Send(userId, subject, body);

            return mapper.Map<WithdrawalRequestView>(updatedRequest);
        }
    }
}