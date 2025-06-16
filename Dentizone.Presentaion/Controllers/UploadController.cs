using Dentizone.Application.Interfaces;
using Dentizone.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UploadController(IUploadService uploadService) : ControllerBase
    {
        [HttpPost("image")]
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

            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;


            var asset = await uploadService.UploadImageAsync(file, userId);


            return Ok(asset);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetById(string id)
        {
            var asset = await uploadService.FindAssetById(id);

            return Ok(asset);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssetById(string id)
        {
            var asset = await uploadService.FindAssetById(id);
            if (asset == null)
            {
                throw new NotFoundException("Asset not found");
            }

            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            await uploadService.DeleteAssetById(id, userId);
            return NoContent();
        }
    }
}