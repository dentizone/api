using Dentizone.Application.DTOs.Catalog;
using Dentizone.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController(CatalogService catalogService) : ControllerBase
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
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            var createdCategory = await catalogService.CreateCategory(categoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new { categoryId = createdCategory.Id }, createdCategory);
        }

        [HttpPut("categories")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto categoryDto)
        {
            try
            {
                var updatedCategory = await catalogService.UpdateCategory(categoryDto);
                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("categories/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(string categoryId)
        {
            try
            {
                var deletedCategory = await catalogService.DeleteCategory(categoryId);
                return Ok(deletedCategory);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("categories/{categoryId}/subcategories")]
        public async Task<IActionResult> GetSubCategoriesByCategoryId(string categoryId)
        {
            try
            {
                var subCategories = await catalogService.GetSubCategoriesByCategoryId(categoryId);
                return Ok(subCategories);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
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
            try
            {
                var subCategory = await catalogService.GetSubCategoryById(subCategoryId);
                return Ok(subCategory);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("subcategories")]
        public async Task<IActionResult> CreateSubCategory([FromBody] SubCategoryDTO subCategoryDto)
        {
            var createdSubCategory = await catalogService.CreateSubCategory(subCategoryDto);
            return CreatedAtAction(nameof(GetSubCategoryById), new { subCategoryId = createdSubCategory.Id },
                                   createdSubCategory);
        }

        [HttpPut("subcategories")]
        public async Task<IActionResult> UpdateSubCategory([FromBody] SubCategoryDTO subCategoryDto)
        {
            try
            {
                var updatedSubCategory = await catalogService.UpdateSubCategory(subCategoryDto);
                return Ok(updatedSubCategory);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("subcategories/{subCategoryId}")]
        public async Task<IActionResult> DeleteSubCategory(string subCategoryId)
        {
            try
            {
                var deletedSubCategory = await catalogService.DeleteSubCategory(subCategoryId);
                return Ok(deletedSubCategory);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("items/{itemsId}")]
        public async Task<IActionResult> GetItemById(string itemsId)
        {
            try
            {
                var item = await catalogService.GetItemById(itemsId);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("items")]
        public async Task<IActionResult> CreateItem([FromBody] ItemDTO createdItemDto)
        {
            var createdItem = await catalogService.CreateItem(createdItemDto);
            return CreatedAtAction(nameof(GetItemById), new { id = createdItem.Id }, createdItem);
        }

        [HttpDelete("items/{itemId}")]
        public async Task<IActionResult> DeleteItem(string itemId)
        {
            try
            {
                var deletedItem = await catalogService.DeleteItem(itemId);
                return Ok(deletedItem);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}