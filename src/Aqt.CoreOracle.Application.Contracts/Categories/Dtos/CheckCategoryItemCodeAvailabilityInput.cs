using System;
using System.ComponentModel.DataAnnotations;

namespace Aqt.CoreOracle.Categories;

public class CheckCategoryItemCodeAvailabilityInput
{
    [Required]
    public Guid CategoryTypeId { get; set; }

    [Required]
    [StringLength(50)]
    public string Code { get; set; }

    public Guid? ExpectedId { get; set; }
} 