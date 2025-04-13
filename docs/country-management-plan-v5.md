# Kế hoạch triển khai chức năng quản lý danh mục quốc gia

## 0. Phân tích nghiệp vụ

### 0.1. Mục tiêu
- Xây dựng chức năng cho phép người quản trị hệ thống (System Administrator) quản lý danh mục các quốc gia được sử dụng trong toàn hệ thống.
- Đảm bảo dữ liệu quốc gia (mã, tên) là nhất quán và chính xác.

### 0.2. Đối tượng sử dụng
- Quản trị viên hệ thống (System Administrator) hoặc người dùng được cấp quyền quản lý danh mục quốc gia.

### 0.3. Yêu cầu chức năng chính (CRUD)
- **Xem danh sách (Read):** Hiển thị danh sách các quốc gia đã có trong hệ thống dưới dạng bảng, hỗ trợ phân trang và sắp xếp theo mã hoặc tên. Có thể có chức năng lọc/tìm kiếm cơ bản (nếu cần).
- **Thêm mới (Create):** Cho phép người dùng thêm một quốc gia mới vào danh mục. Yêu cầu nhập Mã quốc gia (Code) và Tên quốc gia (Name).
- **Sửa (Update):** Cho phép người dùng chỉnh sửa thông tin (Mã, Tên) của một quốc gia đã tồn tại.
- **Xóa (Delete):** Cho phép người dùng xóa một quốc gia khỏi danh mục. Nên sử dụng cơ chế xóa mềm (Soft Delete) để lưu vết và có khả năng khôi phục. Cần có bước xác nhận trước khi xóa.

### 0.4. Yêu cầu dữ liệu
- **Mã quốc gia (Code):**
    - Kiểu dữ liệu: Chuỗi (String).
    - Bắt buộc nhập.
    - Độ dài tối đa: 10 ký tự (theo `CountryConsts`).
    - Phải là duy nhất (unique) trong toàn bộ danh mục.
- **Tên quốc gia (Name):**
    - Kiểu dữ liệu: Chuỗi (String).
    - Bắt buộc nhập.
    - Độ dài tối đa: 256 ký tự (theo `CountryConsts`).
- **Thông tin Audit:** Lưu trữ thông tin về người tạo, thời gian tạo, người sửa cuối cùng, thời gian sửa cuối cùng, trạng thái xóa mềm (IsDeleted). (Kế thừa từ `FullAuditedAggregateRoot`).

### 0.5. Yêu cầu giao diện người dùng (UI)
- **Màn hình danh sách:**
    - Bảng hiển thị các cột: Mã, Tên, (có thể thêm các cột audit nếu cần).
    - Nút "Thêm mới quốc gia".
    - Các nút hành động (Sửa, Xóa) trên mỗi dòng của bảng.
    - Phân trang.
- **Modal Thêm mới/Sửa:**
    - Form nhập liệu cho Mã và Tên.
    - Các nút Lưu và Hủy.

### 0.6. Yêu cầu về phân quyền
- Cần định nghĩa các quyền riêng biệt cho việc xem danh sách, thêm, sửa, xóa quốc gia.
- Chỉ những người dùng được gán quyền tương ứng mới có thể thực hiện các thao tác đó. Giao diện cần ẩn/hiện các nút chức năng dựa trên quyền của người dùng.

### 0.7. Quy tắc nghiệp vụ
- Mã quốc gia không được trùng lặp. Hệ thống phải kiểm tra và thông báo lỗi nếu người dùng cố gắng tạo hoặc sửa thành một mã đã tồn tại.

## Tóm tắt Tiến độ Thực hiện

Đây là tóm tắt các bước chính và trạng thái hiện tại (Cập nhật lần cuối: <thời_gian_cập_nhật>):

