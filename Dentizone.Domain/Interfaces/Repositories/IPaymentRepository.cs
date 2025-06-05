using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories
{
    public interface IPaymentRepository : IBaseRepo<Payment>
    {
        Task<Payment> UpdateAsync(Payment entity);
    }
}