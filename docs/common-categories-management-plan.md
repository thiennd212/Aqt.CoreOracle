# Kế hoạch Chi tiết Xây dựng Chức năng Quản lý Danh mục Dùng chung

**Mục tiêu:** Xây dựng chức năng quản lý danh mục dùng chung, cho phép tạo, sửa, xóa, và tổ chức các danh mục theo cấu trúc phân cấp (cha-con). Các danh mục này có thể được sử dụng lại trong các module khác của hệ thống.

**Công nghệ/Thành phần chính:**

*   ABP Framework
*   Entity Framework Core (Oracle Provider)
*   ASP.NET Core Razor Pages
*   Localization (Tiếng Anh & Tiếng Việt)
*   DataTables & jsTree (cho UI nếu cần hiển thị cây)

---

**Quy ước Chung Quan trọng:**

*   *(Kế thừa từ kế hoạch trước)* **Namespace cho Permissions:** Khi tham chiếu đến các hằng số permission trong mã C#, luôn đảm bảo sử dụng đúng `using` namespace nơi chúng được định nghĩa (ví dụ: `using Aqt.CoreOracle.Permissions;`).
*   *(Kế thừa từ kế hoạch trước)* **Quy ước Tích hợp DataTables với ABP Backend:** Tuân thủ quy ước về việc sử dụng `PagedAndSortedResultRequestDto`, `createAjax`, ánh xạ dữ liệu và `rowAction`.
*   *(Kế thừa từ kế hoạch trước)* **Quy ước Kiểm tra Chất lượng Code:** Áp dụng quy trình kiểm tra linter/build, giải thích, phê duyệt và khắc phục sau *mỗi bước* để đảm bảo code sạch và hoạt động ổn định.

---

## Giai đoạn 1: Thiết lập nền tảng Domain & Application Contracts

- [ ] ### Bước 1: Định nghĩa Constants, Shared Elements (`src/Aqt.CoreOracle.Domain.Shared`)

*   **Mục tiêu:** Định nghĩa các hằng số, enum (nếu có), localization keys dùng chung cho module danh mục.
*   **Thực hiện:**
    *   **Tệp:** `src/Aqt.CoreOracle.Domain.Shared/CoreOracleConsts.cs`
        *   Thêm hằng số cho độ dài: `MaxCategoryNameLength` (ví dụ: 256), `MaxCategoryCodeLength` (ví dụ: 64), `MaxCategoryDescriptionLength` (ví dụ: 1024).
    *   **Tệp:** `src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/en.json`
        *   Thêm các keys: `Menu:CommonManagement`, `Menu:CommonCategories`, `CommonCategories`, `NewCategory`, `EditCategory`, `Category:Name`, `Category:Code`, `Category:Description`, `Category:Parent`, `Category:IsActive`, `Category:SelectParent`, `SuccessfullySaved`, `Actions`, `RemoveCategoryConfirmation`, `CannotDeleteCategoryWithChildren`, `CategoryTree`, `MoveCategory`.
        *   Bổ sung keys cho mã lỗi: `"CoreOracle:10001": "Category code '{0}' already exists"`, `"CoreOracle:10002": "Parent category with ID '{0}' was not found"`, `"CoreOracle:10003": "Cannot delete a category that has child categories"`, `"CoreOracle:10004": "Cannot move a category under itself or one of its descendants"`.
    *   **Tệp:** Tạo `src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/vi.json`
        *   Thêm các keys tiếng Việt tương ứng với `en.json`. Ví dụ: "Quản lý Chung", "Danh mục Chung", "Thêm Danh mục", "Sửa Danh mục", "Tên Danh mục", "Mã Danh mục", "Mô tả", "Danh mục Cha", "Hoạt động", "Chọn Danh mục Cha", "Lưu thành công", "Hành động", "Bạn có chắc muốn xóa danh mục này?", "Không thể xóa danh mục có chứa danh mục con", "Cây Danh mục", "Di chuyển Danh mục".
        *   Bổ sung keys tiếng Việt cho mã lỗi: `"CoreOracle:10001": "Mã danh mục '{0}' đã tồn tại"`, `"CoreOracle:10002": "Không tìm thấy danh mục cha có ID '{0}'"`, `"CoreOracle:10003": "Không thể xóa danh mục có chứa danh mục con"`, `"CoreOracle:10004": "Không thể di chuyển danh mục vào chính nó hoặc danh mục con của nó"`.
    *   **Tệp:** `src/Aqt.CoreOracle.Domain.Shared/CoreOracleDomainErrorCodes.cs`
        *   Thêm mã lỗi: `CategoryCodeAlreadyExists`, `ParentCategoryNotFound`, `CannotDeleteCategoryWithChildren`, `CannotMoveCategoryToChild`.
    *   **Tệp:** `src/Aqt.CoreOracle.Domain.Shared/CoreOracleDomainSharedModule.cs`
        *   Đảm bảo `vi.json` được thêm vào Virtual File System và ngôn ngữ `vi` được đăng ký trong `AbpLocalizationOptions`.

