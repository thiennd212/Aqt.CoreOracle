using Aqt.CoreOracle.Application.Contracts.Countries.Dtos;
using AutoMapper;

namespace Aqt.CoreOracle.Web.Pages.Countries;

public class CountriesWebAutoMapperProfile : Profile
{
    public CountriesWebAutoMapperProfile()
    {
        CreateMap<CountryDto, CreateUpdateCountryDto>();
    }
} 