using System;
using AutoMapper;
using Aqt.CoreOracle.Application.Contracts.OrganizationUnits;
using Aqt.CoreOracle.Domain.OrganizationStructure;
using Volo.Abp.Identity;
using Aqt.CoreOracle.Domain.Positions;
using Aqt.CoreOracle.Application.Contracts.Positions;
using Aqt.CoreOracle.Domain.Shared.OrganizationUnits;

namespace Aqt.CoreOracle;

public class CoreOracleApplicationAutoMapperProfile : Profile
{
    public CoreOracleApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        
        // EmployeePosition Mappings
        CreateMap<EmployeePosition, EmployeePositionDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.Name + " " + src.User.Surname))
            .ForMember(dest => dest.OrganizationUnitName, opt => opt.MapFrom(src => src.OrganizationUnit.DisplayName))
            .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position.Name));
            
        CreateMap<AssignEmployeePositionDto, EmployeePosition>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        
        // OrganizationUnit Mappings
        CreateMap<OrganizationUnit, CustomOrganizationUnitDto>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => 
                src.ExtraProperties.ContainsKey(CoreOracleConsts.OrganizationUnitAddress) 
                    ? src.ExtraProperties[CoreOracleConsts.OrganizationUnitAddress].ToString()
                    : null))
            .ForMember(dest => dest.SyncCode, opt => opt.MapFrom(src => 
                src.ExtraProperties.ContainsKey(CoreOracleConsts.OrganizationUnitSyncCode)
                    ? src.ExtraProperties[CoreOracleConsts.OrganizationUnitSyncCode].ToString()
                    : null));
                    
        CreateMap<OrganizationUnit, OrganizationUnitTreeNodeDto>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => 
                src.ExtraProperties.ContainsKey(CoreOracleConsts.OrganizationUnitAddress) 
                    ? src.ExtraProperties[CoreOracleConsts.OrganizationUnitAddress].ToString()
                    : null))
            .ForMember(dest => dest.SyncCode, opt => opt.MapFrom(src => 
                src.ExtraProperties.ContainsKey(CoreOracleConsts.OrganizationUnitSyncCode)
                    ? src.ExtraProperties[CoreOracleConsts.OrganizationUnitSyncCode].ToString()
                    : null))
            .ForMember(dest => dest.Children, opt => opt.Ignore());
            
        // Position Mappings
        CreateMap<Position, PositionDto>();
        CreateMap<CreateUpdatePositionDto, Position>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ExtraProperties, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore());
    }
}
