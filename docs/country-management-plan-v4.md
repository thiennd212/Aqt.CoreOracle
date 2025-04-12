# Kế hoạch Phát triển Chức năng Quản lý Danh mục Quốc gia (v4)

## Mục tiêu

Xây dựng chức năng quản lý danh mục Quốc gia (Country) theo chuẩn ABP Framework, bao gồm các thao tác CRUD (Create, Read, Update, Delete), tìm kiếm, phân trang, sắp xếp và phân quyền truy cập.

## Tổng quan các tầng

1.  **Domain**: Định nghĩa Entity `Country`, Repository Interface `ICountryRepository`.
2.  **Domain.Shared**: Định nghĩa Constants, Error Codes, Localization Keys.
3.  **Application.Contracts**: Định nghĩa DTOs, AppService Interface `ICountryAppService`, Permissions.
4.  **Application**: Implement `CountryAppService`, cấu hình AutoMapper.
5.  **EntityFrameworkCore**: Implement Repository, cấu hình DbContext, Mapping, Migration.
6.  **Web**: Xây dựng giao diện Razor Pages (Index, Create/Edit Modals), JavaScript cho Datatables.

## Chi tiết thực hiện

### 1. Tầng Domain (`Aqt.CoreOracle.Domain`)

*   **Entity `Country`**:
    *   Tạo file: `src/Aqt.CoreOracle.Domain/Countries/Country.cs`
    *   Kế thừa từ `FullAuditedAggregateRoot<Guid>` (để có tracking và soft delete).
    *   Thuộc tính:
        *   `Code` (string, unique, required, max length: `CountryConsts.MaxCodeLength`)
        *   `Name` (string, required, max length: `CountryConsts.MaxNameLength`)
        *   `IsoAlpha2Code` (string, nullable, max length: 2)
        *   `IsoAlpha3Code` (string, nullable, max length: 3)
        *   `NumericCode` (int, nullable)
    *   Thêm constructor để validate input và `private set` cho các thuộc tính cần bảo vệ.

    ```csharp
    // src/Aqt.CoreOracle.Domain/Countries/Country.cs
    using Aqt.CoreOracle.Domain.Shared.Countries; // For CountryConsts
    using JetBrains.Annotations;
    using System;
    using Volo.Abp;
    using Volo.Abp.Domain.Entities.Auditing;

    namespace Aqt.CoreOracle.Domain.Countries
    {
        public class Country : FullAuditedAggregateRoot<Guid>
        {
            [NotNull]
            public virtual string Code { get; private set; }

            [NotNull]
            public virtual string Name { get; private set; }

            [CanBeNull]
            public virtual string IsoAlpha2Code { get; internal set; } // Use internal set for better control

            [CanBeNull]
            public virtual string IsoAlpha3Code { get; internal set; } // Use internal set for better control

            public virtual int? NumericCode { get; internal set; } // Use internal set for better control

            protected Country() { /* Required by ORM */ }

            public Country(
                Guid id,
                [NotNull] string code,
                [NotNull] string name,
                [CanBeNull] string isoAlpha2Code = null,
                [CanBeNull] string isoAlpha3Code = null,
                int? numericCode = null)
                : base(id)
            {
                SetCode(code);
                SetName(name);
                IsoAlpha2Code = isoAlpha2Code?.Length <= 2 ? isoAlpha2Code : throw new ArgumentException("IsoAlpha2Code cannot be longer than 2 characters.", nameof(isoAlpha2Code));
                IsoAlpha3Code = isoAlpha3Code?.Length <= 3 ? isoAlpha3Code : throw new ArgumentException("IsoAlpha3Code cannot be longer than 3 characters.", nameof(isoAlpha3Code));
                NumericCode = numericCode;
            }

            internal Country ChangeCode([NotNull] string code)
            {
                SetCode(code);
                return this;
            }

             internal Country ChangeName([NotNull] string name)
            {
                SetName(name);
                return this;
            }

            private void SetCode([NotNull] string code)
            {
                Code = Check.NotNullOrWhiteSpace(
                    code,
                    nameof(code),
                    maxLength: CountryConsts.MaxCodeLength
                );
            }

            private void SetName([NotNull] string name)
            {
                Name = Check.NotNullOrWhiteSpace(
                    name,
                    nameof(name),
                    maxLength: CountryConsts.MaxNameLength
                );
            }
        }
    }
    ```

*   **Repository Interface `ICountryRepository`** (Tùy chọn - nếu cần method đặc thù):
    *   Tạo file: `src/Aqt.CoreOracle.Domain/Countries/ICountryRepository.cs`
    *   Kế thừa `IRepository<Country, Guid>`.
    *   Thêm method nếu cần (ví dụ: `Task<Country> FindByCodeAsync(string code, bool includeDetails = true, CancellationToken cancellationToken = default);`).

    ```csharp
    // src/Aqt.CoreOracle.Domain/Countries/ICountryRepository.cs
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Volo.Abp.Domain.Repositories;

    namespace Aqt.CoreOracle.Domain.Countries
    {
        public interface ICountryRepository : IRepository<Country, Guid>
        {
            Task<Country> FindByCodeAsync(
                string code,
                bool includeDetails = true, // Example of including related data if needed
                CancellationToken cancellationToken = default
            );

            // Add other custom methods if needed
        }
    }
    ```

### 2. Tầng Domain.Shared (`Aqt.CoreOracle.Domain.Shared`)

