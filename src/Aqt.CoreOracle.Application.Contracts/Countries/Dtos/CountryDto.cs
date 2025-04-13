using System;
using Volo.Abp.Application.Dtos;

namespace Aqt.CoreOracle.Application.Contracts.Countries.Dtos;

public class CountryDto : AuditedEntityDto<Guid>
{
    public string Code { get; set; }
    public string Name { get; set; }
} 