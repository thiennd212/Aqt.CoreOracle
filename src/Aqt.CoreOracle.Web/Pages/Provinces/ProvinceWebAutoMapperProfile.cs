using Aqt.CoreOracle.Application.Contracts.Provinces.Dtos;
using Aqt.CoreOracle.Web.Pages.Provinces.ViewModels;
using AutoMapper;

namespace Aqt.CoreOracle.Web.Pages.Provinces;

public class ProvinceWebAutoMapperProfile : Profile
{
    public ProvinceWebAutoMapperProfile()
    {
        CreateMap<CreateProvinceViewModel, CreateUpdateProvinceDto>();

        // Add other mappings specific to Province UI/ViewModels here if needed
        // Example: Mapping for EditProvinceViewModel if you create one
        // CreateMap<EditProvinceViewModel, CreateUpdateProvinceDto>();
        // CreateMap<ProvinceDto, EditProvinceViewModel>(); 
    }
} 