using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Dentizone.Domain.Enums;

namespace Dentizone.Infrastructure.Repositories
{
    public class OrderRepository(AppDbContext dbContext) : AbstractRepository(dbContext), IOrderRepository
    {
        public async Task<Order?> GetByIdAsync(string id)
        {
            return await DbContext.Orders
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> UpdateAsync(Order entity)
        {
            DbContext.Orders.Update(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Order> CreateAsync(Order entity)
        {
            await DbContext.Orders.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Order?> FindBy(Expression<Func<Order, bool>> condition,
            Expression<Func<Order, object>>[]? includes = null)
        {
            IQueryable<Order> query = DbContext.Orders;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
        }


        public async Task<PagedResult<Order>> GetAllAsync(int? page,
            Expression<Func<Order, bool>> filter)
        {
            var query = DbContext.Orders.AsQueryable();

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            var count = await query.CountAsync();

            if (page is not null)
            {
                query = query
                    .Skip(CalculatePagination(page.Value))
                    .Take(DefaultPageSize);
            }

            query = query
                .Include(o => o.Buyer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.ShipmentActivities)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Post)
                .ThenInclude(p => p.Seller)
                .Include(o => o.ShipInfo)
                .Include(o => o.OrderStatuses)
                .Include(o => o.Review);

            return new PagedResult<Order>
            {
                Items = await query.AsNoTracking().ToListAsync(),
                Page = page ?? 1,
                PageSize = DefaultPageSize,
                TotalCount = count
            };
        }


        public async Task<Order?> GetOrderDetails(string orderId, string buyerId)
        {
            var query = DbContext.Orders
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
            return await DbContext.Orders
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
            var result = await DbContext.Orders.Where(o => !o.IsDeleted).AsNoTracking().CountAsync();
            return result;
        }

        public async Task<decimal> AverageValueOfOrders()
        {
            var average = await DbContext.Orders
                .AsNoTracking()
                .Where(o => !o.IsDeleted)
                .AverageAsync(o => (decimal?)o.TotalAmount);
            return average ?? 0m;
        }

        public async Task<decimal> TotalRevenue()
        {
            var total = await DbContext.Orders
                .AsNoTracking()
                .Include(o => o.OrderStatuses)
                .Where(o => !o.IsDeleted && o.OrderStatuses.Any(os => os.Status == OrderStatues.Completed))
                .SumAsync(o => (decimal?)o.CommissionAmount);
            return total ?? 0m;
        }
    }
}