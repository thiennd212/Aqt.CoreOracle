# Kế hoạch chi tiết xây dựng chức năng Quản lý Quốc gia (v3)

## 1. Phân tích Yêu cầu

*   **Mục tiêu:** Xây dựng chức năng quản lý danh mục Quốc gia theo chuẩn ABP Framework.
*   **Tính năng:**
    *   Xem danh sách Quốc gia (phân trang, sắp xếp, tìm kiếm/lọc).
    *   Thêm mới Quốc gia.
    *   Cập nhật thông tin Quốc gia.
    *   Xóa Quốc gia (sử dụng Soft Delete).
*   **Thông tin quản lý:**
    *   Tên quốc gia (ví dụ: Việt Nam)
    *   Mã ISO (ví dụ: VN) - Alpha-2 hoặc Alpha-3
    *   Mã số (ví dụ: 704) - Numeric code
    *   Trạng thái: Kích hoạt (Enabled) / Vô hiệu hóa (Disabled)
*   **Ràng buộc:**
    *   Mã ISO phải là duy nhất.
    *   Mã số phải là duy nhất.
    *   Tên quốc gia là bắt buộc.

## 2. Các tầng ABP bị ảnh hưởng

*   **Aqt.CoreOracle.Domain.Shared:** Định nghĩa hằng số (Error Codes, MaxLength), Enum (nếu có), Localization Keys.
*   **Aqt.CoreOracle.Domain:** Định nghĩa `Country` Entity (AggregateRoot), `ICountryRepository` interface, và `CountryManager` (Domain Service) để xử lý logic nghiệp vụ cốt lõi và validation.
*   **Aqt.CoreOracle.Application.Contracts:** Định nghĩa `ICountryAppService` interface, DTOs (`CountryDto`, `CreateUpdateCountryDto`, `GetCountryListInput`), Permission Names (`CountryPermissions`).
*   **Aqt.CoreOracle.Application:** Implement `CountryAppService`, xử lý logic CRUD, validation gọi từ Domain Service, authorization, và object mapping (AutoMapper).
*   **Aqt.CoreOracle.EntityFrameworkCore:** Implement `EfCoreCountryRepository`, cập nhật `DbContext`, cấu hình Entity Mapping (Fluent API).
*   **Aqt.CoreOracle.HttpApi** (Tùy chọn): `CountryController` để expose `ICountryAppService` qua REST API nếu cần thiết cho các client khác.
*   **Aqt.CoreOracle.Web:** Tạo Razor Pages cho giao diện người dùng (Index page với datatable, Create/Edit Modals), cập nhật Menu, và JS/CSS nếu cần.

## 3. Chi tiết Tác vụ theo Tầng

### 3.1. Domain.Shared (`Aqt.CoreOracle.Domain.Shared`)

*   **Tạo/Cập nhật Tệp:** `Countries/CountryConsts.cs`
    *   Nội dung:
        ```csharp
        namespace Aqt.CoreOracle.Domain.Shared.Countries;

        public static class CountryConsts
        {
            public const int MaxNameLength = 100;
            public const int MaxCodeLength = 3;       // Alpha-3 code
            public const int MaxNumericCodeLength = 3; // Numeric code
        }
        ```
*   **Cập nhật Tệp:** `CoreOracleDomainErrorCodes.cs`
    *   Thêm các mã lỗi:
        ```csharp
        public const string CountryNameRequired = "CoreOracle:10001";
        public const string CountryCodeRequired = "CoreOracle:10002";
        public const string CountryNumericCodeRequired = "CoreOracle:10003";
        public const string CountryCodeExists = "CoreOracle:10004";
        public const string CountryNumericCodeExists = "CoreOracle:10005";
        ```
*   **Cập nhật Tệp:** `Localization/CoreOracle/*.json` (ví dụ: `en.json`)
    *   Thêm các localization keys:
        ```json
        {
          "Menu:Countries": "Countries",
          "Countries": "Countries",
          "NewCountry": "New Country",
          "EditCountry": "Edit Country",
          "Country:Name": "Name",
          "Country:Code": "ISO Code",
          "Country:NumericCode": "Numeric Code",
          "Country:IsEnabled": "Enabled",
          "Permission:CountryManagement": "Country Management",
          "Permission:Countries": "Countries",
          "Permission:Countries.Create": "Create",
          "Permission:Countries.Edit": "Edit",
          "Permission:Countries.Delete": "Delete",
          "Message:CountryCodeExists": "The country code '{0}' already exists.",
          "Message:CountryNumericCodeExists": "The country numeric code '{0}' already exists.",
          "SuccessfullyDeleted": "Successfully deleted",
          // Thêm các key khác nếu cần
        }
        ```
*   **Đảm bảo Tệp tồn tại:** `Localization/CoreOracle/CoreOracleResource.cs`

### 3.2. Domain (`Aqt.CoreOracle.Domain`)

*   **Tạo Tệp:** `Countries/Entities/Country.cs`
    *   Namespace: `Aqt.CoreOracle.Domain.Countries.Entities`
    *   Kế thừa: `FullAuditedAggregateRoot<Guid>`
    *   Thuộc tính: `Name`, `Code`, `NumericCode`, `IsEnabled` (với `private set` hoặc `protected set`).
    *   Constructor: `private Country() {}`, `internal Country(Guid id, string name, string code, string numericCode, bool isEnabled)` (sử dụng `internal` để chỉ `CountryManager` mới tạo được).
    *   Phương thức: `internal SetName(string name)`, `internal SetCode(string code)`, `internal SetNumericCode(string numericCode)`, `internal SetIsEnabled(bool isEnabled)` (để `CountryManager` gọi sau khi validate).
        ```csharp
        using System;
        using Aqt.CoreOracle.Domain.Shared.Countries; // For Consts
        using Volo.Abp;
        using Volo.Abp.Domain.Entities.Auditing;

        namespace Aqt.CoreOracle.Domain.Countries.Entities;

        public class Country : FullAuditedAggregateRoot<Guid>
        {
            public virtual string Name { get; protected set; }
            public virtual string Code { get; protected set; } // Alpha code
            public virtual string NumericCode { get; protected set; } // Numeric code
            public virtual bool IsEnabled { get; set; }

            private Country()
            {
                // Required by ORM
            }

            internal Country(
                Guid id,
                string name,
                string code,
                string numericCode,
                bool isEnabled = true) : base(id)
            {
                SetName(name);
                SetCode(code);
                SetNumericCode(numericCode);
                IsEnabled = isEnabled;
            }

            internal Country SetName(string name)
            {
                Name = Check.NotNullOrWhiteSpace(name, nameof(name), maxLength: CountryConsts.MaxNameLength);
                return this;
            }

            internal Country SetCode(string code)
            {
                Code = Check.NotNullOrWhiteSpace(code, nameof(code), maxLength: CountryConsts.MaxCodeLength);
                // Add regex check if needed for alpha code format
                return this;
            }

             internal Country SetNumericCode(string numericCode)
            {
                NumericCode = Check.NotNullOrWhiteSpace(numericCode, nameof(numericCode), maxLength: CountryConsts.MaxNumericCodeLength);
                // Add regex check for numeric format "^\d{3}$"
                if (!System.Text.RegularExpressions.Regex.IsMatch(numericCode, @"^\d{3}$"))
                {
                     throw new ArgumentException("Numeric code must be exactly 3 digits.", nameof(numericCode));
                }
                return this;
            }
        }
        ```
