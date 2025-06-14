using AutoMapper;
using Dentizone.Application.DTOs.PostDTO;
using Dentizone.Application.Interfaces.Asset;
using Dentizone.Application.Interfaces.Post;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;
using Dentizone.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Dentizone.Application.Services
{
    public class PostService(
        IMapper mapper,
        IPostRepository repo,
        IPostAssetRepository postAssetRepository,
        ICategoryRepository categoryRepository,
        ISubCategoryRepository subCategoryRepository,
        IAssetService assetService,
        AppDbContext dbContext)
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

                await ValidateCategoryAndSubCategory(post.CategoryId, post.SubCategoryId);


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


            var updatedPost = await repo.UpdateAsync(post);

            if (updatedPost == null)
            {
                throw new NotFoundException("Post not found");
            }

            return mapper.Map<PostViewDto>(updatedPost);
        }
    }
}