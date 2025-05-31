using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class WalletRepository : AbstractRepository, IWalletRepository
    {
        private AppDbContext DbContext;
        public WalletRepository(AppDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<Wallet> CreateAsync(Wallet entity)
        {
            entity.Balance = 0;
            entity.Status = 0;
            await DbContext.Wallets.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Wallet> DeleteAsync(int id)
        {
            var deleted_wallet = DbContext.Wallets.FirstOrDefault(x => x.Id == id.ToString() && !x.IsDeleted);
            if (deleted_wallet != null)
            {
                throw new Exception("wallet not found or already deleted.");
            }
            deleted_wallet.IsDeleted = true;
            deleted_wallet.UpdatedAt = DateTime.UtcNow;
            DbContext.Wallets.Update(deleted_wallet);
            await DbContext.SaveChangesAsync();
            return deleted_wallet;
        }

        public async Task<IEnumerable<Wallet>> GetAllAsync(int page = 1)
        {
            var wallets = await DbContext.Set<Wallet>().Where(w => !w.IsDeleted).ToListAsync();
            return wallets;
        }

        public async Task<Wallet> GetByIdAsync(int id)
        {
            var wallet = await DbContext.Set<Wallet>().Where(w => w.Id == id.ToString() && !w.IsDeleted).FirstOrDefaultAsync();
            return wallet;
        }

        public async Task<Wallet> UpdateAsync(Wallet entity)
        {
            var existingWallet = await DbContext.Wallets.FirstOrDefaultAsync(w => w.Id == entity.Id && !w.IsDeleted);
            if (existingWallet == null)
            {
                throw new Exception("Wallet not found or already deleted.");
            }
            existingWallet.Balance = entity.Balance;
            existingWallet.Status=entity.Status;
            existingWallet.UpdatedAt = DateTime.UtcNow;
            DbContext.Wallets.Update(existingWallet);

            await DbContext.SaveChangesAsync();

            return existingWallet;

        }
    }
}
