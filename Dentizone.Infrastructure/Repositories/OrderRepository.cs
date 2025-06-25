using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces;
using Dentizone.Domain.Interfaces.Repositories;
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

        private IQueryable<Order> BuildPagedQuery(int page, Expression<Func<Order, bool>>? filter)
        {
            IQueryable<Order> query = dbContext.Orders;
            if (filter != null)
            {
                query = query.Where(filter).OrderByDescending(o => o.CreatedAt)
                             .Skip(CalculatePagination(page))
                             .Take(DefaultPageSize);
            }

            return query;
        }

        public async Task<PagedResult<Order>> GetAllAsync(int page, Expression<Func<Order, bool>>? filter)
        {
            var query = BuildPagedQuery(page, filter);

            var totalCount = await query.CountAsync();

            query = query.Include(o => o.Buyer)
                         .Include(o => o.OrderItems)
                         .ThenInclude(p => p.Post)
                         .Include(o => o.ShipInfo)
                         .Include(o => o.OrderStatuses);


            return new PagedResult<Order>
            {
                Items = await query.AsNoTracking().ToListAsync(),
                Page = page,
                PageSize = DefaultPageSize,
                TotalCount = totalCount
            };
        }


        public async Task<Order?> GetOrderDetails(string orderId, string buyerId)
        {
            var query = dbContext.Orders
                                 .AsNoTracking()
                                 .Where(o => o.Id == orderId && o.BuyerId == buyerId)
                                 .Include(o => o.Buyer)
                                 .Include(o => o.OrderItems)
                                 .ThenInclude(p => p.Post)
                                 .Include(o => o.ShipInfo)
                                 .Include(o => o.OrderStatuses);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<Order>> GetOrdersWithDetails(string buyerId)
        {
            return await dbContext.Orders
                                  .AsNoTracking()
                                  .Where(o => o.BuyerId == buyerId)
                                  .Include(o => o.Buyer)
                                  .Include(o => o.OrderItems)
                                  .ThenInclude(p => p.Post)
                                  .Include(o => o.ShipInfo)
                                  .Include(o => o.OrderStatuses)
                                  .OrderByDescending(o => o.CreatedAt)
                                  .ToListAsync();
        }

        public async Task<int> CountTotalOrders()
        {
            var result = await dbContext.Orders.Where(o => !o.IsDeleted).AsNoTracking().CountAsync();
            return result;
        }

        public async Task<decimal> AverageValueOfOrders()
        {
            var average = await dbContext.Orders
                                         .AsNoTracking()
                                         .Where(o => !o.IsDeleted)
                                         .AverageAsync(o => (decimal?)o.TotalAmount);
            return average ?? 0m;
        }
    }
}