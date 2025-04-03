using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Aqt.CoreOracle.Categories.Dtos;

public class CategoryTypeDto : AuditedEntityDto<Guid>
{
    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public bool AllowMultipleSelect { get; set; }

    public bool IsSystem { get; set; }

    public List<CategoryItemDto> Items { get; set; } = new();
} 