- [ ] ### Bước 2: Định nghĩa Domain Entities (`src/Aqt.CoreOracle.Domain`)

*   **Mục tiêu:** Tạo thực thể nghiệp vụ cho Danh mục Chung.
*   **Thực hiện:**
    *   **Tệp:** Tạo `src/Aqt.CoreOracle.Domain/CommonCategories/CommonCategory.cs`
        *   Kế thừa `FullAuditedAggregateRoot<Guid>`.
        *   Thuộc tính: `ParentId` (Guid?), `Name` (string), `Code` (string), `Description` (string?), `IsActive` (bool).
        *   Constructor với `Check.NotNullOrWhiteSpace` cho `Name` và `Code`. Đặt giá trị mặc định cho `IsActive` là `true`.
        *   Thêm phương thức để cập nhật `ParentId` (có thể cần logic kiểm tra để tránh vòng lặp).

- [ ] ### Bước 3: Định nghĩa Domain Repository Interfaces (`src/Aqt.CoreOracle.Domain`)

*   **Mục tiêu:** Khai báo các phương thức truy cập dữ liệu tùy chỉnh cho danh mục.
*   **Thực hiện:**
    *   **Tệp:** Tạo `src/Aqt.CoreOracle.Domain/CommonCategories/ICommonCategoryRepository.cs` (`: IRepository<CommonCategory, Guid>`).
    *   Thêm các phương thức cần thiết:
        *   `Task<CommonCategory> FindByCodeAsync(string code, bool includeDetails = false);`
        *   `Task<List<CommonCategory>> GetChildrenAsync(Guid? parentId, bool includeDetails = false);`
        *   `Task<List<CommonCategory>> GetAllChildrenWithParentCodeAsync(string parentCode, bool includeDetails = false);` (Hữu ích để lấy cây con)
        *   `Task<List<CommonCategory>> GetListAsync(string sorting, int skipCount, int maxResultCount, string filter = null, bool? isActive = null, Guid? parentId = null);` (Để hỗ trợ lọc và phân trang)
        *   `Task<long> GetCountAsync(string filter = null, bool? isActive = null, Guid? parentId = null);`

- [ ] ### Bước 4: Định nghĩa Application Contract DTOs (`src/Aqt.CoreOracle.Application.Contracts`)

*   **Mục tiêu:** Tạo các đối tượng truyền dữ liệu (DTOs) cho Danh mục Chung.
*   **Thực hiện:**
    *   **Thư mục:** `src/Aqt.CoreOracle.Application.Contracts/CommonCategories/`
        *   Tạo `CommonCategoryDto.cs` (`: FullAuditedEntityDto<Guid>`). Thêm các thuộc tính: `ParentId` (Guid?), `Name` (string), `Code` (string), `Description` (string?), `IsActive` (bool), `ParentName` (string?).
        *   Tạo `CreateUpdateCommonCategoryDto.cs`. Thêm các thuộc tính: `ParentId` (Guid?), `Name` (string), `Code` (string), `Description` (string?), `IsActive` (bool). Sử dụng Data Annotations (`[Required]`, `[StringLength]`).
        *   Tạo `GetCommonCategoryListInput.cs` (`: PagedAndSortedResultRequestDto`). Thêm các thuộc tính lọc: `Filter` (string?), `IsActive` (bool?), `ParentId` (Guid?).
        *   Tạo `CommonCategoryLookupDto.cs` (`: EntityDto<Guid>`, chứa `Name`, `Code`) để sử dụng trong dropdown chọn parent.
        *   Tạo `CommonCategoryTreeNodeDto.cs` (`: EntityDto<Guid>`). Thêm `ParentId` (Guid?), `Code`, `DisplayName` (string), `IsActive` (bool), `Children` (`List<CommonCategoryTreeNodeDto>`).
        *   Tạo `MoveCategoryInput.cs` (`: EntityDto<Guid>`). Thêm `NewParentId` (Guid?).

