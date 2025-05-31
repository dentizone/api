using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces;

interface IShipInfoRepository : IBaseRepo<ShipInfo>
{
    Task<ShipInfo> Update(ShipInfo entity);
}