using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories;

public class SaleTransactionRepository(AppDbContext dbContext)
    : AbstractRepository(dbContext), ISaleTransactionRepository
{
    public async Task<SalesTransaction?> GetByIdAsync(string id)
    {
        return await DbContext.SalesTransactions
            .FirstOrDefaultAsync(st => st.Id == id);
    }


    public async Task<SalesTransaction> CreateAsync(SalesTransaction entity)
    {
        await DbContext.SalesTransactions.AddAsync(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<SalesTransaction?> FindBy(Expression<Func<SalesTransaction, bool>> condition,
        Expression<Func<SalesTransaction, object>>[]? includes)
    {
        IQueryable<SalesTransaction> query = DbContext.SalesTransactions;
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.FirstOrDefaultAsync(condition);
    }


    public async Task<SalesTransaction> UpdateAsync(SalesTransaction entity)
    {
        DbContext.SalesTransactions.Update(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }
}