*   **Constants `CountryConsts`**:
    *   Tạo file: `src/Aqt.CoreOracle.Domain.Shared/Countries/CountryConsts.cs`
    *   Định nghĩa max length cho các thuộc tính.

    ```csharp
    // src/Aqt.CoreOracle.Domain.Shared/Countries/CountryConsts.cs
    namespace Aqt.CoreOracle.Domain.Shared.Countries
    {
        public static class CountryConsts
        {
            public const int MaxCodeLength = 10;
            public const int MaxNameLength = 100;
            public const int IsoAlpha2CodeLength = 2;
            public const int IsoAlpha3CodeLength = 3;
        }
    }
    ```

*   **Error Codes** (Nếu cần):
    *   Thêm vào `src/Aqt.CoreOracle.Domain.Shared/CoreOracleDomainErrorCodes.cs`
    *   Ví dụ: `public const string CountryCodeAlreadyExists = "CoreOracle:01001";`

    ```csharp
    // src/Aqt.CoreOracle.Domain.Shared/CoreOracleDomainErrorCodes.cs
    namespace Aqt.CoreOracle.Domain.Shared
    {
        public static partial class CoreOracleDomainErrorCodes // Use partial if file exists
        {
            // Add Country specific error codes here
            public const string CountryCodeAlreadyExists = "CoreOracle:Countries:00001"; // Example format
        }
    }
    ```

*   **Localization**:
    *   Cập nhật `src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/en.json` và `vi.json`.
    *   Thêm các key: `Menu:Countries`, `Countries`, `Country`, `NewCountry`, `EditCountry`, `Code`, `Name`, `IsoAlpha2Code`, `IsoAlpha3Code`, `NumericCode`, `SuccessfullyDeleted`, `AreYouSure`, `AreYouSureToDelete`, `Permission:Countries`, `Permission:Countries.Create`, `Permission:Countries.Edit`, `Permission:Countries.Delete`.

    ```json
    // Example for en.json
    {
      "culture": "en",
      "texts": {
        "Menu:Countries": "Countries",
        "Countries": "Countries",
        "Country": "Country",
        "NewCountry": "New Country",
        "EditCountry": "Edit Country",
        "Code": "Code",
        "Name": "Name",
        "IsoAlpha2Code": "ISO Alpha-2",
        "IsoAlpha3Code": "ISO Alpha-3",
        "NumericCode": "Numeric Code",
        "SuccessfullyDeleted": "Successfully deleted",
        "AreYouSure": "Are you sure?",
        "AreYouSureToDelete": "Are you sure you want to delete this item: {0}?",
        "Permission:Countries": "Country Management",
        "Permission:Countries.Create": "Create Countries",
        "Permission:Countries.Edit": "Edit Countries",
        "Permission:Countries.Delete": "Delete Countries"
        // ... other keys
      }
    }
    ```
    ```json
    // Example for vi.json
    {
      "culture": "vi",
      "texts": {
        "Menu:Countries": "Quốc gia",
        "Countries": "Quốc gia",
        "Country": "Quốc gia",
        "NewCountry": "Thêm mới Quốc gia",
        "EditCountry": "Sửa Quốc gia",
        "Code": "Mã",
        "Name": "Tên",
        "IsoAlpha2Code": "ISO Alpha-2",
        "IsoAlpha3Code": "ISO Alpha-3",
        "NumericCode": "Mã số",
        "SuccessfullyDeleted": "Xoá thành công",
        "AreYouSure": "Bạn có chắc không?",
        "AreYouSureToDelete": "Bạn có chắc muốn xoá mục này: {0}?",
        "Permission:Countries": "Quản lý Quốc gia",
        "Permission:Countries.Create": "Tạo Quốc gia",
        "Permission:Countries.Edit": "Sửa Quốc gia",
        "Permission:Countries.Delete": "Xoá Quốc gia"
        // ... other keys
      }
    }
    ```

### 3. Tầng Application.Contracts (`Aqt.CoreOracle.Application.Contracts`)

