using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;

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

        Task<WalletView> UpdateBalance(decimal amount, string walletId);
    }

    public class WalletService(IWalletRepository walletRepository, IMapper mapper) : IWalletService
    {
        public async Task CreateWallet(string userId)
        {
            // Check if the wallet already exists for the user
            var existingWallet = await GetWalletByUserIdAsync(userId);
            if (existingWallet != null)
            {
                throw new InvalidOperationException("Wallet already exists for this user.");
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
                throw new InvalidOperationException("Failed to create wallet.");
            }
        }

        public async Task<Wallet?> GetWalletByUserIdAsync(string userId)
        {
            return await walletRepository.FindBy(w => w.UserId == userId);
        }


        public async Task<WalletView> UpdateBalance(decimal amount, string walletId)
        {
            var wallet = await walletRepository.FindBy(w => w.Id == walletId && w.Status == UserWallet.ACTIVE);

            if (wallet is null)
            {
                throw new BadActionException("No Wallet for this Wallet Id");
            }

            wallet.Balance += amount;


            await walletRepository.UpdateAsync(wallet);

            var view = mapper.Map<WalletView>(wallet);

            return view;
        }
    }
}