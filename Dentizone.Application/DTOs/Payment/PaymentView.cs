using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.Payment;

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