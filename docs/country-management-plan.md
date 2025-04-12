# Kế hoạch chi tiết xây dựng chức năng Quản lý Danh mục Quốc gia (Country Management)

**Mục tiêu:** Xây dựng giao diện và API cho phép người dùng quản lý (Xem danh sách, Thêm, Sửa, Xóa) danh mục các quốc gia trong hệ thống theo kiến trúc ABP Framework.

**Các bước thực hiện chi tiết:**

## Phase 1: Định nghĩa Domain & Contracts

1.  **`Aqt.CoreOracle.Domain.Shared`**
    *   **Tạo Constants:**
        *   Tạo file `Countries/CountryConsts.cs`.
        *   Định nghĩa các hằng số về độ dài tối đa cho các trường (ví dụ: `MaxNameLength = 100`, `MaxCodeLength = 3`).
    *   **Localization:**
        *   Thêm các khóa localization cần thiết cho module Quốc gia vào các file JSON trong `Localization/CoreOracle/` (ví dụ: `Menu:Countries`, `Countries`, `NewCountry`, `EditCountry`, `SuccessfullyDeleted`, `CountryName`, `CountryCode`, `NumericCode`, `AreYouSureYouWantToDelete`, etc.).
    *   **Error Codes (Nếu cần):**
        *   Định nghĩa các mã lỗi đặc thù (nếu có) trong `CoreOracleDomainErrorCodes.cs` (ví dụ: `CountryCodeAlreadyExists`).

2.  **`Aqt.CoreOracle.Domain`**
    *   **Tạo Entity:**
        *   Tạo thư mục `Countries`.
        *   Tạo file `Entities/Country.cs`.
        *   Class `Country` kế thừa từ `AuditedAggregateRoot<Guid>`.
        *   Thêm các thuộc tính:
            *   `Name` (string, `[Required]`, `[MaxLength(CountryConsts.MaxNameLength)]`)
            *   `Code` (string, `[Required]`, `[MaxLength(CountryConsts.MaxCodeLength)]`) - *Cân nhắc thêm unique index.*
            *   `NumericCode` (string?, `[MaxLength(CountryConsts.MaxCodeLength)]`) - *Cân nhắc thêm unique index.*
        *   Constructor để khởi tạo và validate (nếu cần logic phức tạp ban đầu).
    *   **Tạo Repository Interface:**
        *   Tạo file `Countries/ICountryRepository.cs`.
        *   Interface `ICountryRepository` kế thừa `IRepository<Country, Guid>`.
        *   (Tùy chọn) Thêm các phương thức truy vấn đặc thù nếu cần (ví dụ: `Task<Country?> FindByCodeAsync(string code)`, `Task<bool> IsCodeExistingAsync(string code, Guid? excludingId = null)`).

3.  **`Aqt.CoreOracle.Application.Contracts`**
    *   **Định nghĩa Permissions:**
        *   Trong `Permissions/CoreOraclePermissions.cs`, thêm một static class `Countries`.
        *   Định nghĩa các quyền: `Default` (View), `Create`, `Edit`, `Delete`. Format: `CoreOracle.Countries.View`, `CoreOracle.Countries.Create`, ...
    *   **Đăng ký Permissions:**
        *   Trong `CoreOraclePermissionDefinitionProvider.cs`, lấy group `CoreOraclePermissions.GroupName` và thêm các permission của `Countries` đã định nghĩa ở trên, sử dụng localization key cho display name.
    *   **Tạo DTOs:**
        *   Tạo thư mục `Countries/Dtos`.
        *   Tạo `CountryDto.cs`: Kế thừa `AuditedEntityDto<Guid>`. Chứa `Id`, `Name`, `Code`, `NumericCode`.
        *   Tạo `CreateUpdateCountryDto.cs`: Chứa `Name`, `Code`, `NumericCode`. Áp dụng `DataAnnotations` (`[Required]`, `[StringLength]`).
        *   Tạo `GetCountryListInput.cs`: Kế thừa `PagedAndSortedResultRequestDto`. Thêm thuộc tính `Filter` (string?) để tìm kiếm.
    *   **Tạo AppService Interface:**
        *   Tạo thư mục `Countries`.
        *   Tạo file `ICountryAppService.cs`.
        *   Interface `ICountryAppService` kế thừa `ICrudAppService<CountryDto, Guid, GetCountryListInput, CreateUpdateCountryDto, CreateUpdateCountryDto>`.

## Phase 2: Implement Logic & Data Access

