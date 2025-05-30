using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class ItemRepository : AbstractRepository, IItemRepository
    {
        private readonly AppDbContext DbContext;

        public ItemRepository(AppDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<Item> CreateAsync(Item entity)
        {
            await DbContext.Items.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Item?> DeleteAsync(string id)
        {
            var deletedItem = DbContext.Items.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (deletedItem is null) return null;
            DbContext.Items.Remove(deletedItem);
            await DbContext.SaveChangesAsync();
            return deletedItem;
        }

        public async Task<IEnumerable<Item>> GetAllAsync(int page = 1)
        {
            var items = await DbContext.Items.Where(i => !i.IsDeleted)
                                       .Skip(CalculatePagination(page))
                                       .Take(DefaultPageSize)
                                       .ToListAsync();
            return items;
        }

        public async Task<Item?> GetByIdAsync(string id)
        {
            var item = await DbContext.Items.Where(i => i.Id == id && !i.IsDeleted)
                                      .FirstOrDefaultAsync();
            return item;
        }
    }
}