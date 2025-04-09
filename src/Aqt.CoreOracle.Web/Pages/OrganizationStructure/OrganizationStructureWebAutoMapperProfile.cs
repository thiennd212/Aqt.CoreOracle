using Aqt.CoreOracle.Application.Contracts.OrganizationUnits;
using Aqt.CoreOracle.Domain.Shared;
using Aqt.CoreOracle.Domain.Shared.OrganizationUnits;
using AutoMapper;

namespace Aqt.CoreOracle.Web.Pages.OrganizationStructure;

public class OrganizationStructureWebAutoMapperProfile : Profile
{
    public OrganizationStructureWebAutoMapperProfile()
    {
        // Create OU: ViewModel -> Input DTO
        CreateMap<CreateModalModel.OrganizationUnitCreateViewModel, CreateOrganizationUnitInput>();

        // Edit OU: ViewModel -> Input DTO
        CreateMap<EditModalModel.OrganizationUnitEditViewModel, UpdateOrganizationUnitInput>();

        // Edit OU: Custom DTO -> ViewModel (AutoMapper handles matching properties by convention)
        CreateMap<CustomOrganizationUnitDto, EditModalModel.OrganizationUnitEditViewModel>();
            
        // Assign Position: ViewModel -> Input DTO
        CreateMap<AssignPositionModalModel.AssignPositionViewModel, AssignEmployeePositionDto>();
    }
} 