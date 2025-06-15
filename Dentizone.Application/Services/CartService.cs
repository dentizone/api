using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dentizone.Application.DTOs.Cart;
using Dentizone.Application.Interfaces.Cart;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces.Repositories;
using StackExchange.Redis;

namespace Dentizone.Application.Services
{
    public class CartService : ICartService
    {
        private readonly IMapper _mapper;
        private readonly IPostAssetRepository postAssetRepository;
        private readonly IPostRepository postRepository;
        private readonly ICartRepository cartRepository;
        public CartService(IMapper _map, IPostRepository postRepository,IPostAssetRepository postAssetRepository, ICartRepository cartRepository)
        {
            _mapper = _map;
            this.postRepository = postRepository;
            this.postAssetRepository = postAssetRepository;
            this.cartRepository = cartRepository;
        }
        public async Task ClearCartAsync(string userId)
        {
            var cartItems = await cartRepository.FindAllBy(c => c.UserId == userId && !c.IsDeleted);
            foreach (var cartItem in cartItems)
            {
                cartRepository.DeleteAsync(cartItem.Id);
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

        public async Task<CartItemDTO> AddToCartAsync(string userId, string postId)
        {
            var post= await postRepository.GetByIdAsync(postId);
            var existingCart = await cartRepository.FindBy(c => c.UserId == userId && c.PostId == postId && !c.IsDeleted);
            if(post != null && post.Status==PostStatus.Active && existingCart==null)
            {
                var cart = new Cart
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    PostId = postId,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                };
                
                var createdCart = await cartRepository.CreateAsync(cart);
                var cartItemDto = _mapper.Map<CartItemDTO>(createdCart);
                cartItemDto.Title = post.Title;
                cartItemDto.Price = post.Price;
                cartItemDto.Url = post.PostAssets
                .Where(pa => !pa.IsDeleted && pa.Asset != null && !pa.Asset.IsDeleted)
                .Select(pa => pa.Asset.Url)
                .FirstOrDefault() ?? string.Empty; 

                return cartItemDto;
            }
            throw new InvalidOperationException("Unable to add item to cart.");

        }
        
        public async Task<IEnumerable<CartItemDTO>> GetCartByUserIdAsync(string userId)
        {
            var cartItems = await cartRepository.FindAllBy(c => c.UserId == userId && !c.IsDeleted);
            var filteredCartItems=new List<Cart>();

            foreach (var cartItem in cartItems)
            {
                var post= await postRepository.GetByIdAsync(cartItem.PostId);
                if(post.Status==PostStatus.Active)
                {
                    filteredCartItems.Add(cartItem);

                }

            }
            return _mapper.Map<IEnumerable<CartItemDTO>>(filteredCartItems);
        }
    }
}