*   **DTOs**:
    *   Tạo thư mục: `src/Aqt.CoreOracle.Application.Contracts/Countries/Dtos`
    *   `CountryDto`:
        *   Tạo file: `Dtos/CountryDto.cs`
        *   Kế thừa `AuditedEntityDto<Guid>`.
        *   Chứa các thuộc tính cần hiển thị: `Id`, `Code`, `Name`, `IsoAlpha2Code`, `IsoAlpha3Code`, `NumericCode`.

        ```csharp
        // src/Aqt.CoreOracle.Application.Contracts/Countries/Dtos/CountryDto.cs
        using System;
        using Volo.Abp.Application.Dtos;

        namespace Aqt.CoreOracle.Application.Contracts.Countries.Dtos
        {
            public class CountryDto : AuditedEntityDto<Guid> // Use FullAuditedEntityDto if needed
            {
                public string Code { get; set; }
                public string Name { get; set; }
                public string IsoAlpha2Code { get; set; }
                public string IsoAlpha3Code { get; set; }
                public int? NumericCode { get; set; }
            }
        }
        ```

    *   `CreateUpdateCountryDto`:
        *   Tạo file: `Dtos/CreateUpdateCountryDto.cs`
        *   Chứa các thuộc tính cần cho tạo/sửa: `Code`, `Name`, `IsoAlpha2Code`, `IsoAlpha3Code`, `NumericCode`.
        *   Thêm `DataAnnotations` (`[Required]`, `[StringLength]`).

        ```csharp
        // src/Aqt.CoreOracle.Application.Contracts/Countries/Dtos/CreateUpdateCountryDto.cs
        using Aqt.CoreOracle.Domain.Shared.Countries; // For CountryConsts
        using System;
        using System.ComponentModel.DataAnnotations;

        namespace Aqt.CoreOracle.Application.Contracts.Countries.Dtos
        {
            public class CreateUpdateCountryDto
            {
                [Required]
                [StringLength(CountryConsts.MaxCodeLength)]
                public string Code { get; set; }

                [Required]
                [StringLength(CountryConsts.MaxNameLength)]
                public string Name { get; set; }

                [StringLength(CountryConsts.IsoAlpha2CodeLength)]
                public string IsoAlpha2Code { get; set; }

                [StringLength(CountryConsts.IsoAlpha3CodeLength)]
                public string IsoAlpha3Code { get; set; }

                public int? NumericCode { get; set; }
            }
        }
        ```

    *   `GetCountryListInput`:
        *   Tạo file: `Dtos/GetCountryListInput.cs`
        *   Kế thừa `PagedAndSortedResultRequestDto`.
        *   Thêm thuộc tính `Filter` (string) để tìm kiếm.

        ```csharp
        // src/Aqt.CoreOracle.Application.Contracts/Countries/Dtos/GetCountryListInput.cs
        using Volo.Abp.Application.Dtos;

        namespace Aqt.CoreOracle.Application.Contracts.Countries.Dtos
        {
            public class GetCountryListInput : PagedAndSortedResultRequestDto
            {
                public string Filter { get; set; }
            }
        }
        ```

*   **AppService Interface `ICountryAppService`**:
    *   Tạo file: `src/Aqt.CoreOracle.Application.Contracts/Countries/ICountryAppService.cs`
    *   Kế thừa `ICrudAppService<CountryDto, Guid, GetCountryListInput, CreateUpdateCountryDto>`.
    *   Thêm các phương thức tùy chỉnh nếu cần.

    ```csharp
    // src/Aqt.CoreOracle.Application.Contracts/Countries/ICountryAppService.cs
    using System;
    using Aqt.CoreOracle.Application.Contracts.Countries.Dtos;
    using Volo.Abp.Application.Services;

    namespace Aqt.CoreOracle.Application.Contracts.Countries
    {
        public interface ICountryAppService :
            ICrudAppService< // Defines CRUD methods
                CountryDto,
                Guid,
                GetCountryListInput,
                CreateUpdateCountryDto>
        {
            // Add custom service methods here, if needed
        }
    }
    ```

*   **Permissions**:
    *   Cập nhật `src/Aqt.CoreOracle.Application.Contracts/Permissions/CoreOraclePermissions.cs`
    *   Thêm `static class Countries` để định nghĩa các quyền.

    ```csharp
    // src/Aqt.CoreOracle.Application.Contracts/Permissions/CoreOraclePermissions.cs
    namespace Aqt.CoreOracle.Permissions
    {
        public static class CoreOraclePermissions
        {
            public const string GroupName = "CoreOracle";

            //Add your own permission names. Example:
            //public const string MyPermission1 = GroupName + ".MyPermission1";

            public static class Countries
            {
                public const string Default = GroupName + ".Countries";
                public const string Create = Default + ".Create";
                public const string Edit = Default + ".Edit";
                public const string Delete = Default + ".Delete";
            }
        }
    }
    ```

*   **Permission Definition Provider**:
    *   Cập nhật `src/Aqt.CoreOracle.Application.Contracts/Permissions/CoreOraclePermissionDefinitionProvider.cs`
    *   Đăng ký các quyền đã định nghĩa.

    ```csharp
    // src/Aqt.CoreOracle.Application.Contracts/Permissions/CoreOraclePermissionDefinitionProvider.cs
    using Aqt.CoreOracle.Localization;
    using Volo.Abp.Authorization.Permissions;
    using Volo.Abp.Localization;

    namespace Aqt.CoreOracle.Permissions
    {
        public class CoreOraclePermissionDefinitionProvider : PermissionDefinitionProvider
        {
            public override void Define(IPermissionDefinitionContext context)
            {
                var myGroup = context.AddGroup(CoreOraclePermissions.GroupName);
                //Define your own permissions here. Example:
                //myGroup.AddPermission(CoreOraclePermissions.MyPermission1, L("Permission:MyPermission1"));

                var countriesPermission = myGroup.AddPermission(CoreOraclePermissions.Countries.Default, L("Permission:Countries"));
                countriesPermission.AddChild(CoreOraclePermissions.Countries.Create, L("Permission:Countries.Create"));
                countriesPermission.AddChild(CoreOraclePermissions.Countries.Edit, L("Permission:Countries.Edit"));
                countriesPermission.AddChild(CoreOraclePermissions.Countries.Delete, L("Permission:Countries.Delete"));
            }

            private static LocalizableString L(string name)
            {
                return LocalizableString.Create<CoreOracleResource>(name);
            }
        }
    }
    ```

### 4. Tầng Application (`Aqt.CoreOracle.Application`)

