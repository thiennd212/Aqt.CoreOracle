using AutoMapper;
using Aqt.CoreOracle.Application.Contracts.Provinces.Dtos;
using Aqt.CoreOracle.Domain.Provinces.Entities;
using Volo.Abp.AutoMapper;

namespace Aqt.CoreOracle.Application.Provinces;

public class ProvinceApplicationAutoMapperProfile : Profile
{
    public ProvinceApplicationAutoMapperProfile()
    {
        CreateMap<Province, ProvinceDto>()
            .ForMember(dest => dest.CountryName,
                opt => opt.MapFrom(src => src.Country != null ? src.Country.Name : null));

        CreateMap<CreateUpdateProvinceDto, Province>()
            .IgnoreAuditedObjectProperties()
            .Ignore(x => x.Id)
            .Ignore(x => x.Country);

        CreateMap<ProvinceDto, CreateUpdateProvinceDto>();
        CreateMap<Province, ProvinceLookupDto>();
    }
} 