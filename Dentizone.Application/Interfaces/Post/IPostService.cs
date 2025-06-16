using Dentizone.Application.DTOs.PostDTO;
using Dentizone.Application.DTOs.PostFilterDTO;
using Dentizone.Domain.Enums;

namespace Dentizone.Application.Interfaces.Post
{
    public interface IPostService
    {
        Task<PostViewDto> CreatePost(CreatePostDto createPostDto, string userId);
        Task<PostViewDto> GetPostById(string postId);
        Task<List<PostViewDto>> GetPostsBySellerId(string sellerId, int page);
        Task<List<PostViewDto>> GetAllPosts(int page);
        Task<SidebarFilterDto> GetSidebarFilterAsync();
        Task<PostViewDto> UpdatePost(string postId, UpdatePostDto updatePostDto);
        Task<PostViewDto> DeletePost(string postId);

        Task<List<PostViewDto>> Search(UserPreferenceDto userPreferenceDto);
        Task<List<Domain.Entity.Post>> ValidatePosts(List<string> postIds);
        Task<PostViewDto> UpdatePostStatus(string postId, PostStatus status);
    }
}