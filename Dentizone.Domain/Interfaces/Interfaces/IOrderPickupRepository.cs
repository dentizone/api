using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces;

interface IOrderPickupRepository : IBaseRepo<OrderPickup>
{
    Task<OrderPickup> UpdateAsync(OrderPickup entity);
    Task<OrderPickup?> DeleteAsync(string id);
}