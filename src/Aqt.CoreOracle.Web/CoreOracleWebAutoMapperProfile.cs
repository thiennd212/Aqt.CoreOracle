using AutoMapper;

namespace Aqt.CoreOracle.Web;

public class CoreOracleWebAutoMapperProfile : Profile
{
    public CoreOracleWebAutoMapperProfile()
    {
        // Define your global object mappings here, for the Web project (if any).
        // Feature-specific mappings are moved to their respective Profile files.

        // Mapping from Province ViewModel was moved to ProvinceWebAutoMapperProfile.cs
        // CreateMap<CreateProvinceViewModel, CreateUpdateProvinceDto>(); // Removed
        
        // Add other Web layer mappings here if needed
    }
}