*   **Tạo Tệp:** `Countries/Repositories/ICountryRepository.cs`
    *   Namespace: `Aqt.CoreOracle.Domain.Countries.Repositories`
    *   Kế thừa: `IRepository<Country, Guid>`
    *   Phương thức tùy chỉnh:
        ```csharp
        using System;
        using System.Collections.Generic;
        using System.Threading;
        using System.Threading.Tasks;
        using Aqt.CoreOracle.Domain.Countries.Entities;
        using Volo.Abp.Domain.Repositories;

        namespace Aqt.CoreOracle.Domain.Countries.Repositories;

        public interface ICountryRepository : IRepository<Country, Guid>
        {
            Task<Country> FindByCodeAsync(
                string code,
                bool includeDetails = true,
                CancellationToken cancellationToken = default
            );

            Task<Country> FindByNumericCodeAsync(
                string numericCode,
                bool includeDetails = true,
                CancellationToken cancellationToken = default
            );

            Task<bool> CodeExistsAsync(
                string code,
                Guid? excludedId = null,
                CancellationToken cancellationToken = default
            );

            Task<bool> NumericCodeExistsAsync(
                string numericCode,
                Guid? excludedId = null,
                CancellationToken cancellationToken = default
            );

            Task<List<Country>> GetListAsync(
                string filterText = null,
                string sorting = null,
                int maxResultCount = int.MaxValue,
                int skipCount = 0,
                CancellationToken cancellationToken = default
            );

            Task<long> GetCountAsync(
                string filterText = null,
                CancellationToken cancellationToken = default
            );
        }
        ```
*   **Tạo Tệp:** `Countries/Services/CountryManager.cs`
    *   Namespace: `Aqt.CoreOracle.Domain.Countries.Services`
    *   Kế thừa: `DomainService`
    *   Inject: `ICountryRepository`, `IStringLocalizer<CoreOracleResource>`, `IGuidGenerator`.
    *   Phương thức:
        *   `Task<Country> CreateAsync(string name, string code, string numericCode, bool isEnabled = true)`: Validate (tên, code, numeric code bắt buộc), kiểm tra trùng `Code` và `NumericCode` bằng `CodeExistsAsync`/`NumericCodeExistsAsync`. Nếu hợp lệ, tạo entity `Country` mới bằng constructor `internal`.
        *   `Task UpdateAsync(Country country, string name, string code, string numericCode, bool isEnabled)`: Validate, kiểm tra trùng `Code` và `NumericCode` (loại trừ `Id` hiện tại). Nếu hợp lệ, gọi các phương thức `Set...` của entity `Country`.
        *   `Task ChangeCodeAsync(Country country, string newCode)` (ví dụ): Kiểm tra `newCode` tồn tại chưa, nếu chưa thì cập nhật.
        ```csharp
        using System;
        using System.Threading.Tasks;
        using Aqt.CoreOracle.Domain.Countries.Entities;
        using Aqt.CoreOracle.Domain.Countries.Repositories;
        using Aqt.CoreOracle.Domain.Shared; // For Error Codes
        using Aqt.CoreOracle.Domain.Shared.Localization;
        using Microsoft.Extensions.Localization;
        using Volo.Abp;
        using Volo.Abp.Domain.Services;
        using Volo.Abp.Guids;

        namespace Aqt.CoreOracle.Domain.Countries.Services;

        public class CountryManager : DomainService
        {
            private readonly ICountryRepository _countryRepository;
            private readonly IStringLocalizer<CoreOracleResource> _localizer;
            private readonly IGuidGenerator _guidGenerator;

            public CountryManager(
                ICountryRepository countryRepository,
                IStringLocalizer<CoreOracleResource> localizer,
                IGuidGenerator guidGenerator)
            {
                _countryRepository = countryRepository;
                _localizer = localizer;
                _guidGenerator = guidGenerator;
            }

            public async Task<Country> CreateAsync(
                string name,
                string code,
                string numericCode,
                bool isEnabled = true)
            {
                Check.NotNullOrWhiteSpace(name, nameof(name));
                Check.NotNullOrWhiteSpace(code, nameof(code));
                Check.NotNullOrWhiteSpace(numericCode, nameof(numericCode));

                await CheckCodeAndNumericCodeDuplicationAsync(code, numericCode);

                var country = new Country(
                    _guidGenerator.Create(),
                    name,
                    code,
                    numericCode,
                    isEnabled
                );

                return country;
            }

            public async Task UpdateAsync(
                Country country,
                string name,
                string code,
                string numericCode,
                bool isEnabled)
            {
                Check.NotNull(country, nameof(country));
                Check.NotNullOrWhiteSpace(name, nameof(name));
                Check.NotNullOrWhiteSpace(code, nameof(code));
                Check.NotNullOrWhiteSpace(numericCode, nameof(numericCode));

                if (country.Code != code || country.NumericCode != numericCode)
                {
                    await CheckCodeAndNumericCodeDuplicationAsync(code, numericCode, country.Id);
                }

                country.SetName(name);
                country.SetCode(code);
                country.SetNumericCode(numericCode);
                country.IsEnabled = isEnabled;
            }

             private async Task CheckCodeAndNumericCodeDuplicationAsync(string code, string numericCode, Guid? excludedId = null)
             {
                if (await _countryRepository.CodeExistsAsync(code, excludedId))
                {
                    throw new BusinessException(CoreOracleDomainErrorCodes.CountryCodeExists)
                        .WithData("Code", code);
                }

                 if (await _countryRepository.NumericCodeExistsAsync(numericCode, excludedId))
                {
                    throw new BusinessException(CoreOracleDomainErrorCodes.CountryNumericCodeExists)
                         .WithData("NumericCode", numericCode);
                }
             }
        }
        ```

