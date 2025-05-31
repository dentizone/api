using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories;

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

    public async Task<IEnumerable<Favourite>> GetAllAsync(int page = 1)
    {
        return await dbContext.Favourites
                              .Where(f => !f.IsDeleted)
                              .Skip(CalculatePagination(page))
                              .Take(DefaultPageSize)
                              .ToListAsync();
    }

    public async Task<Favourite> CreateAsync(Favourite entity)
    {
        await dbContext.Favourites.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
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