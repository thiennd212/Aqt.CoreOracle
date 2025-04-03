# Phân quyền (Permissions)

## Tổng quan
Module quản lý danh mục sử dụng hệ thống phân quyền của ABP Framework để kiểm soát truy cập vào các chức năng. Mỗi thao tác đều được bảo vệ bởi một permission cụ thể.

## Cấu trúc Permission
### 1. CategoryTypes
| Permission | Key | Description |
|------------|-----|-------------|
| View | CoreOracle.CategoryTypes.Default | Xem danh sách loại danh mục |
| Create | CoreOracle.CategoryTypes.Create | Tạo mới loại danh mục |
| Edit | CoreOracle.CategoryTypes.Edit | Chỉnh sửa loại danh mục |
| Delete | CoreOracle.CategoryTypes.Delete | Xóa loại danh mục |
| Export | CoreOracle.CategoryTypes.Export | Xuất dữ liệu loại danh mục |
| Import | CoreOracle.CategoryTypes.Import | Nhập dữ liệu loại danh mục |
| Manage | CoreOracle.CategoryTypes.Manage | Quản lý cấu hình loại danh mục |

### 2. CategoryItems
| Permission | Key | Description |
|------------|-----|-------------|
| View | CoreOracle.CategoryItems.Default | Xem danh sách danh mục |
| Create | CoreOracle.CategoryItems.Create | Tạo mới danh mục |
| Edit | CoreOracle.CategoryItems.Edit | Chỉnh sửa danh mục |
| Delete | CoreOracle.CategoryItems.Delete | Xóa danh mục |
| Export | CoreOracle.CategoryItems.Export | Xuất dữ liệu danh mục |
| Import | CoreOracle.CategoryItems.Import | Nhập dữ liệu danh mục |
| Manage | CoreOracle.CategoryItems.Manage | Quản lý cấu hình danh mục |
| ReOrder | CoreOracle.CategoryItems.ReOrder | Sắp xếp thứ tự danh mục |

## Cấu hình Permission
```csharp
public class CoreOraclePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var categoryGroup = context.AddGroup(CoreOraclePermissions.GroupName);

        var categoryTypes = categoryGroup.AddPermission(CoreOraclePermissions.CategoryTypes.Default);
        categoryTypes.AddChild(CoreOraclePermissions.CategoryTypes.Create);
        categoryTypes.AddChild(CoreOraclePermissions.CategoryTypes.Edit);
        categoryTypes.AddChild(CoreOraclePermissions.CategoryTypes.Delete);
        categoryTypes.AddChild(CoreOraclePermissions.CategoryTypes.Export);
        categoryTypes.AddChild(CoreOraclePermissions.CategoryTypes.Import);
        categoryTypes.AddChild(CoreOraclePermissions.CategoryTypes.Manage);

        var categoryItems = categoryGroup.AddPermission(CoreOraclePermissions.CategoryItems.Default);
        categoryItems.AddChild(CoreOraclePermissions.CategoryItems.Create);
        categoryItems.AddChild(CoreOraclePermissions.CategoryItems.Edit);
        categoryItems.AddChild(CoreOraclePermissions.CategoryItems.Delete);
        categoryItems.AddChild(CoreOraclePermissions.CategoryItems.Export);
        categoryItems.AddChild(CoreOraclePermissions.CategoryItems.Import);
        categoryItems.AddChild(CoreOraclePermissions.CategoryItems.Manage);
        categoryItems.AddChild(CoreOraclePermissions.CategoryItems.ReOrder);
    }
}
```

## Sử dụng Permission

### 1. Trong Application Service
```csharp
[Authorize(CoreOraclePermissions.CategoryItems.Default)]
public class CategoryItemAppService : ApplicationService
{
    [Authorize(CoreOraclePermissions.CategoryItems.Create)]
    public async Task<CategoryItemDto> CreateAsync(CreateCategoryItemDto input)
    {
        await ValidatePermissionsAsync(input);
        // Implementation
    }

    [Authorize(CoreOraclePermissions.CategoryItems.Edit)]
    public async Task<CategoryItemDto> UpdateAsync(Guid id, UpdateCategoryItemDto input)
    {
        await ValidatePermissionsAsync(id, input);
        // Implementation
    }

    [Authorize(CoreOraclePermissions.CategoryItems.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await ValidatePermissionsAsync(id);
        // Implementation
    }

    [Authorize(CoreOraclePermissions.CategoryItems.Export)]
    public async Task<byte[]> ExportAsync(CategoryItemExportDto input)
    {
        // Implementation
    }

    [Authorize(CoreOraclePermissions.CategoryItems.Import)]
    public async Task<ImportResultDto> ImportAsync(IFormFile file)
    {
        // Implementation
    }

    [Authorize(CoreOraclePermissions.CategoryItems.ReOrder)]
    public async Task ReOrderAsync(ReOrderCategoryItemsDto input)
    {
        // Implementation
    }

    private async Task ValidatePermissionsAsync(Guid id)
    {
        var item = await _repository.GetAsync(id);
        if (item.TenantId != CurrentTenant.Id)
        {
            throw new AbpAuthorizationException();
        }
    }
}
```

