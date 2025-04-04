using System;
using AutoMapper;
using Aqt.CoreOracle.Categories.Dtos;

namespace Aqt.CoreOracle.Categories;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CategoryType, CategoryTypeDto>();
        CreateMap<CreateUpdateCategoryTypeDto, CategoryType>();

        CreateMap<CategoryItem, CategoryItemDto>()
            .ForMember(x => x.CategoryType, opt => opt.ExplicitExpansion())
            .ForMember(x => x.Parent, opt => opt.ExplicitExpansion());

        CreateMap<CategoryItem, CategoryItemLookupDto>()
            .ForMember(x => x.CategoryTypeCode, opt => opt.MapFrom(src => src.CategoryType.Code))
            .ForMember(x => x.CategoryTypeName, opt => opt.MapFrom(src => src.CategoryType.Name));

        CreateMap<CreateCategoryItemDto, CategoryItem>();
        CreateMap<UpdateCategoryItemDto, CategoryItem>();
        CreateMap<CategoryItemDto, UpdateCategoryItemDto>();
    }
} 