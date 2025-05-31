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
    internal class SalesTransactionRepository : AbstractRepository, ISalesTransactionRepository
    {
        private readonly AppDbContext DbContext; // Ensure DbContext is strongly typed    

        public SalesTransactionRepository(AppDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<SalesTransaction> CreateAsync(SalesTransaction entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsDeleted = false;
            entity.Amount = 0;
            entity.Status = Domain.Enums.SaleStatus.Pending;
            await DbContext.Set<SalesTransaction>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<SalesTransaction?> DeleteAsync(string id)
        {
            var deletedTransaction = await DbContext.Set<SalesTransaction>()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (deletedTransaction is null) return null;
            deletedTransaction.IsDeleted = true;
            DbContext.Set<SalesTransaction>().Update(deletedTransaction);
            await DbContext.SaveChangesAsync();
            return deletedTransaction;
        }

        public async Task<IEnumerable<SalesTransaction>> GetAllAsync(int page = 1)
        {
            foreach (var transaction in DbContext.Set<SalesTransaction>()
                 .AsNoTracking()
                 .Where(t => !t.IsDeleted)
                 .Skip(CalculatePagination(page))
                 .Take(DefaultPageSize))
            {
               return (IEnumerable<SalesTransaction>)transaction;
            }

            return Enumerable.Empty<SalesTransaction>();
        }

        public Task<SalesTransaction?> GetByIdAsync(string id)
        {
            var transaction = DbContext.Set<SalesTransaction>()
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
            return transaction;

        }
    }
}
