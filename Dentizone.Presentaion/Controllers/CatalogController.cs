using Dentizone.Application.DTOs.Catalog;
using Dentizone.Application.Interfaces.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController(ICatalogService catalogService) : ControllerBase
    {
        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await catalogService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("categories/{categoryId}")]
        public async Task<IActionResult> GetCategoryById(string categoryId)
        {
            try
            {
                var category = await catalogService.GetCategoryById(categoryId);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("categories")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            var createdCategory = await catalogService.CreateCategory(categoryDto);
            return Ok(createdCategory);
        }

        [HttpPut("categories/{categoryId}")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> UpdateCategory(
            string categoryId,
            [FromBody] CategoryDto categoryDto)
        {
            var updatedCategory = await catalogService.UpdateCategory(categoryId, categoryDto);
            return Ok(updatedCategory);
        }

        [HttpDelete("categories/{categoryId}")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> DeleteCategory(string categoryId)
        {
            var deletedCategory = await catalogService.DeleteCategory(categoryId);
            return Ok(deletedCategory);
        }

        [HttpGet("categories/{categoryId}/subcategories")]
        public async Task<IActionResult> GetSubCategoriesByCategoryId(string categoryId)
        {
            var subCategories = await catalogService.GetSubCategoriesByCategoryId(categoryId);
            return Ok(subCategories);
        }

        [HttpGet("subcategories")]
        public async Task<IActionResult> GetAllSubCategories()
        {
            var subCategories = await catalogService.GetAllSubCategories();
            return Ok(subCategories);
        }

        [HttpGet("subcategories/{subCategoryId}")]
        public async Task<IActionResult> GetSubCategoryById(string subCategoryId)
        {
            var subCategory = await catalogService.GetSubCategoryById(subCategoryId);
            return Ok(subCategory);
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpPost("subcategories")]
        public async Task<IActionResult> CreateSubCategory([FromBody] SubCategoryDto subCategoryDto)
        {
            var createdSubCategory = await catalogService.CreateSubCategory(subCategoryDto);
            return CreatedAtAction(nameof(GetSubCategoryById), new { subCategoryId = createdSubCategory.Id },
                createdSubCategory);
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpPut("subcategories")]
        public async Task<IActionResult> UpdateSubCategory([FromBody] SubCategoryDto subCategoryDto)
        {
            var updatedSubCategory = await catalogService.UpdateSubCategory(subCategoryDto);
            return Ok(updatedSubCategory);
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpDelete("subcategories/{subCategoryId}")]
        public async Task<IActionResult> DeleteSubCategory(string subCategoryId)
        {
            var deletedSubCategory = await catalogService.DeleteSubCategory(subCategoryId);
            return Ok(deletedSubCategory);
        }
    }
}