*   **AppService Implementation `CountryAppService`**:
    *   Tạo file: `src/Aqt.CoreOracle.Application/Countries/CountryAppService.cs`
    *   Kế thừa `CrudAppService<Country, CountryDto, Guid, GetCountryListInput, CreateUpdateCountryDto>` và implement `ICountryAppService`.
    *   Inject `IRepository<Country, Guid>` (hoặc `ICountryRepository`).
    *   Ghi đè `CreateAsync`, `UpdateAsync`, `GetListAsync` nếu cần logic đặc biệt (ví dụ: kiểm tra unique code, chuẩn hóa input, custom query).
    *   Thêm `[Authorize]` attributes với permissions đã định nghĩa.

    ```csharp
    // src/Aqt.CoreOracle.Application/Countries/CountryAppService.cs
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Aqt.CoreOracle.Application.Contracts.Countries;
    using Aqt.CoreOracle.Application.Contracts.Countries.Dtos;
    using Aqt.CoreOracle.Domain.Countries; // Country entity & ICountryRepository
    using Aqt.CoreOracle.Permissions; // CoreOraclePermissions
    using Microsoft.AspNetCore.Authorization;
    using Volo.Abp.Application.Dtos;
    using Volo.Abp.Application.Services;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.ObjectMapping; // Required for manual mapping if needed
    using System.Linq.Dynamic.Core; // For sorting

    namespace Aqt.CoreOracle.Application.Countries
    {
        [Authorize(CoreOraclePermissions.Countries.Default)]
        public class CountryAppService :
            CrudAppService<
                Country, //The Domain entity
                CountryDto, //The DTO used to show Countries
                Guid, //Primary key of the Country entity
                GetCountryListInput, //Used for filtering and paging
                CreateUpdateCountryDto>, //Used to create/update a Country
            ICountryAppService //Implement the ICountryAppService
        {
            private readonly ICountryRepository _countryRepository; // Inject custom repository if defined

            public CountryAppService(IRepository<Country, Guid> repository, ICountryRepository countryRepository) // Inject standard and custom repo
                : base(repository)
            {
                 _countryRepository = countryRepository; // Assign custom repository
                // Set permission names for CRUD operations
                GetPolicyName = CoreOraclePermissions.Countries.Default;
                GetListPolicyName = CoreOraclePermissions.Countries.Default;
                CreatePolicyName = CoreOraclePermissions.Countries.Create;
                UpdatePolicyName = CoreOraclePermissions.Countries.Edit;
                DeletePolicyName = CoreOraclePermissions.Countries.Delete;
            }

             // Override GetListAsync to implement filtering
            protected override async Task<IQueryable<Country>> CreateFilteredQueryAsync(GetCountryListInput input)
            {
                var query = await Repository.GetQueryableAsync();

                 // Add filtering based on the input
                if (!string.IsNullOrWhiteSpace(input.Filter))
                {
                    query = query.Where(c => c.Name.Contains(input.Filter) || c.Code.Contains(input.Filter));
                }

                return query;
                // No need to apply sorting and paging here, CrudAppService handles it.
            }


            [Authorize(CoreOraclePermissions.Countries.Create)]
            public override async Task<CountryDto> CreateAsync(CreateUpdateCountryDto input)
            {
                 // Check for duplicate code before creating
                var existingCountry = await _countryRepository.FindByCodeAsync(input.Code);
                if (existingCountry != null)
                {
                    // Use BusinessException with a specific error code
                    throw new Volo.Abp.BusinessException(CoreOracleDomainErrorCodes.CountryCodeAlreadyExists)
                        .WithData("Code", input.Code);
                }

                return await base.CreateAsync(input);
            }

            [Authorize(CoreOraclePermissions.Countries.Edit)]
            public override async Task<CountryDto> UpdateAsync(Guid id, CreateUpdateCountryDto input)
            {
                // Check for duplicate code on update, excluding the current entity
                 var existingCountry = await _countryRepository.FindByCodeAsync(input.Code);
                if (existingCountry != null && existingCountry.Id != id)
                {
                     // Use BusinessException with a specific error code
                    throw new Volo.Abp.BusinessException(CoreOracleDomainErrorCodes.CountryCodeAlreadyExists)
                        .WithData("Code", input.Code);
                }

                return await base.UpdateAsync(id, input);
            }

             // Override other methods like DeleteAsync if specific logic is needed
        }
    }
    ```

*   **AutoMapper Profile**:
    *   Cập nhật `src/Aqt.CoreOracle.Application/CoreOracleApplicationAutoMapperProfile.cs`
    *   Thêm mapping cho `Country` và DTOs. Sử dụng `ConstructUsing` cho `CreateUpdateCountryDto` nếu `Country` entity có constructor yêu cầu `Guid`.

    ```csharp
    // src/Aqt.CoreOracle.Application/CoreOracleApplicationAutoMapperProfile.cs
    using Aqt.CoreOracle.Application.Contracts.Countries.Dtos;
    using Aqt.CoreOracle.Domain.Countries; // Entity
    using AutoMapper;

    namespace Aqt.CoreOracle
    {
        public class CoreOracleApplicationAutoMapperProfile : Profile
        {
            public CoreOracleApplicationAutoMapperProfile()
            {
                /* You can configure your AutoMapper mapping configuration here.
                 * Alternatively, you can split your mapping configurations
                 * into multiple profile classes for a better organization. */

                 CreateMap<Country, CountryDto>();
                CreateMap<CreateUpdateCountryDto, Country>()
                     // If Country constructor requires Id, use ConstructUsing:
                    // Note: CrudAppService typically handles Guid generation.
                    // Use ConstructUsing ONLY if the 'Country' entity's constructor *requires* the ID
                    // and doesn't have a parameterless protected constructor.
                    // Otherwise, ABP handles ID generation automatically when InsertAsync is called.
                    // If using ConstructUsing, inject IGuidGenerator.
                    .ConstructUsing(dto => new Country(GuidGenerator.Create(), dto.Code, dto.Name, dto.IsoAlpha2Code, dto.IsoAlpha3Code, dto.NumericCode))
                    .Ignore(x => x.Id) // Ignore Id if handled by constructor or ABP automatically
                    .IgnoreAuditedObjectProperties() // Ignore audit properties if base DTO doesn't have them
                    .IgnoreFullAuditedObjectProperties() // Ignore soft delete property
                    .IgnoreExtraProperties(); // Ignore ABP extra properties

                 // Add other mappings if needed
                 CreateMap<CountryDto, CreateUpdateCountryDto>();
            }
        }
    }

    ```

