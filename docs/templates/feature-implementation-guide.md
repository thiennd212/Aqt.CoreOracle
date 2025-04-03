# Hướng dẫn triển khai tính năng mới

## Giới thiệu
Document này cung cấp hướng dẫn chi tiết về cách sử dụng template để triển khai các tính năng mới trong dự án CoreOracle.

## Cấu trúc thư mục template
```
docs/templates/
├── feature-implementation-guide.md     # Hướng dẫn này
├── code-templates/                     # Các template code
│   ├── domain/                        # Domain layer templates
│   ├── application/                   # Application layer templates
│   ├── infrastructure/                # Infrastructure layer templates
│   └── presentation/                  # Presentation layer templates
└── checklists/                        # Các checklist
```

## Quy trình sử dụng template

### 1. Khởi tạo tính năng mới
1. Tạo branch mới từ develop:
```bash
git checkout -b feature/[feature-name]
```

2. Copy template cần thiết từ thư mục templates
3. Tạo thư mục cho tính năng mới theo cấu trúc chuẩn

### 2. Checklist trước khi bắt đầu
- [ ] Đã phân tích yêu cầu đầy đủ
- [ ] Đã thiết kế database schema
- [ ] Đã xác định các business rules
- [ ] Đã xác định các permissions cần thiết
- [ ] Đã lập kế hoạch testing

### 3. Các bước triển khai

#### A. Domain Layer
1. Entity
   - Copy template từ `templates/code-templates/domain/entity-template.cs`
   - Thay thế các placeholder với thông tin thực tế
   - Implement các business rules

2. Repository Interface
   - Copy template từ `templates/code-templates/domain/repository-interface-template.cs`
   - Thêm các custom methods cần thiết

#### B. Application Layer
1. DTOs
   - Copy templates từ `templates/code-templates/application/dto-templates/`
   - Customize theo yêu cầu

2. Application Service
   - Copy template interface và implementation
   - Implement business logic

#### C. Infrastructure Layer
1. Repository Implementation
   - Copy template từ `templates/code-templates/infrastructure/`
   - Implement các custom queries

#### D. Presentation Layer
1. Razor Pages
   - Copy templates từ `templates/code-templates/presentation/`
   - Customize UI theo yêu cầu

### 4. Checklist kiểm tra chất lượng
- [ ] Code tuân thủ coding standards
- [ ] Đã implement đầy đủ unit tests
- [ ] Đã implement đầy đủ integration tests
- [ ] Performance đạt yêu cầu
- [ ] Security đã được đảm bảo
- [ ] Documentation đầy đủ

## Template Code Snippets

### 1. Entity Template
```csharp
public class [EntityName] : FullAuditedAggregateRoot<Guid>
{
    // Properties
    public string Code { get; private set; }
    public string Name { get; private set; }
    
    // Constructor
    protected [EntityName]() { }
    
    internal [EntityName](
        Guid id,
        string code,
        string name
    ) : base(id)
    {
        SetCode(code);
        SetName(name);
    }
    
    // Methods
    internal void SetCode(string code)
    {
        Code = Check.NotNullOrWhiteSpace(
            code,
            nameof(code),
            maxLength: [EntityName]Constants.MaxCodeLength
        );
    }
}
```

### 2. Application Service Template
```csharp
[Authorize([ModuleName]Permissions.[EntityName]s.Default)]
public class [EntityName]AppService : ApplicationService, I[EntityName]AppService
{
    private readonly I[EntityName]Repository _repository;
    private readonly [EntityName]Manager _manager;
    
    public [EntityName]AppService(
        I[EntityName]Repository repository,
        [EntityName]Manager manager)
    {
        _repository = repository;
        _manager = manager;
    }
    
    public async Task<[EntityName]Dto> GetAsync(Guid id)
    {
        var entity = await _repository.GetAsync(id);
        return ObjectMapper.Map<[EntityName], [EntityName]Dto>(entity);
    }
}
```

