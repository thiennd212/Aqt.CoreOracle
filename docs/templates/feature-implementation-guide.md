# Hướng dẫn triển khai tính năng mới

## Giới thiệu
Document này cung cấp hướng dẫn chi tiết về cách sử dụng template và script Generate-Feature.ps1 để triển khai các tính năng mới trong dự án CoreOracle.

## Cấu trúc thư mục template
```
docs/templates/
├── feature-implementation-guide.md     # Hướng dẫn này
├── code-templates/                     # Các template code
│   ├── domain/                        # Domain layer templates
│   ├── application/                   # Application layer templates
│   ├── infrastructure/                # Infrastructure layer templates
│   ├── caching/                       # Caching layer templates
│   └── presentation/                  # Presentation layer templates
└── checklists/                        # Các checklist
```

## Sử dụng Generate-Feature.ps1

### 1. Cú pháp cơ bản
```powershell
.\Generate-Feature.ps1 -ModuleName "Categories" -EntityName "CategoryItem"
```

### 2. Các tham số bổ sung
```powershell
-Description "Mô tả về tính năng"
-Force              # Ghi đè files đã tồn tại
-UpdateDbContext    # Cập nhật DbContext
-UpdateMenu         # Cập nhật Menu
-UpdateAutoMapper   # Cập nhật AutoMapper
-CreateMigration    # Tạo migration
-UpdatePermissions  # Cập nhật permissions
-UpdateDocs         # Cập nhật documentation
```

### 3. Tham số cho Caching và Performance
```powershell
-EnableCaching              # Tạo cache layer
-CacheTimeout 30           # Thời gian cache (phút)
-EnableLazyLoading         # Bật lazy loading
-EnableBatchProcessing     # Hỗ trợ xử lý hàng loạt
```

## Quy trình triển khai

### 1. Khởi tạo tính năng
1. Tạo branch mới:
```bash
git checkout -b feature/[feature-name]
```

2. Generate code cơ bản:
```powershell
.\Generate-Feature.ps1 -ModuleName [Module] -EntityName [Entity] -Description [Description]
```

3. Review và customize code được generate

### 2. Implement Business Logic

#### A. Domain Layer
1. Entity
   - Thêm properties
   - Implement domain logic
   - Thêm validation rules

2. Domain Service
   - Implement business rules
   - Xử lý validation phức tạp
   - Implement domain events

#### B. Application Layer
1. DTOs
   - Customize theo yêu cầu
   - Thêm validation attributes
   - Implement custom mapping

2. Application Service
   - Implement CRUD operations
   - Thêm business logic
   - Implement caching
   - Xử lý authorization

#### C. Infrastructure Layer
1. Repository
   - Implement custom queries
   - Optimize performance
   - Implement caching strategy

2. Database
   - Tạo indexes
   - Implement partitioning nếu cần
   - Optimize query performance

### 3. Implement Caching

```csharp
public class [Entity]Cache : ITransientDependency
{
    private readonly IDistributedCache<[Entity]CacheItem> _cache;
    
    public async Task<[Entity]CacheItem> GetAsync(Guid id)
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
}
```

### 4. Performance Optimization
1. Sử dụng Materialized Path cho cấu trúc phân cấp
2. Implement lazy loading cho related entities
3. Sử dụng batch processing cho operations lớn
4. Optimize database queries
5. Implement caching strategy phù hợp

### 5. Testing
1. Unit Tests
   - Test domain logic
   - Test validation rules
   - Test caching
   - Test performance

2. Integration Tests
   - Test API endpoints
   - Test database operations
   - Test caching layer

## Best Practices

### 1. Code Organization
- Tách biệt các layers
- Sử dụng folder structure chuẩn
- Đặt tên file và class rõ ràng

### 2. Performance
- Cache data thường xuyên truy cập
- Optimize database queries
- Sử dụng lazy loading
- Implement paging

### 3. Security
- Implement authorization
- Validate input data
- Prevent SQL injection
- Handle sensitive data

### 4. Error Handling
- Sử dụng custom exceptions
- Log errors đầy đủ
- Return proper error messages
- Implement retry logic

### 5. Testing
- Write unit tests
- Write integration tests
- Test edge cases
- Test performance

## Checklist triển khai

### 1. Setup
- [ ] Generate code base
- [ ] Review generated code
- [ ] Setup database
- [ ] Configure permissions

### 2. Implementation
- [ ] Implement domain logic
- [ ] Implement application service
- [ ] Setup caching
- [ ] Optimize performance

### 3. Testing
- [ ] Unit tests
- [ ] Integration tests
- [ ] Performance tests
- [ ] Security tests

### 4. Documentation
- [ ] API documentation
- [ ] Code documentation
- [ ] User guide
- [ ] Deployment guide

## Tips và Tricks

### 1. Sử dụng Script
- Tận dụng parameters của script
- Customize templates theo nhu cầu
- Tạo thêm templates riêng

### 2. Performance
- Profile code thường xuyên
- Monitor cache usage
- Optimize database queries
- Use appropriate indexes

### 3. Maintenance
- Keep templates up to date
- Document changes
- Share knowledge
- Review regularly

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