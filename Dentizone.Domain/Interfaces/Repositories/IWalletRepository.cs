using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories
{
    public interface IWalletRepository : IBaseRepo<Wallet>
    {
        Task<Wallet> UpdateAsync(Wallet entity);
    }
}