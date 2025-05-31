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
    internal class WalletActivityRepository : AbstractRepository, IWalletActivityRepository
    {
        private AppDbContext DbContext;
        public WalletActivityRepository(AppDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<WalletActivity> CreateAsync(WalletActivity entity)
        {
            
            await DbContext.WalletActivities.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<WalletActivity> DeleteAsync(int id)
        {
            var deleted_wallet = DbContext.WalletActivities.FirstOrDefault(x => x.Id == id.ToString() && !x.IsDeleted);
            if (deleted_wallet != null)
            {
                deleted_wallet.IsDeleted = true;
                deleted_wallet.UpdatedAt = DateTime.UtcNow;
                DbContext.WalletActivities.Update(deleted_wallet);
            }
            
            
            await DbContext.SaveChangesAsync();
            return deleted_wallet;
        }

        public async Task<IEnumerable<WalletActivity>> GetAllAsync(int page = 1)
        {
            var wallets = await DbContext.Set<WalletActivity>().Where(w => !w.IsDeleted).ToListAsync();
            return wallets;
        }

        public async Task<WalletActivity> GetByIdAsync(int id)
        {
            var wallet = await DbContext.Set<WalletActivity>().Where(w => w.Id == id.ToString() && !w.IsDeleted).FirstOrDefaultAsync();
            return wallet;
        }
    }
}
