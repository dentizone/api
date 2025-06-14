using Dentizone.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    public class OrderRepository(AppDbContext dbContext) : AbstractRepository(dbContext), IOrderRepository
    {
        public async Task<Order?> GetByIdAsync(string id)
        {
            return await dbContext.Orders
                                  .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> UpdateAsync(Order entity)
        {
            dbContext.Orders.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Order> CreateAsync(Order entity)
        {
            await dbContext.Orders.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Order?> FindBy(Expression<Func<Order, bool>> condition,
                                         Expression<Func<Order, object>>[]? includes = null)
        {
            IQueryable<Order> query = dbContext.Orders;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
        }

        public async Task<IEnumerable<Order>> GetAllAsync(int page, Expression<Func<Order, bool>> filter,
                                                          Expression<Func<Order, object>> orderBy,
                                                          Expression<Func<Order, object>>[] includes = null)
        {
            IQueryable<Order> query = dbContext.Orders.Where(filter);
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            query = query.OrderBy(orderBy).Skip(CalculatePagination(page)).Take(DefaultPageSize);
            return await query.ToListAsync();
        }
    }
}