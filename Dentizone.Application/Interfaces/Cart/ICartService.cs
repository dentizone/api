using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.DTOs.Cart;

namespace Dentizone.Application.Interfaces.Cart
{
    public  interface ICartService
    {
        Task<IEnumerable<CartItemDTO>> GetCartByUserIdAsync(string userId);
        Task<CartItemDTO> AddToCartAsync(string userId, string postId);
        Task RemoveFromCartAsync(string userId, string cartId);
        Task ClearCartAsync(string userId);
    }
}
