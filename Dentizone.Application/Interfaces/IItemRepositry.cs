using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces
{
    public interface IItemRepository : IBaseRepo<Item>
    {
        Task<Item?> DeleteAsync(string id);
    }
}