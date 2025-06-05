using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface IPickupInfoRepository : IBaseRepo<PickupInfo>
{
    Task<PickupInfo> Update(PickupInfo entity);
}