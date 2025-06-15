using Dentizone.Application.DTOs.PostDTO;
using Dentizone.Application.DTOs.PostFilterDTO;
using Dentizone.Application.Interfaces.Post;
using Dentizone.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController(IPostService postService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllPosts(int page = 1)
        {
            var posts = await postService.GetAllPosts(page);
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(string id)
        {
            var post = await postService.GetPostById(id);
            return Ok(post);
        }

        [Authorize]
        [HttpGet("users/{sellerId}/posts")]
        public async Task<IActionResult> GetPostsBySellerId(string sellerId, int page = 1)
        {
            var posts = await postService.GetPostsBySellerId(sellerId, page);
            return Ok(posts);
        }

        [HttpPost]
        [Authorize]

        //TODO: Require a Claims
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                throw new BadActionException("How in the hell you reached here!");
            }


            var createdPost = await postService.CreatePost(createPostDto, userId);
            ;
            return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Id }, createdPost);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(string id)
        {
            try
            {
                var deletedPost = await postService.DeletePost(id);
                return Ok(deletedPost);
            }
            catch (Exception ex)
            {
                return NotFound($"Error : {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(string id, [FromBody] UpdatePostDto updatePostDto)
        {
            try
            {
                var updatedPost = await postService.UpdatePost(id, updatePostDto);
                return Ok(updatedPost);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error : {ex.Message}");
            }
        }

        [HttpGet("sidebar")]
        public async Task<IActionResult> GetSidebarFilter()
        {
            var sidebarFilter = await postService.GetSidebarFilterAsync();
            return Ok(sidebarFilter);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] UserPreferenceDto userPreferenceDTO)
        {
            var searchResult = await postService.Search(userPreferenceDTO);
            return Ok(searchResult);
        }
    }
}