using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Aqt.CoreOracle.Categories;

public class CategoryItem : FullAuditedEntity<Guid>
{
    [Required]
    public virtual Guid CategoryTypeId { get; set; }

    [Required]
    [StringLength(50)]
    public virtual string Code { get; set; }

    [Required]
    [StringLength(100)]
    public virtual string Name { get; set; }

    [StringLength(500)]
    public virtual string Description { get; set; }

    public virtual int? DisplayOrder { get; set; }

    public virtual Guid? ParentId { get; set; }

    public virtual bool IsActive { get; set; }

    [StringLength(500)]
    public virtual string Value { get; set; }

    [StringLength(100)]
    public virtual string Icon { get; set; }

    [StringLength(2000)]
    public virtual string ExtraProperties { get; set; }
    
    [ForeignKey(nameof(CategoryTypeId))]
    public virtual CategoryType CategoryType { get; set; }

    [ForeignKey(nameof(ParentId))]
    public virtual CategoryItem Parent { get; set; }

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
        int? displayOrder = null,
        Guid? parentId = null,
        bool isActive = true,
        string value = "",
        string icon = "",
        string extraProperties = "") : base(id)
    {
        CategoryTypeId = categoryTypeId;
        Code = code;
        Name = name;
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
        Name = name;
    }

    public void SetCode(string code)
    {
        Code = code;
    }
} 