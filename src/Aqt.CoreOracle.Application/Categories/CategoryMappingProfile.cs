using AutoMapper;
using Aqt.CoreOracle.Categories.Dtos;

namespace Aqt.CoreOracle.Categories;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CategoryType, CategoryTypeDto>();
        CreateMap<CreateUpdateCategoryTypeDto, CategoryType>();

        CreateMap<CategoryItem, CategoryItemDto>();
        CreateMap<CreateUpdateCategoryItemDto, CategoryItem>();
    }
} 