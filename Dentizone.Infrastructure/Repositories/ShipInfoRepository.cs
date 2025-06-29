using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    internal class ShipInfoRepository(AppDbContext dbContext) : AbstractRepository(dbContext), IShipInfoRepository
    {
        public async Task<ShipInfo?> GetByIdAsync(string id)
        {
            return await DbContext.ShipInfos
                .FirstOrDefaultAsync(s => s.Id == id);
        }


        public async Task<ShipInfo> CreateAsync(ShipInfo entity)
        {
            await DbContext.ShipInfos.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<ShipInfo?> FindBy(Expression<Func<ShipInfo, bool>> condition,
            Expression<Func<ShipInfo, object>>[]? includes)
        {
            IQueryable<ShipInfo> query = DbContext.ShipInfos;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
        }
    }
}