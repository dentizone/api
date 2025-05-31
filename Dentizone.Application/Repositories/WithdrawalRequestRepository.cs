using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class WithdrawalRequestRepository : AbstractRepository, IWithdrawalRequestRepository
    {
        private AppDbContext DbContext;

        public WithdrawalRequestRepository(AppDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<WithdrawalRequest> CreateAsync(WithdrawalRequest entity)
        {
            await DbContext.WithdrawalRequests.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<WithdrawalRequest?> DeleteAsync(string id)
        {
            var deleted_request = await GetByIdAsync(id);
            DbContext.WithdrawalRequests.Remove(deleted_request);


            await DbContext.SaveChangesAsync();
            return deleted_request;
        }

        public async Task<IEnumerable<WithdrawalRequest>> GetAllAsync(int page = 1)
        {
            var requests = await DbContext.WithdrawalRequests.Where(w => !w.IsDeleted)
                                          .Skip(CalculatePagination(page))
                                          .Take(DefaultPageSize)
                                          .ToListAsync();
            return requests;
        }

        public async Task<WithdrawalRequest?> GetByIdAsync(string id)
        {
            var request = await DbContext.WithdrawalRequests.Where(w => w.Id == id && !w.IsDeleted)
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