- [X] **Bước 1: Tầng Domain (`Aqt.CoreOracle.Domain`)**
- [X] **Bước 2: Tầng Domain.Shared (`Aqt.CoreOracle.Domain.Shared`)**
- [X] **Bước 3: Tầng Application.Contracts (`Aqt.CoreOracle.Application.Contracts`)**
- [X] **Bước 4: Tầng Application (`Aqt.CoreOracle.Application`)**
- [X] **Bước 5: Tầng EntityFrameworkCore (`Aqt.CoreOracle.EntityFrameworkCore`)** (Hoàn thành cấu hình, repository. *Chưa chạy migrations*)
- [X] **Bước 6: Tầng Web (`Aqt.CoreOracle.Web`)** (Bỏ qua HttpApi)
- [ ] **Bước 7: Các bước triển khai và kiểm thử cuối cùng** (Build, chạy migrations, tạo JS proxies, kiểm thử)

## 1. Tầng Domain (`Aqt.CoreOracle.Domain`)

### Entity
- Tạo thư mục `Countries/Entities`
- Tạo file `Country.cs`:
  ```csharp
  // Add necessary using statements: System, JetBrains.Annotations, Volo.Abp, Volo.Abp.Domain.Entities.Auditing, Aqt.CoreOracle.Domain.Shared.Countries;
  public class Country : FullAuditedAggregateRoot<Guid>
  {
      public string Code { get; private set; }
      public string Name { get; set; }

      protected Country() { /* For ORM */ }

      public Country(Guid id, [NotNull] string code, [NotNull] string name) : base(id)
      {
          SetCode(code);
          SetName(name);
      }

      internal void SetCode([NotNull] string code)
      {
          Check.NotNullOrWhiteSpace(code, nameof(code));
          Check.Length(code, nameof(code), CountryConsts.MaxCodeLength);
          Code = code;
      }

      internal void SetName([NotNull] string name)
      {
          Check.NotNullOrWhiteSpace(name, nameof(name));
          Check.Length(name, nameof(name), CountryConsts.MaxNameLength);
          Name = name;
      }
  }
  ```

### Repository Interface
- Tạo file `ICountryRepository.cs`:
  ```csharp
  // Add necessary using statements: System, System.Threading, System.Threading.Tasks, Aqt.CoreOracle.Domain.Countries.Entities, Volo.Abp.Domain.Repositories;
  public interface ICountryRepository : IRepository<Country, Guid>
  {
      Task<Country?> FindByCodeAsync(string code, CancellationToken cancellationToken = default);
      Task<bool> CodeExistsAsync(string code, Guid? excludedId = null, CancellationToken cancellationToken = default);
  }
  ```

## 2. Tầng Domain.Shared (`Aqt.CoreOracle.Domain.Shared`)

### Constants
- Tạo file `Countries/CountryConsts.cs`:
  ```csharp
  namespace Aqt.CoreOracle.Domain.Shared.Countries;
  public static class CountryConsts
  {
      public const int MaxCodeLength = 10;
      public const int MaxNameLength = 256;
  }
  ```

### Localization
- Cập nhật các file `*.json` trong `Localization/CoreOracle` (ví dụ: `en.json`, `vi.json`):
  ```json
  {
    "Menu:Countries": "Countries",
    "Countries": "Countries",
    "NewCountry": "New Country",
    "EditCountry": "Edit Country",
    "CountryCode": "Code",
    "CountryName": "Name",
    "SuccessfullySaved": "Successfully saved.",
    "SuccessfullyDeleted": "Successfully deleted.",
    "AreYouSure": "Are you sure?",
    "AreYouSureToDelete": "Are you sure you want to delete this item: {0}?", // Added placeholder
    "Permission:Countries": "Country Management",
    "Permission:Create": "Create",
    "Permission:Edit": "Edit", 
    "Permission:Delete": "Delete"
  }
  ```

### Error Codes
- Thêm vào `CoreOracleDomainErrorCodes.cs`:
  ```csharp
  public const string CountryCodeAlreadyExists = "CoreOracle:00011"; // Check next available code
  ```

## 3. Tầng Application.Contracts (`Aqt.CoreOracle.Application.Contracts`)

