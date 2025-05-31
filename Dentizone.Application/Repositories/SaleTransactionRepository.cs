using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories;

public class SaleTransactionRepository : AbstractRepository, ISaleTransactionRepository
{
    public SaleTransactionRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<SalesTransaction?> GetByIdAsync(string id)
    {
        return await dbContext.SalesTransactions
                              .Include(st => st.Wallet)
                              .Include(st => st.Payment)
                              .FirstOrDefaultAsync(st => st.Id == id && !st.IsDeleted);
    }

    public async Task<IEnumerable<SalesTransaction>> GetAllAsync(int page = 1)
    {
        int skip = CalculatePagination(page);
        return await dbContext.SalesTransactions
                              .Include(st => st.Wallet)
                              .Include(st => st.Payment)
                              .Where(st => !st.IsDeleted)
                              .Skip(skip)
                              .Take(DefaultPageSize)
                              .ToListAsync();
    }

    public async Task<SalesTransaction> CreateAsync(SalesTransaction entity)
    {
        dbContext.SalesTransactions.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<SalesTransaction?> DeleteAsync(string id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
        {
            return null;
        }

        dbContext.SalesTransactions.Remove(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<SalesTransaction?> UpdateAsync(SalesTransaction entity)
    {
        var existingEntity = await GetByIdAsync(entity.Id);
        if (existingEntity == null)
        {
            return null;
        }

        dbContext.SalesTransactions.Update(existingEntity);
        await dbContext.SaveChangesAsync();
        return existingEntity;
    }
}