4.  **`Aqt.CoreOracle.EntityFrameworkCore`**
    *   **Implement Repository:**
        *   Tạo thư mục `Countries`.
        *   Tạo file `EfCoreCountryRepository.cs`.
        *   Class `EfCoreCountryRepository` kế thừa `EfCoreRepository<CoreOracleDbContext, Country, Guid>` và implement `ICountryRepository`.
        *   Implement các phương thức tùy chỉnh (ví dụ: `FindByCodeAsync`, `IsCodeExistingAsync`).
    *   **Cập nhật DbContext:**
        *   Trong `CoreOracleDbContext.cs`, thêm `public DbSet<Country> Countries { get; set; }`.
    *   **Cấu hình Entity Mapping:**
        *   Trong `CoreOracleDbContextModelCreatingExtensions.cs`, trong method `ConfigureCoreOracle`, thêm cấu hình cho `Country`:
            *   `builder.Entity<Country>(b => { ... });`
            *   Đặt tên bảng (`b.ToTable(CoreOracleConsts.DbTablePrefix + "Countries", CoreOracleConsts.DbSchema);`).
            *   Áp dụng cấu hình convention (`b.ConfigureByConvention();`).
            *   Cấu hình các thuộc tính (`Name`, `Code`, `NumericCode`) với `IsRequired`, `HasMaxLength`.
            *   Thêm unique index cho `Code` và `NumericCode` (nếu cần) dùng `b.HasIndex(q => q.Code).IsUnique();`.

5.  **`Aqt.CoreOracle.Application`**
    *   **Implement AppService:**
        *   Tạo thư mục `Countries`.
        *   Tạo file `CountryAppService.cs`.
        *   Class `CountryAppService` kế thừa `CrudAppService<Country, CountryDto, Guid, GetCountryListInput, CreateUpdateCountryDto, CreateUpdateCountryDto>` và implement `ICountryAppService`.
        *   Inject `ICountryRepository`.
        *   Áp dụng `[Authorize]` attribute cho các phương thức của class hoặc từng phương thức với permission tương ứng (`CoreOraclePermissions.Countries.*`).
        *   Override `CreateAsync` và `UpdateAsync` để kiểm tra `Code` trùng lặp bằng cách gọi `_countryRepository.IsCodeExistingAsync` trước khi lưu. Nếu trùng, throw `UserFriendlyException` với mã lỗi đã định nghĩa.
        *   Override `ApplyDefaultSorting` nếu cần sắp xếp mặc định khác (ví dụ: theo `Name`).
        *   Override `CreateFilteredQueryAsync` để thêm điều kiện lọc theo `input.Filter` cho các trường `Name`, `Code`.
    *   **Cấu hình AutoMapper:**
        *   Trong `CoreOracleApplicationAutoMapperProfile.cs`, thêm các mapping:
            *   `CreateMap<Country, CountryDto>();`
            *   `CreateMap<CreateUpdateCountryDto, Country>();`

## Phase 3: Xây dựng UI & Migration

