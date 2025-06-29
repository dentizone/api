using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.Payment;

public class PaymentView
{
    public required string Id { get; set; }
    public required string OrderId { get; set; }
    public required string BuyerId { get; set; }
    public required string BuyerName { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}