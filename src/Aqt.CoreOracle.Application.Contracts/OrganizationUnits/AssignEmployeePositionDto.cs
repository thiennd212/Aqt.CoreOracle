using System;
using System.ComponentModel.DataAnnotations;

namespace Aqt.CoreOracle.Application.Contracts.OrganizationUnits;

public class AssignEmployeePositionDto
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid OrganizationUnitId { get; set; }

    [Required]
    public Guid PositionId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }
} 