### DTOs
- Tạo thư mục `Countries/Dtos`
- Tạo file `CountryDto.cs`:
  ```csharp
  // Add necessary using statements: System, Volo.Abp.Application.Dtos;
  namespace Aqt.CoreOracle.Application.Contracts.Countries.Dtos;
  public class CountryDto : AuditedEntityDto<Guid>
  {
      public string Code { get; set; }
      public string Name { get; set; }
  }
  ```
- Tạo file `CreateUpdateCountryDto.cs`:
  ```csharp
  // Add necessary using statements: System.ComponentModel.DataAnnotations, Aqt.CoreOracle.Domain.Shared.Countries;
  namespace Aqt.CoreOracle.Application.Contracts.Countries.Dtos;
  public class CreateUpdateCountryDto
  {
      [Required]
      [StringLength(CountryConsts.MaxCodeLength)]
      public string Code { get; set; }

      [Required]
      [StringLength(CountryConsts.MaxNameLength)]
      public string Name { get; set; }
  }
  ```
- Tạo file `GetCountriesInput.cs`:
  ```csharp
  // Add necessary using statements: Volo.Abp.Application.Dtos;
  namespace Aqt.CoreOracle.Application.Contracts.Countries.Dtos;
  public class GetCountriesInput : PagedAndSortedResultRequestDto
  {
      public string? Filter { get; set; }
  }
  ```

### AppService Interface
- Tạo file `Countries/ICountryAppService.cs`:
  ```csharp
  // Add necessary using statements: System, Aqt.CoreOracle.Application.Contracts.Countries.Dtos, Volo.Abp.Application.Services;
  namespace Aqt.CoreOracle.Application.Contracts.Countries;
  public interface ICountryAppService : ICrudAppService<CountryDto, Guid, GetCountriesInput, CreateUpdateCountryDto>
  {
  }
  ```

### Permissions
- Cập nhật `CoreOraclePermissions.cs`:
  ```csharp
  public static class Countries
  {
      public const string Default = GroupName + ".Countries";
      public const string Create = Default + ".Create";
      public const string Edit = Default + ".Edit";
      public const string Delete = Default + ".Delete";
  }
  ```
- Cập nhật `CoreOraclePermissionDefinitionProvider.cs` (trong phương thức `Define`):
  ```csharp
  var countriesPermission = myGroup.AddPermission(CoreOraclePermissions.Countries.Default, L("Permission:Countries"));
  countriesPermission.AddChild(CoreOraclePermissions.Countries.Create, L("Permission:Create"));
  countriesPermission.AddChild(CoreOraclePermissions.Countries.Edit, L("Permission:Edit"));
  countriesPermission.AddChild(CoreOraclePermissions.Countries.Delete, L("Permission:Delete"));
  ```

## 4. Tầng Application (`Aqt.CoreOracle.Application`)

