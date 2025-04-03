using System;
using Volo.Abp.Application.Dtos;

namespace Aqt.CoreOracle.Categories;

public class CategoryItemLookupDto : EntityDto<Guid>
{
    public string Code { get; set; }
    
    public string Name { get; set; }
    
    public Guid CategoryTypeId { get; set; }
    
    public string CategoryTypeCode { get; set; }
    
    public string CategoryTypeName { get; set; }
} 