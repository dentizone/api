using Dentizone.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;
        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }
        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            try
            {
                var url = await _uploadService.UploadImageAsync(file, file.FileName);
                return Ok(new { Url = url });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("get-asset-by-id/{id}")]
        public async Task<IActionResult> GetAssetById(string id)
        {
            try
            {
                var exists = await _uploadService.getAssetById(id);
                if (!exists)
                {
                    return NotFound($"Asset with id {id} not found.");
                }
                return Ok(new { Message = "Asset exists." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
