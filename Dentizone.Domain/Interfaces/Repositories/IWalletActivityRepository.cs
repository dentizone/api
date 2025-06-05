using Dentizone.Domain.Entity;
using System.Linq.Expressions;

namespace Dentizone.Domain.Interfaces.Repositories
{
    public interface IWalletActivityRepository : IBaseRepo<WalletActivity>
    {
        Task<ICollection<WalletActivity>> GetAllBy(int page,
                                                   Expression<Func<WalletActivity, bool>>? filter);
    }
}