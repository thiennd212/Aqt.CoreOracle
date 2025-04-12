# Kế hoạch chi tiết xây dựng chức năng Quản lý Danh mục Quốc gia (v2 - Đã rà soát theo Rules)

**Mục tiêu:** Xây dựng giao diện và API cho phép người dùng quản lý (Xem danh sách, Thêm, Sửa, Xóa) danh mục các quốc gia trong hệ thống theo kiến trúc ABP Framework và các quy tắc (`*.mdc`) đã cung cấp.

**Các bước thực hiện chi tiết:**

## Phase 1: Định nghĩa Domain & Contracts (Tuân thủ `domain-shared-standard-rule.mdc`, `domain-standard-rule.mdc`, `application-contracts-rule.mdc`)

1.  **`Aqt.CoreOracle.Domain.Shared`**
    *   **Tạo Constants:**
        *   Tạo thư mục `Countries` nếu chưa có.
        *   Tạo file `Countries/CountryConsts.cs`.
        *   Định nghĩa các hằng số `public const int MaxNameLength = 100;`, `public const int MaxCodeLength = 3;`, `public const int MaxNumericCodeLength = 3;`. *(Tuân thủ Naming Conventions)*
    *   **Localization:**
        *   Trong thư mục `Localization/CoreOracle/`, cập nhật các file `*.json` (ví dụ: `en.json`, `vi.json`).
        *   Thêm các khóa cần thiết: `Menu:Countries`, `Countries`, `NewCountry`, `EditCountry`, `SuccessfullySaved`, `SuccessfullyDeleted`, `CountryName`, `CountryCode`, `NumericCode`, `AreYouSure`, `AreYouSureYouWantToDelete`, `Permission:Countries`, `Permission:Countries.Create`, `Permission:Countries.Edit`, `Permission:Countries.Delete`. *(Sử dụng key rõ ràng)*
    *   **Error Codes:**
        *   Trong `CoreOracleDomainErrorCodes.cs`, thêm các mã lỗi (nếu dự kiến cần):
            *   `public const string CountryCodeAlreadyExists = "CoreOracle:10001";`
            *   `public const string CountryNumericCodeAlreadyExists = "CoreOracle:10002";` *(Format chuẩn)*

2.  **`Aqt.CoreOracle.Domain`**
    *   **Tạo Entity:**
        *   Tạo thư mục `Countries` nếu chưa có.
        *   Tạo file `Entities/Country.cs` trong thư mục `Countries`. *(Cấu trúc thư mục)*
        *   Namespace `Aqt.CoreOracle.Domain.Countries.Entities`. *(Namespace chuẩn)*
        *   Class `public class Country : AuditedAggregateRoot<Guid>` *(Kế thừa base class chuẩn)*
        *   Thuộc tính:
            *   `public string Name { get; private set; }` *(Sử dụng `private set`)*
            *   `public string Code { get; private set; }`
            *   `public string? NumericCode { get; private set; }`
        *   Thêm constructor `private Country()` để EF Core sử dụng.
        *   Thêm constructor `internal Country(Guid id, string name, string code, string? numericCode)`:
            *   Gọi `base(id)`.
            *   Sử dụng `Check.NotNullOrWhiteSpace` cho `name` và `code`.
            *   Sử dụng `Check.Length` với `CountryConsts` cho `name`, `code`, `numericCode`.
            *   Gán giá trị vào properties. *(Đảm bảo tính hợp lệ ngay khi tạo)*
        *   Thêm phương thức `internal void Update(...)` để cập nhật thuộc tính, bao gồm validation tương tự constructor. *(Logic thay đổi nên đặt trong Entity)*
    *   **Tạo Repository Interface:**
        *   Tạo file `Countries/ICountryRepository.cs` trong thư mục `Countries`.
        *   Namespace `Aqt.CoreOracle.Domain.Countries`.
        *   Interface `public interface ICountryRepository : IRepository<Country, Guid>` *(Kế thừa `IRepository`)*
        *   Thêm các phương thức cần thiết:
            *   `Task<Country?> FindByCodeAsync(string code, bool includeDetails = false, CancellationToken cancellationToken = default);`
            *   `Task<bool> CheckCodeExistsAsync(string code, Guid? excludingId = null, CancellationToken cancellationToken = default);` *(Tên rõ ràng, kiểm tra trùng lặp)*
            *   `Task<bool> CheckNumericCodeExistsAsync(string numericCode, Guid? excludingId = null, CancellationToken cancellationToken = default);` *(Tên rõ ràng, kiểm tra trùng lặp)*
            *   `Task<List<Country>> GetListAsync(string? filterText = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, bool includeDetails = false, CancellationToken cancellationToken = default);` *(Phương thức lấy danh sách có filter, sort, paging)*
            *   `Task<long> GetCountAsync(string? filterText = null, CancellationToken cancellationToken = default);` *(Phương thức đếm)*

