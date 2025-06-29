using Dentizone.Application.DTOs.Favorites;
using Dentizone.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "IsPartilyVerified")]
    public class FavoritesController(IFavoritesService favoritesService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var favorites = await favoritesService.GetUserFavoritesAsync(userId);
            return Ok(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorites([FromBody] FavoriteDto dto)
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            await favoritesService.AddToFavoritesAsync(userId, dto.PostId);
            return Ok();
        }

        [HttpDelete("{favoriteId}")]
        public async Task<IActionResult> RemoveFromFavorites(string favoriteId)
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            await favoritesService.RemoveFromFavoritesAsync(userId, favoriteId);
            return NoContent();
        }
    }
}