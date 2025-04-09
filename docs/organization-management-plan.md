# Kế hoạch Chi tiết Xây dựng Chức năng Quản lý Cơ cấu Tổ chức

Dựa trên cấu trúc dự án `Aqt.CoreOracle` và yêu cầu bổ sung.

**Mục tiêu:** Xây dựng chức năng quản lý cơ cấu tổ chức, bao gồm Phòng ban (sử dụng `OrganizationUnit` của ABP), Chức vụ (`Position`), và việc gán Nhân viên (`IdentityUser`) vào các Phòng ban với Chức vụ cụ thể. Bổ sung thông tin Địa chỉ, Mã liên thông, Loại cho Phòng ban.

**Công nghệ/Thành phần chính:**

*   ABP Framework (với Identity Module)
*   Entity Framework Core (Oracle Provider)
*   ASP.NET Core Razor Pages (Giả định)
*   Hệ thống Mở rộng Thực thể (Extra Properties)
*   Localization (Tiếng Anh & Tiếng Việt)

---

**Quy ước Chung Quan trọng:**

*   **Namespace cho Permissions:** Khi tham chiếu đến các hằng số permission (ví dụ: `CoreOraclePermissions.GroupName.PermissionName`) trong **bất kỳ mã C# nào** (Application Services, Domain Services, PageModels, Tests, v.v.), **luôn đảm bảo sử dụng đúng `using` namespace** nơi các hằng số permission được định nghĩa. Trong dự án này, đó là `using Aqt.CoreOracle.Permissions;`. Tránh sử dụng các namespace khác như `.Application.Contracts.Permissions` trừ khi đó thực sự là nơi định nghĩa.

---

**Quy ước Tích hợp DataTables với ABP Backend:**

Để đảm bảo tính nhất quán và tận dụng các helper của ABP khi làm việc với DataTables trong giao diện người dùng:

1.  **Backend Service:**
    *   Định nghĩa một phương thức trong Application Service (ví dụ: `GetEntityListAsync`).
    *   Phương thức này nên nhận **một tham số DTO input duy nhất** (ví dụ: `GetEntityListInput`).
    *   DTO input này **phải kế thừa** từ `Volo.Abp.Application.Dtos.PagedAndSortedResultRequestDto` để tự động nhận các tham số `SkipCount`, `MaxResultCount`, `Sorting` từ DataTables.
    *   Thêm các thuộc tính cần thiết khác cho việc lọc dữ liệu (ví dụ: khóa ngoại, filter text) vào DTO input này.
    *   Phương thức backend nên trả về `Task<PagedResultDto<EntityDto>>`, trong đó `EntityDto` là DTO chứa dữ liệu cho mỗi dòng của bảng.

2.  **Frontend JavaScript (DataTables Configuration):**
    *   Sử dụng `serverSide: true` trong cấu hình DataTables.
    *   Trong phần `ajax`, sử dụng `abp.libs.datatables.createAjax` với 2 tham số:
        *   **Tham số 1:** Tham chiếu đến hàm proxy của service backend (ví dụ: `myApp.myModule.myService.getEntityList`). Luôn kiểm tra đường dẫn proxy được tạo tự động. Nên gán proxy vào biến JavaScript ở đầu file để dễ quản lý (`var myService = myApp.myModule.myService;`).
        *   **Tham số 2:** Một hàm callback `function(input) { ... }`. Hàm này nhận đối tượng `input` (đã được `createAjax` điền sẵn các giá trị `SkipCount`, `MaxResultCount`, `Sorting`). Bên trong hàm này, bạn **thêm các giá trị lọc tùy chỉnh** vào đối tượng `input` (ví dụ: `input.filter = $('#MyFilter').val();`). Cuối cùng, hàm callback này phải `return input;`.
    *   Không cần định nghĩa `responseCallback` nếu backend trả về đúng `PagedResultDto`. `createAjax` sẽ tự động xử lý và ánh xạ sang định dạng DataTables yêu cầu (`recordsTotal`, `recordsFiltered`, `data`).
    *   Sử dụng `columnDefs` để định nghĩa các cột, sử dụng `data: "propertyName"` để ánh xạ dữ liệu từ `EntityDto` trả về. Tận dụng `dataFormat` của ABP (ví dụ: `'datetime'`, `'boolean'`) để định dạng dữ liệu chuẩn. Sử dụng `rowAction` để thêm các hành động trên từng dòng.

