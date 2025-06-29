using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    public class WalletRepository(AppDbContext dbContext) : AbstractRepository(dbContext), IWalletRepository
    {
        private AppDbContext _dbContext = dbContext;

        public async Task<Wallet> CreateAsync(Wallet entity)
        {
            await _dbContext.Wallets.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Wallet?> FindBy(Expression<Func<Wallet, bool>> condition,
            Expression<Func<Wallet, object>>[]? includes)
        {
            IQueryable<Wallet> query = _dbContext.Wallets;
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
            var wallet = await _dbContext.Wallets.Where(w => w.Id == id)
                .FirstOrDefaultAsync();
            return wallet;
        }

        public async Task<Wallet> UpdateAsync(Wallet entity)
        {
            _dbContext.Wallets.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}