### 3.3. Application.Contracts (`Aqt.CoreOracle.Application.Contracts`)

*   **Tạo Tệp:** `Countries/Permissions/CountryPermissions.cs`
    *   Namespace: `Aqt.CoreOracle.Application.Contracts.Countries.Permissions`
    *   Định nghĩa constants cho permissions: `GroupName`, `Countries.Default`, `Countries.Create`, `Countries.Edit`, `Countries.Delete`.
        ```csharp
        namespace Aqt.CoreOracle.Application.Contracts.Countries.Permissions;

        public static class CountryPermissions
        {
            public const string GroupName = "CoreOracle"; // Or a more specific group like "Settings" or "Administration"

            public static class Countries
            {
                public const string Default = GroupName + ".Countries";
                public const string Create = Default + ".Create";
                public const string Edit = Default + ".Edit";
                public const string Delete = Default + ".Delete";
            }
        }
        ```
*   **Cập nhật Tệp:** `Permissions/CoreOraclePermissionDefinitionProvider.cs`
    *   Trong phương thức `Define`, đăng ký các permissions đã định nghĩa ở trên sử dụng `context.AddGroup` và `group.AddPermission`.
        ```csharp
        // Inside Define method:
        var countryManagementGroup = context.AddGroup(CountryPermissions.GroupName, L["Permission:CountryManagement"]); // Or use an existing group

        var countriesPermission = countryManagementGroup.AddPermission(CountryPermissions.Countries.Default, L["Permission:Countries"]);
        countriesPermission.AddChild(CountryPermissions.Countries.Create, L["Permission:Countries.Create"]);
        countriesPermission.AddChild(CountryPermissions.Countries.Edit, L["Permission:Countries.Edit"]);
        countriesPermission.AddChild(CountryPermissions.Countries.Delete, L["Permission:Countries.Delete"]);
        ```
*   **Tạo Tệp:** `Countries/Dtos/CountryDto.cs`
    *   Namespace: `Aqt.CoreOracle.Application.Contracts.Countries.Dtos`
    *   Kế thừa: `FullAuditedEntityDto<Guid>`
    *   Thuộc tính: `Name`, `Code`, `NumericCode`, `IsEnabled`.
        ```csharp
        using System;
        using Volo.Abp.Application.Dtos;

        namespace Aqt.CoreOracle.Application.Contracts.Countries.Dtos;

        public class CountryDto : FullAuditedEntityDto<Guid>
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public string NumericCode { get; set; }
            public bool IsEnabled { get; set; }
        }
        ```
*   **Tạo Tệp:** `Countries/Dtos/GetCountryListInput.cs`
    *   Namespace: `Aqt.CoreOracle.Application.Contracts.Countries.Dtos`
    *   Kế thừa: `PagedAndSortedResultRequestDto`
    *   Thuộc tính: `public string Filter { get; set; }`
        ```csharp
        using Volo.Abp.Application.Dtos;

        namespace Aqt.CoreOracle.Application.Contracts.Countries.Dtos;

        public class GetCountryListInput : PagedAndSortedResultRequestDto
        {
            public string Filter { get; set; }
        }
        ```
*   **Tạo Tệp:** `Countries/Dtos/CreateUpdateCountryDto.cs`
    *   Namespace: `Aqt.CoreOracle.Application.Contracts.Countries.Dtos`
    *   Thuộc tính với DataAnnotations: `Name` (`[Required]`, `[StringLength]`), `Code` (`[Required]`, `[StringLength]`), `NumericCode` (`[Required]`, `[StringLength]`, `[RegularExpression(@"^\d{3}$")]`), `IsEnabled`.
        ```csharp
        using System.ComponentModel.DataAnnotations;
        using Aqt.CoreOracle.Domain.Shared.Countries; // For Consts

        namespace Aqt.CoreOracle.Application.Contracts.Countries.Dtos;

        public class CreateUpdateCountryDto
        {
            [Required]
            [StringLength(CountryConsts.MaxNameLength)]
            public string Name { get; set; }

            [Required]
            [StringLength(CountryConsts.MaxCodeLength, MinimumLength = 2)] // Example: Alpha-2 or Alpha-3
            public string Code { get; set; }

            [Required]
            [StringLength(CountryConsts.MaxNumericCodeLength)]
            [RegularExpression(@"^\d{3}$", ErrorMessage = "Numeric code must be exactly 3 digits.")]
            public string NumericCode { get; set; }

            [Required]
            public bool IsEnabled { get; set; } = true;
        }
        ```
*   **Tạo Tệp:** `Countries/Services/ICountryAppService.cs`
    *   Namespace: `Aqt.CoreOracle.Application.Contracts.Countries.Services`
    *   Kế thừa: `ICrudAppService<CountryDto, Guid, GetCountryListInput, CreateUpdateCountryDto>`
        ```csharp
        using System;
        using Aqt.CoreOracle.Application.Contracts.Countries.Dtos;
        using Volo.Abp.Application.Services;

        namespace Aqt.CoreOracle.Application.Contracts.Countries.Services;

        public interface ICountryAppService : ICrudAppService<
                CountryDto,
                Guid,
                GetCountryListInput,
                CreateUpdateCountryDto>
        {
            // Add custom methods if needed
        }
        ```

### 3.4. Application (`Aqt.CoreOracle.Application`)

