using Dentizone.Domain.Entity;
using System.Linq.Expressions;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface IFavouriteRepository : IBaseRepo<Favourite>
{
    Task<Favourite?> DeleteAsync(string id);

    Task<IEnumerable<Favourite>> FindAllByAsync(Expression<Func<Favourite, bool>> condition);
}