namespace Dentizone.Application.DTOs.Order;

public class OrderItemDto
{
    public required string Id { get; set; }
    public required string PostId { get; set; }
    public required string PostTitle { get; set; }
    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; }
}