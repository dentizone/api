using Dentizone.Application.DTOs.Catalog;

namespace Dentizone.Application.Interfaces.Catalog;

public interface ICatalogService
{
    Task<CategoryDTO> CreateCategory(CategoryDTO createdCategoryDto);
    Task<CategoryDTO?> GetCategoryById(string id);
    Task<CategoryDTO> DeleteCategory(string id);
    Task<CategoryDTO> UpdateCategory(CategoryDTO updatedCategoryDto);
    Task<IEnumerable<CategoryDTO>> GetAllCategories();
    Task<IEnumerable<SubCategoryDTO>> GetSubCategoriesByCategoryId(string id);
    Task<SubCategoryDTO> CreateSubCategory(SubCategoryDTO createdSubCategoryDto);
    Task<SubCategoryDTO?> GetSubCategoryById(string id);
    Task<SubCategoryDTO> DeleteSubCategory(string id);
    Task<SubCategoryDTO> UpdateSubCategory(SubCategoryDTO updatedSubCategoryDto);
    Task<IEnumerable<SubCategoryDTO>> GetAllSubCategories();
    Task<CreatedItemDTO> CreateItem(CreatedItemDTO createdItemDto);
    Task<ItemDTO?> GetItemById(string id);
    Task<ItemDTO> DeleteItem(string id);
}