3.  **`Aqt.CoreOracle.Application.Contracts`**
    *   **Định nghĩa Permissions:**
        *   Trong `Permissions/CoreOraclePermissions.cs`, thêm:
          ```csharp
          public static class Countries
          {
              public const string Default = GroupName + ".Countries";
              public const string Create = Default + ".Create";
              public const string Edit = Default + ".Edit";
              public const string Delete = Default + ".Delete";
          }
          ```
          *(Format chuẩn)*
    *   **Đăng ký Permissions:**
        *   Trong `CoreOraclePermissionDefinitionProvider.cs`, trong `Define`, lấy group `CoreOraclePermissions.GroupName` và thêm permission:
          ```csharp
          var countriesPermission = countryGroup.AddPermission(CoreOraclePermissions.Countries.Default, L("Permission:Countries"));
          countriesPermission.AddChild(CoreOraclePermissions.Countries.Create, L("Permission:Create"));
          countriesPermission.AddChild(CoreOraclePermissions.Countries.Edit, L("Permission:Edit"));
          countriesPermission.AddChild(CoreOraclePermissions.Countries.Delete, L("Permission:Delete"));
          ```
          *(Sử dụng Localization Key)*
    *   **Tạo DTOs:**
        *   Tạo thư mục `Countries/Dtos`. *(Cấu trúc thư mục)*
        *   Namespace `Aqt.CoreOracle.Application.Contracts.Countries.Dtos`. *(Namespace chuẩn)*
        *   Tạo `CountryDto.cs`: kế thừa `AuditedEntityDto<Guid>`. Chứa `Id`, `Name`, `Code`, `NumericCode`. *(Hậu tố Dto)*
        *   Tạo `CreateUpdateCountryDto.cs`: Chứa `Name`, `Code`, `NumericCode`. Áp dụng `DataAnnotations` (`[Required]`, `[StringLength(...)]` sử dụng `CountryConsts`). *(DTO cho input, validation)*
        *   Tạo `GetCountryListInput.cs`: Kế thừa `PagedAndSortedResultRequestDto`. Thêm `public string? Filter { get; set; }`. *(Input cho phân trang, sắp xếp, lọc)*
    *   **Tạo AppService Interface:**
        *   Tạo thư mục `Countries`.
        *   Tạo file `ICountryAppService.cs`.
        *   Namespace `Aqt.CoreOracle.Application.Contracts.Countries`.
        *   Interface `public interface ICountryAppService : IApplicationService`
          ```csharp
          // Kế thừa IApplicationService vì logic có thể phức tạp hơn CRUD chuẩn
          // hoặc muốn định nghĩa method rõ ràng hơn là dùng ICrudAppService
          {
              Task<CountryDto> GetAsync(Guid id);
              Task<PagedResultDto<CountryDto>> GetListAsync(GetCountryListInput input);
              Task<CountryDto> CreateAsync(CreateUpdateCountryDto input);
              Task<CountryDto> UpdateAsync(Guid id, CreateUpdateCountryDto input);
              Task DeleteAsync(Guid id);
          }
          ```
          *(Kế thừa `IApplicationService`, định nghĩa rõ contracts)*

## Phase 2: Implement Logic & Data Access (Tuân thủ `entityframeworkcore-rule.mdc`, `application-rule.mdc`, `performance-rule.mdc`)