### AppService Implementation
- Tạo thư mục `Countries`
- Tạo file `CountryAppService.cs`:
  ```csharp
  // Add necessary using statements: System, System.Threading.Tasks, Microsoft.AspNetCore.Authorization, Aqt.CoreOracle.Application.Contracts.Countries, Aqt.CoreOracle.Application.Contracts.Countries.Dtos, Aqt.CoreOracle.Domain.Countries, Aqt.CoreOracle.Domain.Countries.Entities, Aqt.CoreOracle.Permissions, Volo.Abp, Volo.Abp.Application.Dtos, Volo.Abp.Application.Services, Volo.Abp.Domain.Repositories;
  namespace Aqt.CoreOracle.Application.Countries;
  
  [Authorize(CoreOraclePermissions.Countries.Default)] // Authorize the whole service
  public class CountryAppService : 
      CrudAppService<Country, CountryDto, Guid, GetCountriesInput, CreateUpdateCountryDto>,
      ICountryAppService
  {
      private readonly ICountryRepository _countryRepository;

      public CountryAppService(ICountryRepository countryRepository) 
          : base(countryRepository)
      {
          _countryRepository = countryRepository;
          // Policy names are automatically derived by CrudAppService if not set explicitly
          // GetPolicyName = CoreOraclePermissions.Countries.Default; 
          // GetListPolicyName = CoreOraclePermissions.Countries.Default;
          // CreatePolicyName = CoreOraclePermissions.Countries.Create;
          // UpdatePolicyName = CoreOraclePermissions.Countries.Edit;
          // DeletePolicyName = CoreOraclePermissions.Countries.Delete;
      }

      [Authorize(CoreOraclePermissions.Countries.Create)]
      public override async Task<CountryDto> CreateAsync(CreateUpdateCountryDto input)
      {
          await CheckCodeExistsAsync(input.Code);
          var country = ObjectMapper.Map<CreateUpdateCountryDto, Country>(input);
          await Repository.InsertAsync(country, autoSave: true);
          return ObjectMapper.Map<Country, CountryDto>(country);
          // Note: Consider returning await base.CreateAsync(input); if custom logic isn't extensive
      }

      [Authorize(CoreOraclePermissions.Countries.Edit)]
      public override async Task<CountryDto> UpdateAsync(Guid id, CreateUpdateCountryDto input)
      {
          await CheckCodeExistsAsync(input.Code, id);
          var country = await GetEntityByIdAsync(id); // Get existing entity
          ObjectMapper.Map(input, country); // Map input DTO to existing entity
          await Repository.UpdateAsync(country, autoSave: true);
          return ObjectMapper.Map<Country, CountryDto>(country);
          // Note: Consider returning await base.UpdateAsync(id, input); if custom logic isn't extensive
      }
      
      [Authorize(CoreOraclePermissions.Countries.Delete)]
      public override Task DeleteAsync(Guid id)
      {
        // ABP's CrudAppService handles soft delete if the entity implements ISoftDelete
        return base.DeleteAsync(id);
      }

      private async Task CheckCodeExistsAsync(string code, Guid? excludedId = null)
      {
          if (await _countryRepository.CodeExistsAsync(code, excludedId))
          {
              // Use L[] for localization if the error code is defined in localization files
              throw new UserFriendlyException(L[CoreOracleDomainErrorCodes.CountryCodeAlreadyExists]);
          }
      }
  }
  ```

### AutoMapper Profile
- Tạo file `Countries/CountryApplicationAutoMapperProfile.cs`:
  ```csharp
  // Add necessary using statements: AutoMapper, Aqt.CoreOracle.Application.Contracts.Countries.Dtos, Aqt.CoreOracle.Domain.Countries.Entities, Volo.Abp.ObjectMapping;
  namespace Aqt.CoreOracle.Application.Countries;
  
  public class CountryApplicationAutoMapperProfile : Profile
  {
      public CountryApplicationAutoMapperProfile()
      {
          CreateMap<Country, CountryDto>();
          CreateMap<CreateUpdateCountryDto, Country>()
              .IgnoreAuditedObjectProperties() // Ignore base audit properties
              .Ignore(x => x.Id); // Ignore Id as it's the key
              // No need for .ForMember if names match and no custom logic needed
              
          // Add mapping from CountryDto to CreateUpdateCountryDto for EditModal
          CreateMap<CountryDto, CreateUpdateCountryDto>(); 
      }
  }
  ```

### Check AutoMapper Module Configuration
- Mở `AqtCoreOracleApplicationModule.cs`
- Trong `ConfigureServices`, **xác nhận** rằng `options.AddMaps<CoreOracleApplicationModule>();` tồn tại trong `Configure<AbpAutoMapperOptions>(...)` để đảm bảo profile được tự động đăng ký.
  ```csharp
  context.Services.AddAutoMapperObjectMapper<CoreOracleApplicationModule>();
  Configure<AbpAutoMapperOptions>(options =>
  {
      options.AddMaps<CoreOracleApplicationModule>(); // Ensure this line exists
  });
  ```

## 5. Tầng EntityFrameworkCore (`Aqt.CoreOracle.EntityFrameworkCore`)

### DbContext
- Cập nhật `CoreOracleDbContext.cs`:
  ```csharp
  public DbSet<Country> Countries { get; set; }
  ```