---

**Quy ước Kiểm tra Chất lượng Code:**

Sau khi hoàn thành việc tạo mới hoặc chỉnh sửa các tệp trong *mỗi bước* của kế hoạch này, quy trình sau sẽ được tuân thủ **trước khi chuyển sang bước tiếp theo**:

1.  **Kiểm tra Linter Errors & Build:**
    *   Rà soát các tệp vừa thay đổi để phát hiện các lỗi hoặc cảnh báo về quy tắc viết code (ví dụ: style, naming conventions, unused usings, potential null reference issues, adherence to framework conventions).
    *   Thực hiện build solution (ví dụ: dùng `dotnet build`) để đảm bảo không có lỗi biên dịch.
2.  **Giải thích và Giải pháp:**
    *   Liệt kê các vấn đề (lỗi linter hoặc lỗi build) được tìm thấy.
    *   Giải thích nguyên nhân của từng vấn đề.
    *   Đề xuất giải pháp khắc phục cụ thể.
3.  **Lấy Phê duyệt Giải pháp:** Trình bày các vấn đề và giải pháp đề xuất để bạn xem xét và phê duyệt.
4.  **Áp dụng Giải pháp:** Thực hiện các chỉnh sửa đã được phê duyệt để loại bỏ lỗi và đảm bảo build thành công.

Quy trình này nhằm đảm bảo code luôn sạch sẽ, dễ đọc, tuân thủ các best practices, không có lỗi biên dịch và giảm thiểu lỗi tiềm ẩn ngay từ đầu trong từng bước triển khai.

---

## Giai đoạn 1: Thiết lập nền tảng Domain & Application Contracts

- [x] ### Bước 1: Định nghĩa Constants và Shared Elements (`src/Aqt.CoreOracle.Domain.Shared`)

*   **Mục tiêu:** Định nghĩa các hằng số, enum, localization keys, và tên extra properties dùng chung.
*   **Thực hiện:**
    *   **Tệp:** `src/Aqt.CoreOracle.Domain.Shared/CoreOracleConsts.cs`
        *   Thêm hằng số cho độ dài (`MaxPositionNameLength`, `MaxPositionCodeLength`, `MaxOrganizationUnitAddressLength`, `MaxOrganizationUnitSyncCodeLength`).
        *   Thêm hằng số cho tên extra properties: `public const string OrganizationUnitAddress = "Address";`, `public const string OrganizationUnitSyncCode = "SyncCode";`, `public const string OrganizationUnitType = "OrganizationUnitType";`.
    *   **Tệp:** Tạo `src/Aqt.CoreOracle.Domain.Shared/OrganizationUnits/OrganizationUnitTypeEnum.cs`
        *   Định nghĩa enum `OrganizationUnitTypeEnum { Unit = 1, Department = 2 }`.
    *   **Tệp:** `src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/en.json`
        *   Thêm các keys: `Menu:OrganizationManagement`, `Menu:Positions`, `Menu:OrganizationStructure`, `Positions`, `NewPosition`, `EditPosition`, `Position:Name`, `Position:Code`, `Position:Description`, `OrganizationUnit:Address`, `OrganizationUnit:SyncCode`, `OrganizationUnit:OrganizationUnitType`, `OrganizationUnit:Type:Unit`, `OrganizationUnit:Type:Department`, `AssignPosition`, `StartDate`, `EndDate`, `SuccessfullySaved`, `Actions`, `SelectAPosition`, `RemovePositionConfirmation`, `SelectAnOrganizationUnit`, `OrganizationTree`, `Members`, `AssignedPosition`, etc.
        *   Bổ sung keys cho mã lỗi: `"CoreOracle:00001": "Position code '{0}' already exists"`, `"CoreOracle:00002": "User is already assigned to position '{0}' in this organization unit"`, `"CoreOracle:00003": "Position with ID '{0}' was not found"`, `"CoreOracle:00004": "Organization unit with ID '{0}' was not found"`, `"CoreOracle:00005": "Invalid position assignment date range. End date must be greater than start date"`.
    *   **Tệp:** Tạo `src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/vi.json`
        *   Thêm các keys tiếng Việt tương ứng với `en.json`. Ví dụ: "Quản lý Tổ chức", "Chức vụ", "Cơ cấu Tổ chức", "Thêm Chức vụ", "Sửa Chức vụ", "Tên Chức vụ", "Mã Chức vụ", "Mô tả", "Địa chỉ", "Mã liên thông", "Loại", "Đơn vị", "Phòng ban", "Gán Chức vụ", "Ngày bắt đầu", "Ngày kết thúc", "Lưu thành công", "Hành động", "Chọn một Chức vụ", "Bạn có chắc muốn xóa...", "Chọn một Đơn vị/Phòng ban", "Cây Cơ cấu Tổ chức", "Thành viên", "Chức vụ được gán", etc.
        *   Bổ sung keys tiếng Việt cho mã lỗi: `"CoreOracle:00001": "Mã chức vụ '{0}' đã tồn tại"`, `"CoreOracle:00002": "Người dùng đã được gán chức vụ '{0}' trong đơn vị này"`, `"CoreOracle:00003": "Không tìm thấy chức vụ có ID '{0}'"`, `"CoreOracle:00004": "Không tìm thấy đơn vị có ID '{0}'"`, `"CoreOracle:00005": "Khoảng thời gian gán chức vụ không hợp lệ. Ngày kết thúc phải lớn hơn ngày bắt đầu"`.
    *   **Tệp:** `src/Aqt.CoreOracle.Domain.Shared/CoreOracleDomainErrorCodes.cs`
        *   Thêm mã lỗi nếu cần (ví dụ: `PositionCodeAlreadyExists`, `UserAlreadyAssignedToPositionInOU`, `PositionNotFound`, `OrganizationUnitNotFound`, `InvalidPositionAssignmentDateRange`).
    *   **Tệp:** `src/Aqt.CoreOracle.Domain.Shared/CoreOracleDomainSharedModule.cs`
        *   Đảm bảo `vi.json` được thêm vào Virtual File System và ngôn ngữ `vi` được đăng ký trong `AbpLocalizationOptions`.

