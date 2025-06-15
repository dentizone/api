using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
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

        public async Task<IEnumerable<Cart>> FindAllBy(Expression<Func<Cart, bool>> condition, Expression<Func<Cart, object>>[]? includes = null)
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
    }
}