### Repository Implementation
- Tạo thư mục `Countries`
- Tạo file `CountryRepository.cs`:
  ```csharp
  // Add necessary using statements: System, System.Linq, System.Linq.Expressions, System.Threading, System.Threading.Tasks, Microsoft.EntityFrameworkCore, Aqt.CoreOracle.Domain.Countries, Aqt.CoreOracle.Domain.Countries.Entities, Aqt.CoreOracle.EntityFrameworkCore, Volo.Abp.Domain.Repositories.EntityFrameworkCore, Volo.Abp.EntityFrameworkCore;
  namespace Aqt.CoreOracle.EntityFrameworkCore.Countries;
  
  public class CountryRepository : 
      EfCoreRepository<CoreOracleDbContext, Country, Guid>,
      ICountryRepository
  {
      public CountryRepository(IDbContextProvider<CoreOracleDbContext> dbContextProvider)
          : base(dbContextProvider)
      {
      }

      public async Task<Country?> FindByCodeAsync(string code, CancellationToken cancellationToken = default)
      {
          var dbSet = await GetDbSetAsync();
          return await dbSet
              .FirstOrDefaultAsync(x => x.Code == code, GetCancellationToken(cancellationToken));
      }

      public async Task<bool> CodeExistsAsync(string code, Guid? excludedId = null, CancellationToken cancellationToken = default)
      {
          var dbSet = await GetDbSetAsync();
          return await dbSet
              .AnyAsync(x => x.Code == code && (!excludedId.HasValue || x.Id != excludedId.Value), 
                  GetCancellationToken(cancellationToken));
      }
      
      // Optional: Override GetListAsync if custom filtering/sorting needed beyond GetCountriesInput
      /*
      public override async Task<List<Country>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default)
      {
        return await (await GetQueryableAsync()).ToListAsync(GetCancellationToken(cancellationToken));
      }
      */
  }
  ```

### Entity Configuration
- Tạo thư mục `EntityTypeConfigurations/Countries`
- Tạo file `CountryConfiguration.cs`:
  ```csharp
  // Add necessary using statements: Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.Metadata.Builders, Aqt.CoreOracle.Domain.Countries.Entities, Aqt.CoreOracle.Domain.Shared.Countries, Volo.Abp.EntityFrameworkCore.Modeling;
  namespace Aqt.CoreOracle.EntityFrameworkCore.EntityTypeConfigurations.Countries;
  
  public class CountryConfiguration : IEntityTypeConfiguration<Country>
  {
      public void Configure(EntityTypeBuilder<Country> builder)
      {
          // Use constants for table name and schema if defined
          builder.ToTable(CoreOracleConsts.DbTablePrefix + "Countries", CoreOracleConsts.DbSchema);
          builder.ConfigureByConvention(); // Apply ABP conventions

          builder.HasKey(x => x.Id);
          
          builder.Property(x => x.Code)
              .IsRequired()
              .HasMaxLength(CountryConsts.MaxCodeLength)
              .HasColumnName(nameof(Country.Code)); // Explicit mapping
              
          builder.Property(x => x.Name)
              .IsRequired()
              .HasMaxLength(CountryConsts.MaxNameLength)
              .HasColumnName(nameof(Country.Name)); // Explicit mapping

          builder.HasIndex(x => x.Code).IsUnique(); // Ensure Code is unique in DB
      }
  }
  ```
- Cập nhật `CoreOracleDbContextModelCreatingExtensions.cs` (trong `ConfigureCoreOracle`):
  ```csharp
  builder.ApplyConfiguration(new CountryConfiguration());
  ```

### Migration
- Chạy lệnh trong thư mục `Aqt.CoreOracle.EntityFrameworkCore`:
  ```bash
  dotnet ef migrations add Added_Countries_Table
  ```
- Chạy lệnh trong thư mục `Aqt.CoreOracle.DbMigrator`:
  ```bash
  dotnet run
  ```

## 6. Tầng Web (`Aqt.CoreOracle.Web`)

### Menu
- Cập nhật `CoreOracleMenus.cs`:
  ```csharp
  public const string Countries = Prefix + ".Countries";
  ```
