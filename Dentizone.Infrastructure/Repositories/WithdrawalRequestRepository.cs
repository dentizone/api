using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    internal class WithdrawalRequestRepository(AppDbContext dbContext)
        : AbstractRepository(dbContext), IWithdrawalRequestRepository
    {
        private AppDbContext DbContext = dbContext;

        public async Task<WithdrawalRequest> CreateAsync(WithdrawalRequest entity)
        {
            await DbContext.WithdrawalRequests.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<WithdrawalRequest?> FindBy(Expression<Func<WithdrawalRequest, bool>> condition,
                                                     Expression<Func<WithdrawalRequest, object>>[]? includes)
        {
            IQueryable<WithdrawalRequest> query = DbContext.WithdrawalRequests;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
        }

        public async Task<WithdrawalRequest?> DeleteAsync(string id)
        {
            var deleted_request = await GetByIdAsync(id);
            DbContext.WithdrawalRequests.Remove(deleted_request);


            await DbContext.SaveChangesAsync();
            return deleted_request;
        }


        public async Task<WithdrawalRequest?> GetByIdAsync(string id)
        {
            var request = await DbContext.WithdrawalRequests.Where(w => w.Id == id)
                                         .FirstOrDefaultAsync();
            return request;
        }

        public async Task<WithdrawalRequest> UpdateAsync(WithdrawalRequest entity)
        {
            DbContext.WithdrawalRequests.Update(entity);


            await DbContext.SaveChangesAsync();

            return entity;
        }
    }
}