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
        Task<PostViewDTO>CreatePost(CreatePostDTO createPostDTO);
        Task<PostViewDTO> GetPostById(string postId);
        Task<List<PostViewDTO>> GetPostsBySellerId(string sellerId);
        Task<List<PostViewDTO>> GetAllPosts(int page, int pageSize);
        Task<PostViewDTO> UpdatePost(string postId, UpdatePostDTO updatePostDTO);
        Task<PostViewDTO> DeletePost(string postId);

    }
}
