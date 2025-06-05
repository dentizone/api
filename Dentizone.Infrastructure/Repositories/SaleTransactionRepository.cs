using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories;

public class SaleTransactionRepository : AbstractRepository, ISaleTransactionRepository
{
    public SaleTransactionRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<SalesTransaction?> GetByIdAsync(string id)
    {
        return await dbContext.SalesTransactions
                              .FirstOrDefaultAsync(st => st.Id == id);
    }


    public async Task<SalesTransaction> CreateAsync(SalesTransaction entity)
    {
        await dbContext.SalesTransactions.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<SalesTransaction?> FindBy(Expression<Func<SalesTransaction, bool>> condition,
                                                Expression<Func<SalesTransaction, object>>[]? includes)
    {
        IQueryable<SalesTransaction> query = dbContext.SalesTransactions;
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
        dbContext.SalesTransactions.Update(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }
}