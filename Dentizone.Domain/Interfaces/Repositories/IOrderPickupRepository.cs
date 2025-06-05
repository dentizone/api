using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface IOrderPickupRepository : IBaseRepo<OrderPickup>
{
    Task<OrderPickup> UpdateAsync(OrderPickup entity);
    Task<OrderPickup?> DeleteAsync(string id);
}