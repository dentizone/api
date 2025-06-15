using System.Linq.Expressions;
using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface ICartRepository : IBaseRepo<Cart>
{
    Task<Cart?> DeleteAsync(string id);
    Task<IEnumerable<Cart>> FindAllBy(
    Expression<Func<Cart, bool>> condition,
    Expression<Func<Cart, object>>[]? includes = null);

}