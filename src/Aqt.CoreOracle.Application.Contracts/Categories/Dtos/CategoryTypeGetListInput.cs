using Volo.Abp.Application.Dtos;

namespace Aqt.CoreOracle.Categories.Dtos;

public class CategoryTypeGetListInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
    public bool? IsActive { get; set; }
} 