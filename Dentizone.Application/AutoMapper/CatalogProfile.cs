using AutoMapper;
using Dentizone.Application.DTOs.Catalog;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.AutoMapper
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<SubCategoryDTO, SubCategory>().ReverseMap();
            CreateMap<CreatedItemDTO, Item>().ReverseMap();
            CreateMap<ItemDTO, Item>()
                .ReverseMap()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(src => src.SubCategory.Name));
        }
    }
}