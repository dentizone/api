using Dentizone.Domain.Entity;
using System.Linq.Expressions;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface IUserAssetRepository : IBaseRepo<UserAsset>
{
    Task<UserAsset?> DeleteAsync(string id);

    Task<IEnumerable<UserAsset>> GetAllByAsync(int page,
                                               Expression<Func<UserAsset, bool>>? condition);
}