- [ ] ### Bước 5: Định nghĩa Application Contract Service Interfaces (`src/Aqt.CoreOracle.Application.Contracts`)

*   **Mục tiêu:** Khai báo các nghiệp vụ tầng Application cho Danh mục Chung.
*   **Thực hiện:**
    *   **Tệp:** Tạo `src/Aqt.CoreOracle.Application.Contracts/CommonCategories/ICommonCategoryAppService.cs`
        *   Kế thừa `ICrudAppService<CommonCategoryDto, Guid, GetCommonCategoryListInput, CreateUpdateCommonCategoryDto>`.
        *   Định nghĩa các phương thức bổ sung:
            *   `Task<ListResultDto<CommonCategoryLookupDto>> GetParentLookupAsync();` (Lấy danh sách danh mục để chọn làm cha)
            *   `Task<List<CommonCategoryTreeNodeDto>> GetTreeAsync();` (Lấy toàn bộ cây danh mục)
            *   `Task MoveAsync(Guid id, MoveCategoryInput input);` (Di chuyển danh mục)
            *   `Task<PagedResultDto<CommonCategoryDto>> GetListAsync(GetCommonCategoryListInput input);` (Override nếu cần logic lọc/sắp xếp phức tạp hơn CrudAppService)

- [ ] ### Bước 6: Định nghĩa Permissions (`src/Aqt.CoreOracle.Application.Contracts`)

*   **Mục tiêu:** Định nghĩa quyền truy cập cho quản lý danh mục chung.
*   **Thực hiện:**
    *   **Tệp:** `src/Aqt.CoreOracle.Application.Contracts/Permissions/CoreOraclePermissions.cs`
        *   Thêm lớp static `CommonManagement` chứa consts: `CommonCategories.Default`, `CommonCategories.Create`, `CommonCategories.Update`, `CommonCategories.Delete`, `CommonCategories.Move`.
    *   **Tệp:** `src/Aqt.CoreOracle.Application.Contracts/Permissions/CoreOraclePermissionDefinitionProvider.cs`
        *   Trong `Define`, thêm group "CommonManagement" và các permission `CommonCategories` đã định nghĩa ở trên.

---

## Giai đoạn 2: Triển khai Backend

- [ ] ### Bước 7: Triển khai EF Core Mappings (`src/Aqt.CoreOracle.EntityFrameworkCore`)

*   **Mục tiêu:** Cấu hình ánh xạ database cho `CommonCategory`.
*   **Thực hiện:**
    *   **Tệp:** `src/Aqt.CoreOracle.EntityFrameworkCore/EntityFrameworkCore/CoreOracleDbContext.cs`
        *   Thêm `DbSet<CommonCategory> CommonCategories { get; set; }`.
        *   Trong `OnModelCreating`, cấu hình `builder.Entity<CommonCategory>(...)` với `ToTable` (ví dụ: `AppCommonCategories`), `ConfigureByConvention`, khóa ngoại (`HasOne(x => x.Parent).WithMany().HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.Restrict)`), và indexes (`HasIndex(x => x.Code).IsUnique()`, `HasIndex(x => x.ParentId)`).

- [ ] ### Bước 8: Triển khai Custom Repositories (`src/Aqt.CoreOracle.EntityFrameworkCore`)

*   **Mục tiêu:** Cung cấp logic truy cập dữ liệu tùy chỉnh cho `CommonCategory`.
*   **Thực hiện:**
    *   **Tệp:** Tạo `src/Aqt.CoreOracle.EntityFrameworkCore/EntityFrameworkCore/CommonCategories/EfCoreCommonCategoryRepository.cs` (`: EfCoreRepository<CoreOracleDbContext, CommonCategory, Guid>, ICommonCategoryRepository`).
    *   Triển khai các phương thức tùy chỉnh đã định nghĩa trong `ICommonCategoryRepository` (sử dụng LINQ, `AsyncExecuter`, `ToListAsync`, `FirstOrDefaultAsync`, `CountAsync`). Sử dụng `IncludeDetails` để quyết định có `Include(x => x.Parent)` hay không.

- [ ] ### Bước 9: Triển khai Application Services (`src/Aqt.CoreOracle.Application`)

