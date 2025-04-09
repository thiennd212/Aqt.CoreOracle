using System;
using System.ComponentModel.DataAnnotations;

namespace Aqt.CoreOracle.Application.Contracts.Positions;

public class CreateUpdatePositionDto
{
    [Required]
    [StringLength(CoreOracleConsts.MaxPositionNameLength)]
    public string Name { get; set; }

    [Required]
    [StringLength(CoreOracleConsts.MaxPositionCodeLength)]
    public string Code { get; set; }

    [StringLength(CoreOracleConsts.MaxDescriptionLength)]
    public string? Description { get; set; }
} 