### 5. Tầng EntityFrameworkCore (`Aqt.CoreOracle.EntityFrameworkCore`)

*   **Repository Implementation `EfCoreCountryRepository`** (Nếu có `ICountryRepository`):
    *   Tạo file: `src/Aqt.CoreOracle.EntityFrameworkCore/Countries/EfCoreCountryRepository.cs`
    *   Kế thừa `EfCoreRepository<CoreOracleDbContext, Country, Guid>` và implement `ICountryRepository`.
    *   Implement các method tùy chỉnh (ví dụ: `FindByCodeAsync`).

    ```csharp
    // src/Aqt.CoreOracle.EntityFrameworkCore/Countries/EfCoreCountryRepository.cs
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Aqt.CoreOracle.Domain.Countries;
    using Aqt.CoreOracle.EntityFrameworkCore; // DbContext
    using Microsoft.EntityFrameworkCore;
    using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
    using Volo.Abp.EntityFrameworkCore; // IDbContextProvider

    namespace Aqt.CoreOracle.EntityFrameworkCore.Countries
    {
        public class EfCoreCountryRepository
            : EfCoreRepository<CoreOracleDbContext, Country, Guid>,
              ICountryRepository // Implement the custom interface
        {
            public EfCoreCountryRepository(
                IDbContextProvider<CoreOracleDbContext> dbContextProvider)
                : base(dbContextProvider)
            {
            }

            // Implement the custom FindByCodeAsync method
            public async Task<Country> FindByCodeAsync(
                string code,
                bool includeDetails = true, // Example: typically not needed for simple properties
                CancellationToken cancellationToken = default)
            {
                // No need for includeDetails if Country has no complex navigation properties to include
                return await (await GetDbSetAsync())
                    .FirstOrDefaultAsync(c => c.Code == code, GetCancellationToken(cancellationToken));
            }

             // Override other methods from IRepository if needed for EF Core specific implementation
             // Example: Override GetListAsync if you need custom includes not handled by CrudAppService
            // public override async Task<List<Country>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default)
            // {
            //     return includeDetails // Check if details should be included
            //         ? await (await GetDbSetAsync()).Include(/* related data */).ToListAsync(GetCancellationToken(cancellationToken))
            //         : await base.GetListAsync(includeDetails, cancellationToken);
            // }
        }
    }

    ```

*   **DbContext**:
    *   Cập nhật `src/Aqt.CoreOracle.EntityFrameworkCore/EntityFrameworkCore/CoreOracleDbContext.cs`
    *   Thêm `DbSet<Country> Countries { get; set; }`.

    ```csharp
    // src/Aqt.CoreOracle.EntityFrameworkCore/EntityFrameworkCore/CoreOracleDbContext.cs
    using Aqt.CoreOracle.Domain.Countries; // Import Country entity
    using Microsoft.EntityFrameworkCore;
    using Volo.Abp.Data;
    using Volo.Abp.EntityFrameworkCore;
    // ... other usings

    namespace Aqt.CoreOracle.EntityFrameworkCore
    {
        [ConnectionStringName("Default")]
        public class CoreOracleDbContext :
            AbpDbContext<CoreOracleDbContext> // Modify based on your DbContext base
        {
            public DbSet<Country> Countries { get; set; } // Add DbSet for Country

            // ... other DbSets

            public CoreOracleDbContext(DbContextOptions<CoreOracleDbContext> options)
                : base(options)
            {

            }

            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);

                /* Include modules to your migration db context */

                // ... other module configurations

                /* Configure your own tables/entities inside the ConfigureCoreOracle method */

                builder.ConfigureCoreOracle(); // Ensure this calls the configuration for Country
            }
        }
    }
    ```