- Cập nhật `CoreOracleMenuContributor.cs` (trong `ConfigureMainMenuAsync`):
  ```csharp
  if (await context.IsGrantedAsync(CoreOraclePermissions.Countries.Default))
  {
      // Decide where to put the menu item (e.g., under Administration or top-level)
      var administration = context.Menu.GetAdministration();
      administration.AddItem(new ApplicationMenuItem(
          CoreOracleMenus.Countries,
          l["Menu:Countries"],
          "/Countries" // Route for the Razor Page
      ).RequirePermissions(CoreOraclePermissions.Countries.Default)); // Ensure user has permission
      // or context.Menu.AddItem(...)
  }
  ```

### Razor Pages
- Tạo thư mục `Pages/Countries`
- Tạo file `Index.cshtml`:
  ```cshtml
  @page
  @using Aqt.CoreOracle.Permissions
  @using Microsoft.AspNetCore.Authorization
  @using Volo.Abp.AspNetCore.Mvc.UI.Layout
  @using Aqt.CoreOracle.Web.Pages.Countries // Ensure namespace is correct
  @using Aqt.CoreOracle.Localization
  @using Microsoft.AspNetCore.Mvc.Localization
  @model IndexModel
  @inject IHtmlLocalizer<CoreOracleResource> L
  @inject IAuthorizationService AuthorizationService
  @inject IPageLayout PageLayout
  @{
      PageLayout.Content.Title = L["Countries"].Value;
      // Optional: Set breadcrumbs or active menu item if needed
      // PageLayout.Content.MenuItemName = CoreOracleMenus.Countries; 
  }

  @section scripts {
      <abp-script src="/Pages/Countries/index.js" />
  }

  @section content_toolbar {
      @if (await AuthorizationService.IsGrantedAsync(CoreOraclePermissions.Countries.Create))
      {
          <abp-button id="NewCountryButton"
                      text="@L["NewCountry"].Value"
                      icon="plus"
                      button-type="Primary" />
      }
  }

  <abp-card>
      <abp-card-body>
          <abp-table striped-rows="true" id="CountriesTable"></abp-table>
      </abp-card-body>
  </abp-card>
  ```
- Tạo file `Index.cshtml.cs`:
  ```csharp
  // Add necessary using statements: Aqt.CoreOracle.Web.Pages;
  namespace Aqt.CoreOracle.Web.Pages.Countries;
  
  public class IndexModel : CoreOraclePageModel
  {
      // No logic needed here if using datatables with AJAX source
      public void OnGet()
      {
      }
  }
  ```
- Tạo file `CreateModal.cshtml` (Partial View):
  ```cshtml
  @page "/Countries/CreateModal"
  @using Microsoft.AspNetCore.Mvc.Localization
  @using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal
  @using Aqt.CoreOracle.Localization
  @using Aqt.CoreOracle.Web.Pages.Countries // Ensure namespace is correct
  @model CreateModalModel
  @inject IHtmlLocalizer<CoreOracleResource> L
  @{
      Layout = null;
  }
  <abp-dynamic-form abp-model="Country" asp-page="/Countries/CreateModal">
      <abp-modal>
          <abp-modal-header title="@L["NewCountry"].Value"></abp-modal-header>
          <abp-modal-body>
              <abp-form-content />
          </abp-modal-body>
          <abp-modal-footer buttons="@(AbpModalButtons.Cancel | AbpModalButtons.Save)"></abp-modal-footer>
      </abp-modal>
  </abp-dynamic-form>
  ```
- Tạo file `CreateModal.cshtml.cs`:
  ```csharp
  // Add necessary using statements: System.Threading.Tasks, Microsoft.AspNetCore.Mvc, Aqt.CoreOracle.Application.Contracts.Countries, Aqt.CoreOracle.Application.Contracts.Countries.Dtos, Aqt.CoreOracle.Web.Pages;
  namespace Aqt.CoreOracle.Web.Pages.Countries;

  public class CreateModalModel : CoreOraclePageModel
  {
      [BindProperty]
      public CreateUpdateCountryDto? Country { get; set; }

      private readonly ICountryAppService _countryAppService;

      public CreateModalModel(ICountryAppService countryAppService)
      {
          _countryAppService = countryAppService;
          // ObjectMapper is inherited from CoreOraclePageModel / AbpPageModel
      }

      public virtual void OnGet()
      {
          Country = new CreateUpdateCountryDto();
      }

      public virtual async Task<IActionResult> OnPostAsync()
      {
          await _countryAppService.CreateAsync(Country!);
          return NoContent();
      }
  }
  ```
