using System;
using Volo.Abp.Application.Dtos;

namespace Aqt.CoreOracle.Application.Contracts.Provinces.Dtos;

public class ProvinceDto : AuditedEntityDto<Guid>
{
    public Guid CountryId { get; set; }
    public string CountryName { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
} 