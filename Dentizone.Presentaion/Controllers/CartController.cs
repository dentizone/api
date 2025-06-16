using Dentizone.Application.DTOs.Cart;
using Dentizone.Application.Interfaces.Cart;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController(ICartService cartService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;


            var cartItems = await cartService.GetCartByUserIdAsync(userId);
            return Ok(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddCart([FromBody] AddToCartDto request)
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            await cartService.AddToCartAsync(userId, request.PostId);
            return Created();
        }

        [HttpDelete("{cartId}")]
        public async Task<IActionResult> RemoveFromCart(string cartId)
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            await cartService.RemoveFromCartAsync(userId, cartId);
            return NoContent();
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;


            await cartService.ClearCartAsync(userId);
            return NoContent();
        }
    }
}