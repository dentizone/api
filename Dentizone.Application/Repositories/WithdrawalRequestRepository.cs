using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<WithdrawalRequest> DeleteAsync(int id)
        {
            var deleted_request = DbContext.WithdrawalRequests.FirstOrDefault(x => x.Id == id.ToString() && !x.IsDeleted);
            if (deleted_request != null)
            {
                deleted_request.IsDeleted = true;
                deleted_request.UpdatedAt = DateTime.UtcNow;
                DbContext.WithdrawalRequests.Update(deleted_request);
            }


            await DbContext.SaveChangesAsync();
            return deleted_request;
        }

        public async Task<IEnumerable<WithdrawalRequest>> GetAllAsync(int page = 1)
        {
            var requests = await DbContext.Set<WithdrawalRequest>().Where(w => !w.IsDeleted).ToListAsync();
            return requests;
        }

        public async Task<WithdrawalRequest> GetByIdAsync(int id)
        {
            var request = await DbContext.Set<WithdrawalRequest>().Where(w => w.Id == id.ToString() && !w.IsDeleted).FirstOrDefaultAsync();
            return request;
        }

        public async Task<WithdrawalRequest> UpdateAsync(WithdrawalRequest entity)
        {
            var existingRequest = await DbContext.WithdrawalRequests.FirstOrDefaultAsync(w => w.Id == entity.Id && !w.IsDeleted);
            if (existingRequest != null)
            {
                existingRequest.Amount = entity.Amount;
                existingRequest.Status = entity.Status;
                
                existingRequest.UpdatedAt = DateTime.UtcNow;
                DbContext.WithdrawalRequests.Update(existingRequest);


            }
            await DbContext.SaveChangesAsync();

            return existingRequest;

        }
    }
}
