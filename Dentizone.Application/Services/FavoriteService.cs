using AutoMapper;
using Dentizone.Application.DTOs.Favorites;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;

namespace Dentizone.Application.Services
{
    public class FavoriteService(IFavouriteRepository favouriteRepository, IMapper mapper) : IFavoritesService
    {
        public async Task<FavoriteDto> AddToFavoritesAsync(string userId, string postId)
        {
            var existingFavorite =
                await favouriteRepository.FindBy(f => f.UserId == userId && f.PostId == postId && !f.IsDeleted);
            if (existingFavorite != null)
            {
                throw new BadActionException("This post is already in your favorites.");
            }

            var favorite = new Favourite
            {
                UserId = userId,
                PostId = postId,
            };
            await favouriteRepository.CreateAsync(favorite);
            return mapper.Map<FavoriteDto>(favorite);
        }

        public async Task<IEnumerable<FavoriteViewDto>> GetUserFavoritesAsync(string userId)
        {
            var favorites =
                await favouriteRepository.FindAllByAsync(f => f.UserId == userId && !f.IsDeleted
                                                        );

            return !favorites.Any() ? [] : mapper.Map<IEnumerable<FavoriteViewDto>>(favorites);
        }

        public async Task RemoveFromFavoritesAsync(string userId, string favoriteId)
        {
            var favorite = await favouriteRepository.FindBy(f => f.UserId == userId && f.Id == favoriteId);
            if (favorite == null || favorite.UserId != userId)
            {
                throw new UnauthorizedAccessException("Favorite not found or does not belong to the user.");
            }

            await favouriteRepository.DeleteAsync(favoriteId);
        }
    }
}