using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class CartRepository : AbstractRepository, ICartRepository
    {
        public CartRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Cart?> GetByIdAsync(string id)
        {
            return await dbContext.Carts
                                  .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<IEnumerable<Cart>> GetAllAsync(int page = 1)
        {
            return
                await
                    dbContext.Carts
                             .Where(c => !c.IsDeleted)
                             .Skip(CalculatePagination(page))
                             .Take(DefaultPageSize)
                             .ToListAsync();
        }

        public async Task<Cart> CreateAsync(Cart entity)
        {
            await dbContext.Carts.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Cart?> DeleteAsync(string id)
        {
            var toBeDeleted = await GetByIdAsync(id);
            if (toBeDeleted == null)
            {
                return null;
            }

            dbContext.Carts.Remove(toBeDeleted);
            await dbContext.SaveChangesAsync();
            return toBeDeleted;
        }
    }
}