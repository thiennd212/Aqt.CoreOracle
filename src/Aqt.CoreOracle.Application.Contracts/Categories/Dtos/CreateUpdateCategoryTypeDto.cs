using System.ComponentModel.DataAnnotations;

namespace Aqt.CoreOracle.Categories.Dtos;

public class CreateUpdateCategoryTypeDto
{
    [Required]
    [StringLength(50)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public bool AllowMultipleSelect { get; set; }

    public bool IsSystem { get; set; }
} 