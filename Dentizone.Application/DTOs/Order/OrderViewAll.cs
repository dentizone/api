namespace Dentizone.Application.DTOs.Order;

public class OrderViewAll : OrderViewDto
{
    public string BuyerId { get; set; } = string.Empty;

    public ICollection<SellerInfo> Sellers { get; set; } = new List<SellerInfo>();

    public ICollection<OrderItemWithPickup> OrderItems { get; set; } = new List<OrderItemWithPickup>();
}

public class OrderItemWithPickup : OrderItemDto
{
    public string PickupLocation { get; set; } = string.Empty;
}