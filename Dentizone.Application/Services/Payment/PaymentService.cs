using AutoMapper;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;
using Dentizone.Infrastructure;

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


    public class PaymentService(
        IMapper mapper,
        IPaymentRepository repo,
        ISaleTransactionRepository salesRepo,
        IWalletService walletService,
        AppDbContext db)
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

        public async Task<PaymentView> ConfirmPaymentAsync(string orderId)
        {
            await using var databaseTransaction = await db.Database.BeginTransactionAsync();

            try
            {
                // 1. Find The Payment // 2. Get Sales Transaction by PaymentId
                var payment =
                    await
                        repo.FindBy(p => p.OrderId == orderId && p.Status == PaymentStatus.Pending,
                            includes: [p => p.SalesTransactions]);
                // 3. Update Payment Status to Confirmed
                if (payment == null)
                {
                    throw new NotFoundException("Payment not found.");
                }

                payment.Status = PaymentStatus.Success;
                // 4. Update Sales Transaction Status to Completed
                foreach (var transaction in payment.SalesTransactions)
                {
                    if (transaction.Status != SaleStatus.Pending)
                    {
                        throw new
                            InvalidOperationException(
                                $"Sale transaction of id {transaction.Id} is not in pending status.");
                    }

                    transaction.Status = SaleStatus.Completed;

                    await salesRepo.UpdateAsync(transaction);

                    // 5. Transfer Amount to Wallet of the seller
                    var amount = transaction.Amount;
                    await walletService.AddToBalance(amount, transaction.WalletId);
                }

                var updatedPayment = await repo.UpdateAsync(payment);
                if (updatedPayment == null)
                {
                    throw new InvalidOperationException("Failed to confirm payment.");
                }

                // Commit the transaction
                await databaseTransaction.CommitAsync();

                return mapper.Map<PaymentView>(updatedPayment);
            }
            catch (Exception)
            {
                // Rollback the transaction in case of an error
                await databaseTransaction.RollbackAsync();

                throw;
            }
        }

        public async Task CancelPaymentByOrderId(string orderId)
        {
            await using var databaseTransaction = await db.Database.BeginTransactionAsync();
            try
            {
                // Find the payment by order ID
                var payment = await repo.FindBy(p => p.OrderId == orderId && p.Status == PaymentStatus.Pending,
                    includes: [p => p.SalesTransactions]);
                if (payment == null)
                {
                    throw new NotFoundException("Payment not found.");
                }

                // Update the payment status to Cancelled
                payment.Status = PaymentStatus.Cancelled;
                var updatedPayment = await repo.UpdateAsync(payment);
                if (updatedPayment == null)
                {
                    throw new InvalidOperationException("Failed to cancel payment.");
                }

                // Update the sales transactions associated with the payment
                foreach (var transaction in updatedPayment.SalesTransactions)
                {
                    if (transaction.Status != SaleStatus.Pending)
                    {
                        throw new
                            InvalidOperationException(
                                $"Sale transaction of id {transaction.Id} is not in pending status.");
                    }

                    transaction.Status = SaleStatus.Failed;
                    await salesRepo.UpdateAsync(transaction);
                }


                // Commit the transaction
                await databaseTransaction.CommitAsync();
            }
            catch (Exception)
            {
                // Rollback the transaction in case of an error
                await databaseTransaction.RollbackAsync();
                throw;
            }
        }
    }

    public interface IPaymentService
    {
        Task<PaymentView> CreatePaymentAsync(PaymentDto payment);
        Task<PaymentView> GetPaymentByIdAsync(string paymentId);
        Task CreateSaleTransaction(string paymentId, string walletId, decimal amount);

        Task<PaymentView> ConfirmPaymentAsync(string orderId);

        Task CancelPaymentByOrderId(string orderId);
    }
}