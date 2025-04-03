using System;
using Volo.Abp.Application.Dtos;

namespace Aqt.CoreOracle.Categories.Dtos;

public class CategoryItemGetListInput : PagedAndSortedResultRequestDto
{
    public Guid CategoryTypeId { get; set; }
    public string? Filter { get; set; }
    public bool? IsActive { get; set; }
} 