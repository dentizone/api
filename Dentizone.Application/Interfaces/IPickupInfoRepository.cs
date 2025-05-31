using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces;

internal interface IPickupInfoRepository : IBaseRepo<PickupInfo>
{
    Task<PickupInfo> Update(PickupInfo entity);
}