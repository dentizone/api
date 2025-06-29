using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.Payment;

public class PaymentDto
{
    public string OrderId { get; set; }
    public string BuyerId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}