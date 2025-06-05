using System.Linq.Expressions;
using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class WalletActivityRepository : AbstractRepository, IWalletActivityRepository
    {
        public WalletActivityRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<WalletActivity?> GetByIdAsync(string id)
        {
            return await dbContext.WalletActivities
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<WalletActivity> CreateAsync(WalletActivity entity)
        {
            await dbContext.WalletActivities.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<WalletActivity?> FindBy(Expression<Func<WalletActivity, bool>> condition,
            Expression<Func<WalletActivity, object>>[]? includes)
        {
            IQueryable<WalletActivity> query = dbContext.WalletActivities;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
        }

        public async Task<ICollection<WalletActivity>> GetAllBy(int page,
            Expression<Func<WalletActivity, bool>>? filter)
        {
            IQueryable<WalletActivity> query = dbContext.WalletActivities;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query
                .OrderByDescending(w => w.CreatedAt)
                .Skip(CalculatePagination(page))
                .Take(DefaultPageSize)
                .ToListAsync();
        }
    }
}