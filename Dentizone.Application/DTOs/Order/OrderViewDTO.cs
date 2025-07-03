namespace Dentizone.Application.DTOs.Order
{
    public class OrderViewDto
    {
        public string Id { get; set; } = string.Empty;
        public string BuyerName { get; set; } = string.Empty;
        public int TotalAmount { get; set; }

        public OrderShipInfoDto OrderShipmentAddress { get; set; } = new();

        public DateTime CreatedAt { get; set; }
        public IReadOnlyCollection<OrderStatusTimeline> StatusTimeline { get; set; } = new List<OrderStatusTimeline>();
        public List<OrderItemDto> OrderItems { get; set; } = new();

        public bool IsReviewed { get; set; } = false;
    }

    public class SellerInfo
    {
        public string SellerId { get; set; } = string.Empty;
        public string SellerName { get; set; } = string.Empty;
        public string SellerEmail { get; set; } = string.Empty;
    }
}