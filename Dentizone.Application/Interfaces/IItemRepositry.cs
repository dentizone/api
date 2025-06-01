using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces
{
    internal interface IItemRepository : IBaseRepo<Item>
    {
        Task<Item?> DeleteAsync(string id);
    }
}