*   **Tạo Tệp:** `Countries/CountryAppService.cs`
    *   Namespace: `Aqt.CoreOracle.Application.Countries`
    *   Kế thừa: `CrudAppService<Country, CountryDto, Guid, GetCountryListInput, CreateUpdateCountryDto, ICountryRepository>`, `ICountryAppService`
    *   Inject: `ICountryRepository`, `CountryManager`.
    *   Xác định Policy Names dựa trên `CountryPermissions`.
    *   Override các phương thức `GetPolicyName`, `GetListPolicyName`, `CreatePolicyName`, `UpdatePolicyName`, `DeletePolicyName` để trả về policy name tương ứng.
    *   Override `CreateAsync`: Đặt quyền (`await AuthorizationService.CheckAsync(...)`), gọi `_countryManager.CreateAsync`, lưu entity bằng `_countryRepository.InsertAsync`, map và trả về DTO.
    *   Override `UpdateAsync`: Lấy entity (`await GetEntityByIdAsync(id)`), đặt quyền, gọi `_countryManager.UpdateAsync`, cập nhật entity bằng `_countryRepository.UpdateAsync`, map và trả về DTO.
    *   Override `DeleteAsync`: Đặt quyền, gọi `_countryRepository.DeleteAsync(id)`.
    *   Override `GetListAsync`: Đặt quyền, gọi `_countryRepository.GetCountAsync` và `_countryRepository.GetListAsync` với filter/sorting/paging, map và trả về `PagedResultDto<CountryDto>`.
        ```csharp
        using System;
        using System.Collections.Generic;
        using System.Threading.Tasks;
        using Aqt.CoreOracle.Application.Contracts.Countries.Dtos;
        using Aqt.CoreOracle.Application.Contracts.Countries.Permissions;
        using Aqt.CoreOracle.Application.Contracts.Countries.Services;
        using Aqt.CoreOracle.Domain.Countries.Entities;
        using Aqt.CoreOracle.Domain.Countries.Repositories;
        using Aqt.CoreOracle.Domain.Countries.Services;
        using Microsoft.AspNetCore.Authorization;
        using Volo.Abp.Application.Dtos;
        using Volo.Abp.Application.Services;
        using Volo.Abp.Domain.Repositories;

        namespace Aqt.CoreOracle.Application.Countries;

        [Authorize(CountryPermissions.Countries.Default)] // Default policy for the service
        public class CountryAppService
            : CrudAppService<Country, CountryDto, Guid, GetCountryListInput, CreateUpdateCountryDto>,
              ICountryAppService
        {
            private readonly CountryManager _countryManager;
            private readonly ICountryRepository _countryRepository; // Inject specific repository if needed beyond base

            public CountryAppService(
                IRepository<Country, Guid> repository, // Base CrudAppService requires IRepository
                ICountryRepository countryRepository,    // Specific repository for custom methods
                CountryManager countryManager)
                : base(repository)
            {
                _countryManager = countryManager;
                _countryRepository = countryRepository; // Assign specific repository

                // Set policy names for CRUD operations
                GetPolicyName = CountryPermissions.Countries.Default; // Read policy
                GetListPolicyName = CountryPermissions.Countries.Default; // Read policy
                CreatePolicyName = CountryPermissions.Countries.Create;
                UpdatePolicyName = CountryPermissions.Countries.Edit;
                DeletePolicyName = CountryPermissions.Countries.Delete;
            }

            // Override GetAsync if needed (already provided by base)
            // public override async Task<CountryDto> GetAsync(Guid id) { ... }

            [Authorize(CountryPermissions.Countries.Create)]
            public override async Task<CountryDto> CreateAsync(CreateUpdateCountryDto input)
            {
                await CheckCreatePolicyAsync(); // Already checked by Authorize attribute, but good practice

                var country = await _countryManager.CreateAsync(
                    input.Name,
                    input.Code,
                    input.NumericCode,
                    input.IsEnabled
                );

                await Repository.InsertAsync(country, autoSave: true); // Use base Repository or _countryRepository

                return ObjectMapper.Map<Country, CountryDto>(country);
            }

            [Authorize(CountryPermissions.Countries.Edit)]
            public override async Task<CountryDto> UpdateAsync(Guid id, CreateUpdateCountryDto input)
            {
                await CheckUpdatePolicyAsync();

                var country = await GetEntityByIdAsync(id); // Use base method to get entity

                await _countryManager.UpdateAsync(
                    country,
                    input.Name,
                    input.Code,
                    input.NumericCode,
                    input.IsEnabled
                );

                await Repository.UpdateAsync(country, autoSave: true);

                return ObjectMapper.Map<Country, CountryDto>(country);
            }

             [Authorize(CountryPermissions.Countries.Delete)]
             public override async Task DeleteAsync(Guid id)
             {
                 await CheckDeletePolicyAsync();
                 // Soft delete is handled by FullAuditedAggregateRoot and IRepository
                 await Repository.DeleteAsync(id, autoSave: true);
             }

            [Authorize(CountryPermissions.Countries.Default)] // Or GetListPolicyName
            public override async Task<PagedResultDto<CountryDto>> GetListAsync(GetCountryListInput input)
            {
                await CheckGetListPolicyAsync();

                var totalCount = await _countryRepository.GetCountAsync(input.Filter);
                var items = await _countryRepository.GetListAsync(
                    input.Filter,
                    input.Sorting,
                    input.MaxResultCount,
                    input.SkipCount
                );

                return new PagedResultDto<CountryDto>(
                    totalCount,
                    ObjectMapper.Map<List<Country>, List<CountryDto>>(items)
                );
            }

             // GetEntityByIdAsync is provided by CrudAppService
        }
        ```
*   **Cập nhật Tệp:** `CoreOracleApplicationAutoMapperProfile.cs`
    *   Thêm các mapping: `CreateMap<Country, CountryDto>();`, `CreateMap<CreateUpdateCountryDto, Country>();` (Lưu ý: Không map `Id` từ DTO khi tạo mới, `CountryManager` sẽ tạo `Id`).
        ```csharp
        using AutoMapper;
        using Aqt.CoreOracle.Application.Contracts.Countries.Dtos;
        using Aqt.CoreOracle.Domain.Countries.Entities;

        namespace Aqt.CoreOracle.Application;

        public class CoreOracleApplicationAutoMapperProfile : Profile
        {
            public CoreOracleApplicationAutoMapperProfile()
            {
                //... other mappings
                CreateMap<Country, CountryDto>();
                CreateMap<CreateUpdateCountryDto, Country>()
                   .Ignore(x => x.Id) // Do not map Id from DTO
                   .Ignore(x => x.ExtraProperties)
                   .Ignore(x => x.ConcurrencyStamp);
                   // Domain Manager handles creation logic including ID generation
            }
        }
        ```

### 3.5. EntityFrameworkCore (`Aqt.CoreOracle.EntityFrameworkCore`)

*   **Cập nhật Tệp:** `EntityFrameworkCore/CoreOracleDbContext.cs`
    *   Thêm `DbSet`: `public DbSet<Country> Countries { get; set; }`
