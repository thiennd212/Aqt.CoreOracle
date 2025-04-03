# Hướng dẫn sử dụng Template

## Giới thiệu

Template này cung cấp một bộ mã nguồn mẫu để tạo nhanh các chức năng quản lý danh mục trong dự án CoreOracle. Template được thiết kế theo chuẩn ABP Framework và tuân thủ các best practices.

## Cấu trúc thư mục

```
templates/
├── code-templates/
│   ├── domain/                 # Domain layer templates
│   │   ├── entity-template.cs
│   │   └── repository-interface-template.cs
│   ├── infrastructure/         # Infrastructure layer templates
│   │   └── repository-template.cs
│   ├── application/           # Application layer templates
│   │   ├── dtos-template.cs
│   │   ├── app-service-interface-template.cs
│   │   ├── app-service-template.cs
│   │   ├── auto-mapper-profile-template.cs
│   │   └── permissions-template.cs
│   ├── presentation/          # Presentation layer templates
│   │   ├── index-page-template.cshtml
│   │   ├── index-page-model-template.cs
│   │   ├── index-js-template.js
│   │   ├── create-modal-template.cshtml
│   │   ├── create-modal-model-template.cs
│   │   ├── edit-modal-template.cshtml
│   │   └── edit-modal-model-template.cs
│   └── localization/          # Localization templates
│       └── localization-template.json
└── feature-implementation-guide.md
```

## Cách sử dụng

1. **Chuẩn bị**
   - Xác định tên module và entity cần tạo
   - Xác định các thuộc tính và validation rules
   - Xác định các business rules

2. **Thay thế placeholder**
   - Thay thế `[ModuleName]` bằng tên module (ví dụ: Categories)
   - Thay thế `[EntityName]` bằng tên entity (ví dụ: CategoryItem)
   - Thay thế `[entityName]` bằng tên entity viết thường (ví dụ: categoryItem)
   - Thay thế `moduleName` bằng tên module viết thường (ví dụ: categories)

3. **Tạo cấu trúc thư mục**
   ```
   src/Aqt.CoreOracle.Domain/[ModuleName]/
   src/Aqt.CoreOracle.Domain.Shared/[ModuleName]/
   src/Aqt.CoreOracle.Application/[ModuleName]/
   src/Aqt.CoreOracle.Application.Contracts/[ModuleName]/
   src/Aqt.CoreOracle.EntityFrameworkCore/[ModuleName]/
   src/Aqt.CoreOracle.Web/Pages/[ModuleName]/[EntityName]s/
   ```

4. **Copy và điều chỉnh template**
   - Copy từng file template vào thư mục tương ứng
   - Điều chỉnh namespace, tên class và các placeholder
   - Thêm/sửa các thuộc tính theo yêu cầu
   - Cập nhật validation rules
   - Thêm business rules cụ thể

5. **Cập nhật Module**
   - Thêm DbSet vào DbContext
   - Đăng ký AutoMapper Profile
   - Cập nhật menu
   - Thêm localization strings

6. **Kiểm tra và test**
   - Build solution
   - Chạy migration
   - Test CRUD operations
   - Kiểm tra validation
   - Kiểm tra permissions
   - Test UI/UX

## Best Practices

1. **Naming Conventions**
   - Sử dụng PascalCase cho tên class, interface
   - Sử dụng camelCase cho tên biến, parameter
   - Thêm tiền tố I cho interface
   - Sử dụng hậu tố Dto cho Data Transfer Objects

2. **Code Organization**
   - Mỗi entity một thư mục riêng
   - Tách biệt rõ ràng các layer
   - Sử dụng regions cho code dễ đọc
   - Comment đầy đủ cho methods và properties

3. **Validation**
   - Validate ở cả client và server side
   - Sử dụng Data Annotations
   - Implement custom validation khi cần
   - Throw specific exceptions

4. **Security**
   - Implement đầy đủ permissions
   - Authorize ở cả API và UI
   - Validate input data
   - Tránh SQL injection

5. **Performance**
   - Sử dụng async/await
   - Implement paging cho lists
   - Index các cột tìm kiếm
   - Cache data khi cần thiết

## Lưu ý

- Đảm bảo unique constraint cho Code
- Implement soft delete nếu cần
- Thêm logging cho các operations quan trọng
- Viết unit tests
- Cập nhật documentation
- Review code trước khi commit

## Ví dụ

Xem thêm ví dụ cụ thể trong thư mục `examples/` (nếu có). 