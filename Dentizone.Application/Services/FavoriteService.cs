using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dentizone.Application.DTOs.Favorites;
using Dentizone.Application.DTOs.PostDTO;
using Dentizone.Application.Interfaces.Favorites;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;

namespace Dentizone.Application.Services
{
    public class FavoriteService : IFavoritesService
    {
        private readonly IFavouriteRepository _favoritesRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public FavoriteService(IFavouriteRepository favouriteRepository,IPostRepository postRepository, IMapper mapper)
        { 
            _favoritesRepository = favouriteRepository;
            _postRepository = postRepository;
            _mapper = mapper;
        }
        public async Task<FavoriteDto> AddToFavoritesAsync(string userId, string postId)
        {
            var existingFavorite = await _favoritesRepository.FindBy(f => f.UserId == userId && f.PostId == postId && !f.IsDeleted);
            if (existingFavorite != null)
            {
                throw new InvalidOperationException("This post is already in your favorites.");
            }
            var favorite=new Favourite
            {
                UserId = userId,
                PostId = postId,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
            await _favoritesRepository.CreateAsync(favorite);
            return _mapper.Map<FavoriteDto>(favorite);
        }

        public async Task<IEnumerable<PostViewDto>> GetUserFavoritesAsync(string userId)
        {
            var favorites = await _favoritesRepository.FindAllByAsync(f => f.UserId == userId && !f.IsDeleted);
            if (favorites == null || !favorites.Any())
            {
                return Enumerable.Empty<PostViewDto>();
            }
            var postIds = favorites.Select(f => f.PostId).ToList();
            var posts = await _postRepository.GetAllAsync(page: 1,filter: p => postIds.Contains(p.Id) && p.Status == Domain.Enums.PostStatus.Active,orderBy: null,includes: null);
            if (posts == null || !posts.Any())
            {
                return Enumerable.Empty<PostViewDto>();
            }
            return _mapper.Map<IEnumerable<PostViewDto>>(posts);
        }

        public async Task RemoveFromFavoritesAsync(string userId, string favoriteId)
        {
            var favorite = await _favoritesRepository.GetByIdAsync(favoriteId);
            if (favorite == null || favorite.UserId != userId)
            {
                throw new InvalidOperationException("Favorite not found or does not belong to the user.");
            }
            await _favoritesRepository.DeleteAsync(favoriteId);
        }
    }
}