*   **Entity Mapping**:
    *   Cập nhật `src/Aqt.CoreOracle.EntityFrameworkCore/EntityFrameworkCore/CoreOracleDbContextModelCreatingExtensions.cs`
    *   Trong `ConfigureCoreOracle`, thêm cấu hình cho `Country` entity.

    ```csharp
    // src/Aqt.CoreOracle.EntityFrameworkCore/EntityFrameworkCore/CoreOracleDbContextModelCreatingExtensions.cs
    using Aqt.CoreOracle.Domain.Countries; // Country entity
    using Aqt.CoreOracle.Domain.Shared.Countries; // CountryConsts
    using Microsoft.EntityFrameworkCore;
    using Volo.Abp;
    using Volo.Abp.EntityFrameworkCore.Modeling; // ConfigureByConvention()

    namespace Aqt.CoreOracle.EntityFrameworkCore
    {
        public static class CoreOracleDbContextModelCreatingExtensions
        {
            public static void ConfigureCoreOracle(this ModelBuilder builder)
            {
                Check.NotNull(builder, nameof(builder));

                /* Configure your own tables/entities here */

                builder.Entity<Country>(b =>
                {
                    // Set table name and schema (adjust schema if needed)
                    b.ToTable(CoreOracleConsts.DbTablePrefix + "Countries", CoreOracleConsts.DbSchema);

                    b.ConfigureByConvention(); // Apply ABP conventions

                    // Configure properties
                    b.Property(x => x.Code)
                        .IsRequired()
                        .HasMaxLength(CountryConsts.MaxCodeLength)
                        .HasColumnName(nameof(Country.Code)); // Explicit name mapping if needed

                    b.Property(x => x.Name)
                        .IsRequired()
                        .HasMaxLength(CountryConsts.MaxNameLength)
                        .HasColumnName(nameof(Country.Name));

                     b.Property(x => x.IsoAlpha2Code)
                        .HasMaxLength(CountryConsts.IsoAlpha2CodeLength)
                        .HasColumnName(nameof(Country.IsoAlpha2Code));

                     b.Property(x => x.IsoAlpha3Code)
                        .HasMaxLength(CountryConsts.IsoAlpha3CodeLength)
                        .HasColumnName(nameof(Country.IsoAlpha3Code));

                     b.Property(x => x.NumericCode)
                         .HasColumnName(nameof(Country.NumericCode));

                    // Configure indexes
                    b.HasIndex(x => x.Code).IsUnique(); // Ensure Code is unique
                    b.HasIndex(x => x.Name); // Index for searching by name

                    // Configure relationships if any
                    // b.HasMany(x => x.Cities).WithOne().HasForeignKey(x => x.CountryId).IsRequired();

                    b.ApplyObjectExtensionMappings(); // Apply ExtraProperties mappings
                });

                builder.TryConfigureObjectExtensions<Country>(); // Configure ExtraProperties storage

                // Configure other entities...
            }
        }
    }
    ```

*   **Database Migration**:
    *   Mở terminal trong thư mục `src/Aqt.CoreOracle.EntityFrameworkCore`.
    *   Chạy lệnh: `dotnet ef migrations add Added_Country_Entity`
    *   Kiểm tra file migration mới được tạo ra.
    *   Chạy lệnh để cập nhật database (thường chạy từ DbMigrator project hoặc Web project): `dotnet ef database update` (hoặc chạy ứng dụng nếu dùng DbMigrator).

### 6. Tầng Web (`Aqt.CoreOracle.Web`)

*   **Menu**:
    *   Cập nhật `src/Aqt.CoreOracle.Web/Menus/CoreOracleMenuContributor.cs`
    *   Thêm menu item "Countries" vào `ConfigureMainMenuAsync`. Kiểm tra quyền `CoreOraclePermissions.Countries.Default`.

    ```csharp
    // src/Aqt.CoreOracle.Web/Menus/CoreOracleMenuContributor.cs
    using System.Threading.Tasks;
    using Aqt.CoreOracle.Localization;
    using Aqt.CoreOracle.Permissions; // Import Permissions
    using Volo.Abp.UI.Navigation;

    namespace Aqt.CoreOracle.Web.Menus
    {
        public class CoreOracleMenuContributor : IMenuContributor
        {
            public async Task ConfigureMenuAsync(MenuConfigurationContext context)
            {
                if (context.Menu.Name == StandardMenus.Main)
                {
                    await ConfigureMainMenuAsync(context);
                }
            }

            private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
            {
                var administration = context.Menu.GetAdministration();
                var l = context.GetLocalizer<CoreOracleResource>();

                context.Menu.Items.Insert(
                    0,
                    new ApplicationMenuItem(
                        CoreOracleMenus.Home,
                        l["Menu:Home"],
                        "~/",
                        icon: "fas fa-home",
                        order: 0
                    )
                );

                // Add Countries menu item if user has permission
                if (await context.IsGrantedAsync(CoreOraclePermissions.Countries.Default))
                {
                     context.Menu.AddItem(
                        new ApplicationMenuItem(
                            "App.Countries", // Unique name for the menu item
                            l["Menu:Countries"],
                            url: "/Countries", // URL to the Index page
                            icon: "fas fa-globe", // Choose an appropriate icon
                            order: 1 // Adjust order as needed
                        )
                    );
                }


                if (CoreOracleModule.IsMultiTenant)
                {
                    administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
                }
                else
                {
                    administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
                }

                administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
                administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);
            }
        }
    }

    // Ensure CoreOracleMenus class exists (usually in Web project)
    public class CoreOracleMenus
    {
        private const string Prefix = "CoreOracle";
        public const string Home = Prefix + ".Home";
        // Add other menu names here if needed
    }

    ```

