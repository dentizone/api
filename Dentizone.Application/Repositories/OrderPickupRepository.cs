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
    internal class OrderPickupRepository : AbstractRepository, IOrderPickupRepository
    {
        AppDbContext DbContext;
        public OrderPickupRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<OrderPickup> CreateAsync(OrderPickup entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsDeleted = false;
            DbContext.OrderPickups.Add(entity);
            DbContext.SaveChanges();
            return entity;
        }

        public async Task<OrderPickup?> DeleteAsync(string id)
        {
           var pickup = await DbContext.OrderPickups.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (pickup == null) return null;
            pickup.IsDeleted = true;
            DbContext.OrderPickups.Update(pickup);
            await DbContext.SaveChangesAsync();
            return pickup;
        }

        public async Task<IEnumerable<OrderPickup>> GetAllAsync(int page = 1)
        {
           var Pickups = dbContext.OrderPickups
                .AsNoTracking()
                .Where(p => !p.IsDeleted)
                .Skip(CalculatePagination(page))
                .Take(DefaultPageSize);
            return Pickups;
        }

        public Task<OrderPickup?> GetByIdAsync(string id)
        {
           var Pickup = dbContext.OrderPickups
                .AsNoTracking()
                .Where(p => p.Id == id && !p.IsDeleted)
                .FirstOrDefaultAsync();
            return Pickup;

        }
    }
}
