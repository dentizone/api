using AutoMapper;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;

namespace Dentizone.Application.Services.Payment
{
    public class WalletView
    {
        public required string Id { get; set; }
        public decimal Balance { get; set; }
        public UserWallet Status { get; set; } = UserWallet.Active;
        public decimal Pending { get; set; }
        public decimal TotalRevene { get; set; }
        public required string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }


    public interface IWalletService
    {
        Task CreateWallet(string userId);
        Task<Wallet?> GetWalletByUserIdAsync(string userId);

        Task<WalletView> AddToBalance(decimal amount, string walletId);

        Task<WalletView> GetWalletBalanceAsync(string userId);

        Task<WalletView> UpdateWallet(Wallet updatedWallet);
    }

    public class WalletService(
        IWalletRepository walletRepository,
        IWithdrawalRequestRepository withdrawalRequestRepository,
        IMapper mapper
    ) : IWalletService
    {
        public async Task CreateWallet(string userId)
        {
            // Check if the wallet already exists for the user
            var existingWallet = await GetWalletByUserIdAsync(userId);
            if (existingWallet != null)
            {
                throw new BadActionException("Wallet already exists for this user.");
            }

            // Create a new wallet
            var newWallet = new Wallet
            {
                UserId = userId,
            };

            // Save the new wallet to the repository
            var createdWallet = await walletRepository.CreateAsync(newWallet);
            if (createdWallet == null)
            {
                throw new BadActionException("Failed to create wallet.");
            }
        }

        public async Task<Wallet?> GetWalletByUserIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("userId cannot be null or empty.", nameof(userId));
            }

            return await walletRepository.FindBy(w => w.UserId == userId);
        }


        public async Task<WalletView> AddToBalance(decimal amount, string walletId)
        {
            var wallet = await walletRepository.FindBy(w => w.Id == walletId && w.Status == UserWallet.Active);

            if (wallet is null)
            {
                throw new BadActionException("No Wallet for this Wallet Id");
            }

            var newBalance = wallet.Balance + amount;
            if (newBalance < 0)
            {
                throw new BadActionException("Insufficient wallet balance.");
            }

            wallet.Balance = newBalance;
            await walletRepository.UpdateAsync(wallet);


            var view = mapper.Map<WalletView>(wallet);
            return view;
        }

        public async Task<WalletView> GetWalletBalanceAsync(string userId)
        {
            var wallet = await GetWalletByUserIdAsync(userId);
            if (wallet == null)
            {
                throw new NotFoundException("Wallet not found for the user.");
            }

            if (wallet.Status != UserWallet.Active)
            {
                throw new BadActionException("Wallet is not active.");
            }

            // Get Pending Withdrawal Requests
            var pendingWithdrawalRequests = await withdrawalRequestRepository.GetAllAsync(wr =>
                wr.WalletId == wallet.Id && wr.Status == WithdrawalRequestStatus.Pending);

            var successWithdraw = await withdrawalRequestRepository.GetAllAsync(wr =>
                wr.WalletId == wallet.Id && wr.Status == WithdrawalRequestStatus.Approved);

            var pendingAmount = pendingWithdrawalRequests.Sum(wr => wr.Amount);
            var successAmount = successWithdraw.Sum(wr => wr.Amount);

            // Calculate Total Revenue (Wallet Balance - Pending Amount + Success Withdraw)
            var totalRevenue =
                wallet.Balance - pendingAmount + successAmount; // Assuming Success Withdraw is not tracked separately


            var walletView = new WalletView
            {
                Id = wallet.Id,
                Balance = wallet.Balance,
                Status = wallet.Status,
                UserId = wallet.UserId,
                CreatedAt = wallet.CreatedAt,
                UpdatedAt = wallet.UpdatedAt,
                Pending = pendingAmount,
                TotalRevene = totalRevenue
            };


            return walletView;
        }

        public async Task<WalletView> UpdateWallet(Wallet updatedWallet)
        {
            var updatedEntity = await walletRepository.UpdateAsync(updatedWallet);
            if (updatedEntity == null)
            {
                throw new BadActionException("Failed to update wallet.");
            }

            return mapper.Map<WalletView>(updatedEntity);
        }
    }
}