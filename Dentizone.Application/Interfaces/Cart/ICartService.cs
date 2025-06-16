using Dentizone.Application.DTOs.Cart;

namespace Dentizone.Application.Interfaces.Cart
{
    public interface ICartService
    {
        Task<IEnumerable<CartItemDto>> GetCartByUserIdAsync(string userId);
        Task<Domain.Entity.Cart> AddToCartAsync(string userId, string postId);
        Task RemoveFromCartAsync(string userId, string cartId);
        Task ClearCartAsync(string userId);
    }
}