*   **Mục tiêu:** Viết logic nghiệp vụ cho quản lý Danh mục Chung.
*   **Thực hiện:**
    *   **Tệp:** Tạo `src/Aqt.CoreOracle.Application/CommonCategories/CommonCategoryAppService.cs`
        *   Kế thừa `CrudAppService<CommonCategory, CommonCategoryDto, Guid, GetCommonCategoryListInput, CreateUpdateCommonCategoryDto>, ICommonCategoryAppService`.
        *   Set các policy names từ permissions (`CoreOraclePermissions.CommonManagement...`).
        *   Inject `ICommonCategoryRepository`, `IObjectMapper`.
        *   Override `CreateAsync`, `UpdateAsync` để kiểm tra logic nghiệp vụ (ví dụ: check mã code tồn tại, check parent hợp lệ).
        *   Implement `GetParentLookupAsync` (lấy danh sách `Id`, `Name`, `Code`).
        *   Implement `GetTreeAsync` (lấy tất cả danh mục, xây dựng cấu trúc cây trong bộ nhớ).
        *   Implement `MoveAsync` (kiểm tra logic không di chuyển vào chính nó hoặc con cháu, cập nhật `ParentId`, gọi `_repository.UpdateAsync`).
        *   Override `GetListAsync` để sử dụng phương thức `GetListAsync` và `GetCountAsync` tùy chỉnh từ repository, xử lý việc lấy `ParentName` cho DTO.
        *   Áp dụng `[Authorize(...)]` cho các phương thức tùy chỉnh.

- [ ] ### Bước 10: Cấu hình AutoMapper (`src/Aqt.CoreOracle.Application`)

*   **Mục tiêu:** Định nghĩa ánh xạ Entity <-> DTO cho `CommonCategory`.
*   **Thực hiện:**
    *   **Tệp:** `src/Aqt.CoreOracle.Application/CoreOracleApplicationAutoMapperProfile.cs`
        *   Thêm `CreateMap<CommonCategory, CommonCategoryDto>()` (ánh xạ `ParentName` sẽ được xử lý trong service).
        *   Thêm `CreateMap<CreateUpdateCommonCategoryDto, CommonCategory>();`
        *   Thêm `CreateMap<CommonCategory, CommonCategoryLookupDto>();`
        *   Thêm `CreateMap<CommonCategory, CommonCategoryTreeNodeDto>()` (`DisplayName` map từ `Name`).

- [ ] ### Bước 11: Database Migration

*   **Mục tiêu:** Cập nhật schema database để thêm bảng `CommonCategories`.
*   **Thực hiện:**
    1.  Từ thư mục `src/Aqt.CoreOracle.EntityFrameworkCore`, chạy: `abp add-migration Added_Common_Categories` (hoặc tên phù hợp).
    2.  Kiểm tra file migration mới tạo (phải có `CreateTable` cho `AppCommonCategories` với các cột, khóa ngoại, index đã định nghĩa).
    3.  Áp dụng migration vào database bằng một trong hai cách đã mô tả trong kế hoạch trước (`abp update-database` hoặc chạy `DbMigrator`).

---

## Giai đoạn 3: Xây dựng Giao diện Người dùng (UI - Web)

- [ ] ### Bước 12: Phát triển UI (`src/Aqt.CoreOracle.Web`)