*   **Cập nhật Tệp:** `EntityFrameworkCore/CoreOracleDbContextModelCreatingExtensions.cs`
    *   Trong `ConfigureCoreOracle`, thêm cấu hình cho `Country`:
        *   `builder.Entity<Country>(b => { ... });`
        *   Map tới bảng (`ToTable`).
        *   Cấu hình các thuộc tính (`Name`, `Code`, `NumericCode`, `IsEnabled`) với `IsRequired`, `HasMaxLength`.
        *   Tạo Unique Index cho `Code` và `NumericCode` (`HasIndex(...).IsUnique()`).
        *   Sử dụng `b.ConfigureByConvention();` để áp dụng cấu hình chung của ABP.
        ```csharp
        using Aqt.CoreOracle.Domain.Countries.Entities;
        using Aqt.CoreOracle.Domain.Shared.Countries; // For Consts
        using Microsoft.EntityFrameworkCore;
        using Volo.Abp;
        using Volo.Abp.EntityFrameworkCore.Modeling;

        namespace Aqt.CoreOracle.EntityFrameworkCore;

        public static class CoreOracleDbContextModelCreatingExtensions
        {
            public static void ConfigureCoreOracle(
                this ModelBuilder builder)
            {
                Check.NotNull(builder, nameof(builder));

                /* Configure your own tables/entities inside here */

                builder.Entity<Country>(b =>
                {
                    // Use CoreOracleConsts.DbTablePrefix if defined, otherwise use a specific prefix or none
                    b.ToTable(CoreOracleConsts.DbTablePrefix + "Countries", CoreOracleConsts.DbSchema);
                    b.ConfigureByConvention(); //auto configure for the base class props

                    b.Property(x => x.Name)
                        .IsRequired()
                        .HasMaxLength(CountryConsts.MaxNameLength)
                        .HasColumnName(nameof(Country.Name)); // Optional, clarifies column name

                    b.Property(x => x.Code)
                        .IsRequired()
                        .HasMaxLength(CountryConsts.MaxCodeLength)
                        .HasColumnName(nameof(Country.Code));

                    b.Property(x => x.NumericCode)
                        .IsRequired()
                        .HasMaxLength(CountryConsts.MaxNumericCodeLength)
                        .HasColumnName(nameof(Country.NumericCode));

                    b.Property(x => x.IsEnabled)
                        .IsRequired()
                        .HasColumnName(nameof(Country.IsEnabled));

                    b.HasIndex(x => x.Code).IsUnique();
                    b.HasIndex(x => x.NumericCode).IsUnique();

                    // Configure other base properties if needed, though ConfigureByConvention handles most
                    b.Property(x => x.ConcurrencyStamp).HasColumnName(nameof(Country.ConcurrencyStamp));
                });

                //...other entity configurations
            }
        }
        ```
*   **Tạo Tệp:** `Countries/EfCoreCountryRepository.cs`
    *   Namespace: `Aqt.CoreOracle.EntityFrameworkCore.Countries`
    *   Kế thừa: `EfCoreRepository<CoreOracleDbContext, Country, Guid>`, `ICountryRepository`
    *   Implement các phương thức của `ICountryRepository` sử dụng LINQ và `DbContext`: `FindByCodeAsync`, `FindByNumericCodeAsync`, `CodeExistsAsync`, `NumericCodeExistsAsync`, `GetListAsync`, `GetCountAsync`. Sử dụng `WhereIf` cho filter, `OrderBy` cho sorting, `PageBy` cho paging.
        ```csharp
        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Linq.Dynamic.Core; // For OrderBy with string
        using System.Threading;
        using System.Threading.Tasks;
        using Aqt.CoreOracle.Domain.Countries.Entities;
        using Aqt.CoreOracle.Domain.Countries.Repositories;
        using Aqt.CoreOracle.EntityFrameworkCore; // For DbContext
        using Microsoft.EntityFrameworkCore;
        using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
        using Volo.Abp.EntityFrameworkCore;

        namespace Aqt.CoreOracle.EntityFrameworkCore.Countries;

        public class EfCoreCountryRepository
            : EfCoreRepository<CoreOracleDbContext, Country, Guid>,
              ICountryRepository
        {
            public EfCoreCountryRepository(
                IDbContextProvider<CoreOracleDbContext> dbContextProvider)
                : base(dbContextProvider)
            {
            }

            public async Task<Country> FindByCodeAsync(string code, bool includeDetails = true, CancellationToken cancellationToken = default)
            {
                var dbSet = await GetDbSetAsync();
                return await dbSet
                    .FirstOrDefaultAsync(c => c.Code == code, GetCancellationToken(cancellationToken));
            }

            public async Task<Country> FindByNumericCodeAsync(string numericCode, bool includeDetails = true, CancellationToken cancellationToken = default)
            {
                 var dbSet = await GetDbSetAsync();
                 return await dbSet
                     .FirstOrDefaultAsync(c => c.NumericCode == numericCode, GetCancellationToken(cancellationToken));
            }

             public async Task<bool> CodeExistsAsync(string code, Guid? excludedId = null, CancellationToken cancellationToken = default)
             {
                 var dbSet = await GetDbSetAsync();
                 return await dbSet
                    .AnyAsync(c => c.Code == code && (excludedId == null || c.Id != excludedId.Value),
                              GetCancellationToken(cancellationToken));
             }

            public async Task<bool> NumericCodeExistsAsync(string numericCode, Guid? excludedId = null, CancellationToken cancellationToken = default)
            {
                var dbSet = await GetDbSetAsync();
                return await dbSet
                    .AnyAsync(c => c.NumericCode == numericCode && (excludedId == null || c.Id != excludedId.Value),
                              GetCancellationToken(cancellationToken));
            }

             public async Task<List<Country>> GetListAsync(
                 string filterText = null,
                 string sorting = null,
                 int maxResultCount = int.MaxValue,
                 int skipCount = 0,
                 CancellationToken cancellationToken = default)
            {
                 var query = await CreateFilteredQueryAsync(filterText);

                 query = query
                     .OrderBy(string.IsNullOrWhiteSpace(sorting) ? nameof(Country.Name) + " asc" : sorting)
                     .PageBy(skipCount, maxResultCount);

                 return await query.ToListAsync(GetCancellationToken(cancellationToken));
            }

            public async Task<long> GetCountAsync(
                 string filterText = null,
                 CancellationToken cancellationToken = default)
            {
                var query = await CreateFilteredQueryAsync(filterText);
                return await query.LongCountAsync(GetCancellationToken(cancellationToken));
            }

             // Helper method to create a reusable filtered query
             private async Task<IQueryable<Country>> CreateFilteredQueryAsync(string filterText)
             {
                 var dbSet = await GetDbSetAsync();
                 return dbSet
                     .WhereIf(!string.IsNullOrWhiteSpace(filterText), c =>
                         c.Name.Contains(filterText) ||
                         c.Code.Contains(filterText) ||
                         c.NumericCode.Contains(filterText));
             }

             // Override GetQueryableAsync if you need to apply global filters or includes
             // public override async Task<IQueryable<Country>> GetQueryableAsync()
             // {
             //     return (await base.GetQueryableAsync()).Where(c => !c.IsDeleted); // Example
             // }
        }
        ```

