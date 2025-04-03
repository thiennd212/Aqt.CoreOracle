using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Aqt.CoreOracle.Categories;

public class CategoryItem : FullAuditedEntity<Guid>
{
    public Guid CategoryTypeId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public Guid? ParentId { get; set; }
    public bool IsActive { get; set; }
    public string Value { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string ExtraProperties { get; set; } = string.Empty;
    
    public virtual CategoryType? CategoryType { get; set; }
    public virtual CategoryItem? Parent { get; set; }
    public virtual ICollection<CategoryItem> Children { get; set; }
    
    protected CategoryItem()
    {
        Children = new List<CategoryItem>();
    }
    
    public CategoryItem(
        Guid id,
        Guid categoryTypeId,
        string code,
        string name,
        string description = "",
        int displayOrder = 0,
        Guid? parentId = null,
        bool isActive = true,
        string value = "",
        string icon = "",
        string extraProperties = "") : base(id)
    {
        SetCode(code);
        SetName(name);
        CategoryTypeId = categoryTypeId;
        Description = description;
        DisplayOrder = displayOrder;
        ParentId = parentId;
        IsActive = isActive;
        Value = value;
        Icon = icon;
        ExtraProperties = extraProperties;
        Children = new List<CategoryItem>();
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