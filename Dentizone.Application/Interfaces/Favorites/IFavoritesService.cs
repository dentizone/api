using Dentizone.Application.DTOs.Favorites;

namespace Dentizone.Application.Interfaces.Favorites
{
    public interface IFavoritesService
    {
        Task<FavoriteDto> AddToFavoritesAsync(string userId, string postId);
        Task RemoveFromFavoritesAsync(string userId, string favoriteId);
        Task<IEnumerable<FavoriteViewDto>> GetUserFavoritesAsync(string userId);
    }
}