4.  **`Aqt.CoreOracle.EntityFrameworkCore`**
    *   **Implement Repository:**
        *   Tạo thư mục `Countries`.
        *   Tạo file `EfCoreCountryRepository.cs`. *(Cấu trúc thư mục)*
        *   Namespace `Aqt.CoreOracle.EntityFrameworkCore.Countries`. *(Namespace chuẩn)*
        *   Class `public class EfCoreCountryRepository : EfCoreRepository<CoreOracleDbContext, Country, Guid>, ICountryRepository` *(Kế thừa base và implement interface)*
        *   Implement các phương thức của `ICountryRepository`:
            *   `FindByCodeAsync`: Dùng `FirstOrDefaultAsync(x => x.Code == code)`.
            *   `CheckCodeExistsAsync`: Dùng `AnyAsync(x => x.Code == code && (excludingId == null || x.Id != excludingId))`.
            *   `CheckNumericCodeExistsAsync`: Tương tự `CheckCodeExistsAsync`.
            *   `GetListAsync`:
                *   Sử dụng `(await GetDbSetAsync()).AsNoTracking()` *(Performance: No tracking khi chỉ đọc)*.
                *   Áp dụng `WhereIf(!filterText.IsNullOrWhiteSpace(), x => x.Name.Contains(filterText) || x.Code.Contains(filterText))` để lọc.
                *   Áp dụng Sắp xếp (`OrderBy(sorting ?? nameof(Country.Name))`).
                *   Áp dụng Phân trang (`Skip(skipCount).Take(maxResultCount)`).
                *   Sử dụng `ToListAsync()`.
            *   `GetCountAsync`: Tương tự `GetListAsync` nhưng chỉ `WhereIf` và `LongCountAsync()`.
    *   **Cập nhật DbContext:**
        *   Trong `CoreOracleDbContext.cs`, thêm `public DbSet<Country> Countries { get; set; }`.
    *   **Cấu hình Entity Mapping:**
        *   Trong `CoreOracleDbContextModelCreatingExtensions.cs`, trong method `ConfigureCoreOracle`, thêm:
          ```csharp
          builder.Entity<Country>(b =>
          {
              b.ToTable(CoreOracleConsts.DbTablePrefix + "Countries", CoreOracleConsts.DbSchema); // Chuẩn tên bảng
              b.ConfigureByConvention(); // Áp dụng convention

              b.Property(x => x.Name).IsRequired().HasMaxLength(CountryConsts.MaxNameLength);
              b.Property(x => x.Code).IsRequired().HasMaxLength(CountryConsts.MaxCodeLength);
              b.Property(x => x.NumericCode).HasMaxLength(CountryConsts.MaxNumericCodeLength);

              b.HasIndex(x => x.Code).IsUnique(); // Unique index
              b.HasIndex(x => x.NumericCode).IsUnique().HasFilter($"[{nameof(Country.NumericCode)}] IS NOT NULL"); // Unique index cho nullable
          });
          ```
          *(Cấu hình Fluent API đầy đủ)*

5.  **`Aqt.CoreOracle.Application`**
    *   **Implement AppService:**
        *   Tạo thư mục `Countries`.
        *   Tạo file `CountryAppService.cs`. *(Cấu trúc thư mục)*
        *   Namespace `Aqt.CoreOracle.Application.Countries`. *(Namespace chuẩn)*
        *   Class `public class CountryAppService : ApplicationService, ICountryAppService` *(Kế thừa `ApplicationService` và implement interface)*
        *   Inject `ICountryRepository _countryRepository;` và `CountryManager _countryManager;` (Domain Service nếu cần logic phức tạp).
        *   Áp dụng `[Authorize(CoreOraclePermissions.Countries.*)]` cho từng phương thức tương ứng. *(Phân quyền)*
        *   Implement `GetAsync(Guid id)`:
            *   Lấy `country = await _countryRepository.GetAsync(id);`.
            *   Return `ObjectMapper.Map<Country, CountryDto>(country);`. *(Sử dụng AutoMapper)*
        *   Implement `GetListAsync(GetCountryListInput input)`:
            *   Gọi `var totalCount = await _countryRepository.GetCountAsync(input.Filter);`.
            *   Gọi `var items = await _countryRepository.GetListAsync(input.Filter, input.Sorting, input.MaxResultCount, input.SkipCount);`.
            *   Return `new PagedResultDto<CountryDto>(totalCount, ObjectMapper.Map<List<Country>, List<CountryDto>>(items));`.
        *   Implement `CreateAsync(CreateUpdateCountryDto input)`:
            *   Kiểm tra trùng `Code`: `if (await _countryRepository.CheckCodeExistsAsync(input.Code)) { throw new UserFriendlyException(L[CoreOracleDomainErrorCodes.CountryCodeAlreadyExists], CoreOracleDomainErrorCodes.CountryCodeAlreadyExists); }` *(Validate nghiệp vụ trước)*
            *   Kiểm tra trùng `NumericCode` (nếu có).
            *   Tạo entity: `var country = new Country(GuidGenerator.Create(), input.Name, input.Code, input.NumericCode);` *(Sử dụng constructor internal)*
            *   Lưu: `await _countryRepository.InsertAsync(country, autoSave: true);`.
            *   Return `ObjectMapper.Map<Country, CountryDto>(country);`.
        *   Implement `UpdateAsync(Guid id, CreateUpdateCountryDto input)`:
            *   Kiểm tra trùng `Code` (loại trừ bản ghi hiện tại): `if (await _countryRepository.CheckCodeExistsAsync(input.Code, id)) { ... }`
            *   Kiểm tra trùng `NumericCode` (loại trừ bản ghi hiện tại).
            *   Lấy entity: `var country = await _countryRepository.GetAsync(id);`.
            *   Cập nhật entity: `country.Update(input.Name, input.Code, input.NumericCode);` *(Gọi phương thức trong Entity)*
            *   Lưu: `await _countryRepository.UpdateAsync(country, autoSave: true);`.
            *   Return `ObjectMapper.Map<Country, CountryDto>(country);`.
        *   Implement `DeleteAsync(Guid id)`:
            *   Gọi `await _countryRepository.DeleteAsync(id, autoSave: true);`.
    *   **Cấu hình AutoMapper:**
        *   Trong `CoreOracleApplicationAutoMapperProfile.cs`, thêm:
            *   `CreateMap<Country, CountryDto>();`
            *   `CreateMap<CreateUpdateCountryDto, Country>() // Cần cấu hình bỏ qua Id khi mapping nếu dùng constructor`. Hoặc không cần map này nếu tạo entity thủ công trong AppService.