### 3. Repository Template
```csharp
public class [EntityName]Repository : 
    EfCoreRepository<[DbContext], [EntityName], Guid>,
    I[EntityName]Repository
{
    public [EntityName]Repository(
        IDbContextProvider<[DbContext]> dbContextProvider)
        : base(dbContextProvider)
    {
    }
    
    public async Task<[EntityName]> GetByCodeAsync(string code)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Set<[EntityName]>()
            .FirstOrDefaultAsync(x => x.Code == code);
    }
}
```

## Best Practices khi sử dụng template

1. Customization
   - Không copy-paste mù quáng
   - Hiểu và customize template theo yêu cầu cụ thể
   - Loại bỏ các phần không cần thiết

2. Cập nhật template
   - Thường xuyên cập nhật template với các best practices mới
   - Thêm các lessons learned vào template
   - Chia sẻ improvements với team

3. Documentation
   - Document các thay đổi so với template
   - Giải thích lý do của các customizations
   - Cập nhật technical documentation

4. Testing
   - Sử dụng test templates
   - Customize test cases theo yêu cầu cụ thể
   - Đảm bảo coverage

5. Code Review
   - Review các customizations
   - Đảm bảo tuân thủ standards
   - Chia sẻ knowledge với team

## Quy trình review và approval

1. Self Review
   - [ ] Code tuân thủ template
   - [ ] Các customizations hợp lý
   - [ ] Tests đầy đủ
   - [ ] Documentation đầy đủ

2. Team Review
   - [ ] Code review bởi ít nhất 2 team members
   - [ ] Technical review bởi tech lead
   - [ ] Security review nếu cần

3. Final Approval
   - [ ] Approval từ tech lead
   - [ ] Approval từ project manager
   - [ ] Sign-off từ QA

## Tips sử dụng hiệu quả

1. IDE Integration
   - Cấu hình code snippets trong IDE
   - Sử dụng file templates
   - Tạo live templates

2. Automation
   - Sử dụng scripts để generate code từ template
   - Tự động hóa việc tạo file structure
   - Tự động hóa việc kiểm tra tuân thủ

3. Knowledge Sharing
   - Regular team training về template usage
   - Chia sẻ best practices
   - Document common issues và solutions

4. Continuous Improvement
   - Collect feedback về template
   - Regular template reviews
   - Update template based on lessons learned

## Menu và Navigation

### 1. Định nghĩa Menu Constants
Trong file `CoreOracleMenus.cs`:
```csharp
public class CoreOracleMenus
{
    private const string Prefix = "CoreOracle";
    
    // Định nghĩa menu chính
    public const string MainMenu = Prefix + ".MainMenu";
    
    // Định nghĩa menu con
    public const string SubMenu = MainMenu + ".SubMenu";
}
```

### 2. Cấu hình Menu trong MenuContributor
Trong file `CoreOracleMenuContributor.cs`:
```csharp
private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
{
    var l = context.GetLocalizer<CoreOracleResource>();

    // Thêm menu chính
    var mainMenu = new ApplicationMenuItem(
        CoreOracleMenus.MainMenu,
        l["Menu:MainMenu"],
        icon: "fa fa-list",
        order: 1
    );

    // Thêm menu con
    mainMenu.AddItem(
        new ApplicationMenuItem(
            CoreOracleMenus.SubMenu,
            l["Menu:SubMenu"],
            url: "/MainMenu/SubMenu",
            icon: "fa fa-folder",
            requiredPermissionName: CoreOraclePermissions.SubMenu.Default
        )
    );

    context.Menu.AddItem(mainMenu);
    return Task.CompletedTask;
}
```

### 3. Localization cho Menu
Trong file `vi.json`:
```json
{
  "Menu:MainMenu": "Menu chính",
  "Menu:SubMenu": "Menu con"
}
```

### 4. Phân quyền cho Menu
Trong file `CoreOraclePermissions.cs`:
```csharp
public static class CoreOraclePermissions
{
    public static class SubMenu
    {
        public const string Default = "CoreOracle.SubMenu";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
```

### 5. Kiểm tra và Test
1. Kiểm tra hiển thị menu
2. Kiểm tra phân quyền
3. Kiểm tra đường dẫn
4. Kiểm tra icon
5. Kiểm tra thứ tự hiển thị 