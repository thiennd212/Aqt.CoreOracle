using Aqt.CoreOracle;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using Aqt.CoreOracle.Domain.Positions;

namespace Aqt.CoreOracle.Domain.OrganizationStructure;

public class EmployeePosition : FullAuditedEntity<Guid>
{
    public Guid UserId { get; set; }
    public Guid OrganizationUnitId { get; set; }
    public Guid PositionId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    // Navigation Properties
    public virtual IdentityUser User { get; set; } = default!;
    public virtual OrganizationUnit OrganizationUnit { get; set; } = default!;
    public virtual Position Position { get; set; } = default!;

    private EmployeePosition() { } // Constructor cho EF Core

    public EmployeePosition(
        Guid id,
        Guid userId,
        Guid organizationUnitId,
        Guid positionId,
        DateTime startDate,
        DateTime? endDate = null) : base(id)
    {
        UserId = userId;
        OrganizationUnitId = organizationUnitId;
        PositionId = positionId;
        SetDates(startDate, endDate);
    }

    public void SetDates(DateTime startDate, DateTime? endDate)
    {
        if (endDate.HasValue && startDate >= endDate.Value)
        {
            throw new BusinessException(CoreOracleDomainErrorCodes.InvalidPositionAssignmentDateRange); // Sử dụng mã lỗi từ Domain.Shared
        }

        StartDate = startDate;
        EndDate = endDate;
    }
} 