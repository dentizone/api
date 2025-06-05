using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces
{
    public interface IAssetRepository : IBaseRepo<Asset>
    {
        Task<Asset> UpdateAsync(Asset entity);
        Task<Asset?> DeleteAsync(string id);
    }
}