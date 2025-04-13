using AutoMapper;
using Aqt.CoreOracle.Application.Contracts.Countries.Dtos;
using Aqt.CoreOracle.Domain.Countries.Entities;
using Volo.Abp.AutoMapper;

namespace Aqt.CoreOracle.Application.Countries;

public class CountryApplicationAutoMapperProfile : Profile
{
    public CountryApplicationAutoMapperProfile()
    {
        CreateMap<Country, CountryDto>();
        
        CreateMap<CreateUpdateCountryDto, Country>()
            .IgnoreAuditedObjectProperties()
            .Ignore(x => x.Id)
            .IgnoreFullAuditedObjectProperties();
        
        CreateMap<CountryDto, CreateUpdateCountryDto>();
        
        CreateMap<Country, CountryLookupDto>();
    }
} 