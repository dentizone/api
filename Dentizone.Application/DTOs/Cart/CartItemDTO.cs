namespace Dentizone.Application.DTOs.Cart
{
    public class CartItemDto
    {
        public string Id { get; set; }
        public string PostId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Url { get; set; }
    }
}