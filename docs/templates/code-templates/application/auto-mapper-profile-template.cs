using AutoMapper;
using Aqt.CoreOracle.[ModuleName].Dtos;

namespace Aqt.CoreOracle.[ModuleName]
{
    /// <summary>
    /// AutoMapper profile for [EntityName] mappings
    /// </summary>
    public class [EntityName]AutoMapperProfile : Profile
    {
        public [EntityName]AutoMapperProfile()
        {
            CreateMap<[EntityName], [EntityName]ListDto>();
            CreateMap<[EntityName], [EntityName]LookupDto>();
            CreateMap<[EntityName]CreateUpdateDto, [EntityName]>();
        }
    }
} 