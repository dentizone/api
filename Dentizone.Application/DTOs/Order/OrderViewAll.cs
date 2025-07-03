namespace Dentizone.Application.DTOs.Order;

public class OrderViewAll : OrderViewDto
{
    public string BuyerId { get; set; } = string.Empty;

    public ICollection<SellerInfo> Sellers { get; set; } = new List<SellerInfo>();
}