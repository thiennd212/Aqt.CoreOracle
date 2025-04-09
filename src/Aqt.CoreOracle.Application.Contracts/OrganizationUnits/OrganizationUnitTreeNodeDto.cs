using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Aqt.CoreOracle.Domain.Shared.OrganizationUnits;

namespace Aqt.CoreOracle.Application.Contracts.OrganizationUnits;

public class OrganizationUnitTreeNodeDto : EntityDto<Guid>
{
    public Guid? ParentId { get; set; }
    
    public string Code { get; set; }
    
    public string DisplayName { get; set; }
    
    public string? Address { get; set; }
    
    public string? SyncCode { get; set; }
    
    public OrganizationUnitTypeEnum OrganizationUnitType { get; set; }
    
    public List<OrganizationUnitTreeNodeDto> Children { get; set; }
    
    public OrganizationUnitTreeNodeDto()
    {
        Children = new List<OrganizationUnitTreeNodeDto>();
    }
} 