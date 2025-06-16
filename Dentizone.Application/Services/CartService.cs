using AutoMapper;
using Dentizone.Application.DTOs.Cart;
using Dentizone.Application.Interfaces.Cart;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;

namespace Dentizone.Application.Services
{
    public class CartService(
        IMapper map,
        IPostRepository postRepository,
        ICartRepository cartRepository)
        : ICartService
    {
        public async Task ClearCartAsync(string userId)
        {
            var cartItems = await cartRepository.FindAllBy(c => c.UserId == userId && !c.IsDeleted);
            foreach (var cartItem in cartItems)
            {
                await cartRepository.DeleteAsync(cartItem.Id);
            }
        }

        public async Task RemoveFromCartAsync(string userId, string cartId)
        {
            var cartItem = await cartRepository.GetByIdAsync(cartId);
            if (cartItem != null && cartItem.UserId == userId)
            {
                await cartRepository.DeleteAsync(cartId);
            }
        }

        public async Task<Cart> AddToCartAsync(string userId, string postId)
        {
            var post = await postRepository.GetByIdAsync(postId);
            var existingCart =
                await cartRepository.FindBy(c => c.UserId == userId && c.PostId == postId && !c.IsDeleted);

            if (post is { Status: PostStatus.Active } && existingCart == null)
            {
                var cart = new Cart
                {
                    UserId = userId,
                    PostId = postId,
                };

                await cartRepository.CreateAsync(cart);
                return cart;
            }

            throw new BadActionException("This item is already on the cart")
                ;
        }

        public async Task<IEnumerable<CartItemDto>> GetCartByUserIdAsync(string userId)
        {
            var cartItems = await cartRepository.GetCartItemsByUserId(userId);


            return map.Map<IEnumerable<CartItemDto>>(cartItems);
        }
    }
}