- [x] ### Bước 2: Định nghĩa Domain Entities (`src/Aqt.CoreOracle.Domain`)

*   **Mục tiêu:** Tạo các thực thể nghiệp vụ và cấu hình mở rộng cho `OrganizationUnit`.
*   **Thực hiện:**
    *   **Tệp:** Tạo `src/Aqt.CoreOracle.Domain/Positions/Position.cs`
        *   Kế thừa `FullAuditedAggregateRoot<Guid>`.
        *   Thuộc tính: `Name` (string), `Code` (string), `Description` (string?).
        *   Constructor với `Check.NotNullOrWhiteSpace`.
    *   **Tệp:** Tạo `src/Aqt.CoreOracle.Domain/OrganizationStructure/EmployeePosition.cs`
        *   Kế thừa `FullAuditedEntity<Guid>`.
        *   Thuộc tính: `UserId` (Guid), `OrganizationUnitId` (Guid), `PositionId` (Guid), `StartDate` (DateTime), `EndDate` (DateTime?).
    *   **Tệp:** `src/Aqt.CoreOracle.Domain.Shared/CoreOracleModuleExtensionConfigurator.cs` (Thay vì `CoreOracleDomainModule.cs`)
        *   Trong phương thức `ConfigureExtraProperties` (hoặc tạo mới nếu chưa có), dùng `ObjectExtensionManager.Instance.Modules().ConfigureIdentity(...)` để cấu hình các extra properties (`Address`, `SyncCode`, `OrganizationUnitType`) cho `Identity.OrganizationUnit` với các ràng buộc (MaxLength, IsRequired, HasDefaultValue, cấu hình UI nếu cần).
    *   **Tệp:** `src/Aqt.CoreOracle.Domain.Shared/CoreOracleDomainSharedModule.cs` (hoặc `.Domain` nếu Configurator được gọi từ đó)
        *   Đảm bảo phương thức `CoreOracleModuleExtensionConfigurator.Configure()` được gọi trong `PreConfigureServices`.

- [x] ### Bước 3: Định nghĩa Domain Repository Interfaces (`src/Aqt.CoreOracle.Domain`)

