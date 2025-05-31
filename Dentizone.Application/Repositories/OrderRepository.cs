using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dentizone.Application.Repositories
{
    internal class OrderRepository : AbstractRepository, IOrderRepository
    {
        private AppDbContext DbContext;
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<Order> CreateAsync(Order entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsDeleted = false;
            entity.CommissionAmount = 0;
            entity.CompletedAt = DateTime.UtcNow;
            entity.TotalAmount = 0;
            await DbContext.Orders.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Order?> DeleteAsync(string id)
        {
            var order = await DbContext.Orders.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (order == null) return null;
            order.IsDeleted = true;
            DbContext.Orders.Update(order);
            await DbContext.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllAsync(int page = 1)
        {
           var orders = await DbContext.Orders
                .AsNoTracking()
                .Where(o => !o.IsDeleted)
                .Skip(CalculatePagination(page))
                .Take(DefaultPageSize)
                .ToListAsync();
            return orders;

        }

        public Task<Order?> GetByIdAsync(string id)
        {
            var order = DbContext.Orders
                 .AsNoTracking()
                 .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
            return order;
        }
    }
}
