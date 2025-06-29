namespace Dentizone.Application.DTOs.Cart
{
    public class CartItemDto
    {
        public required string Id { get; set; }
        public required string PostId { get; set; }
        public required string Title { get; set; }
        public decimal Price { get; set; }
        public required string Url { get; set; }
    }
}