using Aqt.CoreOracle.Application.Contracts.Positions;
using AutoMapper;

namespace Aqt.CoreOracle.Web.Pages.Positions;

public class PositionsWebAutoMapperProfile : Profile
{
    public PositionsWebAutoMapperProfile()
    {
        // Mappings for Positions
        CreateMap<CreateModalModel.PositionCreateViewModel, CreateUpdatePositionDto>();
        CreateMap<EditModalModel.PositionEditViewModel, CreateUpdatePositionDto>();
        CreateMap<PositionDto, EditModalModel.PositionEditViewModel>();
    }
} 