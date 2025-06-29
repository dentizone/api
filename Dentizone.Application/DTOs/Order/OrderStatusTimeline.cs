namespace Dentizone.Application.DTOs.Order;

public class OrderStatusTimeline
{
    public required string Status { get; set; }
    public DateTime Timestamp { get; set; }
}