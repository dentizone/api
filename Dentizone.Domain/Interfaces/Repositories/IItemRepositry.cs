using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories
{
    public interface IItemRepository : IBaseRepo<Item>
    {
        Task<Item?> DeleteAsync(string id);
    }
}