using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories
{
    public interface IAssetRepository : IBaseRepo<Asset>
    {
        Task<Asset> UpdateAsync(Asset entity);
        Task DeleteAsync(Asset asset);
    }
}