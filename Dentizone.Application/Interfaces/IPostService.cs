using Dentizone.Application.DTOs;
using Dentizone.Application.DTOs.Post;
using Dentizone.Application.DTOs.Post.PostFilterDto;
using Dentizone.Domain.Enums;

namespace Dentizone.Application.Interfaces
{
    public interface IPostService
    {
        Task<PostViewDto> CreatePost(CreatePostDto createPostDto, string userId);
        Task<PostViewDto> GetPostById(string postId);
        Task<List<PostViewDto>> GetPostsBySellerId(string sellerId, int page);
        Task<List<PostViewDto>> GetAllPosts(string userId);
        Task<SidebarFilterDto> GetSidebarFilterAsync();
        Task<PostViewDto> UpdatePost(string postId, UpdatePostDto updatePostDto);
        Task<PostViewDto> DeletePost(string postId);
        Task<PagedResultDto<PostViewDto>> Search(UserPreferenceDto userPreferenceDto);
        Task<List<Domain.Entity.Post>> ValidatePosts(List<string> postIds);
        Task<PostViewDto> UpdatePostStatus(string postId, PostStatus status, string? reason);

        Task<PostViewDto> GetPostBySlug(string slug);
    }
}