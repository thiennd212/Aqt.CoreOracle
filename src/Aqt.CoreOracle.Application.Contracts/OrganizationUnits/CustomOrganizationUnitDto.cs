using System;
using Volo.Abp.Application.Dtos;
using Aqt.CoreOracle.Domain.Shared.OrganizationUnits;

namespace Aqt.CoreOracle.Application.Contracts.OrganizationUnits;

public class CustomOrganizationUnitDto : AuditedEntityDto<Guid>
{
    public Guid? ParentId { get; set; }
    
    public string Code { get; set; }
    
    public string DisplayName { get; set; }
    
    public string? Address { get; set; }
    
    public string? SyncCode { get; set; }
    
    public OrganizationUnitTypeEnum OrganizationUnitType { get; set; }
} 