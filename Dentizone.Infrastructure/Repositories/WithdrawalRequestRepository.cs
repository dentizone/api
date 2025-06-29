using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    internal class WithdrawalRequestRepository(AppDbContext dbContext)
        : AbstractRepository(dbContext), IWithdrawalRequestRepository
    {
        private AppDbContext _dbContext = dbContext;

        public async Task<WithdrawalRequest> CreateAsync(WithdrawalRequest entity)
        {
            await _dbContext.WithdrawalRequests.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<WithdrawalRequest?> FindBy(Expression<Func<WithdrawalRequest, bool>> condition,
            Expression<Func<WithdrawalRequest, object>>[]? includes)
        {
            IQueryable<WithdrawalRequest> query = _dbContext.WithdrawalRequests;
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
            IQueryable<WithdrawalRequest> query = _dbContext.WithdrawalRequests;
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
            _dbContext.WithdrawalRequests.Remove(deletedRequest);


            await _dbContext.SaveChangesAsync();
            return deletedRequest;
        }


        public async Task<WithdrawalRequest?> GetByIdAsync(string id)
        {
            var request = await _dbContext.WithdrawalRequests.Where(w => w.Id == id)
                .Include(w => w.Wallet)
                .Include(w => w.Wallet.User)
                .FirstOrDefaultAsync();
            return request;
        }

        public async Task<WithdrawalRequest> UpdateAsync(WithdrawalRequest entity)
        {
            _dbContext.WithdrawalRequests.Update(entity);


            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}