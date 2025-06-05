using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories
{
    public interface IPostAssetRepository : IBaseRepo<PostAsset>
    {
        Task<PostAsset> UpdateAsync(PostAsset entity);
        Task<PostAsset?> DeleteAsync(string id);
    }
}