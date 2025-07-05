using AutoMapper;
using Dentizone.Application.DTOs.Catalog;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;


namespace Dentizone.Application.Services
{
    public class CatalogService(
        ICategoryRepository categoryRepository,
        IMapper mapper,
        ISubCategoryRepository subCategoryRepository
    )
        : ICatalogService
    {
        public async Task<CategoryView> CreateCategory(CategoryDto createdCategoryDto)
        {
            var category = mapper.Map<Category>(createdCategoryDto);
            var createdCategory = await categoryRepository.CreateAsync(category);
            return mapper.Map<CategoryView>(createdCategory);
        }

        public async Task<CategoryDto?> GetCategoryById(string id)
        {
            var category = await categoryRepository.GetByIdAsync(id);


            if (category == null)
                throw new NotFoundException($"Category with id {id} is not found");

            return mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> DeleteCategory(string id)
        {
            var category = await categoryRepository.Delete(id);
            if (category == null) throw new NotFoundException("Category not found");
            return mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateCategory(string id, CategoryDto updatedCategoryDto)
        {
            var existingCategory = await categoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
                throw new NotFoundException($"Category with id {id} not found");


            existingCategory.IconUrl = updatedCategoryDto.IconUrl;
            existingCategory.Name = updatedCategoryDto.Name;


            var updatedCategory = await categoryRepository.Update(existingCategory);
            if (updatedCategory == null) throw new NotFoundException("Category not found");
            return mapper.Map<CategoryDto>(updatedCategory);
        }

        public async Task<IEnumerable<SingleCategory>> GetAllCategories()
        {
            var categories = await categoryRepository.GetAll();

            return mapper.Map<IEnumerable<SingleCategory>>(categories);
        }

        public async Task<IEnumerable<SubCategoryView>> GetSubCategoriesByCategoryId(string id)
        {
            var category =
                await categoryRepository.FindBy(c => c.Id == id && c.IsDeleted == false, [c => c.SubCategories]);
            if (category == null)
                throw new NotFoundException("This category doesn't exist ");

            var relatedSubCategories = category.SubCategories.Where(s => !s.IsDeleted);
            return relatedSubCategories.Select(mapper.Map<SubCategoryView>);
        }

        public async Task<CreatedSubCategoryDto> CreateSubCategory(SubCategoryDto createdSubCategoryDto)
        {
            var category = await categoryRepository.GetByIdAsync(createdSubCategoryDto.CategoryId);
            if (category == null)
                throw new NotFoundException($"Category with id {createdSubCategoryDto.CategoryId} not found");

            var subCategory = mapper.Map<SubCategory>(createdSubCategoryDto);
            var createdSubCategory = await subCategoryRepository.CreateAsync(subCategory);
            return mapper.Map<CreatedSubCategoryDto>(createdSubCategory);
        }

        public async Task<SubCategoryDto?> GetSubCategoryById(string id)
        {
            var subCategory = await subCategoryRepository.GetByIdAsync(id);
            if (subCategory == null) throw new NotFoundException("There's no sub category with this id ");
            return mapper.Map<SubCategoryDto>(subCategory);
        }

        public async Task<SubCategoryDto> DeleteSubCategory(string id)
        {
            var subCategory = await subCategoryRepository.DeleteAsync(id);
            if (subCategory == null) throw new NotFoundException("There's no sub category with this id ");
            return mapper.Map<SubCategoryDto>(subCategory);
        }

        public async Task<SubCategoryDto> UpdateSubCategory(SubCategoryDto updatedSubCategoryDto)
        {
            var category = await categoryRepository.GetByIdAsync(updatedSubCategoryDto.CategoryId);
            if (category == null)
                throw new NotFoundException($"Category with id {updatedSubCategoryDto.CategoryId} not found");

            var subCategory = mapper.Map<SubCategory>(updatedSubCategoryDto);
            var existingSubCategory = await subCategoryRepository.GetByIdAsync(subCategory.Id);
            if (existingSubCategory == null)
                throw new NotFoundException($"SubCategory with id {subCategory.Id} not found");


            var updatedSubCategory = await subCategoryRepository.Update(subCategory);
            if (updatedSubCategory == null) throw new NotFoundException("SubCategory not found");
            return mapper.Map<SubCategoryDto>(updatedSubCategory);
        }

        public async Task<IEnumerable<SubCategoryDto>> GetAllSubCategories()
        {
            var subCategories = await subCategoryRepository.GetAll();
            var mapped = subCategories.Select(mapper.Map<SubCategoryDto>).ToList();
            return mapped;
        }
    }
}