*   **Mục tiêu:** Khai báo các phương thức truy cập dữ liệu tùy chỉnh.
*   **Thực hiện:**
    *   **Tệp:** Tạo `src/Aqt.CoreOracle.Domain/Positions/IPositionRepository.cs` (`: IRepository<Position, Guid>`).
    *   **Tệp:** Tạo `src/Aqt.CoreOracle.Domain/OrganizationStructure/IEmployeePositionRepository.cs` (`: IRepository<EmployeePosition, Guid>`) và thêm các phương thức: `FindAsync(Guid userId, Guid organizationUnitId, bool includeDetails = false)`, `GetListByOrganizationUnitAsync(...)`, `GetListByUserIdAsync(...)`.

- [x] ### Bước 4: Định nghĩa Application Contract DTOs (`src/Aqt.CoreOracle.Application.Contracts`)

*   **Mục tiêu:** Tạo các đối tượng truyền dữ liệu (DTOs).
*   **Thực hiện:**
    *   **Thư mục:** `src/Aqt.CoreOracle.Application.Contracts/Positions/`
        *   Tạo `PositionDto.cs` (`: FullAuditedEntityDto<Guid>`).
        *   Tạo `CreateUpdatePositionDto.cs` (với Data Annotations).
        *   Tạo `GetPositionListInput.cs` (`: PagedAndSortedResultRequestDto`).
    *   **Thư mục:** `src/Aqt.CoreOracle.Application.Contracts/OrganizationUnits/`
        *   Tạo `CustomOrganizationUnitDto.cs` (`: AuditedEntityDto<Guid>`, chứa `ParentId`, `Code`, `DisplayName`, `Address`, `SyncCode`, `OrganizationUnitType`).
        *   Tạo `OrganizationUnitTreeNodeDto.cs` (`: EntityDto<Guid>`, chứa `ParentId`, `Code`, `DisplayName`, `Address`, `SyncCode`, `OrganizationUnitType`, `Children`).
        *   Tạo `EmployeePositionDto.cs` (`: FullAuditedEntityDto<Guid>`, chứa Id và tên của User, OU, Position).
        *   Tạo `AssignEmployeePositionDto.cs` (Input DTO cho việc gán, với Data Annotations).
        *   Tạo `GetEmployeePositionsInput.cs` (Input DTO để lọc).
        *   Tạo `CreateOrganizationUnitInput.cs` (Input DTO, chứa `ParentId`, `DisplayName` và các trường mở rộng, với Data Annotations).
        *   Tạo `UpdateOrganizationUnitInput.cs` (Input DTO, chứa `DisplayName` và các trường mở rộng, với Data Annotations).
    *   **Tệp:** `src/Aqt.CoreOracle.Application.Contracts/CoreOracleDtoExtensions.cs`
        *   Nếu quyết định không tạo DTO tùy chỉnh mà mở rộng DTO có sẵn, sử dụng `ObjectExtensionManager.Instance.AddOrUpdateProperty<TTargetDto>(...)` ở đây. (Ít khuyến khích hơn).

- [x] ### Bước 5: Định nghĩa Application Contract Service Interfaces (`src/Aqt.CoreOracle.Application.Contracts`)

*   **Mục tiêu:** Khai báo các nghiệp vụ tầng Application.
*   **Thực hiện:**
    *   **Tệp:** Tạo `src/Aqt.CoreOracle.Application.Contracts/Positions/IPositionAppService.cs`
        *   Kế thừa `ICrudAppService<PositionDto, Guid, GetPositionListInput, CreateUpdatePositionDto>` để tận dụng các phương thức CRUD có sẵn.
        *   Không cần định nghĩa thêm phương thức do `ICrudAppService` đã đủ cho yêu cầu cơ bản.
    *   **Tệp:** Tạo `src/Aqt.CoreOracle.Application.Contracts/OrganizationUnits/ICustomOrganizationUnitAppService.cs`
        *   Kế thừa `IApplicationService` để có sự linh hoạt tối đa trong việc định nghĩa contract.
        *   Định nghĩa các phương thức CRUD với DTOs tùy chỉnh.
        *   Định nghĩa các phương thức quản lý cấu trúc cây OU.
        *   Định nghĩa các phương thức quản lý gán chức vụ cho người dùng.
        *   Định nghĩa các phương thức tìm kiếm và di chuyển OU.

- [x] ### Bước 6: Định nghĩa Permissions (`src/Aqt.CoreOracle.Application.Contracts`)

