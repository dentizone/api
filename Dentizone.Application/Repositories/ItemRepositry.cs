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
using Microsoft.Extensions.DependencyInjection;

namespace Dentizone.Application.Repositories
{
    internal class ItemRepositry : AbstractRepository, IItemRepository
    {
        private AppDbContext DbContext { set; get; }

        public ItemRepositry(AppDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<Item> CreateAsync(Item entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsDeleted = false;
            await DbContext.Items.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Item> DeleteAsync(int id)
        {
            var deleted_item = DbContext.Items.FirstOrDefault(x => x.Id == id.ToString() && !x.IsDeleted);
            if (deleted_item == null)
            {
                throw new Exception("Item not found or already deleted.");
            }

            deleted_item.IsDeleted = true;
            deleted_item.UpdatedAt = DateTime.UtcNow;
            DbContext.Items.Update(deleted_item);
            await DbContext.SaveChangesAsync();
            return deleted_item;
        }

        public async Task<IEnumerable<Item>> GetAllAsync(int page = 1)
        {
            var items = await DbContext.Set<Item>().Where(i => !i.IsDeleted).ToListAsync();
            return items;
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            var items = await DbContext.Set<Item>().Where(i => i.Id == id.ToString() && !i.IsDeleted)
                                       .FirstOrDefaultAsync();
            return items;
        }
    }
}