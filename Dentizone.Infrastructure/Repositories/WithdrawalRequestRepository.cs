using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    internal class WithdrawalRequestRepository(AppDbContext dbContext)
        : AbstractRepository(dbContext), IWithdrawalRequestRepository
    {
        public async Task<WithdrawalRequest> CreateAsync(WithdrawalRequest entity)
        {
            await dbContext.WithdrawalRequests.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<WithdrawalRequest?> FindBy(Expression<Func<WithdrawalRequest, bool>> condition,
            Expression<Func<WithdrawalRequest, object>>[]? includes)
        {
            IQueryable<WithdrawalRequest> query = dbContext.WithdrawalRequests;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
        }

        public async Task<IEnumerable<WithdrawalRequest>> GetAllAsync(
            int page, Expression<Func<WithdrawalRequest, bool>>? condition)
        {
            IQueryable<WithdrawalRequest> query = dbContext.WithdrawalRequests;
            if (condition != null)
            {
                query = query.Where(condition);
            }

            return await query
                .OrderByDescending(u => u.CreatedAt)
                .Skip(CalculatePagination(page))
                .Take(DefaultPageSize)
                .ToListAsync();
        }

        public async Task<WithdrawalRequest?> DeleteAsync(string id)
        {
            var deletedRequest = await GetByIdAsync(id);
            dbContext.WithdrawalRequests.Remove(deletedRequest);


            await dbContext.SaveChangesAsync();
            return deletedRequest;
        }


        public async Task<WithdrawalRequest?> GetByIdAsync(string id)
        {
            var request = await dbContext.WithdrawalRequests.Where(w => w.Id == id)
                .Include(w => w.Wallet)
                .Include(w => w.Wallet.User)
                .FirstOrDefaultAsync();
            return request;
        }

        public async Task<WithdrawalRequest> UpdateAsync(WithdrawalRequest entity)
        {
            dbContext.WithdrawalRequests.Update(entity);


            await dbContext.SaveChangesAsync();

            return entity;
        }
    }
}