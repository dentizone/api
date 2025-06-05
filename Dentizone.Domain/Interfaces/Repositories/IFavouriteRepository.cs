using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface IFavouriteRepository : IBaseRepo<Favourite>
{
    Task<Favourite?> DeleteAsync(string id);
}