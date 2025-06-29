using Dentizone.Application.DTOs.Payment;

namespace Dentizone.Application.Interfaces;

public interface IPaymentService
{
    Task<PaymentView> CreatePaymentAsync(PaymentDto payment);
    Task<PaymentView> GetPaymentByIdAsync(string paymentId);
    Task CreateSaleTransaction(string paymentId, string walletId, decimal amount);

    Task<PaymentView> ConfirmPaymentAsync(string orderId);

    Task CancelPaymentByOrderId(string orderId);
}