### 2. Trong Razor Pages
```csharp
public class IndexModel : CoreOraclePageModel
{
    public bool CanExport { get; set; }
    public bool CanImport { get; set; }
    public bool CanReOrder { get; set; }

    public async Task OnGetAsync()
    {
        await CheckPermissionAsync(CoreOraclePermissions.CategoryItems.Default);
        
        CanExport = await AuthorizationService.IsGrantedAsync(CoreOraclePermissions.CategoryItems.Export);
        CanImport = await AuthorizationService.IsGrantedAsync(CoreOraclePermissions.CategoryItems.Import);
        CanReOrder = await AuthorizationService.IsGrantedAsync(CoreOraclePermissions.CategoryItems.ReOrder);
    }
}
```

```html
@if (await AuthorizationService.IsGrantedAsync(CoreOraclePermissions.CategoryItems.Create))
{
    <abp-button id="NewCategoryItemButton"
                text="@L["NewCategoryItem"].Value"
                icon="plus"
                button-type="Primary"/>
}

@if (Model.CanExport)
{
    <abp-button id="ExportButton"
                text="@L["Export"].Value"
                icon="download"
                button-type="Info"/>
}

@if (Model.CanImport)
{
    <abp-button id="ImportButton"
                text="@L["Import"].Value"
                icon="upload"
                button-type="Info"/>
}

@if (Model.CanReOrder)
{
    <abp-button id="ReOrderButton"
                text="@L["ReOrder"].Value"
                icon="sort"
                button-type="Info"/>
}
```

### 3. Trong JavaScript
```javascript
// Check multiple permissions
const canManage = await abp.auth.isGrantedAsync('CoreOracle.CategoryItems.Manage');
const canReOrder = await abp.auth.isGrantedAsync('CoreOracle.CategoryItems.ReOrder');

// Check any of multiple permissions
const canExportOrImport = await abp.auth.areAnyGrantedAsync([
    'CoreOracle.CategoryItems.Export',
    'CoreOracle.CategoryItems.Import'
]);

// Check all permissions
const hasAllPermissions = await abp.auth.areAllGrantedAsync([
    'CoreOracle.CategoryItems.Edit',
    'CoreOracle.CategoryItems.Delete'
]);
```

## Kiểm tra Permission
### 1. Sử dụng IAuthorizationService
```csharp
public class CategoryItemAppService : ApplicationService
{
    private readonly IAuthorizationService _authorizationService;

    public async Task DoSomethingAsync()
    {
        if (await _authorizationService.IsGrantedAsync(CoreOraclePermissions.CategoryItems.Edit))
        {
            // Do something
        }

        // Check multiple permissions
        var authResult = await _authorizationService.AuthorizeAsync(CoreOraclePermissions.CategoryItems.Edit);
        if (authResult.Succeeded)
        {
            // Do something
        }
        else
        {
            // Handle unauthorized
            throw new AbpAuthorizationException(authResult.FailureMessage);
        }
    }
}
```

### 2. Sử dụng IPermissionChecker
```csharp
public class CategoryItemManager : DomainService
{
    private readonly IPermissionChecker _permissionChecker;

    public async Task DoSomethingAsync()
    {
        // Check single permission
        if (await _permissionChecker.IsGrantedAsync(CoreOraclePermissions.CategoryItems.Edit))
        {
            // Do something
        }

        // Check multiple permissions
        if (await _permissionChecker.IsGrantedAsync(CoreOraclePermissions.CategoryItems.Edit) &&
            await _permissionChecker.IsGrantedAsync(CoreOraclePermissions.CategoryItems.Delete))
        {
            // Do something
        }

        // Check for specific user
        var userId = CurrentUser.Id;
        if (await _permissionChecker.IsGrantedAsync(userId, CoreOraclePermissions.CategoryItems.Edit))
        {
            // Do something
        }
    }
}
```

## Best Practices
1. Luôn sử dụng permission constants thay vì hard-coded strings
2. Kiểm tra permission ở cả client và server side
3. Sử dụng [Authorize] attribute cho tất cả public methods
4. Implement proper error handling cho permission denied
5. Cache permission checks khi cần thiết
6. Sử dụng policy-based authorization cho complex rules
7. Implement multi-tenancy support trong permission checks
8. Sử dụng resource-based authorization khi cần thiết
9. Regular audit của permission assignments
10. Implement proper logging cho permission checks

## Permission Management UI
ABP Framework cung cấp sẵn UI để quản lý permissions:
1. Truy cập `/Identity/Users` hoặc `/Identity/Roles`
2. Click vào nút Permissions
3. Cấu hình permissions cho user/role
4. Hỗ trợ phân quyền theo tenant
5. Hỗ trợ import/export permission settings
6. Hỗ trợ permission inheritance

## Security Considerations
1. Không expose permission keys trong error messages
2. Validate permissions ở cả client và server side
3. Implement proper logging cho permission checks
4. Regular audit của permission assignments
5. Implement principle of least privilege
6. Sử dụng resource-based authorization khi cần thiết
7. Implement proper error handling cho permission denied
8. Cache permission checks một cách hợp lý
9. Implement rate limiting cho permission checks
10. Regular review của permission assignments 