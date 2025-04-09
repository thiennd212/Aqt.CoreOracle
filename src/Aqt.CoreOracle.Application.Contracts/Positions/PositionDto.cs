using System;
using Volo.Abp.Application.Dtos;

namespace Aqt.CoreOracle.Application.Contracts.Positions;

public class PositionDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string? Description { get; set; }
} 