*   **Mục tiêu:** Tạo giao diện người dùng cho quản lý Danh mục Chung.
*   **Thực hiện (Giả định Razor Pages):**
    *   **(Lưu ý quan trọng):** Áp dụng các quy ước về `<abp-dynamic-form>` với modal, biến proxy JavaScript, biến permission JavaScript, truyền permission từ C# sang JS, query string cho modal, và vị trí AutoMapper profile (trong Web) như đã nêu trong kế hoạch trước.
    *   **Tạo Trang chính:**
        *   Tạo `Pages/CommonCategories/Index.cshtml` + `.cs`.
        *   Phân quyền `CommonCategories.Default` trên PageModel.
        *   Hiển thị bảng DataTables (`<abp-table>`) danh sách các danh mục.
        *   Cấu hình DataTables (`Index.js`):
            *   Sử dụng `abp.libs.datatables.createAjax(commonCategoryService.getList, function(input) { ... })`.
            *   Thêm các bộ lọc (text filter, dropdown `IsActive`) và gán giá trị vào `input` trong callback `createAjax`.
            *   Định nghĩa các cột (`columnDefs`): `Name`, `Code`, `ParentName` (có thể null), `IsActive` (`dataFormat: 'boolean'`), `CreationTime` (`dataFormat: 'datetime'`).
            *   Thêm cột `Actions` sử dụng `rowAction` với các nút Edit, Delete (kiểm tra quyền `CommonCategories.Update`/`Delete` bằng `visible: abp.auth.isGranted(...)`).
            *   Thêm nút "New Category" (kiểm tra quyền `CommonCategories.Create`).
        *   Trong PageModel (`Index.cshtml.cs`): Inject `ICommonCategoryAppService`, tạo thuộc tính ViewModel chứa các chuỗi permission cần thiết và serialize sang JavaScript.
    *   **Modal Tạo Danh mục:**
        *   Tạo `Pages/CommonCategories/CreateModal.cshtml` + `.cs`.
        *   Phân quyền `CommonCategories.Create`.
        *   Sử dụng `<abp-dynamic-form>` bao bọc `<abp-modal>`.
        *   ViewModel (`CreateCategoryViewModel`): `ParentId` (Guid?), `Name`, `Code`, `Description`, `IsActive` (bool, default true). Trang trí bằng Data Annotations.
        *   **Dropdown Chọn Parent:** Trong `CreateModal.cshtml`, bên trong `<abp-modal-body>` và `<abp-form-content>`, thêm `<abp-select asp-for="Input.ParentId" asp-items="@Model.ParentCategoryList" label="@L["Category:Parent"].Value" />`.
        *   Trong PageModel (`CreateModal.cshtml.cs`):
            *   Inject `ICommonCategoryAppService`.
            *   Thêm thuộc tính `[BindProperty]` cho `CreateCategoryViewModel Input { get; set; }`.
            *   Thêm thuộc tính `public SelectList ParentCategoryList { get; set; }`.
            *   Trong `OnGetAsync()`, gọi `_commonCategoryService.GetParentLookupAsync()` để lấy danh sách lookup, tạo `SelectList` từ kết quả và gán vào `ParentCategoryList` (thêm một option "--- Select Parent ---" với value null).
            *   Trong `OnPostAsync()`, gọi `_commonCategoryService.CreateAsync(ObjectMapper.Map<CreateCategoryViewModel, CreateUpdateCommonCategoryDto>(Input))`.
            *   Tạo `MyProjectWebAutoMapperProfile.cs` (nếu chưa có) và thêm `CreateMap<CreateCategoryViewModel, CreateUpdateCommonCategoryDto>();`.
    *   **Modal Sửa Danh mục:**
        *   Tạo `Pages/CommonCategories/EditModal.cshtml` + `.cs`.
        *   Phân quyền `CommonCategories.Update`.
        *   Sử dụng `<abp-dynamic-form>` bao bọc `<abp-modal>`.
        *   ViewModel (`EditCategoryViewModel`): Tương tự `CreateCategoryViewModel`, nhưng có thêm `Id` (Guid).
        *   **Dropdown Chọn Parent:** Tương tự CreateModal.
        *   Trong PageModel (`EditModal.cshtml.cs`):
            *   Inject `ICommonCategoryAppService`.
            *   Thêm thuộc tính `[BindProperty]` cho `EditCategoryViewModel Input { get; set; }`.
            *   Thêm thuộc tính `public SelectList ParentCategoryList { get; set; }`.
            *   Trong `OnGetAsync(Guid id)`, gọi `_commonCategoryService.GetAsync(id)` để lấy dữ liệu category hiện tại, map sang `EditCategoryViewModel`. Đồng thời, gọi `GetParentLookupAsync()` để chuẩn bị `ParentCategoryList` (có thể cần loại bỏ category hiện tại khỏi danh sách cha).
            *   Trong `OnPostAsync()`, gọi `_commonCategoryService.UpdateAsync(Input.Id, ObjectMapper.Map<EditCategoryViewModel, CreateUpdateCommonCategoryDto>(Input))`.
            *   Trong `MyProjectWebAutoMapperProfile.cs`, thêm `CreateMap<EditCategoryViewModel, CreateUpdateCommonCategoryDto>();` và `CreateMap<CommonCategoryDto, EditCategoryViewModel>();`.
    *   **Xử lý Xóa:**
        *   Trong `Index.js`, khi nút Delete được click, hiển thị thông báo xác nhận `abp.message.confirm`.
        *   Nếu xác nhận, gọi `commonCategoryService.delete(record.id)`.
        *   Reload lại DataTable khi thành công (`dataTable.ajax.reload()`).
    *   **(Tùy chọn - Giao diện Cây):** Nếu cần hiển thị cây, có thể thêm thư viện jsTree, gọi `commonCategoryService.getTree()`