*   **Mục tiêu:** Định nghĩa quyền truy cập.
*   **Thực hiện:**
    *   **Tệp:** `src/Aqt.CoreOracle.Application.Contracts/Permissions/CoreOraclePermissions.cs`
        *   Thêm lớp static `OrganizationManagement` chứa consts: `Positions.Default`, `Positions.Create`, `Positions.Update`, `Positions.Delete`, `AssignPositions`.
    *   **Tệp:** `src/Aqt.CoreOracle.Application.Contracts/Permissions/CoreOraclePermissionDefinitionProvider.cs`
        *   Trong `Define`, thêm group "OrganizationManagement" và các permission đã định nghĩa ở trên.

---

## Giai đoạn 2: Triển khai Backend

- [x] ### Bước 7: Triển khai EF Core Mappings (`src/Aqt.CoreOracle.EntityFrameworkCore`)

*   **Mục tiêu:** Cấu hình ánh xạ database.
*   **Thực hiện:**
    *   **Tệp:** `src/Aqt.CoreOracle.EntityFrameworkCore/EntityFrameworkCore/CoreOracleDbContext.cs`
        *   Thêm `DbSet<Position> Positions { get; set; }`.
        *   Thêm `DbSet<EmployeePosition> EmployeePositions { get; set; }`.
        *   Trong `OnModelCreating`, cấu hình `builder.Entity<Position>(...)` và `builder.Entity<EmployeePosition>(...)` với `ToTable`, `ConfigureByConvention`, khóa ngoại (`HasOne`, `WithMany`, `HasForeignKey`, `OnDelete`), và indexes (`HasIndex`).
        *   *Lưu ý:* Cấu hình extra properties cho `OrganizationUnit` đã được xử lý tự động.

- [x] ### Bước 8: Triển khai Custom Repositories (`src/Aqt.CoreOracle.EntityFrameworkCore`)

*   **Mục tiêu:** Cung cấp logic truy cập dữ liệu tùy chỉnh.
*   **Thực hiện:**
    *   **Tệp:** Tạo `src/Aqt.CoreOracle.EntityFrameworkCore/EntityFrameworkCore/Positions/PositionRepository.cs` (`: EfCoreRepository<...>, IPositionRepository`).
    *   **Tệp:** Tạo `src/Aqt.CoreOracle.EntityFrameworkCore/EntityFrameworkCore/OrganizationStructure/EmployeePositionRepository.cs` (`: EfCoreRepository<...>, IEmployeePositionRepository`) và triển khai các phương thức tùy chỉnh.

- [x] ### Bước 9: Triển khai Application Services (`src/Aqt.CoreOracle.Application`)

*   **Mục tiêu:** Viết logic nghiệp vụ.
*   **Thực hiện:**
    *   **Tệp:** Tạo `src/Aqt.CoreOracle.Application/Positions/PositionAppService.cs`
        *   Kế thừa `CrudAppService<...>`. Set các policy names từ permissions.
    *   **Tệp:** Tạo `src/Aqt.CoreOracle.Application/OrganizationUnits/CustomOrganizationUnitAppService.cs`
        *   Kế thừa `ApplicationService` (thay vì `OrganizationUnitAppService` do sử dụng DTO tùy chỉnh). Implement `ICustomOrganizationUnitAppService`.
        *   Inject repositories, managers (`OrganizationUnitManager`, `IdentityUserManager`), `IObjectMapper`.
        *   Implement logic CRUD cho OU (sử dụng DTO tùy chỉnh), đọc/ghi extra properties bằng `ouEntity.SetProperty(...)` và `ouEntity.GetProperty<T>(...)`. Gọi `_organizationUnitRepository.UpdateAsync(...)` sau khi set property.
        *   Implement logic xây dựng cây OU, gán/xóa position, lấy danh sách employee positions.
        *   Áp dụng `[Authorize(...)]` cho các phương thức.

- [x] ### Bước 10: Cấu hình AutoMapper (`src/Aqt.CoreOracle.Application`)

