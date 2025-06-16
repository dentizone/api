namespace Dentizone.Application.DTOs.Order;

public class OrderItemDto
{
    public string Id { get; set; }
    public string PostId { get; set; }
    public string PostTitle { get; set; }
    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; }
}