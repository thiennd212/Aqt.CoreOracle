using System;
using Volo.Abp.Application.Dtos;

namespace Aqt.CoreOracle.Application.Contracts.Provinces.Dtos;

public class ProvinceLookupDto : EntityDto<Guid>
{
    public string Name { get; set; }
} 