### 3.6. Web (`Aqt.CoreOracle.Web`)

*   **Cập nhật Tệp:** `Menus/CoreOracleMenuContributor.cs`
    *   Thêm một `ApplicationMenuItem` cho "Countries", trỏ tới `/Countries`, đặt trong nhóm menu phù hợp (ví dụ: Administration), và yêu cầu quyền `CountryPermissions.Countries.Default`.
        ```csharp
         // Inside ConfigureMainMenuAsync method:
         var administration = context.Menu.GetAdministration(); // Or find a suitable parent

         if (await context.IsGrantedAsync(CountryPermissions.Countries.Default)) // Check permission
         {
             administration.AddItem(new ApplicationMenuItem(
                 "Countries", // Menu item name (unique key)
                 l["Menu:Countries"], // Display name from localization
                 url: "/Countries" // URL to the Razor Page
             ).RequirePermissions(CountryPermissions.Countries.Default)); // Optional redundant check
         }
        ```
*   **Tạo Thư mục:** `Pages/Countries/`
*   **Tạo Tệp:** `Pages/Countries/Index.cshtml`
    *   Sử dụng LeptonX theme components: `abp-card`, `abp-table` (với `id`), `abp-button` (cho New), `abp-dropdown` (cho Actions: Edit, Delete).
    *   Thêm ô tìm kiếm (`abp-input` hoặc standard html input) và nút tìm kiếm/clear.
    *   Cấu hình `abp-table` để sử dụng datatables.net AJAX source, trỏ tới một Page Handler (ví dụ `OnGetListAsync`) hoặc trực tiếp API nếu có.
        ```html
        @page
        @model Aqt.CoreOracle.Web.Pages.Countries.IndexModel
        @using Aqt.CoreOracle.Web.Pages.Countries
        @using Microsoft.AspNetCore.Authorization
        @using Microsoft.AspNetCore.Mvc.Localization
        @using Aqt.CoreOracle.Domain.Shared.Localization
        @using Aqt.CoreOracle.Application.Contracts.Countries.Permissions; // For permissions
        @inject IHtmlLocalizer<CoreOracleResource> L
        @inject IAuthorizationService AuthorizationService

        @{
            ViewData["Title"] = L["Countries"];
        }

        @section scripts {
            <abp-script src="/Pages/Countries/Index.js" />
        }

        <abp-card>
            <abp-card-header>
                <abp-row>
                    <abp-column size-md="_6">
                        <h2>@L["Countries"]</h2>
                    </abp-column>
                    <abp-column size-md="_6" class="text-end">
                         @if (await AuthorizationService.IsGrantedAsync(CountryPermissions.Countries.Create))
                         {
                            <abp-button id="NewCountryButton"
                                        text="@L["NewCountry"]"
                                        icon="plus"
                                        button-type="Primary" />
                         }
                    </abp-column>
                </abp-row>
            </abp-card-header>
            <abp-card-body>
                 <!-- Optional Filter Section -->
                 <abp-row class="mb-3">
                     <abp-column size-md="_12">
                         <form method="get" id="CountrySearchForm">
                              <div class="input-group">
                                  <input type="search" name="Filter" id="Filter" class="form-control" value="@Model.Filter" placeholder="@L["Search"]" />
                                  <button type="submit" class="btn btn-primary"><i class="fa fa-search"></i></button>
                                  <button type="button" id="ClearFilterButton" class="btn btn-secondary"><i class="fa fa-times"></i></button>
                              </div>
                         </form>
                     </abp-column>
                 </abp-row>

                <abp-table striped-rows="true" id="CountriesTable">
                    <thead>
                    <tr>
                        <th>@L["Actions"]</th>
                        <th>@L["Country:Name"]</th>
                        <th>@L["Country:Code"]</th>
                        <th>@L["Country:NumericCode"]</th>
                        <th>@L["Country:IsEnabled"]</th>
                        <th>@L["CreationTime"]</th>
                    </tr>
                    </thead>
                </abp-table>
            </abp-card-body>
        </abp-card>
        ```
*   **Tạo Tệp:** `Pages/Countries/Index.cshtml.cs` (`IndexModel`)
    *   Inject `ICountryAppService`.
    *   Thuộc tính `[BindProperty(SupportsGet = true)] public string Filter { get; set; }`.
    *   Phương thức `OnGetAsync()` (có thể để trống nếu dùng AJAX).
    *   Phương thức `OnPostAsync()` để xử lý Delete action (gọi `_countryAppService.DeleteAsync(id)`).
    *   *Quan trọng:* Thêm Page Handler `OnGetListAsync` trả về `JsonResult` chứa `PagedResultDto<CountryDto>` để Datatables AJAX gọi nếu không dùng API Controller.
        ```csharp
        using System.Threading.Tasks;
        using Microsoft.AspNetCore.Mvc;
        // using Aqt.CoreOracle.Application.Contracts.Countries.Dtos; // Not needed directly if using DataTables AJAX
        // using Volo.Abp.Application.Dtos; // Not needed directly if using DataTables AJAX
        using Volo.Abp.AspNetCore.Mvc.UI.RazorPages; // For AbpPageModel

        namespace Aqt.CoreOracle.Web.Pages.Countries;

        // No need to inject ICountryAppService here if all data loading is via JS/DataTables
        public class IndexModel : AbpPageModel
        {
             [BindProperty(SupportsGet = true)]
             public string Filter { get; set; }

             // OnGet can be simple, loading is done via JS
             public void OnGet()
             {
             }

             // If you want server-side filtering on initial load (without JS),
             // you'd inject ICountryAppService and load data in OnGet.
             // However, the standard approach with DataTables is client-side/AJAX loading.
        }
        ```