- Tạo file `EditModal.cshtml` (Partial View):
  ```cshtml
  @page "/Countries/EditModal"
  @using Microsoft.AspNetCore.Mvc.Localization
  @using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal
  @using Aqt.CoreOracle.Localization
  @using Aqt.CoreOracle.Web.Pages.Countries // Ensure namespace is correct
  @model EditModalModel
  @inject IHtmlLocalizer<CoreOracleResource> L
  @{
      Layout = null;
  }
  <abp-dynamic-form abp-model="Country" asp-page="/Countries/EditModal">
      <abp-modal>
          <abp-modal-header title="@L["EditCountry"].Value"></abp-modal-header>
          <abp-modal-body>
              @* Id is needed for the request but shouldn't be displayed in the form *@
              <input type="hidden" asp-for="Id" /> 
              <abp-form-content />
          </abp-modal-body>
          <abp-modal-footer buttons="@(AbpModalButtons.Cancel | AbpModalButtons.Save)"></abp-modal-footer>
      </abp-modal>
  </abp-dynamic-form>
  ```
- Tạo file `EditModal.cshtml.cs`:
  ```csharp
  // Add necessary using statements: System, System.Threading.Tasks, Microsoft.AspNetCore.Mvc, Aqt.CoreOracle.Application.Contracts.Countries, Aqt.CoreOracle.Application.Contracts.Countries.Dtos, Aqt.CoreOracle.Web.Pages;
  namespace Aqt.CoreOracle.Web.Pages.Countries;

  public class EditModalModel : CoreOraclePageModel
  {
      [HiddenInput] // Keep Id hidden in the form
      [BindProperty(SupportsGet = true)]
      public Guid Id { get; set; }

      [BindProperty]
      public CreateUpdateCountryDto? Country { get; set; }

      private readonly ICountryAppService _countryAppService;

      public EditModalModel(ICountryAppService countryAppService)
      {
          _countryAppService = countryAppService;
          // ObjectMapper is inherited from CoreOraclePageModel / AbpPageModel
      }

      public virtual async Task OnGetAsync()
      {
          var dto = await _countryAppService.GetAsync(Id);
          // Map from the read DTO to the CreateUpdate DTO for the form
          Country = ObjectMapper.Map<CountryDto, CreateUpdateCountryDto>(dto);
      }

      public virtual async Task<IActionResult> OnPostAsync()
      {
          await _countryAppService.UpdateAsync(Id, Country!);
          return NoContent();
      }
  }
  ```