*   **Mục tiêu:** Định nghĩa ánh xạ Entity <-> DTO.
*   **Thực hiện:**
    *   **Tệp:** `src/Aqt.CoreOracle.Application/CoreOracleApplicationAutoMapperProfile.cs`
        *   Thêm `CreateMap` cho `Position` <-> `PositionDto`/`CreateUpdatePositionDto`.
        *   Thêm `CreateMap` cho `EmployeePosition` -> `EmployeePositionDto`.
        *   Thêm `CreateMap` cho `OrganizationUnit` -> `CustomOrganizationUnitDto` và `OrganizationUnitTreeNodeDto`, map extra properties bằng `.MapFrom(src => src.GetProperty<T>(...))`.
        *   **(Lưu ý quan trọng):** Việc lấy giá trị cho các trường tên (UserName, OU Name, Position Name) trong `EmployeePositionDto` **nên được thực hiện trong Application Service** (ví dụ: lấy danh sách entity, lookup các tên cần thiết, sau đó mới gọi `ObjectMapper.Map` với danh sách đã có đủ thông tin) thay vì sử dụng custom value resolver phức tạp trong AutoMapper để đảm bảo sự rõ ràng và dễ bảo trì.

- [x] ### Bước 11: Database Migration

*   **Mục tiêu:** Cập nhật schema database.
*   **Thực hiện:**
    1.  Từ thư mục `src/Aqt.CoreOracle.EntityFrameworkCore`, chạy: `abp add-migration image.png`
    2.  Kiểm tra file migration mới tạo (phải có `AddColumn` cho `AbpOrganizationUnits` và `CreateTable` cho `Positions`, `EmployeePositions`).
    3.  Áp dụng migration vào database bằng một trong hai cách:
        *   **Cách 1 (Development):** Từ thư mục `src/Aqt.CoreOracle.EntityFrameworkCore`, chạy: `abp update-database`
        *   **Cách 2 (Automation/Explicit):** Chạy project `DbMigrator` (ví dụ: `dotnet run --project ../Aqt.CoreOracle.DbMigrator/Aqt.CoreOracle.DbMigrator.csproj` nếu đang ở thư mục `EntityFrameworkCore`, hoặc điều chỉnh đường dẫn tương đối cho phù hợp).

---

## Giai đoạn 3: Xây dựng Giao diện Người dùng (UI - Web)

- [ ] ### Bước 12: Phát triển UI (`src/Aqt.CoreOracle.Web`)

