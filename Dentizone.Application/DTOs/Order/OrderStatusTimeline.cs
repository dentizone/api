using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.Order;

public class OrderStatusTimeline
{
    public OrderStatues Status { get; set; }
    public DateTime Timestamp { get; set; }
}