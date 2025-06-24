using AutoMapper;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;

namespace Dentizone.Application.Services.Payment
{
    public class PaymentDto
    {
        public string OrderId { get; set; }
        public string BuyerId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }

    public class PaymentView
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string BuyerId { get; set; }
        public string BuyerName { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }


    public class PaymentService(IMapper mapper, IPaymentRepository repo, ISaleTransactionRepository salesRepo)
        : IPaymentService
    {
        public async Task<PaymentView> CreatePaymentAsync(PaymentDto payment)
        {
            var paymentEntity = new Domain.Entity.Payment
            {
                OrderId = payment.OrderId,
                BuyerId = payment.BuyerId,
                Amount = payment.Amount,
                Status = PaymentStatus.Pending,
                Method = PaymentMethod.COD
            };

            var createdPayment = await repo.CreateAsync(paymentEntity);
            if (createdPayment == null)
            {
                throw new InvalidOperationException("Failed to create payment.");
            }

            var paymentView = mapper.Map<PaymentView>(createdPayment);

            return paymentView;
        }

        public async Task<PaymentView> GetPaymentByIdAsync(string paymentId)
        {
            var payment = await repo.FindBy(p => p.Id == paymentId);
            if (payment == null)
            {
                throw new NotFoundException("Payment not found.");
            }

            return mapper.Map<PaymentView>(payment);
        }

        public async Task CreateSaleTransaction(string paymentId, string walletId, decimal amount)
        {
            var saleTransaction = new SalesTransaction
            {
                PaymentId = paymentId,
                WalletId = walletId,
                Amount = amount,
                Status = SaleStatus.Pending
            };
            var createdTransaction = await salesRepo.CreateAsync(saleTransaction);
            if (createdTransaction == null)
            {
                throw new BadActionException("Failed to create sale transaction.");
            }
        }
    }

    public interface IPaymentService
    {
        Task<PaymentView> CreatePaymentAsync(PaymentDto payment);
        Task<PaymentView> GetPaymentByIdAsync(string paymentId);
        Task CreateSaleTransaction(string paymentId, string walletId, decimal amount);
    }
}