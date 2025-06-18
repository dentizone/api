using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    internal class WalletRepository(AppDbContext dbContext) : AbstractRepository(dbContext), IWalletRepository
    {
        private AppDbContext DbContext = dbContext;

        public async Task<Wallet> CreateAsync(Wallet entity)
        {
            await DbContext.Wallets.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Wallet?> FindBy(Expression<Func<Wallet, bool>> condition,
                                          Expression<Func<Wallet, object>>[]? includes)
        {
            IQueryable<Wallet> query = DbContext.Wallets;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
        }


        public async Task<Wallet?> GetByIdAsync(string id)
        {
            var wallet = await DbContext.Wallets.Where(w => w.Id == id)
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