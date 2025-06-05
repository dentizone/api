using Dentizone.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Dentizone.Domain.Interfaces.Repositories;

namespace Dentizone.Infrastructure.Repositories;

public class FavouriteRepository : AbstractRepository, IFavouriteRepository
{
    public FavouriteRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Favourite?> GetByIdAsync(string id)
    {
        return await dbContext.Favourites
                              .FirstOrDefaultAsync(f => f.Id == id && !f.IsDeleted);
    }


    public async Task<Favourite> CreateAsync(Favourite entity)
    {
        await dbContext.Favourites.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Favourite?> FindBy(Expression<Func<Favourite, bool>> condition,
                                         Expression<Func<Favourite, object>>[]? incldues)
    {
        IQueryable<Favourite> query = dbContext.Favourites;
        if (incldues != null)
        {
            foreach (var include in incldues)
            {
                query = query.Include(include);
            }
        }

        return await query.FirstOrDefaultAsync(condition);
    }

    public async Task<Favourite?> DeleteAsync(string id)
    {
        var toBeDeleted = await GetByIdAsync(id);
        if (toBeDeleted == null)
        {
            return null;
        }

        dbContext.Favourites.Remove(toBeDeleted);
        await dbContext.SaveChangesAsync();
        return toBeDeleted;
    }
}