*   **Razor Pages**:
    *   Tạo thư mục: `src/Aqt.CoreOracle.Web/Pages/Countries`
    *   `Index.cshtml`:
        *   Sử dụng `abp-card`, `abp-table` và `abp-dynamic-datatable`.
        *   Cấu hình datatable để gọi `CountryAppService.GetListAsync`.
        *   Thêm nút "New Country" (mở modal) và actions Edit/Delete cho từng dòng.

        ```html
        @page
        @using Microsoft.AspNetCore.Authorization
        @using Microsoft.AspNetCore.Mvc.Localization
        @using Aqt.CoreOracle.Localization
        @using Aqt.CoreOracle.Permissions
        @using Aqt.CoreOracle.Web.Pages.Countries
        @model IndexModel
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
                        @if (await AuthorizationService.IsGrantedAsync(CoreOraclePermissions.Countries.Create))
                        {
                            <abp-button id="NewCountryButton"
                                        text="@L["NewCountry"].Value"
                                        icon="plus"
                                        button-type="Primary" />
                        }
                    </abp-column>
                </abp-row>
            </abp-card-header>
            <abp-card-body>
                 <abp-table striped-rows="true" id="CountriesTable"></abp-table>
            </abp-card-body>
        </abp-card>
        ```

    *   `Index.cshtml.cs`:
        *   `IndexModel` kế thừa từ `CoreOraclePageModel`.
        *   Không cần inject AppService vì datatable sẽ tự gọi API. `OnGet` để trống.

        ```csharp
        // src/Aqt.CoreOracle.Web/Pages/Countries/Index.cshtml.cs
        using Microsoft.AspNetCore.Mvc.RazorPages; // Or your base PageModel

        namespace Aqt.CoreOracle.Web.Pages.Countries
        {
            public class IndexModel : CoreOraclePageModel // Use your base PageModel
            {
                public void OnGet()
                {
                    // No logic needed here, data is loaded by datatable via API call
                }
            }
        }
        ```

    *   `CreateModal.cshtml`:
        *   Sử dụng `abp-modal`, `abp-dynamic-form`.
        *   Binding model tới `CreateUpdateCountryDto`.

        ```html
        @page
        @using Microsoft.AspNetCore.Mvc.Localization
        @using Aqt.CoreOracle.Localization
        @using Aqt.CoreOracle.Web.Pages.Countries
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
                <abp-modal-footer buttons="@(AbpModalButtons.Cancel|AbpModalButtons.Save)"></abp-modal-footer>
            </abp-modal>
        </abp-dynamic-form>
        ```

    *   `CreateModal.cshtml.cs`:
        *   `CreateModalModel` kế thừa `CoreOraclePageModel`.
        *   Inject `ICountryAppService`.
        *   `[BindProperty]` cho `CreateUpdateCountryDto`.
        *   Implement `OnPostAsync` để gọi `_countryAppService.CreateAsync()`.

        ```csharp
        // src/Aqt.CoreOracle.Web/Pages/Countries/CreateModal.cshtml.cs
        using System.Threading.Tasks;
        using Aqt.CoreOracle.Application.Contracts.Countries; // ICountryAppService
        using Aqt.CoreOracle.Application.Contracts.Countries.Dtos; // CreateUpdateCountryDto
        using Microsoft.AspNetCore.Mvc;

        namespace Aqt.CoreOracle.Web.Pages.Countries
        {
            public class CreateModalModel : CoreOraclePageModel // Use your base PageModel
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
                   // Country = new CreateUpdateCountryDto(); // Already initialized in constructor
                }

                public async Task<IActionResult> OnPostAsync()
                {
                    await _countryAppService.CreateAsync(Country);
                    return NoContent(); // Indicates success to ABP modal manager
                }
            }
        }
        ```

    *   `EditModal.cshtml`:
        *   Tương tự `CreateModal.cshtml` nhưng thêm hidden field cho `Id`.
        *   Binding model tới `EditViewModel` (hoặc trực tiếp `CreateUpdateCountryDto` nếu đơn giản).

        ```html
        @page "{id}"
        @using Microsoft.AspNetCore.Mvc.Localization
        @using Aqt.CoreOracle.Localization
        @using Aqt.CoreOracle.Web.Pages.Countries
        @model EditModalModel
        @inject IHtmlLocalizer<CoreOracleResource> L
        @{
            Layout = null;
        }

        <abp-dynamic-form abp-model="Country" asp-page="/Countries/EditModal">
             <input type="hidden" asp-for="Id" /> @* Pass Id back on submit *@
            <abp-modal>
                <abp-modal-header title="@L["EditCountry"].Value"></abp-modal-header>
                <abp-modal-body>
                    <abp-form-content />
                </abp-modal-body>
                <abp-modal-footer buttons="@(AbpModalButtons.Cancel|AbpModalButtons.Save)"></abp-modal-footer>
            </abp-modal>
        </abp-dynamic-form>
        ```

    *   `EditModal.cshtml.cs`:
        *   `EditModalModel` kế thừa `CoreOraclePageModel`.
        *   Inject `ICountryAppService`.
        *   Thêm `[HiddenInput]` và `[BindProperty(SupportsGet = true)]` cho `Id`.
        *   `[BindProperty]` cho `CreateUpdateCountryDto`.
        *   Implement `OnGetAsync` để gọi `_countryAppService.GetAsync(Id)` và map vào `Country` property.
        *   Implement `OnPostAsync` để gọi `_countryAppService.UpdateAsync(Id, Country)`.

        ```csharp
        // src/Aqt.CoreOracle.Web/Pages/Countries/EditModal.cshtml.cs
        using System;
        using System.Threading.Tasks;
        using Aqt.CoreOracle.Application.Contracts.Countries; // ICountryAppService
        using Aqt.CoreOracle.Application.Contracts.Countries.Dtos; // CreateUpdateCountryDto
        using Microsoft.AspNetCore.Mvc;
        using AutoMapper; // If using view model

        namespace Aqt.CoreOracle.Web.Pages.Countries
        {
            public class EditModalModel : CoreOraclePageModel // Use your base PageModel
            {
                [HiddenInput]
                [BindProperty(SupportsGet = true)]
                public Guid Id { get; set; }

                [BindProperty]
                public CreateUpdateCountryDto Country { get; set; }

                private readonly ICountryAppService _countryAppService;

                public EditModalModel(ICountryAppService countryAppService)
                {
                    _countryAppService = countryAppService;
                }

                public async Task OnGetAsync()
                {
                    var countryDto = await _countryAppService.GetAsync(Id);
                    // Use ObjectMapper if mapping to a different ViewModel,
                    // otherwise assign directly if BindProperty uses the same DTO type
                    Country = ObjectMapper.Map<CountryDto, CreateUpdateCountryDto>(countryDto);
                    // Ensure the mapping 'CreateMap<CountryDto, CreateUpdateCountryDto>();'
                    // exists in the CoreOracleApplicationAutoMapperProfile.
                    // Or if BindProperty is CreateUpdateCountryDto and Get returns CountryDto:
                    // Country = new CreateUpdateCountryDto {
                    //     Code = countryDto.Code,
                    //     Name = countryDto.Name,
                    //     IsoAlpha2Code = countryDto.IsoAlpha2Code,
                    //     //... etc
                    // };
                 }

                public async Task<IActionResult> OnPostAsync()
                {
                    await _countryAppService.UpdateAsync(Id, Country);
                    return NoContent();
                }
            }
        }

        // Helper mapping configuration if needed in the Web project's AutoMapper profile
        // (Usually better to keep mappings in Application layer if possible)
        // CreateMap<CountryDto, CreateUpdateCountryDto>(); // Add to CoreOracleWebAutoMapperProfile if needed

        ```

