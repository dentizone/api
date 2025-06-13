using Dentizone.Application.DTOs.PostDTO;
using Dentizone.Application.Interfaces.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPosts(int page = 1)
        {
            try
            {
                var posts = await _postService.GetAllPosts(page);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return NotFound($"Error : {ex.Message}");

            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(string id)
        {
            try
            {
                var post = await _postService.GetPostById(id);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return NotFound($"Error : {ex.Message}");
            }
        }
        [HttpGet("users/{sellerId}/posts")]
        public async Task<IActionResult> GetPostsBySellerId(string sellerId, int page = 1)
        {
            try
            {
                var posts = await _postService.GetPostsBySellerId(sellerId, page);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error : {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDTO createPostDTO)
        {
            try
            {
                var createdPost = await _postService.CreatePost(createPostDTO);
                return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Id }, createdPost);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error : {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(string id)
        {
            try
            {
                var deletedPost = await _postService.DeletePost(id);
                return Ok(deletedPost);
            }
            catch (Exception ex)
            {
                return NotFound($"Error : {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(string id, [FromBody] UpdatePostDTO updatePostDTO)
        {
            try
            {
                var updatedPost = await _postService.UpdatePost(id, updatePostDTO);
                return Ok(updatedPost);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error : {ex.Message}");
            }   
        }
    }
}
