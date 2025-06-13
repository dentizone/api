using Dentizone.Application.Interfaces;
using Dentizone.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController(IUploadService uploadService) : ControllerBase
    {
        [HttpPost("image")]
        // [Authorize]
        public async Task<IActionResult> UploadImageAsync(IFormFile file)
        {
            var permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                throw new BadActionException("Why you upload non image man?");

            if (!file.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
                throw new BadActionException("Why you upload non image man?");


            if (file.Length > 10 * 1024 * 1024) // 10 MB limit

                throw new BadActionException("File exceeded the file limit");

            var userId =
                "000"; // "000" is a placeholder for the user ID, replace it with actual user ID retrieval logic after frontend


            var asset = await uploadService.UploadImageAsync(file, userId);


            return Ok(asset);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetById(string id)
        {
            var asset = await uploadService.FindAssetById(id);

            return Ok(asset);
        }
    }
}