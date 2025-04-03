# Hướng Dẫn Sử Dụng Generate-Feature.ps1

## Giới Thiệu

Script `Generate-Feature.ps1` được sử dụng để tự động tạo code cho một tính năng mới trong dự án ABP Framework. Script này sẽ tạo đầy đủ các file cần thiết cho tất cả các layer (Domain, Application, Infrastructure, Presentation) và cập nhật các cấu hình liên quan.

## Yêu Cầu

- PowerShell 7.0 trở lên
- .NET Core SDK
- ABP Framework
- Visual Studio hoặc VS Code

## Cấu Trúc Template

Templates được đặt trong thư mục `docs/templates/code-templates` với cấu trúc sau:

```
code-templates/
├── domain/
│   ├── entity-template.cs
│   └── repository-interface-template.cs
├── infrastructure/
│   └── repository-template.cs
├── application/
│   ├── dtos-template.cs
│   ├── app-service-interface-template.cs
│   ├── app-service-template.cs
│   ├── auto-mapper-profile-template.cs
│   └── permissions-template.cs
├── presentation/
│   ├── index-page-template.cshtml
│   ├── index-page-model-template.cs
│   ├── index-js-template.js
│   ├── create-modal-template.cshtml
│   ├── create-modal-model-template.cs
│   ├── edit-modal-template.cshtml
│   └── edit-modal-model-template.cs
└── localization/
    └── localization-template.json
```

## Tham Số

| Tham số | Bắt buộc | Mô tả |
|---------|----------|--------|
| ModuleName | Có | Tên module (ví dụ: Products) |
| EntityName | Có | Tên entity (ví dụ: Product) |
| Description | Không | Mô tả cho entity |
| Force | Không | Ghi đè file nếu đã tồn tại |
| SkipBackup | Không | Bỏ qua việc backup file |
| UpdateDbContext | Không | Tự động cập nhật DbContext |
| UpdateMenu | Không | Tự động cập nhật menu |
| UpdateAutoMapper | Không | Tự động cập nhật AutoMapper profile |
| UpdatePermissions | Không | Tự động cập nhật permissions |
| UpdateNavigation | Không | Tự động thêm navigation properties |
| UpdateSeedData | Không | Tự động thêm seed data |
| UpdateDocs | Không | Tự động tạo documentation |
| CreateMigration | Không | Tự động tạo migration |
| PreviewOnly | Không | Chỉ xem trước thay đổi |
| ValidateOnly | Không | Chỉ kiểm tra templates |
| CustomTemplateDir | Không | Thư mục chứa template tùy chỉnh |
| NavigationProperties | Không | Mảng các navigation property |
| SeedData | Không | Hashtable chứa seed data |

## Ví Dụ Sử Dụng

### 1. Tạo tính năng cơ bản

```powershell
.\Generate-Feature.ps1 -ModuleName "Products" -EntityName "Product"
```

### 2. Tạo tính năng với mô tả

```powershell
.\Generate-Feature.ps1 `
    -ModuleName "Products" `
    -EntityName "Product" `
    -Description "Quản lý sản phẩm trong hệ thống"
```

### 3. Tạo tính năng với đầy đủ tính năng tự động

```powershell
.\Generate-Feature.ps1 `
    -ModuleName "Products" `
    -EntityName "Product" `
    -Description "Quản lý sản phẩm trong hệ thống" `
    -UpdateDbContext `
    -UpdateMenu `
    -UpdateAutoMapper `
    -UpdatePermissions `
    -CreateMigration
```

### 4. Tạo tính năng với navigation properties

```powershell
$navProps = @(
    "Category:Category",
    "List<OrderItem>:OrderItems"
)

.\Generate-Feature.ps1 `
    -ModuleName "Sales" `
    -EntityName "Order" `
    -UpdateNavigation `
    -NavigationProperties $navProps
```

### 5. Tạo tính năng với seed data

```powershell
$seedData = @{
    "Item1" = @(
        'name: "Product 1"',
        'price: 100',
        'isActive: true'
    )
    "Item2" = @(
        'name: "Product 2"',
        'price: 200',
        'isActive: true'
    )
}

.\Generate-Feature.ps1 `
    -ModuleName "Products" `
    -EntityName "Product" `
    -UpdateSeedData `
    -SeedData $seedData
```

### 6. Xem trước thay đổi

```powershell
.\Generate-Feature.ps1 `
    -ModuleName "Products" `
    -EntityName "Product" `
    -UpdateAll `
    -PreviewOnly
```

### 7. Kiểm tra templates

```powershell
.\Generate-Feature.ps1 `
    -ModuleName "Products" `
    -EntityName "Product" `
    -ValidateOnly
```

### 8. Sử dụng template tùy chỉnh

```powershell
.\Generate-Feature.ps1 `
    -ModuleName "Products" `
    -EntityName "Product" `
    -CustomTemplateDir "custom/templates"
