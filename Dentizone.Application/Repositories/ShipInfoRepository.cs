using System.Linq.Expressions;
using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class ShipInfoRepository : AbstractRepository, IShipInfoRepository
    {
        public ShipInfoRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ShipInfo?> GetByIdAsync(string id)
        {
            return await dbContext.ShipInfos
                .FirstOrDefaultAsync(s => s.Id == id);
        }


        public async Task<ShipInfo> CreateAsync(ShipInfo entity)
        {
            await dbContext.ShipInfos.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<ShipInfo?> FindBy(Expression<Func<ShipInfo, bool>> condition,
            Expression<Func<ShipInfo, object>>[]? includes)
        {
            IQueryable<ShipInfo> query = dbContext.ShipInfos;
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