namespace Dentizone.Domain.Interfaces.Repositories
{
    public interface IAssetRepository : IBaseRepo<Entity.Asset>
    {
        Task<Entity.Asset> UpdateAsync(Entity.Asset entity);
        Task<Entity.Asset?> DeleteAsync(string id);
    }
}