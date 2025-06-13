using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dentizone.Application.DTOs.PostDTO;
using Dentizone.Application.Interfaces.Post;
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
        public async Task<PostViewDTO> CreatePost(CreatePostDTO createPostDTO)
        {
           var post = _mapper.Map<Domain.Entity.Post>(createPostDTO);
           var createdPost = await _repo.CreateAsync(post);
           return _mapper.Map<PostViewDTO>(createdPost);
        }

        public async Task<PostViewDTO> DeletePost(string postId)
        {
            var deletedPost = await _repo.DeleteAsync(postId);
            if (deletedPost == null)
            {
                throw new NotFoundException("Post not found");
            }
            return _mapper.Map<PostViewDTO>(deletedPost);
        }

        public async Task<List<PostViewDTO>> GetAllPosts(int page)
        {
            var posts = await _repo.GetAllAsync(page, p => !p.IsDeleted, p => p.CreatedAt);
            if (!posts.Any())
            {
                throw new NotFoundException("No posts found");
            }
            return _mapper.Map<List<PostViewDTO>>(posts);
        } 

        public async Task<PostViewDTO> GetPostById(string postId)
        {
            var post = await _repo.GetByIdAsync(postId);
            if (post == null)
            {
                throw new NotFoundException("Post not found");
            }
            return _mapper.Map<PostViewDTO>(post);
        }

        public async Task<List<PostViewDTO>> GetPostsBySellerId(string sellerId, int page)
        {
            var post = await _repo.GetAllAsync(page, p => !p.IsDeleted && p.SellerId == sellerId, p => p.CreatedAt);
            if (!post.Any())
            {
                throw new NotFoundException("No posts found for this seller");
            }
            return _mapper.Map<List<PostViewDTO>>(post);
        }

        public async Task<PostViewDTO> UpdatePost(string postId, UpdatePostDTO updatePostDTO)
        {
            var post = _mapper.Map<Domain.Entity.Post>(updatePostDTO);
            post.Id = postId;
            var updatedPost = await _repo.UpdateAsync(post);

            if (updatedPost == null)
            {
                throw new NotFoundException("Post not found");
            }
            return _mapper.Map<PostViewDTO>(updatedPost);
        }
    }
}
