using System.Linq.Expressions;
using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface IFavouriteRepository : IBaseRepo<Favourite>
{
    Task<Favourite?> DeleteAsync(string id);
    Task<IEnumerable<Favourite>> FindAllByAsync(Expression<Func<Favourite, bool>> condition, Expression<Func<Favourite, object>>[]? includes = null)
}