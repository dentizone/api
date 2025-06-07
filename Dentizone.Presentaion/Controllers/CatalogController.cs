using Dentizone.Application.DTOs.Catalog;
using Dentizone.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogService _catalogService;
        public CatalogController(CatalogService catalogService)
        {
            _catalogService = catalogService;
        }
        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _catalogService.GetAllCategories();
            return Ok(categories);
        }
        [HttpGet("categories/{categoryId}")]
        public async Task<IActionResult> GetCategoryById(string categoryId)
        {
            try
            {
                var category = await _catalogService.GetCategoryById(categoryId);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("categories")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDto)
        {
            var createdCategory = await _catalogService.CreateCategory(categoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
        }
        [HttpPut("categories")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDTO categoryDto)
        {
            try
            {
                var updatedCategory = await _catalogService.UpdateCategory(categoryDto);
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
                var deletedCategory = await _catalogService.DeleteCategory(categoryId);
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
                var subCategories = await _catalogService.GetSubCategoriesByCategoryId(categoryId);
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
            var subCategories = await _catalogService.GetAllSubCategories();
            return Ok(subCategories);
        }
        [HttpGet("subcategories/{subCategoryId}")]
        public async Task<IActionResult> GetSubCategorybyId(string subCategoryId)
        {
            try
            {
                var subCategory = await _catalogService.GetSubCategoryById(subCategoryId);
                return Ok(subCategory);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("subcategories")]
        public async Task<IActionResult> CreateSubCategory([FromBody] SubCategoryDTO subCategoryDTO)
        {
            var createdSubCategory = await _catalogService.CreateSubCategory(subCategoryDTO);
            return CreatedAtAction(nameof(GetSubCategorybyId), new { id = createdSubCategory.Id }, createdSubCategory);

        }

        [HttpPut("subcategories")]
        public async Task<IActionResult> UpdateSubCategory([FromBody] SubCategoryDTO subCategoryDTO)
        {
            try
            {
                var updatedSubCategory = await _catalogService.UpdateSubCategory(subCategoryDTO);
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
                var deletedSubCategory = await _catalogService.DeleteSubCategory(subCategoryId);
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
                var item = await _catalogService.GetItemById(itemsId);
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
            var createdItem = await _catalogService.CreateItem(createdItemDto);
            return CreatedAtAction(nameof(GetItemById), new { id = createdItem.Id }, createdItem);
        }
        [HttpDelete("items/{itemId}")]
        public async Task<IActionResult> DeleteItem(string itemId)
        {
            try
            {
                var deletedItem = await _catalogService.DeleteItem(itemId);
                return Ok(deletedItem);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
