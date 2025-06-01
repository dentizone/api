using System.Linq.Expressions;
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