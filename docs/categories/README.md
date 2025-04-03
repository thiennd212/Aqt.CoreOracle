# Module Quản lý Danh mục (Category Management)

## Tổng quan
Module quản lý danh mục cho phép người dùng tạo và quản lý các danh mục phân cấp trong hệ thống. Mỗi danh mục thuộc về một loại danh mục cụ thể và có thể có danh mục cha.

## Menu và Navigation
### 1. Menu Chính
- **Categories** (fa fa-list)
  - **CategoryTypes** (fa fa-folder): Quản lý loại danh mục
  - **CategoryItems** (fa fa-list-alt): Quản lý danh mục

### 2. Phân quyền Menu
- CategoryTypes: Yêu cầu quyền `CoreOraclePermissions.CategoryTypes.Default`
- CategoryItems: Yêu cầu quyền `CoreOraclePermissions.CategoryItems.Default`

## Giới thiệu
Module quản lý danh mục cung cấp khả năng tạo và quản lý các danh mục phân cấp trong hệ thống. Module này bao gồm hai phần chính:
1. Quản lý loại danh mục (CategoryType)
2. Quản lý danh mục (CategoryItem)

## Tính năng chính
- Quản lý loại danh mục với các thuộc tính: mã, tên, mô tả, trạng thái
- Quản lý danh mục phân cấp với các thuộc tính: mã, tên, loại danh mục, danh mục cha, thứ tự hiển thị, trạng thái
- Hỗ trợ phân quyền chi tiết cho từng thao tác
- Validation chặt chẽ ở cả client và server
- Tìm kiếm và lọc theo nhiều tiêu chí
- Giao diện thân thiện, dễ sử dụng
- Xử lý lỗi và exception handling đầy đủ
- Hỗ trợ caching để tối ưu performance
- Hỗ trợ soft delete và data auditing
- Hỗ trợ concurrent access với optimistic concurrency

## Xử lý lỗi và Validation
### Server-side Validation
- Sử dụng FluentValidation cho business rules
- Implement custom exception classes cho từng loại lỗi
- Xử lý concurrent access với concurrency stamps
- Logging đầy đủ cho mọi lỗi

### Client-side Validation
- Sử dụng jQuery Validation
- Implement dynamic form validation
- Hiển thị thông báo lỗi thân thiện
- Hỗ trợ validation messages đa ngôn ngữ

## Cấu trúc thư mục
```
src/
├── Aqt.CoreOracle.Domain/
│   └── Categories/
│       ├── CategoryType.cs
│       ├── CategoryItem.cs
│       ├── ICategoryTypeRepository.cs
│       └── ICategoryItemRepository.cs
├── Aqt.CoreOracle.Application/
│   └── Categories/
│       ├── CategoryTypeAppService.cs
│       └── CategoryItemAppService.cs
└── Aqt.CoreOracle.Web/
    └── Pages/
        └── Categories/
            ├── CategoryTypes/
            └── CategoryItems/
```

## Tài liệu chi tiết
- [Quản lý loại danh mục](./category-types.md)
- [Quản lý danh mục](./category-items.md)
- [API Reference](./api-reference.md)
- [Database Schema](./database-schema.md)
- [Permissions](./permissions.md)
- [Validation Rules](./validation-rules.md)
- [UI Components](./ui-components.md)

## Hướng dẫn cài đặt
1. Thêm module vào `CoreOracleModule`:
```csharp
[DependsOn(typeof(AbpCategoryManagementModule))]
public class CoreOracleModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpCategoryManagementOptions>(options =>
        {
            options.MaxDisplayOrder = 1000;
            options.MaxCodeLength = 50;
            options.MaxNameLength = 100;
            options.MaxDescriptionLength = 2000;
        });
        
        Configure<AbpExceptionHandlingOptions>(options =>
        {
            options.SendExceptionsDetailsToClients = true;
            options.SendStackTraceToClients = false;
        });
    }
}
```

2. Thêm migration cho database:
```bash
dotnet ef migrations add Added_CategoryManagement
dotnet ef database update
```

3. Cấu hình permissions trong `CoreOraclePermissionDefinitionProvider`

4. Thêm menu items trong `CoreOracleMenuContributor`

## Cấu hình
Các cấu hình có thể tùy chỉnh trong `appsettings.json`:
```json
{
  "CategoryManagement": {
    "MaxDisplayOrder": 1000,
    "MaxCodeLength": 50,
    "MaxNameLength": 100,
    "MaxDescriptionLength": 2000,
    "EnableCaching": true,
    "CacheTimeout": 30,
    "EnableSoftDelete": true,
    "EnableAuditing": true,
    "EnableConcurrencyCheck": true
  },
  "ExceptionHandling": {
    "ShowDetails": true,
    "ShowStackTrace": false
  }
}
```

## Performance Optimization
1. Caching
   - Second level caching cho entities
   - Distributed caching cho lookup data
   - Query caching cho read-heavy operations

2. Database
   - Proper indexing
   - Query optimization
   - Connection pooling

3. Validation
   - Client-side validation
   - Server-side caching của validation rules
   - Batch validation cho bulk operations 