## Phase 3: Xây dựng UI & Migration (Tuân thủ `web-rule.mdc`)

6.  **`Aqt.CoreOracle.Web`**
    *   **Tạo Razor Page (List):**
        *   Tạo thư mục `Pages/Countries`.
        *   Tạo Razor Page `Index.cshtml` và `Index.cshtml.cs`. *(Chỉ dùng Razor Pages)*
        *   Namespace `Aqt.CoreOracle.Web.Pages.Countries`.
        *   Trong `Index.cshtml`:
            *   Sử dụng `abp-card`, `abp-table` (`id="CountriesTable"`), `abp-button` (nút "New Country").
            *   Cấu hình `abp-table` với `remote-data-source="@Url.Page(null, "countries")"` và `request-method="get"`. *(Đường dẫn API ngầm định hoặc cấu hình rõ)*
            *   Định nghĩa các cột (`abp-column`) cho `Name`, `Code`, `NumericCode` và cột Actions (`abp-action-column`).
            *   Trong `abp-action-column`, thêm `abp-button` cho Edit và Delete, sử dụng `policy` để kiểm tra quyền (`@CoreOraclePermissions.Countries.Edit`, `@CoreOraclePermissions.Countries.Delete`).
            *   Thêm `abp-card-header` chứa bộ lọc (input `id="FilterText"`) và nút tìm kiếm (`abp-button id="SearchButton"`).
            *   Nút "New Country" (`abp-button id="NewCountryButton"`) đặt trong `abp-card-header`, sử dụng `policy="@CoreOraclePermissions.Countries.Create"`.
    *   **Tạo Modal (Create/Edit):**
        *   Tạo Razor Page `CreateModal.cshtml` và `CreateModal.cshtml.cs` trong `Pages/Countries`.
        *   Tạo Razor Page `EditModal.cshtml` và `EditModal.cshtml.cs` trong `Pages/Countries`.
        *   Trong `PageModel` của các modal:
            *   Inject `ICountryAppService`.
            *   Khai báo `[BindProperty]` cho `public CreateUpdateCountryDto Country { get; set; }`.
            *   Khai báo `[BindProperty(SupportsGet = true)] public Guid Id { get; set; }` cho `EditModalModel`.
            *   `OnGetAsync` (EditModal): `var countryDto = await _countryAppService.GetAsync(Id); Country = ObjectMapper.Map<CountryDto, CreateUpdateCountryDto>(countryDto);` *(Sử dụng ObjectMapper nếu cần map DTO -> ViewModel/Input DTO)*
            *   `OnPostAsync`: Gọi `await _countryAppService.CreateAsync(Country);` hoặc `await _countryAppService.UpdateAsync(Id, Country);`. Return `NoContent();`.
        *   Trong `.cshtml` của các modal:
            *   Sử dụng `abp-modal`, `abp-dynamic-form` (`abp-form`) với `asp-page-handler="OnPost"`.
            *   Bind `abp-dynamic-form` với `Model.Country`.
            *   Thêm `abp-modal-footer` chứa `abp-button` (type="button" data-bs-dismiss="modal", type="submit").
            *   Thêm `abp-validation-summary />` *(Hiển thị lỗi validation)*
    *   **Viết JavaScript:**
        *   Tạo file `wwwroot/pages/countries/index.js`. *(Cấu trúc JS theo module)*
        *   Sử dụng `$(function () { ... });`
        *   Khởi tạo `abp.ModalManager` (`var _modalManager = new abp.ModalManager(...)`).
        *   Khởi tạo `abp.libs.datatables.DataTable`:
            *   `var dataTable = $('#CountriesTable').DataTable(abp.libs.datatables.normalizeConfiguration({ ... }));`
            *   Cấu hình `serverSide: true`, `paging: true`, `ordering: true`, `searching: false`.
            *   `ajax: abp.libs.datatables.createAjax(_countryAppService.getList, getFilter)`.
            *   Định nghĩa `columnDefs` cho các cột, bao gồm cột Actions với render buttons Edit/Delete.
        *   Viết hàm `getFilter()` để trả về object `{ filter: $('#FilterText').val() }`.
        *   Xử lý sự kiện click `#NewCountryButton`: `_modalManager.open({ ..., url: '/Countries/CreateModal' });`.
        *   Xử lý sự kiện click nút Edit trong bảng: Lấy `id` từ `data.record.id`, mở `EditModal`: `_modalManager.open({ ..., url: '/Countries/EditModal?Id=' + id });`.
        *   Xử lý sự kiện click nút Delete: Lấy `id`, hiển thị confirm (`abp.message.confirm`), gọi `_countryAppService.delete(id)`, refresh table (`dataTable.ajax.reload()`), hiển thị thông báo thành công (`abp.notify.success`).
        *   Xử lý sự kiện click `#SearchButton` hoặc Enter trên `#FilterText`: `dataTable.ajax.reload();`.
        *   Refresh table khi modal được lưu thành công: `_modalManager.onResult(function () { dataTable.ajax.reload(); });`.
    *   **Đăng ký JS/CSS:**
        *   Trong `CoreOracleWebModule.cs`, `ConfigureBundling`: Thêm file `index.js` vào bundle phù hợp (ví dụ: global hoặc tạo bundle riêng cho countries).
    *   **Cập nhật Menu:**
        *   Trong `CoreOracleMenuContributor.cs`, `ConfigureMainMenuAsync`:
            *   Kiểm tra quyền `await context.IsGrantedAsync(CoreOraclePermissions.Countries.Default)`.
            *   Thêm `ApplicationMenuItem` cho "Countries" (key `Menu:Countries`, URL `/Countries`, icon).
            *   Đặt menu này vào vị trí hợp lý (ví dụ: dưới menu Administration hoặc tạo group mới).
    *   **Localization:**
        *   Sử dụng `IStringLocalizer<CoreOracleResource>` (ví dụ `L["..."]`) cho mọi text trên UI (`.cshtml` và `PageModel` nếu cần).

