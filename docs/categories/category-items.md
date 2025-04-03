# Quản lý Danh mục (Category Items)

## Tổng quan
Module quản lý danh mục cho phép tạo và quản lý các danh mục theo cấu trúc phân cấp. Mỗi danh mục thuộc về một loại danh mục (Category Type) và có thể có danh mục cha.

## Đặc điểm chính
1. Cấu trúc phân cấp không giới hạn độ sâu
2. Hỗ trợ sắp xếp thứ tự hiển thị
3. Quản lý trạng thái hoạt động
4. Hỗ trợ thêm thuộc tính mở rộng
5. Tích hợp đầy đủ với ABP Framework
6. Hỗ trợ đa ngôn ngữ
7. Tích hợp với Oracle Database 19c

## Cấu trúc dữ liệu
```csharp
public class CategoryItem : FullAuditedAggregateRoot<Guid>
{
    public virtual Guid CategoryTypeId { get; set; }
    public virtual string Code { get; set; }
    public virtual string Name { get; set; }
    public virtual Guid? ParentId { get; set; }
    public virtual int DisplayOrder { get; set; }
    public virtual bool IsActive { get; set; }
    public virtual string Value { get; set; }
    public virtual string Icon { get; set; }
    
    private readonly List<CategoryItem> _children;
    public virtual IReadOnlyList<CategoryItem> Children => _children.AsReadOnly();

    protected CategoryItem()
    {
        _children = new List<CategoryItem>();
    }
}
```

## API Endpoints

### 1. Query APIs
```csharp
// GET /api/app/category-items
[HttpGet]
[Authorize(CoreOraclePermissions.CategoryItems.Default)]
public async Task<PagedResultDto<CategoryItemDto>> GetListAsync(GetCategoryItemsInput input)

// GET /api/app/category-items/{id}
[HttpGet]
[Route("{id}")]
[Authorize(CoreOraclePermissions.CategoryItems.Default)]
public async Task<CategoryItemDto> GetAsync(Guid id)

// GET /api/app/category-items/by-type/{typeId}
[HttpGet]
[Route("by-type/{typeId}")]
[Authorize(CoreOraclePermissions.CategoryItems.Default)]
public async Task<ListResultDto<CategoryItemDto>> GetByTypeAsync(Guid typeId)

// GET /api/app/category-items/hierarchy/{typeId}
[HttpGet]
[Route("hierarchy/{typeId}")]
[Authorize(CoreOraclePermissions.CategoryItems.Default)]
public async Task<ListResultDto<CategoryItemHierarchyDto>> GetHierarchyAsync(Guid typeId)
```

### 2. Command APIs
```csharp
// POST /api/app/category-items
[HttpPost]
[Authorize(CoreOraclePermissions.CategoryItems.Create)]
public async Task<CategoryItemDto> CreateAsync(CreateCategoryItemDto input)

// PUT /api/app/category-items/{id}
[HttpPut]
[Route("{id}")]
[Authorize(CoreOraclePermissions.CategoryItems.Edit)]
public async Task<CategoryItemDto> UpdateAsync(Guid id, UpdateCategoryItemDto input)

// DELETE /api/app/category-items/{id}
[HttpDelete]
[Route("{id}")]
[Authorize(CoreOraclePermissions.CategoryItems.Delete)]
public async Task DeleteAsync(Guid id)

// POST /api/app/category-items/reorder
[HttpPost]
[Route("reorder")]
[Authorize(CoreOraclePermissions.CategoryItems.ReOrder)]
public async Task ReOrderAsync(ReOrderCategoryItemsDto input)

// POST /api/app/category-items/import
[HttpPost]
[Route("import")]
[Authorize(CoreOraclePermissions.CategoryItems.Import)]
public async Task<ImportResultDto> ImportAsync(IFormFile file)

// GET /api/app/category-items/export
[HttpGet]
[Route("export")]
[Authorize(CoreOraclePermissions.CategoryItems.Export)]
public async Task<FileResult> ExportAsync(CategoryItemExportDto input)
```

## Validation Rules
1. Code:
   - Bắt buộc
   - Độ dài 1-50 ký tự
   - Chỉ chấp nhận chữ cái, số và dấu gạch dưới
   - Không trùng lặp trong cùng CategoryType

2. Name:
   - Bắt buộc
   - Độ dài 1-100 ký tự
   - Hỗ trợ Unicode

3. ParentId:
   - Phải thuộc cùng CategoryType
   - Không được tạo vòng lặp trong cây phân cấp
   - Không được chọn chính nó làm cha

