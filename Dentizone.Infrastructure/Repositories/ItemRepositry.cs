using System.Linq.Expressions;
using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class ItemRepository : AbstractRepository, IItemRepository
    {
        public ItemRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Item> CreateAsync(Item entity)
        {
            await dbContext.Items.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Item?> FindBy(Expression<Func<Item, bool>> condition,
            Expression<Func<Item, object>>[]? includes)
        {
            IQueryable<Item> query = dbContext.Items;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
        }

        public async Task<Item?> DeleteAsync(string id)
        {
            var deletedItem = await GetByIdAsync(id);
            dbContext.Items.Remove(deletedItem);
            await dbContext.SaveChangesAsync();
            return deletedItem;
        }


        public async Task<Item?> GetByIdAsync(string id)
        {
            var item = await dbContext.Items.Where(i => i.Id == id && !i.IsDeleted)
                .FirstOrDefaultAsync();
            return item;
        }
    }
}