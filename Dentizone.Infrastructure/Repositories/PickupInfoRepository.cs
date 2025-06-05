using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    internal class PickupInfoRepository : AbstractRepository, IPickupInfoRepository
    {
        public PickupInfoRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PickupInfo?> GetByIdAsync(string id)
        {
            return await dbContext.PickupInfos
                                  .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task<PickupInfo> CreateAsync(PickupInfo entity)
        {
            await dbContext.PickupInfos.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<PickupInfo?> FindBy(Expression<Func<PickupInfo, bool>> condition,
                                              Expression<Func<PickupInfo, object>>[]? includes)
        {
            IQueryable<PickupInfo> query = dbContext.PickupInfos;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
        }


        public async Task<PickupInfo> Update(PickupInfo entity)
        {
            dbContext.PickupInfos.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}