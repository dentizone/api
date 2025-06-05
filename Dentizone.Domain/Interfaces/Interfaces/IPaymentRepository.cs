using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces
{
    internal interface IPaymentRepository : IBaseRepo<Payment>
    {
        Task<Payment> UpdateAsync(Payment entity);
    }
}