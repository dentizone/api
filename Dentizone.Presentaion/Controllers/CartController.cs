using System.Security.Claims;
using Azure.Core;
using Dentizone.Application.DTOs.Cart;
using Dentizone.Application.Interfaces.Cart;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IPostRepository postRepository;
        private readonly ICartRepository cartRepository;

        public CartController(ICartService cartService, IPostRepository postRepository, ICartRepository cartRepository)
        {
            _cartService = cartService;
            this.postRepository = postRepository;
            this.cartRepository = cartRepository;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("User ID not found in token.");
            }
            var cartItems = await _cartService.GetCartByUserIdAsync(userId);
            return Ok(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> Addcart([FromBody] AddToCartDTO request)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("User ID not found in token.");
            }
            var post = await postRepository.GetByIdAsync(request.PostId);
            if (post == null || post.Status != PostStatus.Active)
            {
                return NotFound("Post not found or inactive");

            }
            var existingCart = await cartRepository.FindBy(c => c.UserId == userId && c.PostId == request.PostId && !c.IsDeleted);
            if (existingCart != null)
            {
                return BadRequest("item already in cart");
            }
            var cartItem = await _cartService.AddToCartAsync(userId, request.PostId);
            return Created(string.Empty, cartItem);


        }

        [HttpDelete("{cartId}")]
        public async Task<IActionResult> RemoveFromCart(string cartId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("User ID not found in token.");
            }
            await _cartService.RemoveFromCartAsync(userId, cartId);
            return NoContent();
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("User ID not found in token.");
            }
            await _cartService.ClearCartAsync(userId);
            return NoContent();
        }

    }
    
}
