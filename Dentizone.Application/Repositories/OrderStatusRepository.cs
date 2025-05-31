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
    internal class OrderStatusRepository : AbstractRepository, IOrderStatusRepository
    {
        AppDbContext DbContext;
        public OrderStatusRepository(AppDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<OrderStatus> CreateAsync(OrderStatus entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsDeleted = false;
            entity.Status = Domain.Enums.OrderStatues.Pending;
            DbContext.OrderStatuses.Add(entity);
            DbContext.SaveChanges();
            return entity;
        }

        public async Task<OrderStatus?> DeleteAsync(string id)
        {
            var deletedOrderStatus = await DbContext.OrderStatuses.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (deletedOrderStatus is null) return null;
            deletedOrderStatus.IsDeleted = true;
            DbContext.OrderStatuses.Update(deletedOrderStatus);
            await DbContext.SaveChangesAsync();
            return deletedOrderStatus;

        }

        public async Task<IEnumerable<OrderStatus>> GetAllAsync(int page = 1)
        {
            var orderStatuses = await DbContext.OrderStatuses
                .Where(os => !os.IsDeleted)
                .Skip(CalculatePagination(page))
                .Take(DefaultPageSize)
                .ToListAsync();
            return orderStatuses;

        }

        public Task<OrderStatus?> GetByIdAsync(string id)
        {
            var orderStatus = DbContext.OrderStatuses
                .Where(os => os.Id == id && !os.IsDeleted)
                .FirstOrDefaultAsync();
            return orderStatus;
        }
    }
}