4. DisplayOrder:
   - Giá trị số nguyên không âm
   - Mặc định là 0

5. Value:
   - Độ dài tối đa 500 ký tự
   - Có thể null

6. Icon:
   - Độ dài tối đa 100 ký tự
   - Có thể null

## Xử lý lỗi
```csharp
public class CategoryItemBusinessException : BusinessException
{
    public CategoryItemBusinessException(string code, string message)
        : base(code, message)
    {
    }
}

public static class CategoryItemErrorCodes
{
    public const string DuplicateCode = "CoreOracle:CategoryItem:001";
    public const string InvalidParent = "CoreOracle:CategoryItem:002";
    public const string CircularReference = "CoreOracle:CategoryItem:003";
    public const string InvalidDisplayOrder = "CoreOracle:CategoryItem:004";
    public const string NotFound = "CoreOracle:CategoryItem:005";
}
```

## Caching
```csharp
public class CategoryItemCache : ITransientDependency
{
    private readonly IDistributedCache<CategoryItemCacheItem> _cache;
    private readonly IDistributedCache<List<CategoryItemCacheItem>> _listCache;

    public async Task<CategoryItemCacheItem> GetAsync(Guid id)
    {
        return await _cache.GetOrAddAsync(
            id.ToString(),
            async () => await GetFromDatabaseAsync(id),
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30)
            }
        );
    }

    public async Task<List<CategoryItemCacheItem>> GetListAsync(Guid typeId)
    {
        return await _listCache.GetOrAddAsync(
            $"type-{typeId}",
            async () => await GetListFromDatabaseAsync(typeId),
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30)
            }
        );
    }
}
```

## Performance Optimization
1. Sử dụng Materialized Path cho truy vấn cây phân cấp
2. Index cho các trường thường xuyên tìm kiếm
3. Caching cho danh sách và chi tiết
4. Lazy loading cho các thuộc tính không cần thiết
5. Batch processing cho import/export
6. Sử dụng Oracle Database features:
   - Partitioning theo CategoryTypeId
   - Result Cache cho các query thường xuyên
   - Bitmap Index cho IsActive
   - B-tree Index cho Code và ParentId

## Best Practices
1. Sử dụng Repository Pattern
2. Implement Unit of Work
3. Validate dữ liệu ở nhiều layer
4. Xử lý lỗi phù hợp
5. Ghi log đầy đủ
6. Tối ưu performance
7. Tuân thủ coding standards
8. Viết unit tests đầy đủ
9. Sử dụng dependency injection
10. Implement caching strategy

## Testing
```csharp
public class CategoryItemTests : CoreOracleTestBase
{
    private readonly ICategoryItemRepository _categoryItemRepository;
    private readonly CategoryItemManager _categoryItemManager;

    [Fact]
    public async Task Should_Create_CategoryItem()
    {
        // Arrange
        var input = new CreateCategoryItemDto
        {
            Code = "TEST-001",
            Name = "Test Category",
            CategoryTypeId = _testCategoryType.Id
        };

        // Act
        var result = await _categoryItemManager.CreateAsync(input);

        // Assert
        result.ShouldNotBeNull();
        result.Code.ShouldBe(input.Code);
        result.Name.ShouldBe(input.Name);
    }
}
```

## Localization
```json
{
  "culture": "vi",
  "texts": {
    "CategoryItem": "Danh mục",
    "CategoryItemCode": "Mã danh mục",
    "CategoryItemName": "Tên danh mục",
    "CategoryItemParent": "Danh mục cha",
    "CategoryItemDisplayOrder": "Thứ tự hiển thị",
    "CategoryItemIsActive": "Trạng thái hoạt động",
    "CategoryItemValue": "Giá trị",
    "CategoryItemIcon": "Icon",
    "CategoryItemDuplicateCode": "Mã danh mục đã tồn tại",
    "CategoryItemInvalidParent": "Danh mục cha không hợp lệ",
    "CategoryItemCircularReference": "Không thể tạo vòng lặp trong cây phân cấp",
    "CategoryItemInvalidDisplayOrder": "Thứ tự hiển thị không hợp lệ",
    "CategoryItemNotFound": "Không tìm thấy danh mục"
  }
}
```

## Security
1. Phân quyền chi tiết theo từng thao tác
2. Validate dữ liệu đầu vào
3. Xử lý lỗi an toàn
4. Ghi log đầy đủ
5. Sử dụng HTTPS
6. Implement rate limiting
7. Validate file upload
8. Xử lý concurrent access
9. Implement audit logging
10. Regular security review 