7.  **Database Migration:**
    *   Mở terminal tại thư mục `Aqt.CoreOracle.EntityFrameworkCore`.
    *   Chạy `dotnet ef migrations add Added_Country_Entity -c CoreOracleDbContext`.
    *   Kiểm tra kỹ file migration được tạo.
    *   Mở terminal tại thư mục `Aqt.CoreOracle.DbMigrator` (hoặc Host).
    *   Chạy `dotnet run` để áp dụng migration.

## Phase 4: Kiểm thử và Hoàn thiện

8.  **Kiểm thử:**
    *   **Chức năng CRUD:** Đầy đủ các bước thêm, sửa, xóa, xem danh sách.
    *   **Phân quyền:** Đăng nhập với các user khác nhau (có/không có quyền) để kiểm tra UI và API access.
    *   **Validation:** Cả client-side (form) và server-side (AppService, Entity). Test trường hợp trùng `Code`, `NumericCode`.
    *   **Tìm kiếm/Lọc:** Kiểm tra với các từ khóa khác nhau.
    *   **Phân trang & Sắp xếp:** Kiểm tra các chức năng của DataTable.
    *   **Localization:** Chuyển đổi ngôn ngữ để kiểm tra hiển thị.
    *   **Responsive:** Kiểm tra trên các trình duyệt và kích thước màn hình khác nhau.
    *   **Performance:** Kiểm tra tốc độ tải trang và tương tác với lượng dữ liệu lớn (nếu có thể giả lập).
9.  **Review & Refactor:**
    *   Rà soát code theo các rules (`*.mdc`).
    *   Xóa code/comment thừa.
    *   Tối ưu hóa truy vấn (ví dụ: đảm bảo chỉ `Include` khi cần thiết, dùng `AsNoTracking`).
    *   Chạy linter/analyzer và sửa lỗi/warning.

---

Vui lòng xem xét kế hoạch (v2) này. Nếu được phê duyệt, chúng tôi sẽ bắt đầu triển khai dựa trên phiên bản này. 