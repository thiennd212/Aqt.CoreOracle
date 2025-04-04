using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace Aqt.CoreOracle.Categories;

public class CategoryType : FullAuditedAggregateRoot<Guid>
{
    [Required]
    [StringLength(50)]
    public virtual string Code { get; set; }

    [Required]
    [StringLength(100)]
    public virtual string Name { get; set; }

    [StringLength(500)]
    public virtual string Description { get; set; }

    public virtual bool IsActive { get; set; }

    public virtual bool AllowMultipleSelect { get; set; }

    public virtual bool IsSystem { get; set; }
    
    public virtual ICollection<CategoryItem> Items { get; set; }
    
    protected CategoryType()
    {
        Items = new List<CategoryItem>();
    }
    
    public CategoryType(
        Guid id,
        string code,
        string name,
        string description = "",
        bool isActive = true,
        bool allowMultipleSelect = false,
        bool isSystem = false) : base(id)
    {
        SetCode(code);
        SetName(name);
        Description = description;
        IsActive = isActive;
        AllowMultipleSelect = allowMultipleSelect;
        IsSystem = isSystem;
        Items = new List<CategoryItem>();
    }

    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetCode(string code)
    {
        Code = code;
    }
} 