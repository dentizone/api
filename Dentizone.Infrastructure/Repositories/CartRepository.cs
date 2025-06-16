using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    internal class CartRepository(AppDbContext dbContext) : AbstractRepository(dbContext), ICartRepository
    {
        public async Task<Cart?> GetByIdAsync(string id)
        {
            return await dbContext.Carts
                                  .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }


        public async Task<Cart> CreateAsync(Cart entity)
        {
            await dbContext.Carts.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Cart?> FindBy(Expression<Func<Cart, bool>> condition,
                                        Expression<Func<Cart, object>>[]? includes)
        {
            IQueryable<Cart> query = dbContext.Carts;
            if (includes == null) return await query.FirstOrDefaultAsync(condition);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(condition);
        }

        public async Task<Cart?> DeleteAsync(string id)
        {
            var cart = await GetByIdAsync(id);
            if (cart == null)
            {
                return null;
            }

            dbContext.Carts.Remove(cart);
            await dbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<IEnumerable<Cart>> FindAllBy(Expression<Func<Cart, bool>> condition,
                                                       Expression<Func<Cart, object>>[]? includes = null)
        {
            IQueryable<Cart> query = dbContext.Carts;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.Where(condition).ToListAsync();
        }

        public async Task<IEnumerable<Cart>> GetCartItemsByUserId(string userId)
        {
            var baseQuery = dbContext.Carts
                                     .Where(c => c.UserId == userId && !c.IsDeleted)
                                     .Include(c => c.User)
                                     .Include(c => c.Post)
                                     .ThenInclude(p => p.PostAssets)
                                     .ThenInclude(pa => pa.Asset);

            return await baseQuery.ToListAsync();
        }
    }
}