using System;
using Volo.Abp.Application.Dtos;

namespace Aqt.CoreOracle.Categories.Dtos;

public class CategoryItemDto : AuditedEntityDto<Guid>
{
    public Guid CategoryTypeId { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }

    public Guid? ParentId { get; set; }

    public bool IsActive { get; set; }

    public string Value { get; set; } = string.Empty;

    public string Icon { get; set; } = string.Empty;

    public string ExtraProperties { get; set; } = string.Empty;

    public CategoryTypeDto? CategoryType { get; set; }

    public CategoryItemDto? Parent { get; set; }
} 