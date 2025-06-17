using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories;

public class FavouriteRepository(AppDbContext dbContext) : AbstractRepository(dbContext), IFavouriteRepository
{
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

    public async Task<IEnumerable<Favourite>> FindAllByAsync(Expression<Func<Favourite, bool>> condition)
    {
        IQueryable<Favourite> query = dbContext.Favourites;

        query = query.Include(f => f.Post)
                     .ThenInclude(pa => pa.PostAssets)
                     .ThenInclude(pa => pa.Asset)
                     .AsNoTracking()
                     .AsSplitQuery();
        query = query.Include(f => f.Post.Seller);

        return await query.Where(condition).ToListAsync();
    }
}