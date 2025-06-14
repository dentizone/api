using AutoMapper;
using Dentizone.Application.DTOs.PostDTO;
using Dentizone.Application.DTOs.PostFilterDTO;
using Dentizone.Application.Interfaces.Asset;
using Dentizone.Application.Interfaces.Post;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces;
using Dentizone.Domain.Interfaces.Repositories;
using Dentizone.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Dentizone.Application.Services
{
    public class PostService(
        IMapper mapper,
        IPostRepository repo,
        IPostAssetRepository postAssetRepository,
        IAssetService assetService,
        AppDbContext dbContext,
        IRedisService redisService)
        : IPostService
    {
        private async Task ValidateAssetNotUsed(string assetId, string? postIdToExclude = null)
        {
            var isExist = await postAssetRepository.FindBy(p =>
                                                               !p.IsDeleted &&
                                                               p.AssetId == assetId &&
                                                               (postIdToExclude == null || p.PostId != postIdToExclude)
                                                          );

            if (isExist != null)
                throw new BadActionException("This photo is already used before");
        }

        private async Task<PostAsset> AssociatePostWithAsset(string postId, string assetId,
                                                             string? postIdToExclude = null)
        {
            var asset = await assetService.GetAssetByIdAsync(assetId);
            if (asset == null)
                throw new NotFoundException($"Asset with ID {assetId} not found");

            await ValidateAssetNotUsed(assetId, postIdToExclude);

            var postAsset = new PostAsset
            {
                PostId = postId,
                AssetId = assetId
            };

            await postAssetRepository.CreateAsync(postAsset);
            return postAsset;
        }

        public async Task<PostViewDto> CreatePost(CreatePostDto createPostDto, string userId)
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                var post = mapper.Map<Post>(createPostDto);
                post.SellerId = userId;
                post.Slug = $"{post.Title.ToLower().Replace(" ", "-")}-{Guid.NewGuid().ToString()[..5]}";
                await repo.CreateAsync(post);

                if (createPostDto.AssetIds.Any())
                {
                    foreach (var assetId in createPostDto.AssetIds)
                    {
                        var postAsset = await AssociatePostWithAsset(post.Id, assetId);

                        post.PostAssets.Add(postAsset);
                    }
                }
                else
                {
                    throw new BadHttpRequestException("I Don't know how you reached there, but i handled it :D");
                }

                await repo.UpdateAsync(post);
                await transaction.CommitAsync();

                return mapper.Map<PostViewDto>(post);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task<PostViewDto> DeletePost(string postId)
        {
            var deletedPost = await repo.DeleteAsync(postId);
            if (deletedPost == null)
            {
                throw new NotFoundException("Post not found");
            }

            return mapper.Map<PostViewDto>(deletedPost);
        }

        public async Task<List<PostViewDto>> GetAllPosts(int page)
        {
            var posts = await repo.GetAllAsync(page, p => !p.IsDeleted, p => p.CreatedAt);
            if (!posts.Any())
            {
                throw new NotFoundException("No posts found");
            }

            return mapper.Map<List<PostViewDto>>(posts);
        }

        public async Task<PostViewDto> GetPostById(string postId)
        {
            var post = await repo.GetByIdAsync(postId);
            if (post == null)
            {
                throw new NotFoundException("Post not found");
            }

            return mapper.Map<PostViewDto>(post);
        }

        public async Task<List<PostViewDto>> GetPostsBySellerId(string sellerId, int page)
        {
            var post = await repo.GetAllAsync(page, p => !p.IsDeleted && p.SellerId == sellerId, p => p.CreatedAt);
            if (!post.Any())
            {
                throw new NotFoundException("No posts found for this seller");
            }

            return mapper.Map<List<PostViewDto>>(post);
        }

        public async Task<PostViewDto> UpdatePost(string postId, UpdatePostDto updatePostDto)
        {
            var existingPost = await repo.GetByIdAsync(postId);

            var post = mapper.Map(updatePostDto, existingPost);


            // Logic to handle asset updates


            var updatedPost = await repo.UpdateAsync(post);

            if (updatedPost == null)
            {
                throw new NotFoundException("Post not found");
            }

            return mapper.Map<PostViewDto>(updatedPost);
        }

        public async Task<SidebarFilterDTO> GetSidebarFilterAsync(int page)
        {
            var cached = await redisService.GetValue("sidebar_filter");
            if (!string.IsNullOrEmpty(cached))
            {
                return JsonConvert.DeserializeObject<SidebarFilterDTO>(cached);
            }

            var availablePosts = await repo.GetAllAsync(page, p => !p.IsDeleted && p.Status == PostStatus.Active, p => p.CreatedAt);

            var cities = availablePosts
                .Select(p => p.City)
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            var prices = availablePosts
                .Select(p => p.Price)
                .ToList();

            decimal minPrice = 0;
            decimal maxPrice = 0;
            if (prices.Any())
            {
                minPrice = prices.Min();
                maxPrice = prices.Max();
            }

            var categories = availablePosts
               .GroupBy(p => p.Category.Name)  
               .Select(g => new CategoryFilterDTO
               {
                   CategoryName = g.Key,
                   Subcategories = (List<string>)g
                       .Select(p => p.SubCategory?.Name)   
                       .Distinct()
                       .OrderBy(s => s)
               })
               .OrderBy(c => c.CategoryName)
               .ToList();

            var sidebarFilterResults = new SidebarFilterDTO
            {
                Cities = cities,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Categories = categories
            };

            var json = JsonConvert.SerializeObject(sidebarFilterResults);
            await redisService.SetValue("sidebar_filter", json, TimeSpan.FromHours(6));

            return sidebarFilterResults;
        }

        public async Task<List<PostViewDto>> Search(UserPreferenceDTO userPreferenceDTO)
        {
            var cacheKey = "search_posts_" + JsonConvert.SerializeObject(userPreferenceDTO);
            var cached = await redisService.GetValue(cacheKey);
            if (!string.IsNullOrEmpty(cached))
                return JsonConvert.DeserializeObject<List<PostViewDto>>(cached);

            var postsQuery = await repo.SearchAsync(
                userPreferenceDTO.Keyword, userPreferenceDTO.City, userPreferenceDTO.Category, userPreferenceDTO.SubCategory,
                userPreferenceDTO.Condition, userPreferenceDTO.MinPrice, userPreferenceDTO.MaxPrice,
                userPreferenceDTO.SortBy, userPreferenceDTO.SortDirection, userPreferenceDTO.PageNumber
            );

            var postsWithIncludes = postsQuery
                .Include(p => p.PostAssets).ThenInclude(pa => pa.Asset)
                .Include(p => p.Seller)
                .ToListAsync();

            var mappedPosts = mapper.Map<List<PostViewDto>>(postsWithIncludes);

            await redisService.SetValue(cacheKey, JsonConvert.SerializeObject(mappedPosts), TimeSpan.FromMinutes(10));
            return mappedPosts;
        }
    }
}