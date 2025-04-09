using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;
using Aqt.CoreOracle.Domain.Shared.OrganizationUnits;

namespace Aqt.CoreOracle.Application.Contracts.OrganizationUnits;

public class CreateOrganizationUnitInput
{
    public Guid? ParentId { get; set; }

    [Required]
    [StringLength(CoreOracleConsts.MaxOrganizationUnitDisplayNameLength)]
    public string DisplayName { get; set; }

    [StringLength(CoreOracleConsts.MaxOrganizationUnitAddressLength)]
    public string? Address { get; set; }

    [StringLength(CoreOracleConsts.MaxOrganizationUnitSyncCodeLength)]
    public string? SyncCode { get; set; }

    [Required]
    public OrganizationUnitTypeEnum OrganizationUnitType { get; set; }
} 