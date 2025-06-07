using AutoMapper;
using Dentizone.Application.DTOs.Catalog;
using Dentizone.Application.Interfaces.Catalog;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;


namespace Dentizone.Application.Services
{
    public class CatalogService(
        ICategoryRepository categoryRepository,
        IMapper mapper,
        ISubCategoryRepository subCategoryRepository,
        IItemRepository itemRepository)
        : ICatalogService
    {
        public async Task<CreatedCategoryDTO> CreateCategory(CategoryDto createdCategoryDto)
        {
            var category = mapper.Map<Category>(createdCategoryDto);
            var createdCategory = await categoryRepository.CreateAsync(category);
            return mapper.Map<CreatedCategoryDTO>(createdCategory);
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

        public async Task<CategoryDto> UpdateCategory(CategoryDto updatedCategoryDto)
        {
            var category = mapper.Map<Category>(updatedCategoryDto);
            var updatedCategory = await categoryRepository.Update(category);
            if (updatedCategory == null) throw new NotFoundException("Category not found");
            return mapper.Map<CategoryDto>(updatedCategory);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            var categories = await categoryRepository.GetAll();
            var mapped = categories.Select(c => mapper.Map<CategoryDto>(c)).ToList();
            return mapped;
        }

        public async Task<IEnumerable<SubCategoryDTO>> GetSubCategoriesByCategoryId(string id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException("This category doesn't exist ");

            var relatedSubCategories = category.SubCategories;
            return relatedSubCategories.Select(sc => mapper.Map<SubCategoryDTO>(sc));
        }

        public async Task<CreatedSubCategoryDTO> CreateSubCategory(SubCategoryDTO createdSubCategoryDto)
        {
            var subCategory = mapper.Map<SubCategory>(createdSubCategoryDto);
            var createdSubCategory = await subCategoryRepository.CreateAsync(subCategory);
            return mapper.Map<CreatedSubCategoryDTO>(createdSubCategory);
        }

        public async Task<SubCategoryDTO?> GetSubCategoryById(string id)
        {
            var subCategory = await subCategoryRepository.GetByIdAsync(id);
            if (subCategory == null) throw new NotFoundException("There's no sub category with this id ");
            return mapper.Map<SubCategoryDTO>(subCategory);
        }

        public async Task<SubCategoryDTO> DeleteSubCategory(string id)
        {
            var subCategory = await subCategoryRepository.DeleteAsync(id);
            if (subCategory == null) throw new NotFoundException("There's no sub category with this id ");
            return mapper.Map<SubCategoryDTO>(subCategory);
        }

        public async Task<SubCategoryDTO> UpdateSubCategory(SubCategoryDTO updatedSubCategoryDto)
        {
            var subCategory = mapper.Map<SubCategory>(updatedSubCategoryDto);
            var updatedSubCategory = await subCategoryRepository.Update(subCategory);
            if (updatedSubCategory == null) throw new NotFoundException("SubCategory not found");
            return mapper.Map<SubCategoryDTO>(updatedSubCategory);
        }

        public async Task<IEnumerable<SubCategoryDTO>> GetAllSubCategories()
        {
            var subCategories = await subCategoryRepository.GetAll();
            var mapped = subCategories.Select(mapper.Map<SubCategoryDTO>).ToList();
            return mapped;
        }

        public async Task<CreatedItemDTO> CreateItem(ItemDTO createdItemDto)
        {
            //TODO: Check if both category and sub is really exists 

            var item = mapper.Map<Item>(createdItemDto);
            var createdItem = await itemRepository.CreateAsync(item);
            return mapper.Map<CreatedItemDTO>(createdItem);
        }

        public async Task<ItemViewDTO?> GetItemById(string id)
        {
            var item = await itemRepository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Item with this id is not found");
            return mapper.Map<ItemViewDTO>(item);
        }

        public async Task<ItemViewDTO> DeleteItem(string id)
        {
            var item = await itemRepository.DeleteAsync(id);
            if (item == null) throw new NotFoundException("Item with this id is not found");
            return mapper.Map<ItemViewDTO>(item);
        }
    }
}