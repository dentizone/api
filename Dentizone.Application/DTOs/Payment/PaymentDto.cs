using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.Payment;

public class PaymentDto
{
    public required string OrderId { get; set; }
    public required string BuyerId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}