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
    internal class OrderItemRepository : AbstractRepository, IOrderItemRepository
    {
        AppDbContext DbContext;
        public OrderItemRepository(AppDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<OrderItem> CreateAsync(OrderItem entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsDeleted = false;
            await dbContext.OrderItems.AddAsync(entity);
           await dbContext.SaveChangesAsync();
            return entity;


        }

        public async Task<OrderItem?> DeleteAsync(string id)
        {
            var deletedOrderItem = await dbContext.OrderItems.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (deletedOrderItem is null) return null;
            dbContext.OrderItems.Remove(deletedOrderItem);
            await dbContext.SaveChangesAsync();
            return deletedOrderItem;
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync(int page = 1)
        {
           var orderItems = await dbContext.OrderItems
                .Where(oi => !oi.IsDeleted)
                .Skip(CalculatePagination(page))
                .Take(DefaultPageSize)
                .ToListAsync();
            return orderItems;
        }

        public Task<OrderItem?> GetByIdAsync(string id)
        {
            var orderItem = dbContext.OrderItems
                .Where(oi => oi.Id == id && !oi.IsDeleted)
                .FirstOrDefaultAsync();
            return orderItem;
        }
    }
}
