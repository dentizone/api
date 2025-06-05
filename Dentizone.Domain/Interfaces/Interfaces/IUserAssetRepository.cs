using System.Linq.Expressions;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces;

interface IUserAssetRepository : IBaseRepo<UserAsset>
{
    Task<UserAsset?> DeleteAsync(string id);

    Task<IEnumerable<UserAsset>> GetAllByAsync(int page,
        Expression<Func<UserAsset, bool>>? condition);
}