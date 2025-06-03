using AutoMapper;
using Dentizone.Application.DTOs.CatalogDTOs;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Exceptions;


namespace Dentizone.Application.Services
{
    public class CatalogService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public CatalogService(ICategoryRepository categoryRepository, IMapper mapper,
                              ISubCategoryRepository subCategoryRepository, IItemRepository itemRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _subCategoryRepository = subCategoryRepository;
            _itemRepository = itemRepository;
        }

        public async Task<CategoryDTO> CreateCategory(CategoryDTO createdCategoryDto)
        {
            var category = _mapper.Map<Category>(createdCategoryDto);
            var createdCategory = await _categoryRepository.CreateAsync(category);
            return _mapper.Map<CategoryDTO>(createdCategory);
        }

        public async Task<CategoryDTO?> GetCategoryById(string id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);


            if (category == null)
                throw new NotFoundException($"Category with id {id} is not found");

            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> DeleteCategory(string id)
        {
            var category = await _categoryRepository.Delete(id);
            if (category == null) throw new NotFoundException("Category not found");
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> UpdateCategory(CategoryDTO updatedCategoryDto)
        {
            var category = _mapper.Map<Category>(updatedCategoryDto);
            var updatedCategory = await _categoryRepository.Update(category);
            if (updatedCategory == null) throw new NotFoundException("Category not found");
            return _mapper.Map<CategoryDTO>(updatedCategory);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAll();
            var mapped = categories.Select(c => _mapper.Map<CategoryDTO>(c)).ToList();
            return mapped;
        }

        public async Task<IEnumerable<SubCategoryDTO>> GetSubCategoriesByCategoryId(string id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException("This category has no subcategories ");

            var relatedSubCategories = category.SubCategories;
            return relatedSubCategories.Select(sc => _mapper.Map<SubCategoryDTO>(sc));
        }

        public async Task<SubCategoryDTO> CreateSubCategory(SubCategoryDTO createdSubCategoryDto)
        {
            var subCategory = _mapper.Map<SubCategory>(createdSubCategoryDto);
            var createdSubCategory = await _subCategoryRepository.CreateAsync(subCategory);
            return _mapper.Map<SubCategoryDTO>(createdSubCategory);
        }

        public async Task<SubCategoryDTO?> GetSubCategoryById(string id)
        {
            var subCategory = await _subCategoryRepository.GetByIdAsync(id);
            if (subCategory == null) throw new NotFoundException("There's no sub category with this id ");
            return _mapper.Map<SubCategoryDTO>(subCategory);
        }

        public async Task<SubCategoryDTO> DeleteSubCategory(string id)
        {
            var subCategory = await _subCategoryRepository.DeleteAsync(id);
            if (subCategory == null) throw new NotFoundException("There's no sub category with this id ");
            return _mapper.Map<SubCategoryDTO>(subCategory);
        }

        public async Task<SubCategoryDTO> UpdateSubCategory(SubCategoryDTO updatedSubCategoryDto)
        {
            var subCategory = _mapper.Map<SubCategory>(updatedSubCategoryDto);
            var updatedSubCategory = await _subCategoryRepository.Update(subCategory);
            if (updatedSubCategory == null) throw new NotFoundException("SubCategory not found");
            return _mapper.Map<SubCategoryDTO>(updatedSubCategory);
        }

        public async Task<IEnumerable<SubCategoryDTO>> GetAllSubCategories()
        {
            var subCategories = await _subCategoryRepository.GetAll();
            var mapped = subCategories.Select(sc => _mapper.Map<SubCategoryDTO>(sc)).ToList();
            return mapped;
        }

        public async Task<CreatedItemDTO> CreateItem(CreatedItemDTO createdItemDto)
        {
            var item = _mapper.Map<Item>(createdItemDto);
            var createdItem = await _itemRepository.CreateAsync(item);
            return _mapper.Map<CreatedItemDTO>(createdItem);
        }

        public async Task<ItemDTO?> GetItemById(string id)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Item with this id is not found");
            return _mapper.Map<ItemDTO>(item);
        }

        public async Task<ItemDTO> DeleteItem(string id)
        {
            var item = await _itemRepository.DeleteAsync(id);
            if (item == null) throw new NotFoundException("Item with this id is not found");
            return _mapper.Map<ItemDTO>(item);
        }
    }
}