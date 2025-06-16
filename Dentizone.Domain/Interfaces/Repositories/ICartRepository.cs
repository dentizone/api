using Dentizone.Domain.Entity;
using System.Linq.Expressions;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface ICartRepository : IBaseRepo<Cart>
{
    Task<Cart?> DeleteAsync(string id);

    Task<IEnumerable<Cart>> FindAllBy(Expression<Func<Cart, bool>> condition,
                                      Expression<Func<Cart, object>>[]? includes = null);

    Task<IEnumerable<Cart>> GetCartItemsByUserId(string userId);
}