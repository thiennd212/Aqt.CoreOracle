using System;
using Volo.Abp.Application.Dtos;

namespace Aqt.CoreOracle.Application.Contracts.Provinces.Dtos;

public class GetProvincesInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
    public Guid? CountryId { get; set; }
} 