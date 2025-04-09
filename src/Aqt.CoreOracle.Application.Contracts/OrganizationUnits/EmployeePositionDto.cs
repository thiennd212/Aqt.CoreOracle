using System;
using Volo.Abp.Application.Dtos;

namespace Aqt.CoreOracle.Application.Contracts.OrganizationUnits;

public class EmployeePositionDto : FullAuditedEntityDto<Guid>
{
    public Guid UserId { get; set; }
    
    public string UserName { get; set; }
    
    public string UserFullName { get; set; }
    
    public Guid OrganizationUnitId { get; set; }
    
    public string OrganizationUnitName { get; set; }
    
    public Guid PositionId { get; set; }
    
    public string PositionName { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
} 