*   **Mục tiêu:** Tạo giao diện người dùng.
*   **Thực hiện (Giả định Razor Pages):**
    *   **(Lưu ý quan trọng về cấu trúc Modal với Dynamic Form):** Khi sử dụng `<abp-dynamic-form>` kết hợp với `<abp-modal>`, thẻ `<abp-dynamic-form>` **phải bao bọc toàn bộ** thẻ `<abp-modal>` (bao gồm header, body, và footer). Điều này đảm bảo nút 'Save' chuẩn trong `abp-modal-footer` nằm bên trong thẻ `<form>` được tạo ra và có thể kích hoạt submit form. Ví dụ:
        ```html
        <abp-dynamic-form abp-model="MyViewModel" asp-page="/MyPage/MyModal">
            <abp-modal>
                <abp-modal-header title="..."></abp-modal-header>
                <abp-modal-body>
                    <abp-form-content />
                </abp-modal-body>
                <abp-modal-footer buttons="@(AbpModalButtons.Save | AbpModalButtons.Cancel)"></abp-modal-footer>
            </abp-modal>
        </abp-dynamic-form>
        ```
    *   **(Lưu ý về Proxy JavaScript):** Đường dẫn đến các phương thức Application Service trong JavaScript (ví dụ: khi dùng `abp.libs.datatables.createAjax` hoặc gọi service trực tiếp) được tạo tự động bởi ABP. Cần kiểm tra cẩn thận cấu trúc proxy được tạo ra trong dự án cụ thể. Mặc dù cấu trúc chuẩn thường là `modulename.serviceName.methodName` (ví dụ: `aqt.coreOracle.positions.position.getList`), có thể có trường hợp nó bao gồm cả phần `application` (ví dụ: `aqt.coreOracle.application.positions.position.getList`) tùy thuộc vào cấu hình hoặc quy ước namespace. Luôn kiểm tra trong DevTools hoặc tệp proxy được tạo (nếu có) để đảm bảo sử dụng đúng đường dẫn.
    *   **(Lưu ý về Biến Service Proxy trong JavaScript):** Trong các tệp JavaScript (.js) tương tác nhiều với một Application Service cụ thể (ví dụ: `Index.js` cho một trang CRUD), nên định nghĩa một biến ở đầu tệp để lưu trữ đối tượng service proxy. Ví dụ: `var myServiceNameService = aqt.coreOracle.myModuleName.myServiceName;`. Sau đó, sử dụng biến này cho tất cả các lời gọi phương thức (ví dụ: `myServiceNameService.getList(...)`, `myServiceNameService.delete(...)`). Điều này giúp mã dễ đọc, dễ bảo trì hơn và dễ dàng thay đổi nếu đường dẫn proxy có sự điều chỉnh.
    *   **(Lưu ý về Biến Permissions trong JavaScript):** Tương tự như service proxy, khi cần kiểm tra nhiều quyền (permissions) trong JavaScript (ví dụ: dùng `abp.auth.isGranted(...)` để ẩn/hiện nút, cột trong bảng), nên định nghĩa một đối tượng hoặc các biến ở đầu tệp để lưu trữ các chuỗi tên quyền. Ví dụ: `var permissions = { create: 'MyModule.MyEntity.Create', update: 'MyModule.MyEntity.Update', ... };`. Sau đó, sử dụng các biến này trong các lời gọi `abp.auth.isGranted(permissions.create)`. Điều này giúp tránh lỗi chính tả và dễ dàng quản lý tên quyền.
    *   **(Lưu ý về Đồng bộ Permission C# -> JS):** Để đảm bảo tính nhất quán và tránh lỗi khi kiểm tra quyền phía client, **hãy luôn truyền giá trị permission từ C# (PageModel) sang JavaScript** thay vì sử dụng chuỗi cứng trong JS. Tạo một ViewModel hoặc thuộc tính trong PageModel (`.cshtml.cs`) để giữ các chuỗi permission lấy từ hằng số C# (đã được định nghĩa với `using` namespace đúng), sau đó serialize đối tượng này thành JavaScript trong view (`.cshtml`).
    *   **(Lưu ý về Query String vs Route Parameter cho Modal):** Khi cần truyền tham số (ví dụ: `Id` để chỉnh sửa) vào một modal Razor Page:
        *   **Mặc định & Khuyến khích:** Sử dụng **query string** (ví dụ: `/MyPage/MyModal?id=...`). Để làm điều này, **KHÔNG** sử dụng chỉ thị route parameter (`@page "{id:guid}"`) trên tệp `.cshtml` của modal. Khi gọi `modalManager.open({ id: recordId })`, `abp.ModalManager` sẽ tự động tạo URL với query string.
        *   **Trường hợp Route Parameter:** Nếu bạn *thực sự* muốn sử dụng route parameter (`/MyPage/MyModal/{id}`), bạn cần khai báo `@page "{id:guid}"` trên tệp `.cshtml` và phải tự xây dựng URL trong JavaScript trước khi gọi `modalManager.open({ url: constructedUrl })`. Cách này phức tạp hơn và chỉ nên dùng khi có lý do cụ thể.
    *   **(Lưu ý về Vị trí AutoMapper Profile cho UI):** Các ánh xạ (mappings) giữa các lớp của tầng Web (ví dụ: ViewModels được định nghĩa trong PageModels) và các lớp của tầng Application Contracts (ví dụ: DTOs) **phải được định nghĩa** trong AutoMapper profile của tầng Web (ví dụ: `MyProjectWebAutoMapperProfile.cs`), **không phải** trong profile của tầng Application (`MyProjectApplicationAutoMapperProfile.cs`). Điều này tránh việc tầng Application phụ thuộc vào tầng Web và đảm bảo các ánh xạ liên quan đến UI được quản lý tại đúng nơi.
    *   **Positions UI:**
        *   Tạo `Pages/Positions/Index.cshtml` + `.cs`: Bảng hiển thị Positions (`<abp-table>`), nút Thêm/Sửa/Xóa. Phân quyền `Positions.Default`.
        *   Tạo `Pages/Positions/CreateModal.cshtml` + `.cs`: Form tạo Position (`<abp-dynamic-form>`). Phân quyền `Positions.Create`.
        *   Tạo `Pages/Positions/EditModal.cshtml` + `.cs`: Form sửa Position (`<abp-dynamic-form>`). Phân quyền `Positions.Update`.
    *   **Organization Structure UI:**
        *   **Lưu ý Permission:** Áp dụng phân quyền bằng `[Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Default)]`. Đảm bảo hằng số này đã được định nghĩa trong `CoreOraclePermissions.cs` (tầng `Application.Contracts`) và được đăng ký trong `CoreOraclePermissionDefinitionProvider.cs`. **KHÔNG** sử dụng `IdentityPermissions` hoặc các permission của chức năng khác (như `Positions`) cho việc truy cập trang chính này.
        *   **Quyết định:** Sẽ tạo trang mới `Pages/OrganizationStructure/Index.cshtml` để quản lý cơ cấu tổ chức, thay vì override trang `/Identity/OrganizationUnits` có sẵn, nhằm tăng tính linh hoạt và tránh xung đột khi nâng cấp ABP.
        *   **Cây OU:** Dùng JavaScript (jsTree) để vẽ cây từ dữ liệu `GetOrganizationTreeAsync()`. Phân quyền `IdentityPermissions.OrganizationUnits.Default` (hoặc permission tùy chỉnh nếu cần kiểm soát chặt hơn).
        *   **Chi tiết OU & Thành viên:** Khi chọn node OU trên cây, hiển thị thông tin chi tiết của OU (bao gồm Address, SyncCode, Type) và bảng danh sách thành viên thuộc OU đó. Bảng thành viên cần hiển thị thông tin `EmployeePositionDto` (bao gồm chức vụ được gán, ngày bắt đầu/kết thúc).
        *   **Modal Tạo/Sửa OU:** Tạo các modal riêng (`CreateModal.cshtml`, `EditModal.cshtml`) trong `Pages/OrganizationStructure/`. Thêm các trường `Address`, `SyncCode`, `OrganizationUnitType` (dropdown) vào form (`<abp-dynamic-form>` nếu phù hợp, hoặc form thường). Gọi service `CreateAsync`/`UpdateAsync` với DTO tùy chỉnh. Phân quyền `IdentityPermissions.OrganizationUnits.Create`/`Update`.
        *   **Modal Gán Chức vụ:** Tạo `AssignPositionModal.cshtml` + `.cs` trong `Pages/OrganizationStructure/` (hoặc thư mục con `Shared/Components` nếu muốn tái sử dụng). Form gồm dropdown chọn `Position` (lấy từ `IPositionAppService`), `StartDate`, `EndDate`. Gọi `AssignPositionToUserAsync`. Phân quyền `OrganizationManagement.AssignPositions`.
        *   **Xóa Gán Chức vụ:** Thêm nút/link xóa vào mỗi dòng trong bảng thành viên (nơi hiển thị chức vụ được gán). Khi nhấn, hiển thị xác nhận (`abp.message.confirm`) rồi gọi `RemovePositionFromUserAsync`. Phân quyền `OrganizationManagement.AssignPositions`.
    *   **Menu:** Cập nhật `src/Aqt.CoreOracle.Web/Menus/CoreOracleMenuContributor.cs`: Thêm mục "Chức vụ" (trỏ đến `/Positions`) và "Cơ cấu Tổ chức" (trỏ đến `/OrganizationStructure`) với permission tương ứng.
    *   **Localization:** Sử dụng `IHtmlLocalizer<CoreOracleResource> L` và `@L["KeyName"]` trong các file `.cshtml` và `.cs`.

---

## Giai đoạn 4: Kiểm thử & Hoàn thiện

- [ ] ### Bước 13: Viết Tests (`test/...`)

*   **Mục tiêu:** Đảm bảo chất lượng code.
*   **Thực hiện:**
    *   Viết Unit Test cho logic phức tạp trong Application Services (đặc biệt là việc xử lý extra properties, xây dựng cây).
    *   Viết Integration Test để kiểm tra Application Services -> Repositories -> Database.
    *   Kiểm thử thủ công toàn bộ luồng trên giao diện.

- [ ] ### Bước 14: Rà soát và Tối ưu

*   **Mục tiêu:** Đảm bảo code sạch, hiệu quả, bảo mật.
*   **Thực hiện:**
    *   Review code, loại bỏ code thừa.
    *   Tối ưu các truy vấn LINQ/SQL (nếu cần).
    *   Kiểm tra lại việc áp dụng Permissions trên tất cả các actions/pages.
    *   Kiểm tra logging và xử lý lỗi.

---

Kế hoạch này là một lộ trình chi tiết. Thứ tự các bước trong cùng một giai đoạn có thể linh hoạt điều chỉnh.