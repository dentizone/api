using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface ICartRepository : IBaseRepo<Cart>
{
    Task<Cart?> DeleteAsync(string id);
}