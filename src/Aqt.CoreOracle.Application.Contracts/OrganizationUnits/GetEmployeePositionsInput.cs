using System;
using Volo.Abp.Application.Dtos;

namespace Aqt.CoreOracle.Application.Contracts.OrganizationUnits;

public class GetEmployeePositionsInput : PagedAndSortedResultRequestDto
{
    public Guid? OrganizationUnitId { get; set; }
    
    //public Guid? UserId { get; set; }
    
    //public Guid? PositionId { get; set; }
    
    //public DateTime? ActiveAtDate { get; set; }
} 