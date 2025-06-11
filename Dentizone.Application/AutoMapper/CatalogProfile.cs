using AutoMapper;
using Dentizone.Application.DTOs.Catalog;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.AutoMapper
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<Category, SingleCategory>().ReverseMap();
            CreateMap<CategoryView, SubCategory>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<CategoryView, Category>().ReverseMap();
            CreateMap<SubCategoryView, SubCategory>().ReverseMap();
            CreateMap<SubCategoryDto, SubCategory>().ReverseMap();
            CreateMap<CreatedSubCategoryDTO, SubCategory>().ReverseMap();
        }
    }
}