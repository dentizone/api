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
            await DbContext.Wallets.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public Task<Wallet?> DeleteAsync(string id)
        {
            throw new NotImplementedException(); // TO BE REVMOVED 
        }


        public async Task<IEnumerable<Wallet>> GetAllAsync(int page = 1)
        {
            var wallets = await DbContext.Wallets.Where(w => !w.IsDeleted)
                                         .Skip(CalculatePagination(page))
                                         .Take(DefaultPageSize)
                                         .ToListAsync();
            return wallets;
        }

        public async Task<Wallet?> GetByIdAsync(string id)
        {
            var wallet = await DbContext.Wallets.Where(w => w.Id == id && !w.IsDeleted)
                                        .FirstOrDefaultAsync();
            return wallet;
        }

        public async Task<Wallet> UpdateAsync(Wallet entity)
        {
            DbContext.Wallets.Update(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }
    }
}