```

## Các File Được Tạo

Script sẽ tạo các file sau trong cấu trúc dự án:

```
src/Aqt.CoreOracle/
├── Domain/
│   └── [ModuleName]/
│       ├── [EntityName].cs
│       └── I[EntityName]Repository.cs
├── Domain.Shared/
│   └── [ModuleName]/
├── Application/
│   └── [ModuleName]/
│       ├── [EntityName]AppService.cs
│       └── [EntityName]AutoMapperProfile.cs
├── Application.Contracts/
│   └── [ModuleName]/
│       ├── Dtos/
│       │   └── [EntityName]Dtos.cs
│       └── I[EntityName]AppService.cs
├── EntityFrameworkCore/
│   └── [ModuleName]/
│       └── EfCore[EntityName]Repository.cs
└── Web/
    └── Pages/
        └── [ModuleName]/
            └── [EntityName]s/
                ├── Index.cshtml
                ├── Index.cshtml.cs
                ├── index.js
                ├── CreateModal.cshtml
                ├── CreateModal.cshtml.cs
                ├── EditModal.cshtml
                └── EditModal.cshtml.cs
```

## Xử Lý Lỗi

1. Nếu có lỗi xảy ra trong quá trình tạo code:
   - Script sẽ tự động rollback các thay đổi
   - Các file backup sẽ được khôi phục
   - Thông báo lỗi chi tiết sẽ được hiển thị

2. Để tránh mất dữ liệu:
   - Sử dụng `-PreviewOnly` để xem trước thay đổi
   - Sử dụng `-ValidateOnly` để kiểm tra templates
   - Không tắt script khi đang chạy

## Best Practices

1. Luôn kiểm tra templates trước khi tạo code:
```powershell
.\Generate-Feature.ps1 -ModuleName "Test" -EntityName "Test" -ValidateOnly
```

2. Xem trước thay đổi trước khi áp dụng:
```powershell
.\Generate-Feature.ps1 -ModuleName "Test" -EntityName "Test" -PreviewOnly
```

3. Backup dự án trước khi chạy script với nhiều thay đổi

4. Sử dụng PascalCase cho ModuleName và EntityName

5. Kiểm tra kỹ các tham số trước khi chạy script

## Troubleshooting

1. Lỗi "Template not found":
   - Kiểm tra đường dẫn template
   - Kiểm tra tên file template
   - Sử dụng `-CustomTemplateDir` nếu cần

2. Lỗi "File already exists":
   - Sử dụng `-Force` để ghi đè
   - Hoặc xóa file cũ trước

3. Lỗi khi tạo migration:
   - Kiểm tra connection string
   - Kiểm tra Entity Framework tools
   - Chạy Update-Database thủ công

## Hỗ Trợ

Nếu bạn gặp vấn đề hoặc cần hỗ trợ:
1. Kiểm tra log lỗi
2. Chạy script với `-Verbose` để xem chi tiết
3. Liên hệ team phát triển

## Changelog

### v1.0.0
- Tính năng cơ bản
- Tạo code từ template
- Backup và rollback

### v1.1.0
- Thêm tính năng tự động cập nhật
- Preview changes
- Validate templates

### v1.2.0
- Thêm tính năng navigation properties
- Thêm tính năng seed data
- Thêm tính năng documentation

## Tạo Menu cho tính năng mới

### 1. Cấu trúc tham số
```powershell
.\Generate-Feature.ps1 `
    -FeatureName "Categories" `
    -MenuName "Categories" `
    -MenuIcon "fa fa-list" `
    -MenuOrder 2 `
    -SubMenus @(
        @{
            Name = "CategoryTypes"
            Icon = "fa fa-folder"
            Url = "/Categories/CategoryTypes"
            Permission = "CoreOracle.CategoryTypes"
        },
        @{
            Name = "CategoryItems"
            Icon = "fa fa-list-alt"
            Url = "/Categories/CategoryItems"
            Permission = "CoreOracle.CategoryItems"
        }
    )
```

### 2. Các file được tạo
- CoreOracleMenus.cs: Thêm constants cho menu
- CoreOracleMenuContributor.cs: Thêm cấu hình menu
- vi.json: Thêm localization cho menu

### 3. Ví dụ kết quả
```csharp
// CoreOracleMenus.cs
public class CoreOracleMenus
{
    private const string Prefix = "CoreOracle";
    public const string Categories = Prefix + ".Categories";
    public const string CategoryTypes = Categories + ".Types";
    public const string CategoryItems = Categories + ".Items";
}

// CoreOracleMenuContributor.cs
private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
{
    var l = context.GetLocalizer<CoreOracleResource>();

    var categoriesMenu = new ApplicationMenuItem(
        "Categories",
        l["Menu:Categories"],
        icon: "fa fa-list",
        order: 2
    );

    categoriesMenu.AddItem(
        new ApplicationMenuItem(
            "CategoryTypes",
            l["Menu:CategoryTypes"],
            url: "/Categories/CategoryTypes",
            icon: "fa fa-folder",
            requiredPermissionName: CoreOraclePermissions.CategoryTypes.Default
        )
    );

    categoriesMenu.AddItem(
        new ApplicationMenuItem(
            "CategoryItems",
            l["Menu:CategoryItems"],
            url: "/Categories/CategoryItems",
            icon: "fa fa-list-alt",
            requiredPermissionName: CoreOraclePermissions.CategoryItems.Default
        )
    );

    context.Menu.AddItem(categoriesMenu);
    return Task.CompletedTask;
}

// vi.json
{
  "Menu:Categories": "Danh mục",
  "Menu:CategoryTypes": "Loại danh mục",
  "Menu:CategoryItems": "Mục danh mục"
}
```

### 4. Kiểm tra sau khi tạo
1. Build solution để kiểm tra lỗi
2. Chạy ứng dụng để kiểm tra menu
3. Kiểm tra phân quyền
4. Kiểm tra đường dẫn
5. Kiểm tra localization 