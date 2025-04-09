using Aqt.CoreOracle;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Aqt.CoreOracle.Domain.Positions;

public class Position : FullAuditedAggregateRoot<Guid>
{
    [Required]
    [MaxLength(CoreOracleConsts.MaxPositionNameLength)]
    public string Name { get; set; }

    [Required]
    [MaxLength(CoreOracleConsts.MaxPositionCodeLength)]
    public string Code { get; set; }

    [MaxLength(CoreOracleConsts.MaxDescriptionLength)]
    public string? Description { get; set; }

    protected Position()
    {
        Name = default!;
        Code = default!;
    }

    public Position(
        Guid id,
        [NotNull] string name,
        [NotNull] string code,
        [AllowNull] string? description = null
    ) : base(id)
    {
        SetName(name);
        SetCode(code);
        Description = description;
    }

    internal Position SetName([NotNull] string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), CoreOracleConsts.MaxPositionNameLength);
        return this;
    }

    internal Position SetCode([NotNull] string code)
    {
        Code = Check.NotNullOrWhiteSpace(code, nameof(code), CoreOracleConsts.MaxPositionCodeLength);
        return this;
    }
} 