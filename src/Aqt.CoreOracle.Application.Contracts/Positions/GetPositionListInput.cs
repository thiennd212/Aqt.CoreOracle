using Volo.Abp.Application.Dtos;

namespace Aqt.CoreOracle.Application.Contracts.Positions;

public class GetPositionListInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
} 