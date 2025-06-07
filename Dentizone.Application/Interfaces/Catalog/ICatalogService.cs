using Dentizone.Application.DTOs.Catalog;

namespace Dentizone.Application.Interfaces.Catalog;

public interface ICatalogService
{
    Task<CreatedCategoryDTO> CreateCategory(CategoryDto createdCategoryDto);
    Task<CategoryDto?> GetCategoryById(string id);
    Task<CategoryDto> DeleteCategory(string id);
    Task<CategoryDto> UpdateCategory(CategoryDto updatedCategoryDto);
    Task<IEnumerable<CategoryDto>> GetAllCategories();
    Task<IEnumerable<SubCategoryDTO>> GetSubCategoriesByCategoryId(string id);
    Task<CreatedSubCategoryDTO> CreateSubCategory(SubCategoryDTO createdSubCategoryDto);
    Task<SubCategoryDTO?> GetSubCategoryById(string id);
    Task<SubCategoryDTO> DeleteSubCategory(string id);
    Task<SubCategoryDTO> UpdateSubCategory(SubCategoryDTO updatedSubCategoryDto);
    Task<IEnumerable<SubCategoryDTO>> GetAllSubCategories();
    Task<CreatedItemDTO> CreateItem(ItemDTO createdItemDto);
    Task<ItemViewDTO?> GetItemById(string id);
    Task<ItemViewDTO> DeleteItem(string id);
}