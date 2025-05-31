using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class WalletActivityRepository : AbstractRepository, IWalletActivityRepository
    {
        private readonly AppDbContext DbContext;

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

        public async Task<WalletActivity?> DeleteAsync(string id)
        {
            var activity = await GetByIdAsync(id);
            DbContext.WalletActivities.Remove(activity);

            await DbContext.SaveChangesAsync();
            return activity;
        }

        public async Task<IEnumerable<WalletActivity>> GetAllAsync(int page = 1)
        {
            var wallets = await DbContext.WalletActivities.Where(w => !w.IsDeleted)
                                         .Skip(CalculatePagination(page))
                                         .Take(DefaultPageSize)
                                         .ToListAsync();
            return wallets;
        }

        public async Task<WalletActivity?> GetByIdAsync(string id)
        {
            var wallet = await DbContext.Set<WalletActivity>().Where(w => w.Id == id && !w.IsDeleted)
                                        .FirstOrDefaultAsync();
            return wallet;
        }
    }
}