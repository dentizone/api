using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces
{
    internal interface IWalletRepository : IBaseRepo<Wallet>
    {
        Task<Wallet> UpdateAsync(Wallet entity);
    }
}