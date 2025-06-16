namespace Dentizone.Application.DTOs.Order
{
    public class OrderViewDto
    {
        public string Id { get; set; }
        public string BuyerName { get; set; }
        public int TotalAmount { get; set; }

        public OrderShipInfoDto OrderShipmentAddress { get; set; } = new OrderShipInfoDto();

        public DateTime CreatedAt { get; set; }
        public IReadOnlyCollection<OrderStatusTimeline> StatusTimeline { get; set; } = new List<OrderStatusTimeline>();
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}