6.  **`Aqt.CoreOracle.Web`**
    *   **Tạo Razor Page (List):**
        *   Tạo thư mục `Pages/Countries`.
        *   Tạo Razor Page `Index.cshtml` và `Index.cshtml.cs`.
        *   Trong `Index.cshtml`:
            *   Sử dụng thẻ `abp-card`, `abp-table`, `abp-button` (nút "New Country").
            *   Cấu hình `abp-table` để hiển thị các cột (`Name`, `Code`, `NumericCode`) và cột Actions (Edit, Delete). Sử dụng `data-ajax-url="/api/app/country"` (hoặc URL tương ứng).
            *   Thêm bộ lọc (ô input) và nút tìm kiếm phía trên bảng.
            *   Nút "New Country" (`abp-button`) chỉ hiển thị nếu user có quyền `Create`, sử dụng `policy="@CoreOraclePermissions.Countries.Create"`.
            *   Các nút Edit/Delete trong cột Actions của `abp-table` cũng kiểm tra quyền tương ứng (`Edit`, `Delete`) thông qua thuộc tính `policy`.
    *   **Tạo Modal (Create/Edit):**
        *   Tạo Razor Page `CreateModal.cshtml` và `CreateModal.cshtml.cs` trong `Pages/Countries`.
        *   Tạo Razor Page `EditModal.cshtml` và `EditModal.cshtml.cs` trong `Pages/Countries`.
        *   Trong `PageModel` của các modal:
            *   Inject `ICountryAppService`.
            *   Khai báo `[BindProperty]` cho `CreateUpdateCountryDto ViewModel { get; set; }`.
            *   Khai báo `[BindProperty(SupportsGet = true)] public Guid Id { get; set; }` cho `EditModalModel`.
            *   `OnGetAsync` (cho EditModal): Gọi `_countryAppService.GetAsync(Id)` để lấy dữ liệu, map sang `ViewModel`.
            *   `OnPostAsync`: Gọi `_countryAppService.CreateAsync(ViewModel)` hoặc `_countryAppService.UpdateAsync(Id, ViewModel)`.
        *   Trong `.cshtml` của các modal:
            *   Sử dụng `abp-modal`, `abp-dynamic-form` (`abp-form`) để bind với `ViewModel`.
            *   Thêm `abp-modal-footer` chứa các `abp-button` (Cancel, Save).
    *   **Viết JavaScript:**
        *   Tạo file `wwwroot/pages/countries/index.js`.
        *   Sử dụng ABP JavaScript API (`abp.libs.datatables.createAjax`, `abp.ModalManager`) để:
            *   Khởi tạo Datatable, cấu hình `ajax` để gọi API (`/api/app/country`), định nghĩa các `columnDefs` cho cột Actions (Edit, Delete buttons).
            *   Xử lý sự kiện click nút "New Country" để mở `CreateModal` thông qua `_modalManager.open(...)`.
            *   Xử lý sự kiện click nút "Edit" trong bảng để mở `EditModal` với `id` tương ứng.
            *   Xử lý sự kiện click nút "Delete", hiển thị confirm (`abp.message.confirm`), gọi API delete (`_countryAppService.delete`), và refresh lại table (`dataTable.ajax.reload()`).
            *   Xử lý sự kiện click nút tìm kiếm hoặc nhấn Enter trong ô lọc để lấy giá trị filter và refresh table (`dataTable.ajax.reload()`).
    *   **Cập nhật Menu:**
        *   Trong `CoreOracleMenuContributor.cs`, trong `ConfigureMainMenuAsync`, thêm một `ApplicationMenuItem` mới cho "Countries":
            *   Kiểm tra quyền `CoreOraclePermissions.Countries.Default` (`await context.IsGrantedAsync(...)`).
            *   Sử dụng localization key `L["Menu:Countries"]`.
            *   Link tới `/Countries`.
            *   Đặt menu này trong một group menu hợp lý (ví dụ: "Administration" hoặc "Settings").
    *   **Localization:**
        *   Đảm bảo tất cả các chuỗi text hiển thị trên UI (tên cột, tiêu đề modal, nút bấm, thông báo) đều sử dụng localization key đã thêm ở bước 1.

7.  **Database Migration:**
    *   Mở terminal trong thư mục `Aqt.CoreOracle.EntityFrameworkCore`.
    *   Chạy lệnh: `dotnet ef migrations add Added_Country_Entity -c CoreOracleDbContext`
    *   Kiểm tra file migration được tạo ra.
    *   Mở terminal trong thư mục `Aqt.CoreOracle.DbMigrator` (hoặc Host project).
    *   Chạy lệnh: `dotnet run` để áp dụng migration vào database.

## Phase 4: Kiểm thử và Hoàn thiện

8.  **Kiểm thử:**
    *   **Chức năng:** Kiểm tra chức năng Xem danh sách, Thêm, Sửa, Xóa trên giao diện.
    *   **Phân quyền:** Đăng nhập với user có/không có quyền Create/Edit/Delete để kiểm tra hiển thị nút và truy cập API.
    *   **Validation:** Kiểm tra validation phía client (nếu dùng `abp-dynamic-form`) và server (bỏ trống trường required, nhập quá độ dài, nhập trùng `Code`).
    *   **Tìm kiếm/Lọc:** Kiểm tra chức năng lọc hoạt động đúng.
    *   **Phân trang & Sắp xếp:** Kiểm tra các chức năng của Datatable.
    *   **Localization:** Kiểm tra hiển thị đúng ngôn ngữ (nếu có).
    *   **Responsive:** Kiểm tra giao diện trên các kích thước màn hình khác nhau.
9.  **Review & Refactor:**
    *   Rà soát lại code, đảm bảo tuân thủ các convention và rule đã đặt ra trong `*.mdc` files.
    *   Xóa bỏ code thừa, comment không cần thiết.
    *   Tối ưu các truy vấn LINQ và xử lý logic (nếu cần).
    *   Kiểm tra và sửa lỗi linter (nếu có).

---

Vui lòng xem xét kế hoạch này và cho biết nếu bạn phê duyệt hoặc cần điều chỉnh. 