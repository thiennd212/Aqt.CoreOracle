using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Aqt.CoreOracle.Categories;

public class CategoryType : FullAuditedAggregateRoot<Guid>
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool AllowMultipleSelect { get; set; }
    public bool IsSystem { get; set; }
    
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
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"{nameof(name)} cannot be null, empty or white space!", nameof(name));
        }

        Name = name;
    }

    public void SetCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException($"{nameof(code)} cannot be null, empty or white space!", nameof(code));
        }

        Code = code;
    }
} 