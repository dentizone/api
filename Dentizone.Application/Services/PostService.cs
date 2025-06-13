using AutoMapper;
using Dentizone.Application.DTOs.PostDTO;
using Dentizone.Application.Interfaces.Post;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;

namespace Dentizone.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _repo;

        public PostService(IMapper mapper, IPostRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<PostViewDto> CreatePost(CreatePostDto createPostDto, string userId)
        {
            var post = _mapper.Map<Post>(createPostDto);
            post.SellerId = userId;
            post.Slug = $"{post.Title.ToLower().Replace(" ", "-")}-{Guid.NewGuid().ToString()[..5]}";

            var createdPost = await _repo.CreateAsync(post);
            return _mapper.Map<PostViewDto>(createdPost);
        }

        public async Task<PostViewDto> DeletePost(string postId)
        {
            var deletedPost = await _repo.DeleteAsync(postId);
            if (deletedPost == null)
            {
                throw new NotFoundException("Post not found");
            }

            return _mapper.Map<PostViewDto>(deletedPost);
        }

        public async Task<List<PostViewDto>> GetAllPosts(int page)
        {
            var posts = await _repo.GetAllAsync(page, p => !p.IsDeleted, p => p.CreatedAt);
            if (!posts.Any())
            {
                throw new NotFoundException("No posts found");
            }

            return _mapper.Map<List<PostViewDto>>(posts);
        }

        public async Task<PostViewDto> GetPostById(string postId)
        {
            var post = await _repo.GetByIdAsync(postId);
            if (post == null)
            {
                throw new NotFoundException("Post not found");
            }

            return _mapper.Map<PostViewDto>(post);
        }

        public async Task<List<PostViewDto>> GetPostsBySellerId(string sellerId, int page)
        {
            var post = await _repo.GetAllAsync(page, p => !p.IsDeleted && p.SellerId == sellerId, p => p.CreatedAt);
            if (!post.Any())
            {
                throw new NotFoundException("No posts found for this seller");
            }

            return _mapper.Map<List<PostViewDto>>(post);
        }

        public async Task<PostViewDto> UpdatePost(string postId, UpdatePostDto updatePostDto)
        {
            var post = _mapper.Map<Domain.Entity.Post>(updatePostDto);
            post.Id = postId;
            var updatedPost = await _repo.UpdateAsync(post);

            if (updatedPost == null)
            {
                throw new NotFoundException("Post not found");
            }

            return _mapper.Map<PostViewDto>(updatedPost);
        }
    }
}