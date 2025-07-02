using AutoMapper;
using Dentizone.Application.DTOs.Post;
using Dentizone.Application.DTOs.Post.PostFilterDto;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces;
using Dentizone.Domain.Interfaces.Repositories;
using Dentizone.Infrastructure;
using Dentizone.Infrastructure.Cache;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Dentizone.Application.Services
{
    public class PostService(
        IHttpContextAccessor accessor,
        IMapper mapper,
        IPostRepository repo,
        IPostAssetRepository postAssetRepository,
        ICategoryRepository categoryRepository,
        ISubCategoryRepository subCategoryRepository,
        IAssetService assetService,
        AppDbContext dbContext,
        IRedisService redisService)
        : BaseService(accessor), IPostService
    {
        public async Task<List<Post>> ValidatePosts(List<string> postIds)
        {
            var posts = await repo.ValidatePostsByState(postIds, PostStatus.Active);

            if (posts.Count() != postIds.Count)
            {
                throw new BadActionException("Some of theses posts are not available ");
            }

            if (!posts.Any())
            {
                throw new NotFoundException("No posts found");
            }

            return posts.ToList();
        }

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


        private async Task ValidateCategoryAndSubCategory(string categoryId, string subCategoryId)
        {
            if (!string.IsNullOrEmpty(categoryId))
            {
                var category = await categoryRepository.GetByIdAsync(categoryId);
                if (category == null)
                    throw new NotFoundException($"Category with ID {categoryId} not found");
            }

            if (!string.IsNullOrEmpty(subCategoryId))
            {
                var subCategory = await subCategoryRepository.GetByIdAsync(subCategoryId);
                if (subCategory == null)
                    throw new NotFoundException($"SubCategory with ID {subCategoryId} not found");
            }
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
                post.Status = PostStatus.Active; // ALERT: FOR DEVELOPMENT PURPOSES, SETTING STATUS TO ACTIVE
                post.Slug = $"{post.Title.ToLower().Replace(" ", "-")}-{Guid.NewGuid().ToString()[..5]}";
                await repo.CreateAsync(post);

                if (createPostDto.AssetIds.Any())
                {
                    foreach (var assetId in createPostDto.AssetIds)
                    {
                        await AssociatePostWithAsset(post.Id, assetId);
                    }
                }

                await ValidateCategoryAndSubCategory(post.CategoryId, post.SubCategoryId);


                await repo.UpdateAsync(post);
                await transaction.CommitAsync();

                // Invalidate The Cache
                var cacheKey = CacheHelper.GenerateCacheKey("SidebarFilter");
                await redisService.InvalidateCache(cacheKey);

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

        public async Task<List<PostViewDto>> GetAllPosts(string userId)
        {
            var query = repo.GetAllAsync(p => !p.IsDeleted && p.SellerId == userId, p => p.CreatedAt, includes:
            [
                p => p.Category,
                p => p.SubCategory
            ]);
            query = query.Include(p => p.PostAssets)
                         .ThenInclude(pa => pa.Asset);

            query = query.Include(p => p.Seller)
                         .ThenInclude(s => s.University);
            query = query.AsSplitQuery().AsNoTracking();
            var posts = await query.ToListAsync();

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
            await AuthorizeAdminOrOwnerAsync(postId);

            var post = mapper.Map(updatePostDto, existingPost);


            var updatedPost = await repo.UpdateAsync(post);

            if (updatedPost == null)
            {
                throw new NotFoundException("Post not found");
            }

            return mapper.Map<PostViewDto>(updatedPost);
        }

        public async Task<PostViewDto> UpdatePostStatus(string postId, PostStatus status)
        {
            var post = await repo.GetByIdAsync(postId);
            if (post == null)
            {
                throw new NotFoundException("Post not found");
            }

            post.Status = status;
            var updatedPost = await repo.UpdateAsync(post);
            return mapper.Map<PostViewDto>(updatedPost);
        }

        public async Task<SidebarFilterDto> GetSidebarFilterAsync()
        {
            var cacheKey = CacheHelper.GenerateCacheKey("SidebarFilter");

            var cachedValue = await redisService.GetValue(cacheKey);
            if (!string.IsNullOrEmpty(cachedValue))
            {
                var deserializedValue = JsonConvert.DeserializeObject<SidebarFilterDto>(cachedValue);
                if (deserializedValue != null)
                {
                    return deserializedValue;
                }
            }

            var availablePosts = repo.GetAllAsync(p => !p.IsDeleted && p.Status == PostStatus.Active,
                                                  p => p.CreatedAt, includes:
                                                  [
                                                      p => p.Category,
                                                      p => p.SubCategory,
                                                  ]);

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
                             .Select(g => new CategoryFilterDto
                             {
                                 Id = g.First().Category.Id,
                                 CategoryName = g.Key,
                                 Subcategories = g.Select(p => p.SubCategory.Name)
                                                               .Distinct()
                                                               .OrderBy(s => s).ToList()
                             })
                             .OrderBy(c => c.CategoryName)
                             .ToList();

            var sidebarFilterResults = new SidebarFilterDto
            {
                Cities = cities,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Categories = categories
            };


            // if the sidebarFilterResults is null, we will not cache it
            if (sidebarFilterResults.Cities.Count == 0 && sidebarFilterResults.Categories.Count == 0)
            {
                return sidebarFilterResults;
            }

            var valueAsJson = JsonConvert.SerializeObject(sidebarFilterResults);
            await redisService.SetValue(cacheKey, valueAsJson, TimeSpan.FromHours(6));

            return sidebarFilterResults;
        }

        public async Task<List<PostViewDto>> Search(UserPreferenceDto userPreferenceDto)
        {
            var cacheKey = CacheHelper.GenerateCacheKeyHash("SearchPosts",
                                                            userPreferenceDto);
            var cachedValue = await redisService.GetValue(cacheKey);
            if (!string.IsNullOrEmpty(cachedValue))
            {
                var deserializedValue = JsonConvert.DeserializeObject<List<PostViewDto>>(cachedValue);
                if (deserializedValue != null)
                {
                    return deserializedValue;
                }
            }

            var postsQuery = await repo.SearchAsync(
                                                    userPreferenceDto.Keyword, userPreferenceDto.City,
                                                    userPreferenceDto.Category, userPreferenceDto.SubCategory,
                                                    userPreferenceDto.Condition, userPreferenceDto.MinPrice,
                                                    userPreferenceDto.MaxPrice,
                                                    userPreferenceDto.SortBy, userPreferenceDto.SortDirection,
                                                    userPreferenceDto.PageNumber
                                                   );

            var postsWithIncludes = await postsQuery
                                          .Include(p => p.PostAssets).ThenInclude(pa => pa.Asset)
                                          .Include(p => p.Seller)
                                          .ThenInclude(p => p.University)
                                          .Include(p => p.Category)
                                          .Include(p => p.SubCategory)
                                          .ToListAsync();

            var mappedPosts = mapper.Map<List<PostViewDto>>(postsWithIncludes);


            await redisService.SetValue(cacheKey, JsonConvert.SerializeObject(mappedPosts), TimeSpan.FromMinutes(1));
            return mappedPosts;
        }

        protected override async Task<string> GetOwnerIdAsync(string resourceId)
        {
            var post = await repo.GetByIdAsync(resourceId);

            if (post == null)
            {
                throw new NotFoundException($"Post with id {resourceId} not found");
            }

            return post.SellerId;
        }
    }
}