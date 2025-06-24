using AutoMapper;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Services.Payment
{
    public class WalletView
    {
        public string Id { get; set; }
        public decimal Balance { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }


    public interface IWalletService
    {
        Task CreateWallet(string userId);
        Task<Wallet?> GetWalletByUserIdAsync(string userId);

        Task<WalletView> AddToBalance(decimal amount, string walletId);

        Task<WalletView> GetWalletBalanceAsync(string userId);
    }

    public class WalletService(IWalletRepository walletRepository, IMapper mapper, Infrastructure.AppDbContext dbContext) : IWalletService
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
            if (amount == 0)
            {
                throw new ArgumentException("Amount must not be zero.", nameof(amount));
            }

            await using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var wallet = await walletRepository.FindBy(w => w.Id == walletId && w.Status == UserWallet.ACTIVE);

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

                await transaction.CommitAsync();

                var view = mapper.Map<WalletView>(wallet);
                return view;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<WalletView> GetWalletBalanceAsync(string userId)
        {
            var wallet = await GetWalletByUserIdAsync(userId);
            if (wallet == null)
            {
                throw new NotFoundException("Wallet not found for the user.");
            }

            var walletView = mapper.Map<WalletView>(wallet);
            return walletView;
        }
    }
}