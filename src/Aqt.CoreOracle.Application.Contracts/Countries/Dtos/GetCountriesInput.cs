using Volo.Abp.Application.Dtos;

namespace Aqt.CoreOracle.Application.Contracts.Countries.Dtos;

public class GetCountriesInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
} 