*   **Tạo Tệp:** `Pages/Countries/CreateModal.cshtml`
    *   Sử dụng `abp-modal`, `abp-dynamic-form` bind với `CreateUpdateCountryDto` trong `CreateModel`.
        ```html
        @page
        @model Aqt.CoreOracle.Web.Pages.Countries.CreateModalModel
        @using Microsoft.AspNetCore.Mvc.Localization
        @using Aqt.CoreOracle.Domain.Shared.Localization
        @inject IHtmlLocalizer<CoreOracleResource> L
        @{
            Layout = null; // Modals typically don't have a layout
        }
        <abp-dynamic-form abp-model="Country" asp-page="/Countries/CreateModal">
            <abp-modal>
                <abp-modal-header title="@L["NewCountry"]"></abp-modal-header>
                <abp-modal-body>
                    <abp-form-content /> @* Renders inputs based on DTO attributes *@
                </abp-modal-body>
                <abp-modal-footer buttons="@(AbpModalButtons.Cancel|AbpModalButtons.Save)"></abp-modal-footer>
            </abp-modal>
        </abp-dynamic-form>
        ```
*   **Tạo Tệp:** `Pages/Countries/CreateModal.cshtml.cs` (`CreateModalModel`)
    *   Inject `ICountryAppService`.
    *   Thuộc tính `[BindProperty] public CreateUpdateCountryDto Country { get; set; }`.
    *   `OnGetAsync` để khởi tạo `Country` (ví dụ `IsEnabled = true`).
    *   `OnPostAsync` để gọi `_countryAppService.CreateAsync(Country)`.
        ```csharp
        using System.Threading.Tasks;
        using Aqt.CoreOracle.Application.Contracts.Countries.Dtos;
        using Aqt.CoreOracle.Application.Contracts.Countries.Services;
        using Microsoft.AspNetCore.Mvc;
        using Volo.Abp.AspNetCore.Mvc.UI.RazorPages; // For AbpPageModel

        namespace Aqt.CoreOracle.Web.Pages.Countries;

        public class CreateModalModel : AbpPageModel
        {
            [BindProperty]
            public CreateUpdateCountryDto Country { get; set; }

            private readonly ICountryAppService _countryAppService;

            public CreateModalModel(ICountryAppService countryAppService)
            {
                _countryAppService = countryAppService;
                Country = new CreateUpdateCountryDto(); // Initialize DTO
            }

            public void OnGet()
            {
                 Country = new CreateUpdateCountryDto { IsEnabled = true }; // Set defaults
            }

            public async Task<IActionResult> OnPostAsync()
            {
                await _countryAppService.CreateAsync(Country);
                return NoContent(); // Indicates success for AJAX form
            }
        }
        ```
*   **Tạo Tệp:** `Pages/Countries/EditModal.cshtml` (Tương tự `CreateModal.cshtml`)
*   **Tạo Tệp:** `Pages/Countries/EditModal.cshtml.cs` (`EditModalModel`)
    *   Inject `ICountryAppService`.
    *   Thuộc tính `[HiddenInput] [BindProperty(SupportsGet = true)] public Guid Id { get; set; }`.
    *   Thuộc tính `[BindProperty] public CreateUpdateCountryDto Country { get; set; }`.
    *   `OnGetAsync`: Gọi `_countryAppService.GetAsync(Id)` để lấy `CountryDto`, sau đó `ObjectMapper.Map` sang `CreateUpdateCountryDto` để gán cho `Country`.
    *   `OnPostAsync`: Gọi `_countryAppService.UpdateAsync(Id, Country)`.
        ```csharp
        using System;
        using System.Threading.Tasks;
        using Aqt.CoreOracle.Application.Contracts.Countries.Dtos;
        using Aqt.CoreOracle.Application.Contracts.Countries.Services;
        using Microsoft.AspNetCore.Mvc;
        using Volo.Abp.AspNetCore.Mvc.UI.RazorPages; // For AbpPageModel
        using Volo.Abp.ObjectMapping; // If mapping Dto -> CreateUpdateDto

        namespace Aqt.CoreOracle.Web.Pages.Countries;

        public class EditModalModel : AbpPageModel
        {
            [HiddenInput]
            [BindProperty(SupportsGet = true)]
            public Guid Id { get; set; }

            [BindProperty]
            public CreateUpdateCountryDto Country { get; set; }

            private readonly ICountryAppService _countryAppService;
             // Inject IObjectMapper if mapping from CountryDto to CreateUpdateCountryDto
             // private readonly IObjectMapper _objectMapper;

            public EditModalModel(ICountryAppService countryAppService) //, IObjectMapper objectMapper)
            {
                _countryAppService = countryAppService;
                 // _objectMapper = objectMapper;
            }

            public async Task OnGetAsync()
            {
                var countryDto = await _countryAppService.GetAsync(Id);
                // Map from the read DTO to the editable DTO
                // If CreateUpdateCountryDto has the same properties, manual mapping is simple:
                Country = new CreateUpdateCountryDto
                {
                     Name = countryDto.Name,
                     Code = countryDto.Code,
                     NumericCode = countryDto.NumericCode,
                     IsEnabled = countryDto.IsEnabled
                };
                 // Or use ObjectMapper if defined:
                 // Country = _objectMapper.Map<CountryDto, CreateUpdateCountryDto>(countryDto);
            }

            public async Task<IActionResult> OnPostAsync()
            {
                await _countryAppService.UpdateAsync(Id, Country);
                return NoContent();
            }
        }
        ```
