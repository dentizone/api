using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.DTOs.Favorites;
using Dentizone.Application.DTOs.PostDTO;

namespace Dentizone.Application.Interfaces.Favorites
{
    public interface IFavoritesService
    {
        Task<FavoriteDto> AddToFavoritesAsync(string userId, string postId);
        Task RemoveFromFavoritesAsync(string userId, string favoriteId);
        Task<IEnumerable<PostViewDto>> GetUserFavoritesAsync(string userId);
    }
}