*   **JavaScript**:
    *   Tạo file: `src/Aqt.CoreOracle.Web/Pages/Countries/Index.js`
    *   Khởi tạo ABP Datatable, xử lý mở modal, xử lý nút delete.

    ```javascript
    // src/Aqt.CoreOracle.Web/Pages/Countries/Index.js
    $(function () {
        var l = abp.localization.getResource('CoreOracle'); // Localization resource name
        var createModal = new abp.ModalManager(abp.appPath + 'Countries/CreateModal');
        var editModal = new abp.ModalManager(abp.appPath + 'Countries/EditModal');
        var countryService = aqt.coreOracle.countries.country; // Proxy service

        var dataTable = $('#CountriesTable').DataTable(
            abp.libs.datatables.normalizeConfiguration({
                serverSide: true,
                paging: true,
                order: [[1, "asc"]], // Default sorting by Code
                searching: false, // Disable default search box, we use custom filter if needed
                scrollX: true,
                ajax: abp.libs.datatables.createAjax(countryService.getList), // Use generated proxy
                columnDefs: [
                    {
                        title: l('Actions'),
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
                                    visible: abp.auth.isGranted('CoreOracle.Countries.Delete'), // Check permission
                                    confirmMessage: function (data) {
                                        return l('AreYouSureToDelete', data.record.name); // Show confirmation message
                                    },
                                    action: function (data) {
                                        countryService
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload(); // Reload table data
                                            });
                                    }
                                }
                            ]
                        }
                    },
                    {
                        title: l('Code'),
                        data: "code"
                    },
                    {
                        title: l('Name'),
                        data: "name"
                    },
                     {
                        title: l('IsoAlpha2Code'),
                        data: "isoAlpha2Code",
                        orderable: false // Example: disable sorting for this column
                    },
                     {
                        title: l('IsoAlpha3Code'),
                        data: "isoAlpha3Code",
                         orderable: false
                    },
                     {
                        title: l('NumericCode'),
                        data: "numericCode",
                         orderable: false
                    }
                    // Add other columns from CountryDto as needed
                ]
            })
        );

        // Handle 'New Country' button click
        $('#NewCountryButton').click(function (e) {
            e.preventDefault();
            createModal.open();
        });

        // Reload table when modals are closed successfully
        createModal.onResult(function () {
            dataTable.ajax.reload();
        });

        editModal.onResult(function () {
            dataTable.ajax.reload();
        });

        // Add custom filtering logic here if needed, e.g., on a button click or input change
        // $('#SearchButton').click(function (e) {
        //     e.preventDefault();
        //     dataTable.ajax.reload(); // Will trigger createAjax again with updated filter parameters
        // });
    });

    ```

## Kiểm thử

*   Viết Unit Test cho `CountryAppService` (validation, permission checks).
*   Viết Integration Test để kiểm tra CRUD operations qua API/AppService.
*   Kiểm tra thủ công trên giao diện web.

## Phân quyền

*   Đảm bảo các action trên UI (button, menu item) được ẩn/hiện dựa trên quyền (`abp.auth.isGranted` trong JS, `AuthorizationService.IsGrantedAsync` trong C#).
*   Đảm bảo các phương thức AppService được bảo vệ bằng `[Authorize]` attribute.
