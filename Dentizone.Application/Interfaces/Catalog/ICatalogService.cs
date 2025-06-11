using Dentizone.Application.DTOs.Catalog;

namespace Dentizone.Application.Interfaces.Catalog;

public interface ICatalogService
{
    Task<CategoryView> CreateCategory(CategoryDto createdCategoryDto);
    Task<CategoryDto?> GetCategoryById(string id);
    Task<CategoryDto> DeleteCategory(string id);
    Task<CategoryDto> UpdateCategory(string userId, CategoryDto updatedCategoryDto);
    Task<IEnumerable<SingleCategory>> GetAllCategories();
    Task<IEnumerable<SubCategoryView>> GetSubCategoriesByCategoryId(string id);
    Task<CreatedSubCategoryDTO> CreateSubCategory(SubCategoryDto createdSubCategoryDto);
    Task<SubCategoryDto?> GetSubCategoryById(string id);
    Task<SubCategoryDto> DeleteSubCategory(string id);
    Task<SubCategoryDto> UpdateSubCategory(SubCategoryDto updatedSubCategoryDto);
    Task<IEnumerable<SubCategoryDto>> GetAllSubCategories();
}