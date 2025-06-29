using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Dentizone.Application.DTOs.Post;
using Dentizone.Application.DTOs.Post.PostFilterDto;
using Dentizone.Application.Interfaces;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController(IPostService postService) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPosts(int page = 1)
        {
            var posts = await postService.GetAllPosts(page);
            return Ok(posts);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPostById(string id)
        {
            var post = await postService.GetPostById(id);
            return Ok(post);
        }

        [HttpGet("users/{sellerId}/posts")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> GetPostsBySellerId(string sellerId, int page = 1)
        {
            var posts = await postService.GetPostsBySellerId(sellerId, page);
            return Ok(posts);
        }

        [HttpPost]
        [Authorize(Policy = "IsVerified")]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var createdPost = await postService.CreatePost(createPostDto, userId);
            return Ok(createdPost);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "IsVerified")]
        public async Task<IActionResult> DeletePost(string id)
        {
            var deletedPost = await postService.DeletePost(id);
            return Ok(deletedPost);
        }

        [Authorize(Policy = "IsVerified")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(string id, [FromBody] UpdatePostDto updatePostDto)
        {
            var updatedPost = await postService.UpdatePost(id, updatePostDto);
            return Ok(updatedPost);
        }

        [AllowAnonymous]
        [HttpGet("sidebar")]
        public async Task<IActionResult> GetSidebarFilter()
        {
            var sidebarFilter = await postService.GetSidebarFilterAsync();
            return Ok(sidebarFilter);
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] UserPreferenceDto userPreferenceDto)
        {
            var searchResult = await postService.Search(userPreferenceDto);
            return Ok(searchResult);
        }
    }
}