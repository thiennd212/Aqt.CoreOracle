using System;
using Volo.Abp.Application.Dtos;

namespace Aqt.CoreOracle.Application.Contracts.Countries.Dtos;

public class CountryLookupDto : EntityDto<Guid>
{
    public string Name { get; set; }
} 