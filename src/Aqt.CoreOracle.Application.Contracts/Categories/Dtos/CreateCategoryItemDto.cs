using System;
using System.ComponentModel.DataAnnotations;

namespace Aqt.CoreOracle.Categories.Dtos;

public class CreateCategoryItemDto
{
    public Guid Id { get; set; }

    [Required]
    public Guid CategoryTypeId { get; set; }

    [Required]
    [StringLength(50)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }

    public Guid? ParentId { get; set; }

    public bool IsActive { get; set; } = true;

    [StringLength(500)]
    public string Value { get; set; } = string.Empty;

    [StringLength(100)]
    public string Icon { get; set; } = string.Empty;

    [StringLength(2000)]
    public string ExtraProperties { get; set; } = string.Empty;
} 