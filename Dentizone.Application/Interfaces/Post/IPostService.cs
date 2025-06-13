using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.DTOs.PostDTO;

namespace Dentizone.Application.Interfaces.Post
{
    public interface IPostService
    {
        Task<PostViewDto> CreatePost(CreatePostDto createPostDto, string userId);
        Task<PostViewDto> GetPostById(string postId);
        Task<List<PostViewDto>> GetPostsBySellerId(string sellerId, int page);
        Task<List<PostViewDto>> GetAllPosts(int page);
        Task<PostViewDto> UpdatePost(string postId, UpdatePostDto updatePostDto);
        Task<PostViewDto> DeletePost(string postId);
    }
}