### JavaScript
- Tạo thư mục `wwwroot/pages/countries` (nếu chưa có)
- Tạo file `wwwroot/pages/countries/index.js`:
  ```javascript
  $(function () {
      var l = abp.localization.getResource('CoreOracle');
      
      // Service proxies generated by ABP CLI
      var countryService = aqt.coreOracle.countries.country;
      
      var createModal = new abp.ModalManager({
          viewUrl: abp.appPath + 'Countries/CreateModal',
          scriptUrl: '/Pages/Countries/createModal.js', // Optional: if CreateModal has specific JS
          modalClass: 'countryCreateModal' // Optional: CSS class for modal
      });
      
      var editModal = new abp.ModalManager({
          viewUrl: abp.appPath + 'Countries/EditModal',
          scriptUrl: '/Pages/Countries/editModal.js', // Optional: if EditModal has specific JS
          modalClass: 'countryEditModal' // Optional: CSS class for modal
      });

      var dataTable = $('#CountriesTable').DataTable(
          abp.libs.datatables.normalizeConfiguration({
              serverSide: true,
              paging: true,
              order: [[1, "asc"]], // Default sort by Code
              searching: false, // Disable native search, use custom filter if needed
              scrollX: true,
              ajax: abp.libs.datatables.createAjax(countryService.getList), // Use service proxy
              columnDefs: [
                  {
                      title: l('Actions'),
                      rowAction: {
                          items: [
                              {
                                  text: l('Edit'),
                                  icon: "fa fa-pencil-alt", // Optional icon
                                  visible: abp.auth.isGranted('CoreOracle.Countries.Edit'),
                                  action: function (data) {
                                      editModal.open({ id: data.record.id });
                                  }
                              },
                              {
                                  text: l('Delete'),
                                  icon: "fa fa-trash", // Optional icon
                                  visible: abp.auth.isGranted('CoreOracle.Countries.Delete'),
                                  confirmMessage: function (data) {
                                      // Use localization with placeholder
                                      return l('AreYouSureToDelete', data.record.name || data.record.code);
                                  },
                                  action: function (data) {
                                      countryService.delete(data.record.id)
                                          .then(function () {
                                              abp.notify.success(l('SuccessfullyDeleted'));
                                              dataTable.ajax.reload(); // Reload table data
                                          })
                                          .catch(function (error) {
                                             abp.notify.error(error.message || l('Error'));
                                          });
                                  }
                              }
                          ]
                      }
                  },
                  {
                      title: l('CountryCode'),
                      data: "code",
                      orderable: true // Allow sorting by Code
                  },
                  {
                      title: l('CountryName'),
                      data: "name",
                      orderable: true // Allow sorting by Name
                  }
              ]
          })
      );

      // Refresh table on modal close/save
      createModal.onResult(function () {
          dataTable.ajax.reload();
      });

      editModal.onResult(function () {
          dataTable.ajax.reload();
      });

      // Handle 'New Country' button click
      $('#NewCountryButton').click(function (e) {
          e.preventDefault();
          createModal.open();
      });
      
      // Optional: Add filter functionality if needed
      // $('#SearchButton').click(function (e) {
      //     e.preventDefault();
      //     dataTable.ajax.reload(); // Will pass filter values via getListInput parameter in createAjax
      // });
      
      // Optional: Custom filter input for datatables
      // abp.libs.datatables.createAjax = function (getServiceMethod, getListInput) {
      //     return function (requestData, callback, settings) { ... }
      // };

  });
  ```
- Đăng ký JS trong `CoreOracleWebModule.cs` (nếu chưa tự động bundle):
  ```csharp
  Configure<AbpBundlingOptions>(options =>
  {
      options.ScriptBundles
          .Configure(StandardBundles.Scripts.Global, bundle =>
          {
              // Add your global scripts here
          });
      // Add bundle for Countries page if needed, or ensure Pages/**/*.js are included
  });
  ```

## Các bước triển khai

1. Tạo các file và thư mục theo cấu trúc đã định nghĩa.
2. Triển khai code cho từng file theo thứ tự từ Domain -> Domain.Shared -> Application.Contracts -> Application -> EntityFrameworkCore -> Web.
3. Chạy `dotnet build` để kiểm tra lỗi biên dịch.
4. Chạy migration để cập nhật database.
5. Chạy ứng dụng Web.
6. Kiểm thử chức năng.

## Kiểm tra và xác nhận

1.  Quyền: Đăng nhập với user có/không có quyền Create/Edit/Delete để kiểm tra nút bấm và API.
2.  Validation:
    *   Thử tạo/sửa với Code/Name rỗng.
    *   Thử tạo/sửa với Code/Name dài hơn MaxLength.
    *   Thử tạo/sửa với Code đã tồn tại.
3.  CRUD:
    *   Tạo mới quốc gia.
    *   Xem danh sách, kiểm tra phân trang, sắp xếp.
    *   Sửa quốc gia.
    *   Xóa quốc gia (kiểm tra confirm message).
4.  Giao diện: Kiểm tra giao diện table, modal hiển thị đúng, hoạt động mượt mà.
5.  Localization: Kiểm tra các label, thông báo hiển thị đúng ngôn ngữ. 