*   **Tạo Tệp:** `wwwroot/Pages/Countries/Index.js`
    *   Sử dụng jQuery và `abp.libs.datatables.createAjax` để cấu hình DataTable.
    *   Định nghĩa `columnDefs` cho các cột, bao gồm cột Actions với các nút Edit/Delete (sử dụng `abp.auth.isGranted` để kiểm tra quyền trước khi hiển thị nút).
    *   Xử lý sự kiện click nút "NewCountryButton" để mở `CreateModal` (dùng `abp.ModalManager`).
    *   Xử lý sự kiện click nút Edit trong table để mở `EditModal`.
    *   Xử lý sự kiện click nút Delete: Hiển thị confirmation (`abp.message.confirm`), nếu xác nhận thì gọi `countryAppService.delete` (cần expose AppService qua JS proxy hoặc dùng `abp.ajax`), sau đó reload DataTable (`dataTable.ajax.reload()`).
    *   Xử lý submit form tìm kiếm.
    *   Xử lý nút Clear Filter.
        ```javascript
        $(function () {
            // Namespace JS proxy (adjust if needed)
            var countryAppService = aqt.coreOracle.countries.country; // Example, check generated proxy name

            var createModal = new abp.ModalManager(abp.appPath + 'Countries/CreateModal');
            var editModal = new abp.ModalManager(abp.appPath + 'Countries/EditModal');

            // DataTable initialization
            var dataTable = $('#CountriesTable').DataTable(
                abp.libs.datatables.normalizeConfiguration({
                    serverSide: true,
                    paging: true,
                    order: [[1, "asc"]], // Default sort by Name
                    searching: false, // We use the custom search input
                    scrollX: true,
                    ajax: abp.libs.datatables.createAjax(countryAppService.getList, function () {
                         // Get filter value from the search input
                         return { filter: $('#Filter').val() };
                    }),
                    columnDefs: [
                        {
                            title: l('Actions'), // Localized title
                            rowAction: {
                                items: [
                                    {
                                        text: l('Edit'),
                                        visible: abp.auth.isGranted('CoreOracle.Countries.Edit'), // Check permission
                                        action: function (data) {
                                            editModal.open({ id: data.record.id });
                                        }
                                    },
                                    {
                                        text: l('Delete'),
                                        visible: abp.auth.isGranted('CoreOracle.Countries.Delete'),
                                        confirmMessage: function (data) {
                                            // Dynamic confirmation message
                                            return l('CountryDeletionConfirmationMessage', data.record.name);
                                        },
                                        action: function (data) {
                                            countryAppService
                                                .delete(data.record.id)
                                                .then(function () {
                                                    abp.notify.info(l('SuccessfullyDeleted'));
                                                    dataTable.ajax.reload(); // Refresh table
                                                });
                                        }
                                    }
                                ]
                            }
                        },
                        {
                            title: l('Country:Name'),
                            data: "name"
                        },
                        {
                            title: l('Country:Code'),
                            data: "code"
                        },
                        {
                             title: l('Country:NumericCode'),
                             data: "numericCode"
                        },
                        {
                            title: l('Country:IsEnabled'),
                            data: "isEnabled",
                             render: function (data) {
                                 // Display a badge or icon for boolean status
                                 return data
                                     ? '<span class="badge bg-success">' + l('Yes') + '</span>'
                                     : '<span class="badge bg-secondary">' + l('No') + '</span>';
                             }
                        },
                         {
                             title: l('CreationTime'),
                             data: "creationTime",
                             render: function (data) {
                                 // Format date
                                 return luxon.DateTime.fromISO(data, {
                                     locale: abp.localization.currentCulture.name,
                                 }).toLocaleString();
                             },
                         }
                    ]
                })
            );

            // Handle New Country button click
            $('#NewCountryButton').click(function (e) {
                e.preventDefault();
                createModal.open();
            });

            // Handle modal submission success
            createModal.onResult(function () {
                dataTable.ajax.reload();
            });

            editModal.onResult(function () {
                dataTable.ajax.reload();
            });

             // Handle search form submission
             $('#CountrySearchForm').submit(function (e) {
                 e.preventDefault();
                 dataTable.ajax.reload(); // Reload DataTable with new filter
             });

              // Handle clear filter button
              $('#ClearFilterButton').click(function () {
                  $('#Filter').val(''); // Clear the input
                  dataTable.ajax.reload(); // Reload DataTable
              });

        });

        ```
*   **Cập nhật Tệp:** `CoreOracleWebAutoMapperProfile.cs`
    *   Ví dụ: `CreateMap<CountryDto, EditCountryViewModel>();`

## 4. Database Schema (Dự kiến)

*   **Bảng:** `AppCountries` (Tên bảng có thể thay đổi tùy theo `DbTablePrefix`)
*   **Cột:**
    *   `Id` (PK, uniqueidentifier/Guid)
    *   `Name` (nvarchar(100), not null)
    *   `Code` (varchar(3), not null, unique)
    *   `NumericCode` (varchar(3), not null, unique)
    *   `IsEnabled` (bit, not null)
    *   `ExtraProperties` (nvarchar(max), nullable)
    *   `ConcurrencyStamp` (varchar(40), nullable)
    *   `CreationTime` (datetime2, not null)
    *   `CreatorId` (uniqueidentifier, nullable)
    *   `LastModificationTime` (datetime2, nullable)
    *   `LastModifierId` (uniqueidentifier, nullable)
    *   `IsDeleted` (bit, not null, default: 0)
    *   `DeleterId` (uniqueidentifier, nullable)
    *   `DeletionTime` (datetime2, nullable)
*   **Index:**
    *   Unique Index trên `Code`.
    *   Unique Index trên `NumericCode`.

## 5. Kiểm thử

*   **Unit Tests:**
    *   `CountryManager` (Domain): Kiểm tra logic validation, kiểm tra trùng lặp, tạo entity.
    *   `CountryAppService` (Application): Kiểm tra authorization policy, gọi đúng phương thức Manager/Repository, mapping DTO.
*   **Integration Tests:**
    *   Kiểm tra luồng từ `CountryAppService` -> `CountryManager` -> `EfCoreCountryRepository` -> Database. Test các kịch bản CRUD, filter, validation trùng lặp.
*   **UI Tests (Thủ công hoặc tự động):**
    *   Kiểm tra hiển thị danh sách, phân trang, sắp xếp, lọc.
    *   Kiểm tra mở modal Create/Edit.
    *   Kiểm tra validation trên form Create/Edit.
    *   Kiểm tra lưu dữ liệu thành công.
    *   Kiểm tra xóa (với confirmation).
    *   Kiểm tra phân quyền (ẩn/hiện nút theo quyền).

## 6. Các Bước Triển Khai (Gợi ý)

1.  **Domain & Domain.Shared:** Định nghĩa Entity, Consts, Error Codes, Repository Interface, Domain Service (Manager).
2.  **Application.Contracts:** Định nghĩa DTOs, Permissions, AppService Interface.
3.  **EntityFrameworkCore:** Cập nhật DbContext, cấu hình Mapping, Implement Repository. Tạo migration và cập nhật database.
4.  **Application:** Implement AppService, AutoMapper Profile.
5.  **Web:** Cập nhật Menu, tạo Razor Pages (Index, Modals), viết Javascript cho DataTable và Modals.
6.  **Kiểm thử:** Viết Unit/Integration Tests.
7.  **Hoàn thiện:** Localization, UI tinh chỉnh. 