This file is a merged representation of a subset of the codebase, containing files not matching ignore patterns, combined into a single document by Repomix.
The content has been processed where comments have been removed, empty lines have been removed, line numbers have been added, content has been formatted for parsing in markdown style, content has been compressed (code blocks are separated by ⋮---- delimiter), security check has been disabled.

# File Summary

## Purpose
This file contains a packed representation of the entire repository's contents.
It is designed to be easily consumable by AI systems for analysis, code review,
or other automated processes.

## File Format
The content is organized as follows:
1. This summary section
2. Repository information
3. Directory structure
4. Multiple file entries, each consisting of:
  a. A header with the file path (## File: path/to/file)
  b. The full contents of the file in a code block

## Usage Guidelines
- This file should be treated as read-only. Any changes should be made to the
  original repository files, not this packed version.
- When processing this file, use the file path to distinguish
  between different files in the repository.
- Be aware that this file may contain sensitive information. Handle it with
  the same level of security as you would the original repository.

## Notes
- Some files may have been excluded based on .gitignore rules and Repomix's configuration
- Binary files are not included in this packed representation. Please refer to the Repository Structure section for a complete list of file paths, including binary files
- Files matching these patterns are excluded: etc/**, **/*.abppkg*, **/Migrations/*, **/abp.resourcemapping.js, .editorconfig, .gitattributes, .gitignore, Aqt.CoreOracle.abpmdl, Aqt.CoreOracle.abpsln, common.props, README.md
- Files matching patterns in .gitignore are excluded
- Files matching default ignore patterns are excluded
- Code comments have been removed from supported file types
- Empty lines have been removed from all files
- Line numbers have been added to the beginning of each line
- Content has been formatted for parsing in markdown style
- Content has been compressed - code blocks are separated by ⋮---- delimiter
- Security check has been disabled - content may contain sensitive information
- Files are sorted by Git change count (files with more changes are at the bottom)

## Additional Info

# Directory Structure
```
src/
  Aqt.CoreOracle.Application/
    Properties/
      AssemblyInfo.cs
    Aqt.CoreOracle.Application.csproj
    CoreOracleApplicationAutoMapperProfile.cs
    CoreOracleApplicationModule.cs
    CoreOracleAppService.cs
  Aqt.CoreOracle.Application.Contracts/
    Permissions/
      CoreOraclePermissionDefinitionProvider.cs
      CoreOraclePermissions.cs
    Aqt.CoreOracle.Application.Contracts.csproj
    CoreOracleApplicationContractsModule.cs
    CoreOracleDtoExtensions.cs
  Aqt.CoreOracle.DbMigrator/
    appsettings.json
    appsettings.secrets.json
    Aqt.CoreOracle.DbMigrator.csproj
    CoreOracleDbMigratorModule.cs
    DbMigratorHostedService.cs
    Dockerfile
    Dockerfile.local
    Program.cs
  Aqt.CoreOracle.Domain/
    Data/
      CoreOracleDbMigrationService.cs
      ICoreOracleDbSchemaMigrator.cs
      NullCoreOracleDbSchemaMigrator.cs
    Identity/
      ChangeIdentityPasswordPolicySettingDefinitionProvider.cs
    OpenIddict/
      OpenIddictDataSeedContributor.cs
    Properties/
      AssemblyInfo.cs
    Settings/
      CoreOracleSettingDefinitionProvider.cs
      CoreOracleSettings.cs
    Aqt.CoreOracle.Domain.csproj
    CoreOracleConsts.cs
    CoreOracleDomainModule.cs
  Aqt.CoreOracle.Domain.Shared/
    Localization/
      CoreOracle/
        ar.json
        de-DE.json
        en.json
        es.json
        fi.json
        fr.json
        hi.json
        hr.json
        hu.json
        it.json
        pt-BR.json
        ru.json
        sk.json
        sl.json
        sv.json
        tr.json
        zh-Hans.json
      CoreOracleResource.cs
    MultiTenancy/
      MultiTenancyConsts.cs
    Aqt.CoreOracle.Domain.Shared.csproj
    CoreOracleDomainErrorCodes.cs
    CoreOracleDomainSharedModule.cs
    CoreOracleGlobalFeatureConfigurator.cs
    CoreOracleModuleExtensionConfigurator.cs
  Aqt.CoreOracle.EntityFrameworkCore/
    EntityFrameworkCore/
      CoreOracleDbContext.cs
      CoreOracleDbContextFactory.cs
      CoreOracleEfCoreEntityExtensionMappings.cs
      CoreOracleEntityFrameworkCoreModule.cs
      EntityFrameworkCoreCoreOracleDbSchemaMigrator.cs
    Properties/
      AssemblyInfo.cs
    Aqt.CoreOracle.EntityFrameworkCore.csproj
  Aqt.CoreOracle.HttpApi/
    Controllers/
      CoreOracleController.cs
    Models/
      Test/
        TestModel.cs
    Aqt.CoreOracle.HttpApi.csproj
    CoreOracleHttpApiModule.cs
  Aqt.CoreOracle.HttpApi.Client/
    Aqt.CoreOracle.HttpApi.Client.csproj
    CoreOracleHttpApiClientModule.cs
  Aqt.CoreOracle.Web/
    Components/
      _ViewImports.cshtml
    HealthChecks/
      CoreOracleDatabaseCheck.cs
      HealthChecksBuilderExtensions.cs
    Menus/
      CoreOracleMenuContributor.cs
      CoreOracleMenus.cs
      CoreOracleToolbarContributor.cs
    Pages/
      _ViewImports.cshtml
      CoreOraclePageModel.cs
      Index.cshtml
      Index.cshtml.cs
      Index.css
    Properties/
      AssemblyInfo.cs
      launchSettings.json
    Views/
      _ViewImports.cshtml
    wwwroot/
      images/
        getting-started/
          discord.svg
          instagram.svg
          stack-overflow.svg
          x-white.svg
          youtube.svg
      global-scripts.js
      global-styles.css
    appsettings.Development.json
    appsettings.json
    Aqt.CoreOracle.Web.csproj
    CoreOracleBrandingProvider.cs
    CoreOracleWebAutoMapperProfile.cs
    CoreOracleWebModule.cs
    Dockerfile
    Dockerfile.local
    package.json
    Program.cs
    web.config
test/
  Aqt.CoreOracle.Application.Tests/
    Samples/
      SampleAppServiceTests.cs
    Aqt.CoreOracle.Application.Tests.csproj
    CoreOracleApplicationTestBase.cs
    CoreOracleApplicationTestModule.cs
  Aqt.CoreOracle.Domain.Tests/
    Samples/
      SampleDomainTests.cs
    Aqt.CoreOracle.Domain.Tests.csproj
    CoreOracleDomainTestBase.cs
    CoreOracleDomainTestModule.cs
  Aqt.CoreOracle.EntityFrameworkCore.Tests/
    EntityFrameworkCore/
      Applications/
        EfCoreSampleAppServiceTests.cs
      Domains/
        EfCoreSampleDomainTests.cs
      Samples/
        SampleRepositoryTests.cs
      CoreOracleEntityFrameworkCoreCollection.cs
      CoreOracleEntityFrameworkCoreCollectionFixtureBase.cs
      CoreOracleEntityFrameworkCoreFixture.cs
      CoreOracleEntityFrameworkCoreTestBase.cs
      CoreOracleEntityFrameworkCoreTestModule.cs
    Aqt.CoreOracle.EntityFrameworkCore.Tests.csproj
  Aqt.CoreOracle.HttpApi.Client.ConsoleTestApp/
    appsettings.json
    appsettings.secrets.json
    Aqt.CoreOracle.HttpApi.Client.ConsoleTestApp.csproj
    ClientDemoService.cs
    CoreOracleConsoleApiClientModule.cs
    Program.cs
  Aqt.CoreOracle.TestBase/
    Security/
      FakeCurrentPrincipalAccessor.cs
    appsettings.json
    appsettings.secrets.json
    Aqt.CoreOracle.TestBase.csproj
    CoreOracleTestBase.cs
    CoreOracleTestBaseModule.cs
    CoreOracleTestConsts.cs
    CoreOracleTestDataBuilder.cs
  Aqt.CoreOracle.Web.Tests/
    Pages/
      Index_Tests.cs
    Aqt.CoreOracle.Web.Tests.csproj
    CoreOracleWebCollection.cs
    CoreOracleWebTestBase.cs
    CoreOracleWebTestModule.cs
    Program.cs
    xunit.runner.json
Aqt.CoreOracle.sln
Aqt.CoreOracle.sln.DotSettings
NuGet.Config
```

# Files

## File: src/Aqt.CoreOracle.Application/Properties/AssemblyInfo.cs
```csharp

```

## File: src/Aqt.CoreOracle.Application/Aqt.CoreOracle.Application.csproj
```
<Project Sdk="Microsoft.NET.Sdk">

  <Import Project="..\..\common.props" />

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <RootNamespace>Aqt.CoreOracle</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Aqt.CoreOracle.Domain\Aqt.CoreOracle.Domain.csproj" />
    <ProjectReference Include="..\Aqt.CoreOracle.Application.Contracts\Aqt.CoreOracle.Application.Contracts.csproj" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Volo.Abp.PermissionManagement.Application" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.FeatureManagement.Application" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.SettingManagement.Application" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.Account.Application" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.Identity.Application" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.TenantManagement.Application" Version="9.1.0" />
  </ItemGroup>

</Project>
```

## File: src/Aqt.CoreOracle.Application/CoreOracleApplicationAutoMapperProfile.cs
```csharp
public class CoreOracleApplicationAutoMapperProfile : Profile
```

## File: src/Aqt.CoreOracle.Application/CoreOracleApplicationModule.cs
```csharp
public class CoreOracleApplicationModule : AbpModule
⋮----
public override void ConfigureServices(ServiceConfigurationContext context)
```

## File: src/Aqt.CoreOracle.Application/CoreOracleAppService.cs
```csharp
public abstract class CoreOracleAppService : ApplicationService
```

## File: src/Aqt.CoreOracle.Application.Contracts/Permissions/CoreOraclePermissionDefinitionProvider.cs
```csharp
public class CoreOraclePermissionDefinitionProvider : PermissionDefinitionProvider
⋮----
public override void Define(IPermissionDefinitionContext context)
⋮----
var myGroup = context.AddGroup(CoreOraclePermissions.GroupName);
⋮----
private static LocalizableString L(string name)
```

## File: src/Aqt.CoreOracle.Application.Contracts/Permissions/CoreOraclePermissions.cs
```csharp
public static class CoreOraclePermissions
```

## File: src/Aqt.CoreOracle.Application.Contracts/Aqt.CoreOracle.Application.Contracts.csproj
```
<Project Sdk="Microsoft.NET.Sdk">

  <Import Project="..\..\common.props" />

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <RootNamespace>Aqt.CoreOracle</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Aqt.CoreOracle.Domain.Shared\Aqt.CoreOracle.Domain.Shared.csproj" />
  </ItemGroup>
  
  <ItemGroup>
    <PackageReference Include="Volo.Abp.PermissionManagement.Application.Contracts" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.FeatureManagement.Application.Contracts" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.SettingManagement.Application.Contracts" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.Identity.Application.Contracts" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.Account.Application.Contracts" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.TenantManagement.Application.Contracts" Version="9.1.0" />
  </ItemGroup>

</Project>
```

## File: src/Aqt.CoreOracle.Application.Contracts/CoreOracleApplicationContractsModule.cs
```csharp
public class CoreOracleApplicationContractsModule : AbpModule
⋮----
public override void PreConfigureServices(ServiceConfigurationContext context)
⋮----
CoreOracleDtoExtensions.Configure();
```

## File: src/Aqt.CoreOracle.Application.Contracts/CoreOracleDtoExtensions.cs
```csharp
public static class CoreOracleDtoExtensions
⋮----
private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();
public static void Configure()
⋮----
OneTimeRunner.Run(() =>
```

## File: src/Aqt.CoreOracle.DbMigrator/appsettings.json
```json
{
  "ConnectionStrings": {
    "Default": "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=FREEPDB1)));User Id=corefw_admin;Password=123456a@;"
  },
  "OpenIddict": {
    "Applications": {
      "CoreOracle_App": {
        "ClientId": "CoreOracle_App"      },
      "CoreOracle_Swagger": {
        "ClientId": "CoreOracle_Swagger",
        "RootUrl": "https://localhost:44388/"
      }
    }
  }
}
```

## File: src/Aqt.CoreOracle.DbMigrator/appsettings.secrets.json
```json
{
}
```

## File: src/Aqt.CoreOracle.DbMigrator/Aqt.CoreOracle.DbMigrator.csproj
```
<Project Sdk="Microsoft.NET.Sdk">

  <Import Project="..\..\common.props" />

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Serilog.Extensions.Logging" Version="9.0.0" />
    <PackageReference Include="Serilog.Sinks.Async" Version="2.1.0" />
    <PackageReference Include="Serilog.Sinks.File" Version="6.0.0" />
    <PackageReference Include="Serilog.Sinks.Console" Version="6.0.0" />
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="9.0.0" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Volo.Abp.Autofac" Version="9.1.0" />
    <ProjectReference Include="..\Aqt.CoreOracle.EntityFrameworkCore\Aqt.CoreOracle.EntityFrameworkCore.csproj" />
    <ProjectReference Include="..\Aqt.CoreOracle.Application.Contracts\Aqt.CoreOracle.Application.Contracts.csproj" />
  </ItemGroup>

  <ItemGroup>
    <Compile Remove="Logs\**" />
    <Content Remove="Logs\**" />

    <EmbeddedResource Remove="Logs\**" />
    <None Remove="Logs\**" />
  </ItemGroup>

  <ItemGroup>
    <None Remove="appsettings.json" />
    <Content Include="appsettings.json">
      <CopyToPublishDirectory>PreserveNewest</CopyToPublishDirectory>
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
    <None Remove="appsettings.secrets.json" />
    <Content Include="appsettings.secrets.json">
      <CopyToPublishDirectory>PreserveNewest</CopyToPublishDirectory>
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
  </ItemGroup>

</Project>
```

## File: src/Aqt.CoreOracle.DbMigrator/CoreOracleDbMigratorModule.cs
```csharp
public class CoreOracleDbMigratorModule : AbpModule
```

## File: src/Aqt.CoreOracle.DbMigrator/DbMigratorHostedService.cs
```csharp
public class DbMigratorHostedService : IHostedService
⋮----
private readonly IHostApplicationLifetime _hostApplicationLifetime;
private readonly IConfiguration _configuration;
⋮----
public async Task StartAsync(CancellationToken cancellationToken)
⋮----
options.Services.ReplaceConfiguration(_configuration);
options.UseAutofac();
options.Services.AddLogging(c => c.AddSerilog());
options.AddDataMigrationEnvironment();
⋮----
await application.InitializeAsync();
⋮----
.MigrateAsync();
await application.ShutdownAsync();
_hostApplicationLifetime.StopApplication();
⋮----
public Task StopAsync(CancellationToken cancellationToken)
```

## File: src/Aqt.CoreOracle.DbMigrator/Dockerfile
```
FROM mcr.microsoft.com/dotnet/aspnet:9.0
COPY bin/Release/net9.0/publish/ app/
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "Aqt.CoreOracle.DbMigrator.dll"]
```

## File: src/Aqt.CoreOracle.DbMigrator/Dockerfile.local
```
FROM mcr.microsoft.com/dotnet/aspnet:9.0
  COPY bin/Release/net9.0/publish/ app/
  WORKDIR /app
  ENTRYPOINT ["dotnet", "Aqt.CoreOracle.DbMigrator.dll"]
```

## File: src/Aqt.CoreOracle.DbMigrator/Program.cs
```csharp
class Program
⋮----
static async Task Main(string[] args)
⋮----
Log.Logger = new LoggerConfiguration()
.MinimumLevel.Information()
.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
.MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
⋮----
.MinimumLevel.Override("Aqt.CoreOracle", LogEventLevel.Debug)
⋮----
.MinimumLevel.Override("Aqt.CoreOracle", LogEventLevel.Information)
⋮----
.Enrich.FromLogContext()
.WriteTo.Async(c => c.File("Logs/logs.txt"))
.WriteTo.Async(c => c.Console())
.CreateLogger();
await CreateHostBuilder(args).RunConsoleAsync();
⋮----
public static IHostBuilder CreateHostBuilder(string[] args) =>
Host.CreateDefaultBuilder(args)
.AddAppSettingsSecretsJson()
.ConfigureLogging((context, logging) => logging.ClearProviders())
.ConfigureServices((hostContext, services) =>
```

## File: src/Aqt.CoreOracle.Domain/Data/CoreOracleDbMigrationService.cs
```csharp
public class CoreOracleDbMigrationService : ITransientDependency
⋮----
private readonly IDataSeeder _dataSeeder;
⋮----
private readonly ITenantRepository _tenantRepository;
private readonly ICurrentTenant _currentTenant;
⋮----
public async Task MigrateAsync()
⋮----
Logger.LogInformation("Started database migrations...");
⋮----
Logger.LogInformation($"Successfully completed host database migrations.");
⋮----
var tenants = await _tenantRepository.GetListAsync(includeDetails: true);
⋮----
using (_currentTenant.Change(tenant.Id))
⋮----
if (tenant.ConnectionStrings.Any())
⋮----
.Select(x => x.Value)
.ToList();
if (!migratedDatabaseSchemas.IsSupersetOf(tenantConnectionStrings))
⋮----
migratedDatabaseSchemas.AddIfNotContains(tenantConnectionStrings);
⋮----
Logger.LogInformation($"Successfully completed {tenant.Name} tenant database migrations.");
⋮----
Logger.LogInformation("Successfully completed all database migrations.");
⋮----
Logger.LogInformation("You can safely end this process...");
⋮----
private async Task MigrateDatabaseSchemaAsync(Tenant? tenant = null)
⋮----
Logger.LogInformation(
⋮----
await migrator.MigrateAsync();
⋮----
private async Task SeedDataAsync(Tenant? tenant = null)
⋮----
Logger.LogInformation($"Executing {(tenant == null ? "host" : tenant.Name + " tenant")} database seed...");
await _dataSeeder.SeedAsync(new DataSeedContext(tenant?.Id)
.WithProperty(IdentityDataSeedContributor.AdminEmailPropertyName,
⋮----
.WithProperty(IdentityDataSeedContributor.AdminPasswordPropertyName,
⋮----
private bool AddInitialMigrationIfNotExist()
⋮----
Logger.LogWarning("Couldn't determinate if any migrations exist : " + e.Message);
⋮----
private bool DbMigrationsProjectExists()
⋮----
private bool MigrationsFolderExists()
⋮----
return dbMigrationsProjectFolder != null && Directory.Exists(Path.Combine(dbMigrationsProjectFolder, "Migrations"));
⋮----
private void AddInitialMigration()
⋮----
Logger.LogInformation("Creating initial migration...");
⋮----
if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
⋮----
var procStartInfo = new ProcessStartInfo(fileName,
⋮----
Process.Start(procStartInfo);
⋮----
throw new Exception("Couldn't run ABP CLI...");
⋮----
private string? GetEntityFrameworkCoreProjectFolderPath()
⋮----
throw new Exception("Solution folder not found!");
⋮----
var srcDirectoryPath = Path.Combine(slnDirectoryPath, "src");
return Directory.GetDirectories(srcDirectoryPath)
.FirstOrDefault(d => d.EndsWith(".EntityFrameworkCore"));
⋮----
private string? GetSolutionDirectoryPath()
⋮----
var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
while (currentDirectory != null && Directory.GetParent(currentDirectory.FullName) != null)
⋮----
currentDirectory = Directory.GetParent(currentDirectory.FullName);
if (currentDirectory != null && Directory.GetFiles(currentDirectory.FullName).FirstOrDefault(f => f.EndsWith(".sln")) != null)
```

## File: src/Aqt.CoreOracle.Domain/Data/ICoreOracleDbSchemaMigrator.cs
```csharp
public interface ICoreOracleDbSchemaMigrator
⋮----
Task MigrateAsync();
```

## File: src/Aqt.CoreOracle.Domain/Data/NullCoreOracleDbSchemaMigrator.cs
```csharp
public class NullCoreOracleDbSchemaMigrator : ICoreOracleDbSchemaMigrator, ITransientDependency
⋮----
public Task MigrateAsync()
```

## File: src/Aqt.CoreOracle.Domain/Identity/ChangeIdentityPasswordPolicySettingDefinitionProvider.cs
```csharp
public class ChangeIdentityPasswordPolicySettingDefinitionProvider : SettingDefinitionProvider
⋮----
public override void Define(ISettingDefinitionContext context)
⋮----
var requireNonAlphanumeric = context.GetOrNull(IdentitySettingNames.Password.RequireNonAlphanumeric);
⋮----
requireNonAlphanumeric.DefaultValue = false.ToString();
⋮----
var requireLowercase = context.GetOrNull(IdentitySettingNames.Password.RequireLowercase);
⋮----
requireLowercase.DefaultValue = false.ToString();
⋮----
var requireUppercase = context.GetOrNull(IdentitySettingNames.Password.RequireUppercase);
⋮----
requireUppercase.DefaultValue = false.ToString();
⋮----
var requireDigit = context.GetOrNull(IdentitySettingNames.Password.RequireDigit);
⋮----
requireDigit.DefaultValue = false.ToString();
```

## File: src/Aqt.CoreOracle.Domain/OpenIddict/OpenIddictDataSeedContributor.cs
```csharp
public class OpenIddictDataSeedContributor : IDataSeedContributor, ITransientDependency
⋮----
private readonly IConfiguration _configuration;
private readonly IOpenIddictApplicationRepository _openIddictApplicationRepository;
private readonly IAbpApplicationManager _applicationManager;
private readonly IOpenIddictScopeRepository _openIddictScopeRepository;
private readonly IOpenIddictScopeManager _scopeManager;
private readonly IPermissionDataSeeder _permissionDataSeeder;
⋮----
public virtual async Task SeedAsync(DataSeedContext context)
⋮----
private async Task CreateScopesAsync()
⋮----
if (await _openIddictScopeRepository.FindByNameAsync("CoreOracle") == null)
⋮----
await _scopeManager.CreateAsync(new OpenIddictScopeDescriptor {
⋮----
private async Task CreateApplicationsAsync()
⋮----
var configurationSection = _configuration.GetSection("OpenIddict:Applications");
⋮----
if (!consoleAndAngularClientId.IsNullOrWhiteSpace())
⋮----
if (!swaggerClientId.IsNullOrWhiteSpace())
⋮----
clientUri: swaggerRootUrl.EnsureEndsWith('/') + "swagger",
⋮----
private async Task CreateApplicationAsync(
⋮----
if (!string.IsNullOrEmpty(secret) && string.Equals(type, OpenIddictConstants.ClientTypes.Public,
⋮----
throw new BusinessException(L["NoClientSecretCanBeSetForPublicApplications"]);
⋮----
if (string.IsNullOrEmpty(secret) && string.Equals(type, OpenIddictConstants.ClientTypes.Confidential,
⋮----
throw new BusinessException(L["TheClientSecretIsRequiredForConfidentialApplications"]);
⋮----
var client = await _openIddictApplicationRepository.FindByClientIdAsync(name);
var application = new AbpApplicationDescriptor {
⋮----
Check.NotNullOrEmpty(grantTypes, nameof(grantTypes));
Check.NotNullOrEmpty(scopes, nameof(scopes));
if (new[] { OpenIddictConstants.GrantTypes.AuthorizationCode, OpenIddictConstants.GrantTypes.Implicit }.All(
⋮----
application.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.CodeIdToken);
if (string.Equals(type, OpenIddictConstants.ClientTypes.Public, StringComparison.OrdinalIgnoreCase))
⋮----
application.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.CodeIdTokenToken);
application.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.CodeToken);
⋮----
if (!redirectUri.IsNullOrWhiteSpace() || !postLogoutRedirectUri.IsNullOrWhiteSpace())
⋮----
application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.EndSession);
⋮----
application.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode);
application.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.Code);
⋮----
application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Authorization);
⋮----
application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Token);
application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Revocation);
application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Introspection);
⋮----
application.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.ClientCredentials);
⋮----
application.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.Implicit);
⋮----
application.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.Password);
⋮----
application.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.RefreshToken);
⋮----
application.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.DeviceCode);
application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.DeviceAuthorization);
⋮----
application.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.IdToken);
⋮----
application.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.IdTokenToken);
application.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.Token);
⋮----
if (!buildInGrantTypes.Contains(grantType))
⋮----
application.Permissions.Add(OpenIddictConstants.Permissions.Prefixes.GrantType + grantType);
⋮----
if (buildInScopes.Contains(scope))
⋮----
application.Permissions.Add(scope);
⋮----
application.Permissions.Add(OpenIddictConstants.Permissions.Prefixes.Scope + scope);
⋮----
if (!redirectUri.IsNullOrEmpty())
⋮----
if (!Uri.TryCreate(redirectUri, UriKind.Absolute, out var uri) || !uri.IsWellFormedOriginalString())
⋮----
throw new BusinessException(L["InvalidRedirectUri", redirectUri]);
⋮----
if (application.RedirectUris.All(x => x != uri))
⋮----
application.RedirectUris.Add(uri);
⋮----
if (!postLogoutRedirectUri.IsNullOrEmpty())
⋮----
if (!Uri.TryCreate(postLogoutRedirectUri, UriKind.Absolute, out var uri) ||
!uri.IsWellFormedOriginalString())
⋮----
throw new BusinessException(L["InvalidPostLogoutRedirectUri", postLogoutRedirectUri]);
⋮----
if (application.PostLogoutRedirectUris.All(x => x != uri))
⋮----
application.PostLogoutRedirectUris.Add(uri);
⋮----
await _permissionDataSeeder.SeedAsync(
⋮----
await _applicationManager.CreateAsync(application);
⋮----
client.RedirectUris = JsonSerializer.Serialize(application.RedirectUris.Select(q => q.ToString().RemovePostFix("/")));
client.PostLogoutRedirectUris = JsonSerializer.Serialize(application.PostLogoutRedirectUris.Select(q => q.ToString().RemovePostFix("/")));
await _applicationManager.UpdateAsync(client.ToModel());
⋮----
client.Permissions = JsonSerializer.Serialize(application.Permissions.Select(q => q.ToString()));
⋮----
private bool HasSameRedirectUris(OpenIddictApplication existingClient, AbpApplicationDescriptor application)
⋮----
return existingClient.RedirectUris == JsonSerializer.Serialize(application.RedirectUris.Select(q => q.ToString().RemovePostFix("/")));
⋮----
private bool HasSameScopes(OpenIddictApplication existingClient, AbpApplicationDescriptor application)
⋮----
return existingClient.Permissions == JsonSerializer.Serialize(application.Permissions.Select(q => q.ToString().TrimEnd('/')));
```

## File: src/Aqt.CoreOracle.Domain/Properties/AssemblyInfo.cs
```csharp

```

## File: src/Aqt.CoreOracle.Domain/Settings/CoreOracleSettingDefinitionProvider.cs
```csharp
public class CoreOracleSettingDefinitionProvider : SettingDefinitionProvider
⋮----
public override void Define(ISettingDefinitionContext context)
```

## File: src/Aqt.CoreOracle.Domain/Settings/CoreOracleSettings.cs
```csharp
public static class CoreOracleSettings
```

## File: src/Aqt.CoreOracle.Domain/Aqt.CoreOracle.Domain.csproj
```
<Project Sdk="Microsoft.NET.Sdk">

  <Import Project="..\..\common.props" />

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <RootNamespace>Aqt.CoreOracle</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Aqt.CoreOracle.Domain.Shared\Aqt.CoreOracle.Domain.Shared.csproj" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Volo.Abp.Emailing" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.Caching" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.PermissionManagement.Domain.Identity" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.PermissionManagement.Domain.OpenIddict" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.BackgroundJobs.Domain" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.AuditLogging.Domain" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.FeatureManagement.Domain" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.SettingManagement.Domain" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.BlobStoring.Database.Domain" Version="9.1.0" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Volo.Abp.OpenIddict.Domain" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.Identity.Domain" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.TenantManagement.Domain" Version="9.1.0" />
  </ItemGroup>

</Project>
```

## File: src/Aqt.CoreOracle.Domain/CoreOracleConsts.cs
```csharp
public static class CoreOracleConsts
```

## File: src/Aqt.CoreOracle.Domain/CoreOracleDomainModule.cs
```csharp
public class CoreOracleDomainModule : AbpModule
⋮----
public override void ConfigureServices(ServiceConfigurationContext context)
⋮----
options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
options.Languages.Add(new LanguageInfo("en", "en", "English"));
options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi"));
options.Languages.Add(new LanguageInfo("it", "it", "Italiano"));
options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch"));
options.Languages.Add(new LanguageInfo("es", "es", "Español"));
options.Languages.Add(new LanguageInfo("sv", "sv", "Svenska"));
⋮----
context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
```

## File: src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/ar.json
```json
{
  "culture": "ar",
  "texts": {
    "AppName": "CoreOracle",
    "Menu:Home": "منزل، بيت",
    "Menu:ContactUs": "اتصل بنا",
    "Menu:ArticleSample": "عينة المادة",
    "Home": "منزل، بيت",
    "Welcome": "مرحبا",
    "LongWelcomeMessage": "مرحبا بكم في التطبيق. هذا مشروع بدء التشغيل يعتمد على إطار عمل برنامج ABP. لمزيد من المعلومات قم بزيارة",
    "Date": "تاريخ",
    "Permission:Dashboard": "لوحة القيادة",
    "Menu:Dashboard": "لوحة القيادة",
    "Menu:HomePage": "الصفحة الرئيسية",
    "Dashboard": "لوحة القيادة",
    "ExternalProvider:Google": "جوجل",
    "ExternalProvider:Google:ClientId": "معرف العميل",
    "ExternalProvider:Google:ClientSecret": "سر العميل",
    "ExternalProvider:Microsoft": "مايكروسوفت",
    "ExternalProvider:Microsoft:ClientId": "معرف العميل",
    "ExternalProvider:Microsoft:ClientSecret": "سر العميل",
    "ExternalProvider:Twitter": "تويتر",
    "ExternalProvider:Twitter:ConsumerKey": "مفتاح المستهلك",
    "ExternalProvider:Twitter:ConsumerSecret": "سر المستهلك",
    "NewsletterHeader": "اشترك في النشرة الإخبارية!",
    "NewsletterInfo": "احصل على معلومات حول آخر الأحداث.",
    "NewsletterPreference_Default": "النشرة الإخبارية الافتراضية",
    "NewsletterPrivacyAcceptMessage": "أوافق على <a href='/privacy-policy'> سياسة الخصوصية </a>.",
    "Language": "لغة",
    "Search": "يبحث",
    "LoadMore": "تحميل المزيد",
    "Settings": "إعدادات",
    "Theme": "سمة",
    "DeviceTheme": "موضوع الجهاز",
    "Dark": "مظلم",
    "Light": "ضوء",
    "Unspecified": "نظام",
    "SeeAllUsers": "رؤية كافة المستخدمين",
    "TakePhoto": "تصوير",
    "ChoosePhoto": "اختر صورة"
  }
}
```

## File: src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/de-DE.json
```json
{
  "culture": "de-DE",
  "texts": {
    "AppName": "CoreOracle",
    "Menu:Home": "Home",
    "Menu:ContactUs": "Kontaktiere uns",
    "Menu:ArticleSample": "Artikelmuster",
    "Home": "Home",
    "Welcome": "Willkommen",
    "LongWelcomeMessage": "Willkommen in der Anwendung. Dies ist ein Startup-Projekt basierend auf dem ABP-Framework. Weitere Informationen finden Sie unter",
    "Date": "Datum",
    "Permission:Dashboard": "Dashboard",
    "Menu:Dashboard": "Dashboard",
    "Menu:HomePage": "Startseite",
    "Dashboard": "Dashboard",
    "ExternalProvider:Google": "Google",
    "ExternalProvider:Google:ClientId": "Kunden ID",
    "ExternalProvider:Google:ClientSecret": "Client-Geheimnis",
    "ExternalProvider:Microsoft": "Microsoft",
    "ExternalProvider:Microsoft:ClientId": "Kunden ID",
    "ExternalProvider:Microsoft:ClientSecret": "Client-Geheimnis",
    "ExternalProvider:Twitter": "Twitter",
    "ExternalProvider:Twitter:ConsumerKey": "Verbraucherschlüssel",
    "ExternalProvider:Twitter:ConsumerSecret": "Verbrauchergeheimnis",
    "NewsletterHeader": "Abonnieren Sie den Newsletter!",
    "NewsletterInfo": "Informieren Sie sich über aktuelle Ereignisse.",
    "NewsletterPreference_Default": "Standard-Newsletter",
    "NewsletterPrivacyAcceptMessage": "Ich akzeptiere die <a href='/privacy-policy'>Datenschutzerklärung</a>.",
    "Language": "Sprache",
    "Search": "Suchen",
    "LoadMore": "Mehr laden",
    "Settings": "Einstellungen",
    "Theme": "Thema",
    "DeviceTheme": "Gerätethema",
    "Dark": "Dunkel",
    "Light": "Licht",
    "Unspecified": "System",
    "SeeAllUsers": "Alle Benutzer anzeigen",
    "TakePhoto": "Foto machen",
    "ChoosePhoto": "Wähle ein Foto aus"
  }
}
```

## File: src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/en.json
```json
{
  "Culture": "en",
  "Texts": {
    "AppName": "CoreOracle",
    "Menu:Home": "Home",
    "LongWelcomeMessage": "Welcome to the application. This is a startup project based on the ABP framework. For more information visit",
    "Welcome": "Welcome"
  }
}
```

## File: src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/es.json
```json
{
  "culture": "es",
  "texts": {
    "AppName": "CoreOracle",
    "Menu:Home": "Hogar",
    "Menu:ContactUs": "Contáctenos",
    "Menu:ArticleSample": "Muestra de artículo",
    "Home": "Hogar",
    "Welcome": "Bienvenido",
    "LongWelcomeMessage": "Bienvenido a la aplicacion. Este es un proyecto de inicio basado en el marco ABP. Para obtener mas informacion, visite",
    "Date": "Fecha",
    "Permission:Dashboard": "Panel de control",
    "Menu:Dashboard": "Panel de control",
    "Menu:HomePage": "Página de inicio",
    "Dashboard": "Panel de control",
    "ExternalProvider:Google": "Google",
    "ExternalProvider:Google:ClientId": "ID de cliente",
    "ExternalProvider:Google:ClientSecret": "Client Secret",
    "ExternalProvider:Microsoft": "Microsoft",
    "ExternalProvider:Microsoft:ClientId": "ID de cliente",
    "ExternalProvider:Microsoft:ClientSecret": "Client Secret",
    "ExternalProvider:Twitter": "Twitter",
    "ExternalProvider:Twitter:ConsumerKey": "Clave de consumidor",
    "ExternalProvider:Twitter:ConsumerSecret": "Consumer Secret",
    "NewsletterHeader": "Suscríbete a la newsletter!",
    "NewsletterInfo": "Obtenga información sobre los últimos acontecimientos.",
    "NewsletterPreference_Default": "Boletín de noticias predeterminado",
    "NewsletterPrivacyAcceptMessage": "Acepto la <a href='/privacy-policy'> Política de privacidad </a>.",
    "Language": "Idioma",
    "Search": "Buscar",
    "LoadMore": "Carga más",
    "Settings": "Ajustes",
    "Theme": "Tema",
    "DeviceTheme": "Tema del dispositivo",
    "Dark": "Oscuro",
    "Light": "Luz",
    "Unspecified": "Sistema",
    "SeeAllUsers": "Ver todos los usuarios",
    "TakePhoto": "Tomar foto",
    "ChoosePhoto": "Escoge una foto"
  }
}
```

## File: src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/fi.json
```json
{
  "culture": "fi",
  "texts": {
    "AppName": "CoreOracle",
    "Menu:Home": "Koti",
    "Menu:ContactUs": "Ota meihin yhteyttä",
    "Menu:ArticleSample": "Artikkelinäyte",
    "Home": "Koti",
    "Welcome": "Tervetuloa",
    "LongWelcomeMessage": "Tervetuloa sovellukseen. Tämä on ABP-kehykseen perustuva käynnistysprojekti. Lisätietoja on osoitteessa",
    "Date": "Päivämäärä",
    "Permission:Dashboard": "Kojelauta",
    "Menu:Dashboard": "Kojelauta",
    "Menu:HomePage": "Kotisivu",
    "Dashboard": "Kojelauta",
    "ExternalProvider:Google": "Google",
    "ExternalProvider:Google:ClientId": "Asiakastunnus",
    "ExternalProvider:Google:ClientSecret": "Asiakkaan salaisuus",
    "ExternalProvider:Microsoft": "Microsoft",
    "ExternalProvider:Microsoft:ClientId": "Asiakastunnus",
    "ExternalProvider:Microsoft:ClientSecret": "Asiakkaan salaisuus",
    "ExternalProvider:Twitter": "Viserrys",
    "ExternalProvider:Twitter:ConsumerKey": "Kuluttaja-avain",
    "ExternalProvider:Twitter:ConsumerSecret": "Kuluttajan salaisuus",
    "NewsletterHeader": "Tilaa uutiskirje!",
    "NewsletterInfo": "Hanki tietoa viimeisimmistä tapahtumista.",
    "NewsletterPreference_Default": "Oletusuutiskirje",
    "NewsletterPrivacyAcceptMessage": "Hyväksyn <a href='/privacy-policy'> tietosuojakäytännön </a>.",
    "Language": "Kieli",
    "Search": "Hae",
    "LoadMore": "Lataa lisää",
    "Settings": "asetukset",
    "Theme": "Teema",
    "DeviceTheme": "Laitteen teema",
    "Dark": "Tumma",
    "Light": "Kevyt",
    "Unspecified": "Järjestelmä",
    "SeeAllUsers": "Katso Kaikki käyttäjät",
    "TakePhoto": "Ota valokuva",
    "ChoosePhoto": "Valitse Valokuva"
  }
}
```

## File: src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/fr.json
```json
{
  "culture": "fr",
  "texts": {
    "AppName": "CoreOracle",
    "Menu:Home": "Acceuil",
    "Menu:ContactUs": "Nous contacter",
    "Menu:ArticleSample": "Échantillon d'article",
    "Home": "Acceuil",
    "Welcome": "Bienvenue",
    "LongWelcomeMessage": "Bienvenue dans l'application. Il s'agit d'un projet de démarrage basé sur le framework ABP. Pour plus d'informations, visitez",
    "Date": "Date",
    "Permission:Dashboard": "Tableau de bord",
    "Menu:Dashboard": "Tableau de bord",
    "Menu:HomePage": "Page d'accueil",
    "Dashboard": "Tableau de bord",
    "ExternalProvider:Google": "Google",
    "ExternalProvider:Google:ClientId": "identité du client",
    "ExternalProvider:Google:ClientSecret": "Secret du client",
    "ExternalProvider:Microsoft": "Microsoft",
    "ExternalProvider:Microsoft:ClientId": "identité du client",
    "ExternalProvider:Microsoft:ClientSecret": "Secret du client",
    "ExternalProvider:Twitter": "Twitter",
    "ExternalProvider:Twitter:ConsumerKey": "La clé du consommateur",
    "ExternalProvider:Twitter:ConsumerSecret": "Secret du consommateur",
    "NewsletterHeader": "Abonnez-vous à la newsletter!",
    "NewsletterInfo": "Obtenez des informations sur les derniers événements.",
    "NewsletterPreference_Default": "Newsletter par défaut",
    "NewsletterPrivacyAcceptMessage": "J'accepte la <a href='/privacy-policy'> Politique de confidentialité </a>.",
    "Language": "Langue",
    "Search": "Recherche",
    "LoadMore": "Charger plus",
    "Settings": "Paramètres",
    "Theme": "Thème",
    "DeviceTheme": "Thème de l&#39;appareil",
    "Dark": "Sombre",
    "Light": "Lumière",
    "Unspecified": "Système",
    "SeeAllUsers": "Voir tous les utilisateurs",
    "TakePhoto": "Prendre une photo",
    "ChoosePhoto": "Choisissez une photo"
  }
}
```

## File: src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/hi.json
```json
{
  "culture": "hi",
  "texts": {
    "AppName": "CoreOracle",
    "Menu:Home": "घर",
    "Menu:ContactUs": "संपर्क करें",
    "Menu:ArticleSample": "आलेख नमूना",
    "Home": "घर",
    "Welcome": "स्वागत हे",
    "LongWelcomeMessage": "आवेदन करने के लिए आपका स्वागत है। यह एबीपी ढांचे पर आधारित एक स्टार्टअप परियोजना है। अधिक जानकारी के लिए पर जाएँ।",
    "Date": "दिनांक",
    "Permission:Dashboard": "डैशबोर्ड",
    "Menu:Dashboard": "डैशबोर्ड",
    "Menu:HomePage": "मुख पृष्ठ",
    "Dashboard": "डैशबोर्ड",
    "ExternalProvider:Google": "गूगल",
    "ExternalProvider:Google:ClientId": "ग्राहक ID",
    "ExternalProvider:Google:ClientSecret": "ग्राहक गुप्त",
    "ExternalProvider:Microsoft": "माइक्रोसॉफ्ट",
    "ExternalProvider:Microsoft:ClientId": "ग्राहक ID",
    "ExternalProvider:Microsoft:ClientSecret": "ग्राहक गुप्त",
    "ExternalProvider:Twitter": "ट्विटर",
    "ExternalProvider:Twitter:ConsumerKey": "उपभोक्ता कुंजी",
    "ExternalProvider:Twitter:ConsumerSecret": "उपभोक्ता रहस्य",
    "NewsletterHeader": "न्यूज़लेटर की सदस्यता लें!",
    "NewsletterInfo": "नवीनतम घटनाओं के बारे में जानकारी प्राप्त करें।",
    "NewsletterPreference_Default": "डिफ़ॉल्ट न्यूज़लैटर",
    "NewsletterPrivacyAcceptMessage": "मैं <a href='/privacy-policy'> गोपनीयता नीति </a> स्वीकार करता हूं।",
    "Language": "भाषा",
    "Search": "खोज",
    "LoadMore": "और लोड करें",
    "Settings": "समायोजन",
    "Theme": "विषय",
    "DeviceTheme": "डिवाइस थीम",
    "Dark": "अँधेरा",
    "Light": "रोशनी",
    "Unspecified": "प्रणाली",
    "SeeAllUsers": "सभी उपयोगकर्ता देखें",
    "TakePhoto": "फोटो लो",
    "ChoosePhoto": "तस्विर का चयन करो"
  }
}
```

## File: src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/hr.json
```json
{
    "culture": "hr",
    "texts": {
      "AppName": "CoreOracle",
      "Menu:Home": "Dom",
      "Menu:ContactUs": "Kontaktirajte nas",
      "Menu:ArticleSample": "Uzorak članka",
      "Home": "Dom",
      "Welcome": "Dobrodošli",
      "LongWelcomeMessage": "Dobrodošli u aplikaciju. Ovo je startup projekt temeljen na ABP okviru. Za više informacija posjetite",
      "Date": "Datum",
      "Permission:Dashboard": "Nadzorna ploča",
      "Menu:Dashboard": "Nadzorna ploča",
      "Menu:HomePage": "Početna stranica",
      "Dashboard": "Nadzorna ploča",
      "ExternalProvider:Google": "Google",
      "ExternalProvider:Google:ClientId": "ID klijenta",
      "ExternalProvider:Google:ClientSecret": "Tajna klijenta",
      "ExternalProvider:Microsoft": "Microsoft",
      "ExternalProvider:Microsoft:ClientId": "ID klijenta",
      "ExternalProvider:Microsoft:ClientSecret": "Tajna klijenta",
      "ExternalProvider:Twitter": "Cvrkut",
      "ExternalProvider:Twitter:ConsumerKey": "Ključ potrošača",
      "ExternalProvider:Twitter:ConsumerSecret": "Potrošačka tajna",
      "NewsletterHeader": "Pretplatite se na newsletter!",
      "NewsletterInfo": "Dobijte informacije o najnovijim događanjima.",
      "NewsletterPreference_Default": "Zadani bilten",
      "NewsletterPrivacyAcceptMessage": "Prihvaćam <a href='/privacy-policy'>Politiku privatnosti</a> .",
      "Language": "Jezik",
      "Search": "traži",
      "LoadMore": "Učitaj više",
      "Settings": "postavke",
      "Theme": "Tema",
      "DeviceTheme": "Tema uređaja",
      "Dark": "tamno",
      "Light": "Svjetlo",
      "Unspecified": "Sustav",
      "SeeAllUsers": "Vidi sve korisnike",
      "TakePhoto": "Uslikaj",
      "ChoosePhoto": "Odaberite fotografiju"
    }
  }
```

## File: src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/hu.json
```json
{
  "culture": "hu",
  "texts": {
    "AppName": "CoreOracle",
    "Menu:Home": "Kezdőlap",
    "Menu:ContactUs": "Lépjen kapcsolatba velünk",
    "Menu:ArticleSample": "Cikkminta",
    "Home": "Kezdőlap",
    "Welcome": "Üdvözöljük",
    "LongWelcomeMessage": "Üdvözöljük az alkalmazásban. Ez egy ABP keretrendszeren alapuló startup projekt. További információért látogasson el az oldalra",
    "Date": "dátum",
    "Permission:Dashboard": "Irányítópult",
    "Menu:Dashboard": "Irányítópult",
    "Menu:HomePage": "Kezdőlap",
    "Dashboard": "Irányítópult",
    "ExternalProvider:Google": "Google",
    "ExternalProvider:Google:ClientId": "Ügyfélazonosító",
    "ExternalProvider:Google:ClientSecret": "Ügyfél titkos kulcs",
    "ExternalProvider:Microsoft": "Microsoft",
    "ExternalProvider:Microsoft:ClientId": "Ügyfélazonosító",
    "ExternalProvider:Microsoft:ClientSecret": "Ügyfél titkos kulcs",
    "ExternalProvider:Twitter": "Twitter",
    "ExternalProvider:Twitter:ConsumerKey": "Fogyasztói kulcs",
    "ExternalProvider:Twitter:ConsumerSecret": "Fogyasztói titkos kulcs",
    "NewsletterHeader": "Iratkozz fel a hírlevélre!",
    "NewsletterInfo": "Tájékozódjon a legfrissebb eseményekről.",
    "NewsletterPreference_Default": "Alapértelmezett hírlevél",
    "NewsletterPrivacyAcceptMessage": "Elfogadom az <a href='/privacy-policy'>Adatvédelmi szabályzatot</a> .",
    "Language": "Nyelv",
    "Search": "Keresés",
    "LoadMore": "Load More",
    "Settings": "Beállítások",
    "Theme": "Téma",
    "DeviceTheme": "Eszköz téma",
    "Dark": "Sötét",
    "Light": "Fény",
    "Unspecified": "Rendszer",
    "SeeAllUsers": "Lásd: Összes felhasználó",
    "TakePhoto": "Fotót készíteni",
    "ChoosePhoto": "Válassz képet"
  }
}
```

## File: src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/it.json
```json
{
  "culture": "it",
  "texts": {
    "AppName": "CoreOracle",
    "Menu:Home": "Home",
    "Menu:ContactUs": "Contattaci",
    "Menu:ArticleSample": "Esempio di articolo",
    "Home": "Home",
    "Welcome": "Benvenuto",
    "LongWelcomeMessage": "Benvenuto nell'applicazione. Questo è un progetto di avvio basato sul framework ABP. Per maggiori informazioni visita",
    "Date": "Data",
    "Permission:Dashboard": "Cruscotto",
    "Menu:Dashboard": "Cruscotto",
    "Menu:HomePage": "Pagina iniziale",
    "Dashboard": "Cruscotto",
    "ExternalProvider:Google": "Google",
    "ExternalProvider:Google:ClientId": "Client ID",
    "ExternalProvider:Google:ClientSecret": "Client Secret",
    "ExternalProvider:Microsoft": "Microsoft",
    "ExternalProvider:Microsoft:ClientId": "Client ID",
    "ExternalProvider:Microsoft:ClientSecret": "Client Secret",
    "ExternalProvider:Twitter": "Twitter",
    "ExternalProvider:Twitter:ConsumerKey": "Consumer Key",
    "ExternalProvider:Twitter:ConsumerSecret": "Consumer Secret",
    "NewsletterHeader": "Iscriviti alla newsletter!",
    "NewsletterInfo": "Ottieni informazioni sugli ultimi avvenimenti.",
    "NewsletterPreference_Default": "Newsletter predefinita",
    "NewsletterPrivacyAcceptMessage": "Accetto la <a href='/privacy-policy'>Informativa sulla privacy</a>.",
    "Language": "Lingua",
    "Search": "Ricerca",
    "LoadMore": "Carica altro",
    "Settings": "Impostazioni",
    "Theme": "Tema",
    "DeviceTheme": "Tema del dispositivo",
    "Dark": "Buio",
    "Light": "Leggero",
    "Unspecified": "Sistema",
    "SeeAllUsers": "Visualizza tutti gli utenti",
    "TakePhoto": "Fare foto",
    "ChoosePhoto": "Scegli la foto"
  }
}
```

## File: src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/pt-BR.json
```json
{
  "culture": "pt-BR",
  "texts": {
    "AppName": "CoreOracle",
    "Menu:Home": "Principal",
    "Menu:ContactUs": "Entre em contato conosco",
    "Menu:ArticleSample": "Amostra de Artigo",
    "Home": "Casa",
    "Welcome": "Seja bem-vindo!",
    "LongWelcomeMessage": "Bem-vindo a esta aplicação. Este é um projeto inicial baseado no ABP framework. Para mais informações, visite",
    "Date": "Data",
    "Permission:Dashboard": "Painel",
    "Menu:Dashboard": "Painel",
    "Menu:HomePage": "Pagina inicial",
    "Dashboard": "Painel",
    "ExternalProvider:Google": "Google",
    "ExternalProvider:Google:ClientId": "ID do Cliente",
    "ExternalProvider:Google:ClientSecret": "Segredo do cliente",
    "ExternalProvider:Microsoft": "Microsoft",
    "ExternalProvider:Microsoft:ClientId": "ID do Cliente",
    "ExternalProvider:Microsoft:ClientSecret": "Segredo do cliente",
    "ExternalProvider:Twitter": "Twitter",
    "ExternalProvider:Twitter:ConsumerKey": "Chave do consumidor",
    "ExternalProvider:Twitter:ConsumerSecret": "consumidor secreto",
    "NewsletterHeader": "Assine a newsletter!",
    "NewsletterInfo": "Obtenha informações sobre os últimos acontecimentos.",
    "NewsletterPreference_Default": "Boletim informativo padrão",
    "NewsletterPrivacyAcceptMessage": "Eu aceito a <a href='/privacy-policy'> Política de Privacidade </a>.",
    "Language": "Linguagem",
    "Search": "Procurar",
    "LoadMore": "Carregue mais",
    "Settings": "Configurações",
    "Theme": "Tema",
    "DeviceTheme": "Tema do dispositivo",
    "Dark": "Escuro",
    "Light": "Luz",
    "Unspecified": "Sistema",
    "SeeAllUsers": "Ver todos os usuários",
    "TakePhoto": "Tirar fotos",
    "ChoosePhoto": "Escolher Foto"
  }
}
```

## File: src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/ru.json
```json
{
  "culture": "ru",
  "texts": {
    "AppName": "CoreOracle",
    "Menu:Home": "Дома",
    "Menu:ContactUs": "Связаться с нами",
    "Menu:ArticleSample": "Образец статьи",
    "Home": "Дома",
    "Welcome": "Добро пожаловать",
    "LongWelcomeMessage": "Добро пожаловать в приложение. Это стартап-проект, основанный на структуре ABP. Для получения дополнительной информации посетите сайт",
    "Date": "датировать",
    "Permission:Dashboard": "Панель приборов",
    "Menu:Dashboard": "Панель приборов",
    "Menu:HomePage": "Домашняя страница",
    "Dashboard": "Панель приборов",
    "ExternalProvider:Google": "Google",
    "ExternalProvider:Google:ClientId": "ID клиента",
    "ExternalProvider:Google:ClientSecret": "Секрет клиента",
    "ExternalProvider:Microsoft": "Майкрософт",
    "ExternalProvider:Microsoft:ClientId": "ID клиента",
    "ExternalProvider:Microsoft:ClientSecret": "Секрет клиента",
    "ExternalProvider:Twitter": "Твиттер",
    "ExternalProvider:Twitter:ConsumerKey": "Потребительский ключ",
    "ExternalProvider:Twitter:ConsumerSecret": "Потребительский секрет",
    "NewsletterHeader": "Подпишитесь на рассылку!",
    "NewsletterInfo": "Получайте информацию о последних событиях.",
    "NewsletterPreference_Default": "Информационный бюллетень по умолчанию",
    "NewsletterPrivacyAcceptMessage": "Я принимаю <a href='/privacy-policy'>Политику конфиденциальности</a>.",
    "Language": "Язык",
    "Search": "Поиск",
    "LoadMore": "Загрузи больше",
    "Settings": "Настройки",
    "Theme": "Тема",
    "DeviceTheme": "Тема устройства",
    "Dark": "Темный",
    "Light": "Свет",
    "Unspecified": "Система",
    "SeeAllUsers": "Просмотреть всех пользователей",
    "TakePhoto": "Сделать фотографию",
    "ChoosePhoto": "Выбрать фото"
  }
}
```

## File: src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/sk.json
```json
{
  "culture": "sk",
  "texts": {
    "AppName": "CoreOracle",
    "Menu:Home": "Domov",
    "Menu:ContactUs": "Kontaktujte nás",
    "Menu:ArticleSample": "Ukážka článku",
    "Home": "Domov",
    "Welcome": "Vitajte",
    "LongWelcomeMessage": "Vitajte v aplikácii. Toto je štartovací projekt založený na ABP frameworku. Viac informácií nájdete na stránke",
    "Date": "Dátum",
    "Permission:Dashboard": "Dashboard",
    "Menu:Dashboard": "Dashboard",
    "Menu:HomePage": "Domovská stránka",
    "Dashboard": "Dashboard",
    "ExternalProvider:Google": "Google",
    "ExternalProvider:Google:ClientId": "Client ID",
    "ExternalProvider:Google:ClientSecret": "Client Secret",
    "ExternalProvider:Microsoft": "Microsoft",
    "ExternalProvider:Microsoft:ClientId": "Client ID",
    "ExternalProvider:Microsoft:ClientSecret": "Client Secret",
    "ExternalProvider:Twitter": "Twitter",
    "ExternalProvider:Twitter:ConsumerKey": "Consumer Key",
    "ExternalProvider:Twitter:ConsumerSecret": "Consumer Secret",
    "NewsletterHeader": "Prihláste sa na odber noviniek!",
    "NewsletterInfo": "Získajte informácie o najnovšiom dianí.",
    "NewsletterPreference_Default": "Predvolený odber noviniek",
    "NewsletterPrivacyAcceptMessage": "Súhlasím so <a href='/privacy-policy'>Zásadami ochrany osobných údajov</a>.",
    "Language": "Jazyk",
    "Search": "Vyhľadávanie",
    "LoadMore": "Načítať viac",
    "Settings": "nastavenie",
    "Theme": "Téma",
    "DeviceTheme": "Téma zariadenia",
    "Dark": "Tmavý",
    "Light": "Svetlo",
    "Unspecified": "Systém",
    "SeeAllUsers": "Pozrite si Všetci používatelia",
    "TakePhoto": "Odfoť",
    "ChoosePhoto": "Vyberte fotografiu"
  }
}
```

## File: src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/sl.json
```json
{
  "culture": "sl",
  "texts": {
    "AppName": "CoreOracle",
    "Menu:Home": "Domov",
    "Menu:ContactUs": "Kontaktiraj nas",
    "Menu:ArticleSample": "Vzorec članka",
    "Home": "Domov",
    "Welcome": "Dobrodošli",
    "LongWelcomeMessage": "Dobrodošli v aplikaciji. To je osnovni projekt, ki temelji na razvojnem okolju ABP. Za več informacij obiščite",
    "Date": "datum",
    "Permission:Dashboard": "Nadzorna plošča",
    "Menu:Dashboard": "Nadzorna plošča",
    "Menu:HomePage": "Domača stran",
    "Dashboard": "Nadzorna plošča",
    "ExternalProvider:Google": "Google",
    "ExternalProvider:Google:ClientId": "ID stranke",
    "ExternalProvider:Google:ClientSecret": "Skrivnost stranke",
    "ExternalProvider:Microsoft": "Microsoft",
    "ExternalProvider:Microsoft:ClientId": "ID stranke",
    "ExternalProvider:Microsoft:ClientSecret": "Skrivnost stranke",
    "ExternalProvider:Twitter": "Twitter",
    "ExternalProvider:Twitter:ConsumerKey": "Potrošniški ključ",
    "ExternalProvider:Twitter:ConsumerSecret": "Potrošnikova skrivnost",
    "NewsletterHeader": "Naročite se na novice!",
    "NewsletterInfo": "Pridobite informacije o najnovejših dogodkih.",
    "NewsletterPreference_Default": "Privzeto glasilo",
    "NewsletterPrivacyAcceptMessage": "Sprejemam <a href='/privacy-policy'>pravilnik o zasebnosti</a>.",
    "Language": "Jezik",
    "Search": "Iskanje",
    "LoadMore": "Naloži več",
    "Settings": "nastavitve",
    "Theme": "Tema",
    "DeviceTheme": "Tema naprave",
    "Dark": "Temno",
    "Light": "Svetloba",
    "Unspecified": "Sistem",
    "SeeAllUsers": "Glejte Vsi uporabniki",
    "TakePhoto": "Posnemi fotografijo",
    "ChoosePhoto": "Izberite fotografijo"
  }
}
```

## File: src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/sv.json
```json
{
	"Culture": "sv",
	"Texts": {
	  "AppName": "CoreOracle",
	  "Menu:ContactUs": "Kontakta oss",
	  "Menu:ArticleSample": "Artikelexempel",
	  "Home": "Hem",
	  "Date": "Datum",
	  "Permission:Dashboard": "Dashboard",
	  "Menu:Dashboard": "Dashboard",
	  "Menu:HomePage": "Hemsida",
	  "Dashboard": "Dashboard",
	  "ExternalProvider:Google": "Google",
	  "ExternalProvider:Google:ClientId": "Klient-ID",
	  "ExternalProvider:Google:ClientSecret": "Klienthemlighet",
	  "ExternalProvider:Microsoft": "Microsoft",
	  "ExternalProvider:Microsoft:ClientId": "Klient-ID",
	  "ExternalProvider:Microsoft:ClientSecret": "Klienthemlighet",
	  "ExternalProvider:Twitter": "Twitter (X)",
	  "ExternalProvider:Twitter:ConsumerKey": "Konsumentnyckel",
	  "ExternalProvider:Twitter:ConsumerSecret": "Konsumenthemlighet",
	  "NewsletterHeader": "Prenumerera på nyhetsbrevet!",
	  "NewsletterInfo": "Få information om de senaste händelserna.",
	  "NewsletterPreference_Default": "Standardnyhetsbrev",
	  "NewsletterPrivacyAcceptMessage": "Jag accepterar <a href='/privacy-policy'>Sekretesspolicy</a>.",
	  "Language": "Språk",
	  "Search": "Söka",
	  "LoadMore": "Ladda mer",
	  "Settings": "Inställningar",
	  "Theme": "Tema",
	  "DeviceTheme": "Enhetstema",
	  "Dark": "Mörk",
	  "Light": "Ljus",
	  "Unspecified": "System",
	  "SeeAllUsers": "Se Alla användare",
	  "TakePhoto": "Ta foto",
	  "ChoosePhoto": "Välj Foto",
	  "Menu:Home": "Hem",
	  "LongWelcomeMessage": "Välkommen till ansökan. ",
	  "Welcome": "Välkomna"
	}
  }
```

## File: src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/tr.json
```json
{
  "culture": "tr",
  "texts": {
    "AppName": "CoreOracle",
    "Menu:Home": "Ana sayfa",
    "Menu:ContactUs": "Bize Ulaşın",
    "Menu:ArticleSample": "Yazı Örneği",
    "Home": "Ana sayfa",
    "Welcome": "Hoşgeldiniz",
    "LongWelcomeMessage": "Uygulamaya hoşgeldiniz. Bu, ABP framework'ü üzerine bina edilmiş bir başlangıç projesidir. Daha fazla bilgi için ziyaret edebilirsiniz",
    "Date": "Tarih",
    "Permission:Dashboard": "Genel görünüm",
    "Menu:Dashboard": "Genel görünüm",
    "Menu:HomePage": "ana sayfa",
    "Dashboard": "Genel görünüm",
    "ExternalProvider:Google": "Google",
    "ExternalProvider:Google:ClientId": "Client ID",
    "ExternalProvider:Google:ClientSecret": "Client Secret",
    "ExternalProvider:Microsoft": "Microsoft",
    "ExternalProvider:Microsoft:ClientId": "Client ID",
    "ExternalProvider:Microsoft:ClientSecret": "Client Secret",
    "ExternalProvider:Twitter": "Twitter",
    "ExternalProvider:Twitter:ConsumerKey": "Consumer Key",
    "ExternalProvider:Twitter:ConsumerSecret": "Consumer Secret",
    "NewsletterHeader": "Bültene abone olun!",
    "NewsletterInfo": "Son gelişmeler hakkında bilgi alın.",
    "NewsletterPreference_Default": "Varsayılan Bülten",
    "NewsletterPrivacyAcceptMessage": "<a href='/privacy-policy'>Gizlilik Politikası</a>'nı kabul ediyorum.",
    "Language": "Dil",
    "Search": "Ara",
    "LoadMore": "Daha fazla yükle",
    "Settings": "Ayarlar",
    "Theme": "Tema",
    "DeviceTheme": "Cihaz Teması",
    "Dark": "Koyu",
    "Light": "Açık",
    "Unspecified": "Sistem",
    "SeeAllUsers": "Tüm Kullanıcıları Gör",
    "TakePhoto": "Fotoğraf Çek",
    "ChoosePhoto": "Fotoğraf Seç"
  }
}
```

## File: src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracle/zh-Hans.json
```json
{
  "culture": "zh-Hans",
  "texts": {
    "AppName": "CoreOracle",
    "Menu:Home": "首页",
    "Menu:ContactUs": "联系我们",
    "Menu:ArticleSample": "文章示例",
    "Home": "首页",
    "Welcome": "欢迎",
    "LongWelcomeMessage": "欢迎使用本应用程序。这是一个基于 ABP 框架的启动项目。欲了解更多信息，请访问",
    "Date": "日期",
    "Permission:Dashboard": "仪表板",
    "Menu:Dashboard": "仪表板",
    "Menu:HomePage": "主页",
    "Dashboard": "仪表板",
    "ExternalProvider:Google": "Google",
    "ExternalProvider:Google:ClientId": "Client Id",
    "ExternalProvider:Google:ClientSecret": "Client Secret",
    "ExternalProvider:Microsoft": "Microsoft",
    "ExternalProvider:Microsoft:ClientId": "Client Id",
    "ExternalProvider:Microsoft:ClientSecret": "Client Secret",
    "ExternalProvider:Twitter": "Twitter",
    "ExternalProvider:Twitter:ConsumerKey": "Consumer Key",
    "ExternalProvider:Twitter:ConsumerSecret": "Consumer Secret",
    "NewsletterHeader": "订阅时事通讯！",
    "NewsletterInfo": "了解最新动态。",
    "NewsletterPreference_Default": "默认通讯",
    "NewsletterPrivacyAcceptMessage": "我接受<a href='/privacy-policy'>隐私政策</a>。",
    "Language": "语言",
    "Search": "搜索",
    "LoadMore": "加载更多",
    "Settings": "设置",
    "Theme": "主题",
    "DeviceTheme": "设备主题",
    "Dark": "Dark",
    "Light": "Light",
    "Unspecified": "System",
    "SeeAllUsers": "查看所有用户",
    "TakePhoto": "拍摄照片",
    "ChoosePhoto": "选择照片"
  }
}
```

## File: src/Aqt.CoreOracle.Domain.Shared/Localization/CoreOracleResource.cs
```csharp
public class CoreOracleResource
```

## File: src/Aqt.CoreOracle.Domain.Shared/MultiTenancy/MultiTenancyConsts.cs
```csharp
public static class MultiTenancyConsts
```

## File: src/Aqt.CoreOracle.Domain.Shared/Aqt.CoreOracle.Domain.Shared.csproj
```
<Project Sdk="Microsoft.NET.Sdk">

  <Import Project="..\..\common.props" />

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <RootNamespace>Aqt.CoreOracle</RootNamespace>
    <GenerateEmbeddedFilesManifest>true</GenerateEmbeddedFilesManifest>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Volo.Abp.BackgroundJobs.Domain.Shared" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.AuditLogging.Domain.Shared" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.FeatureManagement.Domain.Shared" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.PermissionManagement.Domain.Shared" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.SettingManagement.Domain.Shared" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.BlobStoring.Database.Domain.Shared" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.GlobalFeatures" Version="9.1.0" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Volo.Abp.OpenIddict.Domain.Shared" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.Identity.Domain.Shared" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.TenantManagement.Domain.Shared" Version="9.1.0" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.FileProviders.Embedded" Version="9.0.0" />
  </ItemGroup>

  <ItemGroup>
    <EmbeddedResource Include="Localization\CoreOracle\*.json" />
    <Content Remove="Localization\CoreOracle\*.json" />
  </ItemGroup>

</Project>
```

## File: src/Aqt.CoreOracle.Domain.Shared/CoreOracleDomainErrorCodes.cs
```csharp
public static class CoreOracleDomainErrorCodes
```

## File: src/Aqt.CoreOracle.Domain.Shared/CoreOracleDomainSharedModule.cs
```csharp
public class CoreOracleDomainSharedModule : AbpModule
⋮----
public override void PreConfigureServices(ServiceConfigurationContext context)
⋮----
CoreOracleGlobalFeatureConfigurator.Configure();
CoreOracleModuleExtensionConfigurator.Configure();
⋮----
public override void ConfigureServices(ServiceConfigurationContext context)
⋮----
.AddBaseTypes(typeof(AbpValidationResource))
.AddVirtualJson("/Localization/CoreOracle");
⋮----
options.MapCodeNamespace("CoreOracle", typeof(CoreOracleResource));
```

## File: src/Aqt.CoreOracle.Domain.Shared/CoreOracleGlobalFeatureConfigurator.cs
```csharp
public static class CoreOracleGlobalFeatureConfigurator
⋮----
private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();
public static void Configure()
⋮----
OneTimeRunner.Run(() =>
```

## File: src/Aqt.CoreOracle.Domain.Shared/CoreOracleModuleExtensionConfigurator.cs
```csharp
public static class CoreOracleModuleExtensionConfigurator
⋮----
private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();
public static void Configure()
⋮----
OneTimeRunner.Run(() =>
⋮----
private static void ConfigureExistingProperties()
⋮----
private static void ConfigureExtraProperties()
```

## File: src/Aqt.CoreOracle.EntityFrameworkCore/EntityFrameworkCore/CoreOracleDbContext.cs
```csharp
public class CoreOracleDbContext :
AbpDbContext<CoreOracleDbContext>,
ITenantManagementDbContext,
IIdentityDbContext
⋮----
protected override void OnModelCreating(ModelBuilder builder)
⋮----
base.OnModelCreating(builder);
builder.ConfigurePermissionManagement();
builder.ConfigureSettingManagement();
builder.ConfigureBackgroundJobs();
builder.ConfigureAuditLogging();
builder.ConfigureFeatureManagement();
builder.ConfigureIdentity();
builder.ConfigureOpenIddict();
builder.ConfigureTenantManagement();
builder.ConfigureBlobStoring();
⋮----
b.Property(q => q.Payload).HasColumnType("clob").HasMaxLength(4000);
⋮----
b.Property(x => x.Exceptions).HasColumnType("clob").HasMaxLength(4000);
⋮----
b.Property(x => x.Parameters).HasColumnType("clob").HasMaxLength(4000);
```

## File: src/Aqt.CoreOracle.EntityFrameworkCore/EntityFrameworkCore/CoreOracleDbContextFactory.cs
```csharp
public class CoreOracleDbContextFactory : IDesignTimeDbContextFactory<CoreOracleDbContext>
⋮----
public CoreOracleDbContext CreateDbContext(string[] args)
⋮----
CoreOracleEfCoreEntityExtensionMappings.Configure();
⋮----
.UseOracle(configuration.GetConnectionString("Default"), opt => opt.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion23));
return new CoreOracleDbContext(builder.Options);
⋮----
private static IConfigurationRoot BuildConfiguration()
⋮----
var builder = new ConfigurationBuilder()
.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Aqt.CoreOracle.DbMigrator/"))
.AddJsonFile("appsettings.json", optional: false);
return builder.Build();
```

## File: src/Aqt.CoreOracle.EntityFrameworkCore/EntityFrameworkCore/CoreOracleEfCoreEntityExtensionMappings.cs
```csharp
public static class CoreOracleEfCoreEntityExtensionMappings
⋮----
private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();
public static void Configure()
⋮----
CoreOracleGlobalFeatureConfigurator.Configure();
CoreOracleModuleExtensionConfigurator.Configure();
OneTimeRunner.Run(() =>
```

## File: src/Aqt.CoreOracle.EntityFrameworkCore/EntityFrameworkCore/CoreOracleEntityFrameworkCoreModule.cs
```csharp
public class CoreOracleEntityFrameworkCoreModule : AbpModule
⋮----
public override void PreConfigureServices(ServiceConfigurationContext context)
⋮----
CoreOracleEfCoreEntityExtensionMappings.Configure();
⋮----
public override void ConfigureServices(ServiceConfigurationContext context)
⋮----
options.AddDefaultRepositories(includeAllEntities: true);
⋮----
options.UseOracle();
```

## File: src/Aqt.CoreOracle.EntityFrameworkCore/EntityFrameworkCore/EntityFrameworkCoreCoreOracleDbSchemaMigrator.cs
```csharp
public class EntityFrameworkCoreCoreOracleDbSchemaMigrator
: ICoreOracleDbSchemaMigrator, ITransientDependency
⋮----
private readonly IServiceProvider _serviceProvider;
⋮----
public async Task MigrateAsync()
⋮----
.MigrateAsync();
```

## File: src/Aqt.CoreOracle.EntityFrameworkCore/Properties/AssemblyInfo.cs
```csharp

```

## File: src/Aqt.CoreOracle.EntityFrameworkCore/Aqt.CoreOracle.EntityFrameworkCore.csproj
```
<Project Sdk="Microsoft.NET.Sdk">

  <Import Project="..\..\common.props" />

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <RootNamespace>Aqt.CoreOracle</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Aqt.CoreOracle.Domain\Aqt.CoreOracle.Domain.csproj" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Volo.Abp.EntityFrameworkCore.Oracle" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.PermissionManagement.EntityFrameworkCore" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.SettingManagement.EntityFrameworkCore" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.BackgroundJobs.EntityFrameworkCore" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.AuditLogging.EntityFrameworkCore" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.FeatureManagement.EntityFrameworkCore" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.BlobStoring.Database.EntityFrameworkCore" Version="9.1.0" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Volo.Abp.OpenIddict.EntityFrameworkCore" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.Identity.EntityFrameworkCore" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.TenantManagement.EntityFrameworkCore" Version="9.1.0" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.0">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
      <PrivateAssets>compile; contentFiles; build; buildMultitargeting; buildTransitive; analyzers; native</PrivateAssets>
    </PackageReference>
  </ItemGroup>

</Project>
```

## File: src/Aqt.CoreOracle.HttpApi/Controllers/CoreOracleController.cs
```csharp
public abstract class CoreOracleController : AbpControllerBase
```

## File: src/Aqt.CoreOracle.HttpApi/Models/Test/TestModel.cs
```csharp
public class TestModel
```

## File: src/Aqt.CoreOracle.HttpApi/Aqt.CoreOracle.HttpApi.csproj
```
<Project Sdk="Microsoft.NET.Sdk">

  <Import Project="..\..\common.props" />

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <RootNamespace>Aqt.CoreOracle</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Aqt.CoreOracle.Application.Contracts\Aqt.CoreOracle.Application.Contracts.csproj" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Volo.Abp.PermissionManagement.HttpApi" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.FeatureManagement.HttpApi" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.SettingManagement.HttpApi" Version="9.1.0" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Volo.Abp.Identity.HttpApi" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.Account.HttpApi" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.TenantManagement.HttpApi" Version="9.1.0" />
  </ItemGroup>

</Project>
```

## File: src/Aqt.CoreOracle.HttpApi/CoreOracleHttpApiModule.cs
```csharp
public class CoreOracleHttpApiModule : AbpModule
⋮----
public override void ConfigureServices(ServiceConfigurationContext context)
⋮----
private void ConfigureLocalization()
⋮----
.AddBaseTypes(
```

## File: src/Aqt.CoreOracle.HttpApi.Client/Aqt.CoreOracle.HttpApi.Client.csproj
```
<Project Sdk="Microsoft.NET.Sdk">

  <Import Project="..\..\common.props" />

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <RootNamespace>Aqt.CoreOracle</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Aqt.CoreOracle.Application.Contracts\Aqt.CoreOracle.Application.Contracts.csproj" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Volo.Abp.PermissionManagement.HttpApi.Client" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.FeatureManagement.HttpApi.Client" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.SettingManagement.HttpApi.Client" Version="9.1.0" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Volo.Abp.Identity.HttpApi.Client" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.Account.HttpApi.Client" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.TenantManagement.HttpApi.Client" Version="9.1.0" />
  </ItemGroup>

  <ItemGroup>
    <EmbeddedResource Include="**\*generate-proxy.json" />
    <Content Remove="**\*generate-proxy.json" />
  </ItemGroup>

</Project>
```

## File: src/Aqt.CoreOracle.HttpApi.Client/CoreOracleHttpApiClientModule.cs
```csharp
public class CoreOracleHttpApiClientModule : AbpModule
⋮----
public override void ConfigureServices(ServiceConfigurationContext context)
⋮----
context.Services.AddHttpClientProxies(
```

## File: src/Aqt.CoreOracle.Web/Components/_ViewImports.cshtml
```
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, Volo.Abp.AspNetCore.Mvc.UI
@addTagHelper *, Volo.Abp.AspNetCore.Mvc.UI.Bootstrap
@addTagHelper *, Volo.Abp.AspNetCore.Mvc.UI.Bundling
```

## File: src/Aqt.CoreOracle.Web/HealthChecks/CoreOracleDatabaseCheck.cs
```csharp
public class CoreOracleDatabaseCheck : IHealthCheck, ITransientDependency
⋮----
protected readonly IIdentityRoleRepository IdentityRoleRepository;
⋮----
public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
⋮----
await IdentityRoleRepository.GetListAsync(sorting: nameof(IdentityRole.Id), maxResultCount: 1, cancellationToken: cancellationToken);
return HealthCheckResult.Healthy($"Could connect to database and get record.");
⋮----
return HealthCheckResult.Unhealthy($"Error when trying to get database record. ", e);
```

## File: src/Aqt.CoreOracle.Web/HealthChecks/HealthChecksBuilderExtensions.cs
```csharp
public static class HealthChecksBuilderExtensions
⋮----
public static void AddCoreOracleHealthChecks(this IServiceCollection services)
⋮----
var healthChecksBuilder = services.AddHealthChecks();
⋮----
services.ConfigureHealthCheckEndpoint("/health-status");
var configuration = services.GetConfiguration();
⋮----
if (string.IsNullOrEmpty(healthCheckUrl))
⋮----
var healthChecksUiBuilder = services.AddHealthChecksUI(settings =>
⋮----
settings.AddHealthCheckEndpoint("CoreOracle Health Status", healthCheckUrl);
⋮----
healthChecksUiBuilder.AddInMemoryStorage();
services.MapHealthChecksUiEndpoints(options =>
⋮----
private static IServiceCollection ConfigureHealthCheckEndpoint(this IServiceCollection services, string path)
⋮----
options.EndpointConfigureActions.Add(endpointContext =>
⋮----
endpointContext.Endpoints.MapHealthChecks(
new PathString(path.EnsureStartsWith('/')),
new HealthCheckOptions
⋮----
private static IServiceCollection MapHealthChecksUiEndpoints(this IServiceCollection services, Action<global::HealthChecks.UI.Configuration.Options>? setupOption = null)
⋮----
routerOptions.EndpointConfigureActions.Add(endpointContext =>
⋮----
endpointContext.Endpoints.MapHealthChecksUI(setupOption);
```

## File: src/Aqt.CoreOracle.Web/Menus/CoreOracleMenuContributor.cs
```csharp
public class CoreOracleMenuContributor : IMenuContributor
⋮----
public async Task ConfigureMenuAsync(MenuConfigurationContext context)
⋮----
private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
⋮----
context.Menu.AddItem(
new ApplicationMenuItem(
⋮----
var administration = context.Menu.GetAdministration();
⋮----
administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);
⋮----
administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
⋮----
administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
⋮----
administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);
administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 7);
```

## File: src/Aqt.CoreOracle.Web/Menus/CoreOracleMenus.cs
```csharp
public class CoreOracleMenus
```

## File: src/Aqt.CoreOracle.Web/Menus/CoreOracleToolbarContributor.cs
```csharp
public class CoreOracleToolbarContributor : IToolbarContributor
⋮----
public virtual Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
```

## File: src/Aqt.CoreOracle.Web/Pages/_ViewImports.cshtml
```
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, Volo.Abp.AspNetCore.Mvc.UI
@addTagHelper *, Volo.Abp.AspNetCore.Mvc.UI.Bootstrap
@addTagHelper *, Volo.Abp.AspNetCore.Mvc.UI.Bundling
@addTagHelper *, Owl.reCAPTCHA
```

## File: src/Aqt.CoreOracle.Web/Pages/CoreOraclePageModel.cs
```csharp
public abstract class CoreOraclePageModel : AbpPageModel
```

## File: src/Aqt.CoreOracle.Web/Pages/Index.cshtml
```
@page
@model Aqt.CoreOracle.Web.Pages.IndexModel
@using Aqt.CoreOracle.Web.Menus
@using Volo.Abp.AspNetCore.Mvc.UI.Layout
@using Microsoft.AspNetCore.Mvc.Localization
@using Aqt.CoreOracle.Localization
@using Volo.Abp.Users
@inject IPageLayout PageLayout
@inject IHtmlLocalizer<CoreOracleResource> L
@inject ICurrentUser CurrentUser
@{
    ViewBag.PageTitle = L["Home"];
    PageLayout.Content.MenuItemName = CoreOracleMenus.Home;
}

@section styles {
    <abp-style src="/Pages/Index.css" />
}

<div class="row mb-3">
    <div class="col-xl-6 col-12 d-flex">
        <div class="card h-lg-100 w-100 overflow-hidden">
            <div class="card-body">
                <div class="starting-content pe-5">
                    @if (!CurrentUser.IsAuthenticated)
                    {
                        <h3>@L["Welcome"]</h3>
                        <p class="lead text-muted mb-2">@L["LongWelcomeMessage"]</p>
                        <a href="/Account/Login" class="btn btn-primary mb-4"><i class="fa-solid fa-right-to-bracket me-1"></i><span>@L["Login"]</span></a>
                    }

                    <h4>Getting Started</h4>
                    <p class="text-muted mb-2">Learn how to create and run
                        a
                        new web application using the application startup
                        template.</p>

                    <a href="https://abp.io/docs/latest/getting-started" class="btn  btn-brand  mb-2" target="_blank">Getting Started</a>

                    <h4>Web Application Development Tutorial</h4>
                    <p class="text-muted mb-2">Learn how to build an ABP based
                        web
                        application named Acme.BookStore.</p>
                    <a href="https://abp.io/docs/latest/tutorials/book-store/part-1?UI=MVC&DB=EF" class="btn btn-primary soft mb-2" target="_blank">Explore Tutorial</a>

                    <h4>Customize Lepton Theme</h4>
                    <p class="text-muted mb-2">Learn how to customize LeptonX
                        Theme
                        as you wish.</p>
                    <a href="https://abp.io/docs/commercial/latest/themes/lepton-x/index" class="btn btn-primary soft mb-5 mb-xl-0" target="_blank">Customize Lepton</a>
                </div>
                <img class="card-bg-image" src="/images/getting-started/bg-01.png">
            </div>
        </div>
    </div>
    <div class="col-xl-3 col-md-6 d-flex">
        <div class="row">
            <div class="col-12 d-flex">
                <div class="card overflow-hidden mb-3">
                    <div class="card-body d-flex flex-column">
                        <div class="abp-support abp-logo mb-2"></div>
                        <p class="text-muted mb-2">You can check for
                            similar problems and solutions, or open a
                            new topic to discuss your specific issue.</p>
                        <a href="https://abp.io/support/questions" class="btn btn-brand soft" target="_blank">Visit Support</a>
                        <img style="margin-bottom: -24px;" class="w-100 mt-auto" src="/images/getting-started/img-support.png">
                    </div>
                </div>
            </div>
            <div class="col-12 d-flex">
                <div class="card h-md-100 overflow-hidden">
                    <div class="card-body d-flex flex-column">
                        <div class="abp-blog abp-logo mb-2"></div>
                        <p class="text-muted mb-2">You can find content
                            on .NET development, cross-platform, ASP.NET
                            application templates, ABP-related news, and
                            more.</p>
                        <a href="https://abp.io/blog" class="btn  btn-brand soft" target="_blank">Visit Blog</a>

                        <img style="margin-bottom: -24px;" class="w-100 mt-auto" src="/images/getting-started/img-blog.png">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-xl-3 col-md-6 d-flex">
        <div class="card h-100 overflow-hidden">
            <div class="card-body">
                <div class="abp-community abp-logo mb-2"></div>
                <p class="lead text-muted">A unique community platform
                    for <span class="fw-bold">ABP Lovers!</span></p>
                <p class="text-muted mb-2">Explore all ABP users'
                    experiences with the ABP Framework, discover
                    articles and videos on how to use ABP, and join
                    raffles for a chance to win surprise gifts!</p>
                <a class="btn btn-brand soft mb-3" href="https://abp.io/community/" target="_blank">Join ABP
                    Community</a>
            </div>
            <img class="mt-3 w-100" src="/images/getting-started/img-community.png">
        </div>
    </div>
</div>

<div class="row">
    <div class="col-xl-3 col-lg-4">
        <div class="card">
            <div class="card-header">
                <div class="card-title text-body">More from ABP.IO</div>
            </div>
            <div class="card-body pt-0">

                <div>
                    <table class="table mb-0">
                        <tbody>
                            <tr>
                                <td class="ps-0 d-flex">
                                    <div class="rounded-circle me-2
                                        flex-center bg-brand soft" style="width: 20px; height:
                                        20px;"><i class="bi bi-check fs-4"></i></div>
                                    <div> Latest Release Logs </div>
                                </td>
                                <td class="text-end pe-0 opacity-25">
                                    <a href="https://github.com/abpframework/abp/releases" target="_blank">
                                        <i class="bi
                                            bi-box-arrow-up-right
                                            text-dark"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class="ps-0 d-flex">
                                    <div class="rounded-circle me-2
                                        flex-center bg-brand soft" style="width: 20px; height:
                                        20px;"><i class="bi bi-check fs-4"></i></div>
                                    <div> Video Courses </div>
                                </td>
                                <td class="text-end pe-0 opacity-25">
                                    <a href="https://abp.io/video-courses/essentials" target="_blank">
                                        <i class="bi
                                            bi-box-arrow-up-right
                                            text-dark"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class="ps-0 d-flex">
                                    <div class="rounded-circle me-2
                                        flex-center bg-brand soft" style="width: 20px; height:
                                        20px;"><i class="bi bi-check fs-4"></i></div>
                                    <div> Samples </div>
                                </td>
                                <td class="text-end pe-0 opacity-25">
                                    <a href="https://abp.io/docs/latest/Samples/Index" target="_blank">
                                        <i class="bi
                                            bi-box-arrow-up-right
                                            text-dark"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class="ps-0 d-flex">
                                    <div class="rounded-circle me-2
                                        flex-center bg-brand soft" style="width: 20px; height:
                                        20px;"><i class="bi bi-check fs-4"></i></div>
                                    <div> Books </div>
                                </td>
                                <td class="text-end pe-0 opacity-25">
                                    <a href="https://abp.io/books" target="_blank">
                                        <i class="bi
                                            bi-box-arrow-up-right
                                            text-dark"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class="ps-0 d-flex border-bottom-0">
                                    <div class="rounded-circle me-2
                                        flex-center bg-brand soft" style="width: 20px; height:
                                        20px;"><i class="bi bi-check fs-4"></i></div>
                                    <div> FAQ </div>
                                </td>
                                <td class="text-end pe-0 opacity-25
                                    border-bottom-0">
                                    <a href="https://abp.io/faq" target="_blank">
                                        <i class="bi
                                            bi-box-arrow-up-right
                                            text-dark"></i>
                                    </a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div class="col-xl-6 col-lg-8 d-flex">
        <div class="card">
            <div class="card-body d-flex">
                <div class="row align-items-center">
                    <div class="col-md-8">
                        <p class="text-muted mb-0">THE OFFICIAL GUIDE</p>
                        <h3>Mastering ABP Framework</h3>
                        <p class="text-muted">Written by the creator of the ABP
                            Framework, this book will help you gain a complete
                            understanding of the framework
                            and modern web application development techniques.</p>
                        <div class="d-md-flex mb-2 mb-md-0">
                            <a href="https://www.amazon.com/gp/product/B097Z2DM8Q" target="_blank" class="btn btn-primary soft
                                me-md-2 mb-2 mb-md-0">
                                Buy on Amazon US
                            </a>
                            <a href="https://www.packtpub.com/product/mastering-abp-framework/9781801079242" target="_blank" class="btn btn-primary soft mb-2
                                mb-md-0">
                                Buy on PACKT
                            </a>
                        </div>
                    </div>
                    <div class="col">
                        <img class="w-100" src="/images/getting-started/book.png">
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div class="col-xl-3 col-lg-4">
        <div class="card">
            <div class="card-header">
                <div class="card-title text-body">Follow us on Social Media</div>
            </div>
            <div class="card-body pt-0">
                <div>
                    <table class="table mb-0">
                        <tbody>
                            <tr>
                                <td class="ps-0 d-flex">
                                    <img src="/images/getting-started/x-white.svg" class="me-2" style="background-color: black;">
                                    <div>X.com</div>
                                </td>
                                <td class="text-end pe-0 opacity-25">
                                    <a href="https://twitter.com/abpframework" target="_blank">
                                        <i class="bi bi-box-arrow-up-right
                                            text-dark"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class="ps-0 d-flex">
                                    <img src="/images/getting-started/discord.svg" class="me-2">
                                    <div>Discord</div>
                                </td>
                                <td class="text-end pe-0 opacity-25">
                                    <a href="https://abp.io/community/discord" target="_blank">
                                        <i class="bi bi-box-arrow-up-right
                                            text-dark"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class="ps-0 d-flex">
                                    <img src="/images/getting-started/stack-overflow.svg" class="me-2">
                                    <div>Stack Overflow</div>
                                </td>
                                <td class="text-end pe-0 opacity-25">
                                    <a href="https://stackoverflow.com/questions/tagged/abp" target="_blank">
                                        <i class="bi bi-box-arrow-up-right
                                            text-dark"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class="ps-0 d-flex">
                                    <img src="/images/getting-started/youtube.svg" class="me-2">
                                    <div>YouTube</div>
                                </td>
                                <td class="text-end pe-0 opacity-25">
                                    <a href="https://www.youtube.com/@@Volosoft" target="_blank">
                                        <i class="bi bi-box-arrow-up-right
                                            text-dark"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class="ps-0 d-flex border-bottom-0">
                                    <img src="/images/getting-started/instagram.svg" class="me-2">
                                    <div>Instagram</div>
                                </td>
                                <td class="text-end pe-0 opacity-25
                                    border-bottom-0">
                                    <a href="https://www.instagram.com/abpframework/" target="_blank">
                                        <i class="bi bi-box-arrow-up-right
                                            text-dark"></i>
                                    </a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>
```

## File: src/Aqt.CoreOracle.Web/Pages/Index.cshtml.cs
```csharp
public class IndexModel : CoreOraclePageModel
```

## File: src/Aqt.CoreOracle.Web/Pages/Index.css
```css
.starting-content {
.card-bg-image {
```

## File: src/Aqt.CoreOracle.Web/Properties/AssemblyInfo.cs
```csharp

```

## File: src/Aqt.CoreOracle.Web/Properties/launchSettings.json
```json
{
  "iisSettings": {
    "windowsAuthentication": false,
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "https://localhost:44388/",
      "sslPort": 44388
    }
  },
  "profiles": {
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "Aqt.CoreOracle.Web": {
      "commandName": "Project",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "applicationUrl": "https://localhost:44388/"
    }
  }
}
```

## File: src/Aqt.CoreOracle.Web/Views/_ViewImports.cshtml
```
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, Volo.Abp.AspNetCore.Mvc.UI
@addTagHelper *, Volo.Abp.AspNetCore.Mvc.UI.Bootstrap
@addTagHelper *, Volo.Abp.AspNetCore.Mvc.UI.Bundling
```

## File: src/Aqt.CoreOracle.Web/wwwroot/images/getting-started/discord.svg
```
<svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
<g id="Frame 1416">
<g id="Group">
<path id="Vector" d="M16.9419 3.29661C15.6473 2.69088 14.263 2.25066 12.8157 2C12.638 2.32134 12.4304 2.75355 12.2872 3.09738C10.7487 2.86601 9.22445 2.86601 7.7143 3.09738C7.57116 2.75355 7.3588 2.32134 7.17947 2C5.73067 2.25066 4.3448 2.6925 3.05016 3.29982C0.438869 7.24582 -0.269009 11.0938 0.0849305 14.8872C1.81688 16.1805 3.49534 16.9662 5.14548 17.4804C5.55291 16.9196 5.91628 16.3235 6.22931 15.6953C5.63313 15.4688 5.06211 15.1892 4.52256 14.8647C4.6657 14.7586 4.80571 14.6478 4.94098 14.5337C8.23183 16.0729 11.8074 16.0729 15.0589 14.5337C15.1958 14.6478 15.3358 14.7586 15.4774 14.8647C14.9362 15.1908 14.3637 15.4704 13.7675 15.697C14.0805 16.3235 14.4423 16.9212 14.8513 17.4819C16.503 16.9678 18.183 16.1822 19.915 14.8872C20.3303 10.4897 19.2056 6.67705 16.9419 3.29661ZM6.67765 12.5543C5.68977 12.5543 4.87963 11.632 4.87963 10.509C4.87963 9.38591 5.67247 8.46208 6.67765 8.46208C7.68285 8.46208 8.49297 9.38429 8.47567 10.509C8.47723 11.632 7.68285 12.5543 6.67765 12.5543ZM13.3223 12.5543C12.3344 12.5543 11.5243 11.632 11.5243 10.509C11.5243 9.38591 12.3171 8.46208 13.3223 8.46208C14.3275 8.46208 15.1376 9.38429 15.1203 10.509C15.1203 11.632 14.3275 12.5543 13.3223 12.5543Z" fill="#5865F2"/>
</g>
</g>
</svg>
```

## File: src/Aqt.CoreOracle.Web/wwwroot/images/getting-started/instagram.svg
```
<svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
<g clip-path="url(#clip0_425_4536)">
<path d="M10.014 -0.00549316C5.8391 -0.00549316 4.6181 -0.00118628 4.38076 0.0185023C3.524 0.0897197 2.99087 0.224617 2.41005 0.513794C1.96244 0.73606 1.60943 0.993704 1.26104 1.35487C0.626541 2.01351 0.241998 2.82382 0.102793 3.78702C0.0351137 4.25463 0.0154251 4.35 0.0114258 6.73847C0.00988765 7.53463 0.0114258 8.58243 0.0114258 9.98786C0.0114258 14.1594 0.0160403 15.3792 0.0360366 15.616C0.105254 16.4497 0.235999 16.9742 0.51287 17.548C1.042 18.6462 2.05258 19.4707 3.24313 19.7783C3.65536 19.8845 4.11066 19.9429 4.69516 19.9706C4.94281 19.9814 7.46695 19.9891 9.99263 19.9891C12.5183 19.9891 15.044 19.986 15.2855 19.9737C15.9623 19.9418 16.3553 19.8891 16.7898 19.7768C17.988 19.4676 18.9802 18.6555 19.5201 17.5418C19.7916 16.9819 19.9292 16.4374 19.9915 15.6473C20.0051 15.475 20.0107 12.7283 20.0107 9.98524C20.0107 7.24176 20.0046 4.50012 19.9911 4.32785C19.928 3.52492 19.7903 2.98502 19.5101 2.41436C19.2801 1.94722 19.0248 1.59836 18.6541 1.24166C17.9925 0.609776 17.1834 0.225233 16.2193 0.0861819C15.7522 0.0186561 15.6591 -0.0013401 13.2688 -0.00549316H10.014Z" fill="url(#paint0_radial_425_4536)"/>
<path d="M10.014 -0.00549316C5.8391 -0.00549316 4.6181 -0.00118628 4.38076 0.0185023C3.524 0.0897197 2.99087 0.224617 2.41005 0.513794C1.96244 0.73606 1.60943 0.993704 1.26104 1.35487C0.626541 2.01351 0.241998 2.82382 0.102793 3.78702C0.0351137 4.25463 0.0154251 4.35 0.0114258 6.73847C0.00988765 7.53463 0.0114258 8.58243 0.0114258 9.98786C0.0114258 14.1594 0.0160403 15.3792 0.0360366 15.616C0.105254 16.4497 0.235999 16.9742 0.51287 17.548C1.042 18.6462 2.05258 19.4707 3.24313 19.7783C3.65536 19.8845 4.11066 19.9429 4.69516 19.9706C4.94281 19.9814 7.46695 19.9891 9.99263 19.9891C12.5183 19.9891 15.044 19.986 15.2855 19.9737C15.9623 19.9418 16.3553 19.8891 16.7898 19.7768C17.988 19.4676 18.9802 18.6555 19.5201 17.5418C19.7916 16.9819 19.9292 16.4374 19.9915 15.6473C20.0051 15.475 20.0107 12.7283 20.0107 9.98524C20.0107 7.24176 20.0046 4.50012 19.9911 4.32785C19.928 3.52492 19.7903 2.98502 19.5101 2.41436C19.2801 1.94722 19.0248 1.59836 18.6541 1.24166C17.9925 0.609776 17.1834 0.225233 16.2193 0.0861819C15.7522 0.0186561 15.6591 -0.0013401 13.2688 -0.00549316H10.014Z" fill="url(#paint1_radial_425_4536)"/>
<path d="M10.0093 2.60938C8.00417 2.60938 7.75252 2.61814 6.96498 2.65398C6.17897 2.68998 5.64246 2.81441 5.17301 2.99699C4.68741 3.18557 4.27549 3.43783 3.8651 3.84837C3.45441 4.25876 3.20215 4.67068 3.01295 5.15613C2.82991 5.62573 2.70532 6.1624 2.66994 6.9481C2.63472 7.73564 2.62549 7.98744 2.62549 9.9926C2.62549 11.9978 2.63441 12.2486 2.6701 13.0362C2.70624 13.8222 2.83068 14.3587 3.01311 14.8282C3.20184 15.3138 3.4541 15.7257 3.86464 16.1361C4.27487 16.5468 4.68679 16.7996 5.17209 16.9882C5.64184 17.1708 6.17851 17.2952 6.96436 17.3312C7.75191 17.3671 8.0034 17.3758 10.0084 17.3758C12.0137 17.3758 12.2646 17.3671 13.0521 17.3312C13.8381 17.2952 14.3753 17.1708 14.845 16.9882C15.3305 16.7996 15.7418 16.5468 16.152 16.1361C16.5627 15.7257 16.815 15.3138 17.0042 14.8283C17.1857 14.3587 17.3103 13.822 17.3472 13.0363C17.3826 12.2488 17.3918 11.9978 17.3918 9.9926C17.3918 7.98744 17.3826 7.7358 17.3472 6.94825C17.3103 6.16225 17.1857 5.62573 17.0042 5.15628C16.815 4.67068 16.5627 4.25876 16.152 3.84837C15.7413 3.43768 15.3306 3.18542 14.8446 2.99699C14.3739 2.81441 13.8371 2.68998 13.0511 2.65398C12.2635 2.61814 12.0128 2.60938 10.007 2.60938H10.0093ZM9.34699 3.93989C9.54357 3.93959 9.76291 3.93989 10.0093 3.93989C11.9807 3.93989 12.2143 3.94697 12.9928 3.98235C13.7126 4.01526 14.1033 4.13555 14.3636 4.23661C14.7081 4.37043 14.9538 4.5304 15.212 4.78881C15.4705 5.04722 15.6304 5.29333 15.7646 5.63788C15.8656 5.89783 15.9861 6.28853 16.0188 7.00839C16.0542 7.78671 16.0619 8.02051 16.0619 9.99091C16.0619 11.9613 16.0542 12.1951 16.0188 12.9734C15.9859 13.6933 15.8656 14.084 15.7646 14.3439C15.6307 14.6885 15.4705 14.9338 15.212 15.1921C14.9536 15.4505 14.7083 15.6105 14.3636 15.7443C14.1036 15.8458 13.7126 15.9658 12.9928 15.9987C12.2145 16.0341 11.9807 16.0418 10.0093 16.0418C8.03785 16.0418 7.80421 16.0341 7.02589 15.9987C6.30603 15.9655 5.91533 15.8452 5.65492 15.7441C5.31037 15.6103 5.06426 15.4503 4.80585 15.1919C4.54743 14.9335 4.38746 14.688 4.25334 14.3433C4.15228 14.0834 4.03184 13.6927 3.99908 12.9728C3.9637 12.1945 3.95662 11.9607 3.95662 9.98906C3.95662 8.01744 3.9637 7.78486 3.99908 7.00655C4.03199 6.28668 4.15228 5.89599 4.25334 5.63573C4.38716 5.29118 4.54743 5.04507 4.80585 4.78666C5.06426 4.52824 5.31037 4.36828 5.65492 4.23415C5.91518 4.13263 6.30603 4.01265 7.02589 3.97958C7.70699 3.94882 7.97094 3.93959 9.34699 3.93805V3.93989ZM13.9504 5.16582C13.4613 5.16582 13.0644 5.5622 13.0644 6.0515C13.0644 6.54064 13.4613 6.93748 13.9504 6.93748C14.4396 6.93748 14.8364 6.54064 14.8364 6.0515C14.8364 5.56236 14.4396 5.16551 13.9504 5.16551V5.16582ZM10.0093 6.20101C7.91542 6.20101 6.21774 7.89869 6.21774 9.9926C6.21774 12.0865 7.91542 13.7834 10.0093 13.7834C12.1032 13.7834 13.8003 12.0865 13.8003 9.9926C13.8003 7.89869 12.1032 6.20101 10.0093 6.20101ZM10.0093 7.53153C11.3685 7.53153 12.4704 8.63332 12.4704 9.9926C12.4704 11.3517 11.3685 12.4537 10.0093 12.4537C8.65005 12.4537 7.54825 11.3517 7.54825 9.9926C7.54825 8.63332 8.65005 7.53153 10.0093 7.53153Z" fill="white"/>
</g>
<defs>
<radialGradient id="paint0_radial_425_4536" cx="0" cy="0" r="1" gradientUnits="userSpaceOnUse" gradientTransform="translate(5.32327 21.5291) rotate(-90) scale(19.8161 18.4355)">
<stop stop-color="#FFDD55"/>
<stop offset="0.1" stop-color="#FFDD55"/>
<stop offset="0.5" stop-color="#FF543E"/>
<stop offset="1" stop-color="#C837AB"/>
</radialGradient>
<radialGradient id="paint1_radial_425_4536" cx="0" cy="0" r="1" gradientUnits="userSpaceOnUse" gradientTransform="translate(-3.33936 1.43488) rotate(78.6776) scale(8.85796 36.5221)">
<stop stop-color="#3771C8"/>
<stop offset="0.128" stop-color="#3771C8"/>
<stop offset="1" stop-color="#6600FF" stop-opacity="0"/>
</radialGradient>
<clipPath id="clip0_425_4536">
<rect width="20" height="20" fill="white"/>
</clipPath>
</defs>
</svg>
```

## File: src/Aqt.CoreOracle.Web/wwwroot/images/getting-started/stack-overflow.svg
```
<svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
<path d="M14.418 16.5774V12.291H15.8406V18H3V12.291H4.42263V16.5774H14.418Z" fill="#BCBBBB"/>
<path d="M5.99302 11.8845L12.9769 13.3441L13.2725 11.94L6.28863 10.4804L5.99302 11.8845ZM6.91681 8.55889L13.3833 11.5704L13.9745 10.2771L7.50803 7.24711L6.91681 8.55889ZM8.70896 5.38106L14.1963 9.94457L15.1016 8.8545L9.61427 4.29099L8.70896 5.38106ZM12.2563 2L11.1108 2.84988L15.3602 8.57737L16.5057 7.72748L12.2563 2ZM5.84521 15.1363H12.9769V13.7136H5.84521V15.1363Z" fill="#F48023"/>
</svg>
```

## File: src/Aqt.CoreOracle.Web/wwwroot/images/getting-started/x-white.svg
```
<svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
<path d="M11.5158 8.77569L17.4682 2H16.0582L10.8875 7.88203L6.76083 2H2L8.24173 10.8955L2 18H3.41003L8.86681 11.787L13.2258 18H17.9867M3.91893 3.04127H6.08513L16.0571 17.0099H13.8904" fill="white"/>
</svg>
```

## File: src/Aqt.CoreOracle.Web/wwwroot/images/getting-started/youtube.svg
```
<svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
<g clip-path="url(#clip0_425_4528)">
<path d="M19.5818 5.18637C19.3513 4.32532 18.6746 3.64867 17.8136 3.41818C16.2545 3 9.99996 3 9.99996 3C9.99996 3 3.74547 3 2.18636 3.41818C1.32532 3.64867 0.648666 4.32532 0.418176 5.18637C1.25176e-07 6.74547 0 10.0003 0 10.0003C0 10.0003 1.25176e-07 13.2552 0.418176 14.8143C0.648666 15.6753 1.32532 16.352 2.18636 16.5825C3.74547 17.0007 9.99996 17.0007 9.99996 17.0007C9.99996 17.0007 16.2545 17.0007 17.8136 16.5825C18.6746 16.352 19.3513 15.6753 19.5818 14.8143C20 13.2552 20 10.0003 20 10.0003C20 10.0003 19.9983 6.74547 19.5818 5.18637Z" fill="#FF0000"/>
<path d="M7.99854 13.0003L13.1945 10.0007L7.99854 7.00098V13.0003Z" fill="white"/>
</g>
<defs>
<clipPath id="clip0_425_4528">
<rect width="20" height="20" fill="white"/>
</clipPath>
</defs>
</svg>
```

## File: src/Aqt.CoreOracle.Web/wwwroot/global-scripts.js
```javascript

```

## File: src/Aqt.CoreOracle.Web/wwwroot/global-styles.css
```css
:root .lpx-brand-logo {
```

## File: src/Aqt.CoreOracle.Web/appsettings.Development.json
```json
{
}
```

## File: src/Aqt.CoreOracle.Web/appsettings.json
```json
{
  "App": {
    "SelfUrl": "https://localhost:44388",
    "HealthCheckUrl": "/health-status"
  },
  "ConnectionStrings": {
    "Default": "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=FREEPDB1)));User Id=corefw_admin;Password=123456a@;"
  },
  "AuthServer": {
    "Authority": "https://localhost:44388",
    "RequireHttpsMetadata": true,
    "CertificatePassPhrase": "f7b801fa-36b0-4d6d-ad18-0e68d6321419"
  },
  "StringEncryption": {
    "DefaultPassPhrase": "M5y5n5jauEVCg9LI"
  }
}
```

## File: src/Aqt.CoreOracle.Web/Aqt.CoreOracle.Web.csproj
```
<Project Sdk="Microsoft.NET.Sdk.Web">

  <Import Project="..\..\common.props" />

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <AspNetCoreHostingModel>InProcess</AspNetCoreHostingModel>
    <RootNamespace>Aqt.CoreOracle.Web</RootNamespace>
    <AssetTargetFallback>$(AssetTargetFallback);portable-net45+win8+wp8+wpa81;</AssetTargetFallback>
    <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects>
    <GenerateBindingRedirectsOutputType>true</GenerateBindingRedirectsOutputType>
    <GenerateRuntimeConfigurationFiles>true</GenerateRuntimeConfigurationFiles>
    <MvcRazorExcludeRefAssembliesFromPublish>false</MvcRazorExcludeRefAssembliesFromPublish>
    <PreserveCompilationReferences>true</PreserveCompilationReferences>
  </PropertyGroup>

  <ItemGroup Condition="Exists('./openiddict.pfx')">
    <None Remove="openiddict.pfx" />
    <EmbeddedResource Include="openiddict.pfx">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </EmbeddedResource>
  </ItemGroup>

  <ItemGroup>
    <Compile Remove="Logs\**" />
    <Content Remove="Logs\**" />
    <EmbeddedResource Remove="Logs\**" />
    <None Remove="Logs\**" />
  </ItemGroup>

  <ItemGroup>
    <Content Include="Pages\**\*.js">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
    <Content Include="Pages\**\*.css">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="AspNetCore.HealthChecks.UI" Version="9.0.0" />
    <PackageReference Include="AspNetCore.HealthChecks.UI.Client" Version="9.0.0" />
    <PackageReference Include="AspNetCore.HealthChecks.UI.InMemory.Storage" Version="9.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="9.0.0" />
    <PackageReference Include="Serilog.AspNetCore" Version="9.0.0" />
    <PackageReference Include="Serilog.Sinks.Async" Version="2.1.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Aqt.CoreOracle.Application\Aqt.CoreOracle.Application.csproj" />
    <ProjectReference Include="..\Aqt.CoreOracle.HttpApi\Aqt.CoreOracle.HttpApi.csproj" />
    <ProjectReference Include="..\Aqt.CoreOracle.EntityFrameworkCore\Aqt.CoreOracle.EntityFrameworkCore.csproj" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Volo.Abp.Autofac" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.AspNetCore.Serilog" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.AspNetCore.Authentication.JwtBearer" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.Swashbuckle" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.FeatureManagement.Web" Version="9.1.0" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite" Version="4.1.0" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Volo.Abp.Account.Web.OpenIddict" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.Identity.Web" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.TenantManagement.Web" Version="9.1.0" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Volo.Abp.Studio.Client.AspNetCore" Version="0.9.25" />
  </ItemGroup>

</Project>
```

## File: src/Aqt.CoreOracle.Web/CoreOracleBrandingProvider.cs
```csharp
public class CoreOracleBrandingProvider : DefaultBrandingProvider
```

## File: src/Aqt.CoreOracle.Web/CoreOracleWebAutoMapperProfile.cs
```csharp
public class CoreOracleWebAutoMapperProfile : Profile
```

## File: src/Aqt.CoreOracle.Web/CoreOracleWebModule.cs
```csharp
public class CoreOracleWebModule : AbpModule
⋮----
public override void PreConfigureServices(ServiceConfigurationContext context)
⋮----
var hostingEnvironment = context.Services.GetHostingEnvironment();
var configuration = context.Services.GetConfiguration();
⋮----
options.AddAssemblyResource(
⋮----
builder.AddValidation(options =>
⋮----
options.AddAudiences("CoreOracle");
options.UseLocalServer();
options.UseAspNetCore();
⋮----
if (!hostingEnvironment.IsDevelopment())
⋮----
serverBuilder.AddProductionEncryptionAndSigningCertificate("openiddict.pfx", configuration["AuthServer:CertificatePassPhrase"]!);
serverBuilder.SetIssuer(new Uri(configuration["AuthServer:Authority"]!));
⋮----
public override void ConfigureServices(ServiceConfigurationContext context)
⋮----
private void ConfigureHealthChecks(ServiceConfigurationContext context)
⋮----
context.Services.AddCoreOracleHealthChecks();
⋮----
private void ConfigureBundles()
⋮----
options.StyleBundles.Configure(
⋮----
bundle.AddFiles("/global-scripts.js");
bundle.AddFiles("/global-styles.css");
⋮----
private void ConfigureUrls(IConfiguration configuration)
⋮----
private void ConfigureAuthentication(ServiceConfigurationContext context)
⋮----
context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
⋮----
private void ConfigureAutoMapper()
⋮----
private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
⋮----
if (hostingEnvironment.IsDevelopment())
⋮----
options.FileSets.ReplaceEmbeddedByPhysical<CoreOracleDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}Aqt.CoreOracle.Domain.Shared", Path.DirectorySeparatorChar)));
options.FileSets.ReplaceEmbeddedByPhysical<CoreOracleDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}Aqt.CoreOracle.Domain", Path.DirectorySeparatorChar)));
options.FileSets.ReplaceEmbeddedByPhysical<CoreOracleApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}Aqt.CoreOracle.Application.Contracts", Path.DirectorySeparatorChar)));
options.FileSets.ReplaceEmbeddedByPhysical<CoreOracleApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}Aqt.CoreOracle.Application", Path.DirectorySeparatorChar)));
options.FileSets.ReplaceEmbeddedByPhysical<CoreOracleHttpApiModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Aqt.CoreOracle.HttpApi", Path.DirectorySeparatorChar)));
⋮----
private void ConfigureNavigationServices()
⋮----
options.MenuContributors.Add(new CoreOracleMenuContributor());
⋮----
options.Contributors.Add(new CoreOracleToolbarContributor());
⋮----
private void ConfigureAutoApiControllers()
⋮----
options.ConventionalControllers.Create(typeof(CoreOracleApplicationModule).Assembly);
⋮----
private void ConfigureSwaggerServices(IServiceCollection services)
⋮----
services.AddAbpSwaggerGen(
⋮----
options.SwaggerDoc("v1", new OpenApiInfo { Title = "CoreOracle API", Version = "v1" });
options.DocInclusionPredicate((docName, description) => true);
options.CustomSchemaIds(type => type.FullName);
⋮----
public override void OnApplicationInitialization(ApplicationInitializationContext context)
⋮----
var app = context.GetApplicationBuilder();
var env = context.GetEnvironment();
app.UseForwardedHeaders();
if (env.IsDevelopment())
⋮----
app.UseDeveloperExceptionPage();
⋮----
app.UseAbpRequestLocalization();
if (!env.IsDevelopment())
⋮----
app.UseErrorPage();
app.UseHsts();
⋮----
app.UseCorrelationId();
app.MapAbpStaticAssets();
app.UseAbpStudioLink();
app.UseRouting();
app.UseAbpSecurityHeaders();
app.UseAuthentication();
app.UseAbpOpenIddictValidation();
⋮----
app.UseMultiTenancy();
⋮----
app.UseUnitOfWork();
app.UseDynamicClaims();
app.UseAuthorization();
app.UseSwagger();
app.UseAbpSwaggerUI(options =>
⋮----
options.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreOracle API");
⋮----
app.UseAuditing();
app.UseAbpSerilogEnrichers();
app.UseConfiguredEndpoints();
```

## File: src/Aqt.CoreOracle.Web/Dockerfile
```
FROM mcr.microsoft.com/dotnet/aspnet:9.0
COPY bin/Release/net9.0/publish/ app/
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "Aqt.CoreOracle.Web.dll"]
```

## File: src/Aqt.CoreOracle.Web/Dockerfile.local
```
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
COPY bin/Release/net9.0/publish/ app/
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
RUN dotnet dev-certs https -v -ep openiddict.pfx -p f7b801fa-36b0-4d6d-ad18-0e68d6321419

FROM base AS final
WORKDIR /app
COPY --from=build /src .

ENTRYPOINT ["dotnet", "Aqt.CoreOracle.Web.dll"]
```

## File: src/Aqt.CoreOracle.Web/package.json
```json
{
	"version": "1.0.0",
	"name": "my-app",
	"private": true,
	"dependencies": {
    "@abp/aspnetcore.mvc.ui.theme.leptonxlite": "~4.1.0"
	}
}
```

## File: src/Aqt.CoreOracle.Web/Program.cs
```csharp
public class Program
⋮----
public async static Task<int> Main(string[] args)
⋮----
Log.Logger = new LoggerConfiguration()
.WriteTo.Async(c => c.File("Logs/logs.txt"))
.WriteTo.Async(c => c.Console())
.CreateBootstrapLogger();
⋮----
Log.Information("Starting web host.");
var builder = WebApplication.CreateBuilder(args);
⋮----
.AddAppSettingsSecretsJson()
.UseAutofac()
.UseSerilog((context, services, loggerConfiguration) =>
⋮----
.MinimumLevel.Debug()
⋮----
.MinimumLevel.Information()
⋮----
.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
.Enrich.FromLogContext()
⋮----
.WriteTo.Async(c => c.AbpStudio(services));
⋮----
var app = builder.Build();
await app.InitializeApplicationAsync();
await app.RunAsync();
⋮----
Log.Fatal(ex, "Host terminated unexpectedly!");
⋮----
Log.CloseAndFlush();
```

## File: src/Aqt.CoreOracle.Web/web.config
```
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <location path="." inheritInChildApplications="false">
    <system.webServer>
      <handlers>
        <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified" />
      </handlers>
      <aspNetCore processPath="dotnet" arguments=".\Aqt.CoreOracle.Web.dll" stdoutLogEnabled="false" stdoutLogFile=".\Logs\stdout" hostingModel="inprocess" />
    </system.webServer>
  </location>
  <system.webServer>
    <httpProtocol>
      <customHeaders>
        <remove name="x-powered-by" />
      </customHeaders>
    </httpProtocol>
  </system.webServer>
</configuration>
```

## File: test/Aqt.CoreOracle.Application.Tests/Samples/SampleAppServiceTests.cs
```csharp
public abstract class SampleAppServiceTests<TStartupModule> : CoreOracleApplicationTestBase<TStartupModule>
where TStartupModule : IAbpModule
⋮----
private readonly IIdentityUserAppService _userAppService;
⋮----
public async Task Initial_Data_Should_Contain_Admin_User()
⋮----
var result = await _userAppService.GetListAsync(new GetIdentityUsersInput());
result.TotalCount.ShouldBeGreaterThan(0);
result.Items.ShouldContain(u => u.UserName == "admin");
```

## File: test/Aqt.CoreOracle.Application.Tests/Aqt.CoreOracle.Application.Tests.csproj
```
<Project Sdk="Microsoft.NET.Sdk">

  <Import Project="..\..\common.props" />

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <RootNamespace>Aqt.CoreOracle</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\src\Aqt.CoreOracle.Application\Aqt.CoreOracle.Application.csproj" />
    <ProjectReference Include="..\Aqt.CoreOracle.Domain.Tests\Aqt.CoreOracle.Domain.Tests.csproj" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.12.0" />
  </ItemGroup>

</Project>
```

## File: test/Aqt.CoreOracle.Application.Tests/CoreOracleApplicationTestBase.cs
```csharp
public abstract class CoreOracleApplicationTestBase<TStartupModule> : CoreOracleTestBase<TStartupModule>
where TStartupModule : IAbpModule
```

## File: test/Aqt.CoreOracle.Application.Tests/CoreOracleApplicationTestModule.cs
```csharp
public class CoreOracleApplicationTestModule : AbpModule
```

## File: test/Aqt.CoreOracle.Domain.Tests/Samples/SampleDomainTests.cs
```csharp
public abstract class SampleDomainTests<TStartupModule> : CoreOracleDomainTestBase<TStartupModule>
where TStartupModule : IAbpModule
⋮----
private readonly IIdentityUserRepository _identityUserRepository;
private readonly IdentityUserManager _identityUserManager;
⋮----
public async Task Should_Set_Email_Of_A_User()
⋮----
IdentityUser adminUser;
⋮----
.FindByNormalizedUserNameAsync("ADMIN");
await _identityUserManager.SetEmailAsync(adminUser, "newemail@abp.io");
await _identityUserRepository.UpdateAsync(adminUser);
⋮----
adminUser = await _identityUserRepository.FindByNormalizedUserNameAsync("ADMIN");
adminUser.Email.ShouldBe("newemail@abp.io");
```

## File: test/Aqt.CoreOracle.Domain.Tests/Aqt.CoreOracle.Domain.Tests.csproj
```
<Project Sdk="Microsoft.NET.Sdk">

  <Import Project="..\..\common.props" />

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <RootNamespace>Aqt.CoreOracle</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\src\Aqt.CoreOracle.Domain\Aqt.CoreOracle.Domain.csproj" />
    <ProjectReference Include="..\Aqt.CoreOracle.TestBase\Aqt.CoreOracle.TestBase.csproj" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.12.0" />
  </ItemGroup>

</Project>
```

## File: test/Aqt.CoreOracle.Domain.Tests/CoreOracleDomainTestBase.cs
```csharp
public abstract class CoreOracleDomainTestBase<TStartupModule> : CoreOracleTestBase<TStartupModule>
where TStartupModule : IAbpModule
```

## File: test/Aqt.CoreOracle.Domain.Tests/CoreOracleDomainTestModule.cs
```csharp
public class CoreOracleDomainTestModule : AbpModule
```

## File: test/Aqt.CoreOracle.EntityFrameworkCore.Tests/EntityFrameworkCore/Applications/EfCoreSampleAppServiceTests.cs
```csharp
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<CoreOracleEntityFrameworkCoreTestModule>
```

## File: test/Aqt.CoreOracle.EntityFrameworkCore.Tests/EntityFrameworkCore/Domains/EfCoreSampleDomainTests.cs
```csharp
public class EfCoreSampleDomainTests : SampleDomainTests<CoreOracleEntityFrameworkCoreTestModule>
```

## File: test/Aqt.CoreOracle.EntityFrameworkCore.Tests/EntityFrameworkCore/Samples/SampleRepositoryTests.cs
```csharp
public class SampleRepositoryTests : CoreOracleEntityFrameworkCoreTestBase
⋮----
public async Task Should_Query_AppUser()
⋮----
.FirstOrDefaultAsync(u => u.UserName == "admin");
adminUser.ShouldNotBeNull();
```

## File: test/Aqt.CoreOracle.EntityFrameworkCore.Tests/EntityFrameworkCore/CoreOracleEntityFrameworkCoreCollection.cs
```csharp
public class CoreOracleEntityFrameworkCoreCollection : ICollectionFixture<CoreOracleEntityFrameworkCoreFixture>
```

## File: test/Aqt.CoreOracle.EntityFrameworkCore.Tests/EntityFrameworkCore/CoreOracleEntityFrameworkCoreCollectionFixtureBase.cs
```csharp
public class CoreOracleEntityFrameworkCoreCollectionFixtureBase : ICollectionFixture<CoreOracleEntityFrameworkCoreFixture>
```

## File: test/Aqt.CoreOracle.EntityFrameworkCore.Tests/EntityFrameworkCore/CoreOracleEntityFrameworkCoreFixture.cs
```csharp
public class CoreOracleEntityFrameworkCoreFixture : IDisposable
⋮----
public void Dispose()
```

## File: test/Aqt.CoreOracle.EntityFrameworkCore.Tests/EntityFrameworkCore/CoreOracleEntityFrameworkCoreTestBase.cs
```csharp
public abstract class CoreOracleEntityFrameworkCoreTestBase : CoreOracleTestBase<CoreOracleEntityFrameworkCoreTestModule>
```

## File: test/Aqt.CoreOracle.EntityFrameworkCore.Tests/EntityFrameworkCore/CoreOracleEntityFrameworkCoreTestModule.cs
```csharp
public class CoreOracleEntityFrameworkCoreTestModule : AbpModule
⋮----
public override void ConfigureServices(ServiceConfigurationContext context)
⋮----
context.Services.AddAlwaysDisableUnitOfWorkTransaction();
⋮----
private void ConfigureInMemorySqlite(IServiceCollection services)
⋮----
options.Configure(context =>
⋮----
context.DbContextOptions.UseSqlite(_sqliteConnection);
⋮----
public override void OnApplicationShutdown(ApplicationShutdownContext context)
⋮----
private static SqliteConnection CreateDatabaseAndGetConnection()
⋮----
var connection = new SqliteConnection("Data Source=:memory:");
connection.Open();
⋮----
.UseSqlite(connection)
⋮----
using (var context = new CoreOracleDbContext(options))
⋮----
context.GetService<IRelationalDatabaseCreator>().CreateTables();
```

## File: test/Aqt.CoreOracle.EntityFrameworkCore.Tests/Aqt.CoreOracle.EntityFrameworkCore.Tests.csproj
```
<Project Sdk="Microsoft.NET.Sdk">

  <Import Project="..\..\common.props" />

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <RootNamespace>Aqt.CoreOracle</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\src\Aqt.CoreOracle.EntityFrameworkCore\Aqt.CoreOracle.EntityFrameworkCore.csproj" />
    <ProjectReference Include="..\Aqt.CoreOracle.Application.Tests\Aqt.CoreOracle.Application.Tests.csproj" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Volo.Abp.EntityFrameworkCore.Sqlite" Version="9.1.0" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.12.0" />
  </ItemGroup>

</Project>
```

## File: test/Aqt.CoreOracle.HttpApi.Client.ConsoleTestApp/appsettings.json
```json
{
  "RemoteServices": {
    "Default": {
      "BaseUrl": "https://localhost:44300/"
    }
  },
  "IdentityClients": {
    "Default": {
      "GrantType": "password",
      "ClientId": "CoreOracle_App",
      "UserName": "admin",
      "UserPassword": "1q2w3E*",
      "Authority": "https://localhost:44301",
      "Scope": "CoreOracle"
    }
  }
}
```

## File: test/Aqt.CoreOracle.HttpApi.Client.ConsoleTestApp/appsettings.secrets.json
```json
{
}
```

## File: test/Aqt.CoreOracle.HttpApi.Client.ConsoleTestApp/Aqt.CoreOracle.HttpApi.Client.ConsoleTestApp.csproj
```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <None Remove="appsettings.json" />
    <Content Include="appsettings.json">
      <CopyToPublishDirectory>PreserveNewest</CopyToPublishDirectory>
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
    <None Remove="appsettings.secrets.json" />
    <Content Include="appsettings.secrets.json">
      <CopyToPublishDirectory>PreserveNewest</CopyToPublishDirectory>
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\src\Aqt.CoreOracle.HttpApi.Client\Aqt.CoreOracle.HttpApi.Client.csproj" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Volo.Abp.Autofac" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.Http.Client.IdentityModel" Version="9.1.0" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="9.0.0" />
    <PackageReference Include="Microsoft.Extensions.Http.Polly" Version="9.0.0" />
  </ItemGroup>

</Project>
```

## File: test/Aqt.CoreOracle.HttpApi.Client.ConsoleTestApp/ClientDemoService.cs
```csharp
public class ClientDemoService : ITransientDependency
⋮----
private readonly IProfileAppService _profileAppService;
private readonly IIdentityUserAppService _identityUserAppService;
⋮----
public async Task RunAsync()
⋮----
var profileDto = await _profileAppService.GetAsync();
Console.WriteLine($"UserName : {profileDto.UserName}");
Console.WriteLine($"Email    : {profileDto.Email}");
Console.WriteLine($"Name     : {profileDto.Name}");
Console.WriteLine($"Surname  : {profileDto.Surname}");
Console.WriteLine();
var resultDto = await _identityUserAppService.GetListAsync(new GetIdentityUsersInput());
Console.WriteLine($"Total users: {resultDto.TotalCount}");
⋮----
Console.WriteLine($"- [{identityUserDto.Id}] {identityUserDto.Name}");
```

## File: test/Aqt.CoreOracle.HttpApi.Client.ConsoleTestApp/CoreOracleConsoleApiClientModule.cs
```csharp
public class CoreOracleConsoleApiClientModule : AbpModule
⋮----
public override void PreConfigureServices(ServiceConfigurationContext context)
⋮----
options.ProxyClientBuildActions.Add((remoteServiceName, clientBuilder) =>
⋮----
clientBuilder.AddTransientHttpErrorPolicy(
policyBuilder => policyBuilder.WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(Math.Pow(2, i)))
```

## File: test/Aqt.CoreOracle.HttpApi.Client.ConsoleTestApp/Program.cs
```csharp
class Program
⋮----
static async Task Main(string[] args)
⋮----
var builder = new ConfigurationBuilder();
builder.AddJsonFile("appsettings.json", false);
builder.AddJsonFile("appsettings.secrets.json", true);
options.Services.ReplaceConfiguration(builder.Build());
options.UseAutofac();
⋮----
await application.InitializeAsync();
⋮----
await demo.RunAsync();
Console.WriteLine("Press ENTER to stop application...");
Console.ReadLine();
await application.ShutdownAsync();
```

## File: test/Aqt.CoreOracle.TestBase/Security/FakeCurrentPrincipalAccessor.cs
```csharp
public class FakeCurrentPrincipalAccessor : ThreadCurrentPrincipalAccessor
⋮----
protected override ClaimsPrincipal GetClaimsPrincipal()
⋮----
private ClaimsPrincipal GetPrincipal()
⋮----
return new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
⋮----
new Claim(AbpClaimTypes.UserId, "2e701e62-0953-4dd3-910b-dc6cc93ccb0d"),
new Claim(AbpClaimTypes.UserName, "admin"),
new Claim(AbpClaimTypes.Email, "admin@abp.io")
```

## File: test/Aqt.CoreOracle.TestBase/appsettings.json
```json
{

}
```

## File: test/Aqt.CoreOracle.TestBase/appsettings.secrets.json
```json
{
}
```

## File: test/Aqt.CoreOracle.TestBase/Aqt.CoreOracle.TestBase.csproj
```
<Project Sdk="Microsoft.NET.Sdk">

  <Import Project="..\..\common.props" />

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <RootNamespace>Aqt.CoreOracle</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <None Remove="appsettings.json" />
    <Content Include="appsettings.json">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
      <ExcludeFromSingleFile>true</ExcludeFromSingleFile>
      <CopyToPublishDirectory>PreserveNewest</CopyToPublishDirectory>
    </Content>
    <None Remove="appsettings.secrets.json" />
    <Content Include="appsettings.secrets.json">
      <CopyToPublishDirectory>PreserveNewest</CopyToPublishDirectory>
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Volo.Abp.Autofac" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.TestBase" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.Authorization" Version="9.1.0" />
    <PackageReference Include="Volo.Abp.BackgroundJobs.Abstractions" Version="9.1.0" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.12.0" />
    <PackageReference Include="NSubstitute" Version="5.3.0" />
    <PackageReference Include="NSubstitute.Analyzers.CSharp" Version="1.0.17">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Shouldly" Version="4.2.1" />
    <PackageReference Include="xunit" Version="2.9.3" />
    <PackageReference Include="xunit.extensibility.execution" Version="2.9.3" />
    <PackageReference Include="xunit.runner.visualstudio" Version="3.0.1" />
  </ItemGroup>

</Project>
```

## File: test/Aqt.CoreOracle.TestBase/CoreOracleTestBase.cs
```csharp
public abstract class CoreOracleTestBase<TStartupModule> : AbpIntegratedTest<TStartupModule>
where TStartupModule : IAbpModule
⋮----
protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
⋮----
options.UseAutofac();
⋮----
protected override void BeforeAddApplication(IServiceCollection services)
⋮----
var builder = new ConfigurationBuilder();
builder.AddJsonFile("appsettings.json", false);
builder.AddJsonFile("appsettings.secrets.json", true);
services.ReplaceConfiguration(builder.Build());
⋮----
protected virtual Task WithUnitOfWorkAsync(Func<Task> func)
⋮----
return WithUnitOfWorkAsync(new AbpUnitOfWorkOptions(), func);
⋮----
protected virtual async Task WithUnitOfWorkAsync(AbpUnitOfWorkOptions options, Func<Task> action)
⋮----
using (var scope = ServiceProvider.CreateScope())
⋮----
using (var uow = uowManager.Begin(options))
⋮----
await uow.CompleteAsync();
⋮----
protected virtual Task<TResult> WithUnitOfWorkAsync<TResult>(Func<Task<TResult>> func)
⋮----
protected virtual async Task<TResult> WithUnitOfWorkAsync<TResult>(AbpUnitOfWorkOptions options, Func<Task<TResult>> func)
```

## File: test/Aqt.CoreOracle.TestBase/CoreOracleTestBaseModule.cs
```csharp
public class CoreOracleTestBaseModule : AbpModule
⋮----
public override void ConfigureServices(ServiceConfigurationContext context)
⋮----
context.Services.AddAlwaysAllowAuthorization();
⋮----
public override void OnApplicationInitialization(ApplicationInitializationContext context)
⋮----
private static void SeedTestData(ApplicationInitializationContext context)
⋮----
AsyncHelper.RunSync(async () =>
⋮----
using (var scope = context.ServiceProvider.CreateScope())
⋮----
.SeedAsync();
```

## File: test/Aqt.CoreOracle.TestBase/CoreOracleTestConsts.cs
```csharp
public static class CoreOracleTestConsts
```

## File: test/Aqt.CoreOracle.TestBase/CoreOracleTestDataBuilder.cs
```csharp
public class CoreOracleTestDataSeedContributor : IDataSeedContributor, ITransientDependency
⋮----
private readonly ICurrentTenant _currentTenant;
⋮----
public Task SeedAsync(DataSeedContext context)
⋮----
using (_currentTenant.Change(context?.TenantId))
```

## File: test/Aqt.CoreOracle.Web.Tests/Pages/Index_Tests.cs
```csharp
public class Index_Tests : CoreOracleWebTestBase
⋮----
public async Task Welcome_Page()
⋮----
response.ShouldNotBeNull();
```

## File: test/Aqt.CoreOracle.Web.Tests/Aqt.CoreOracle.Web.Tests.csproj
```
<Project Sdk="Microsoft.NET.Sdk">

  <Import Project="..\..\common.props" />

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <OutputType>Exe</OutputType>
    <AssetTargetFallback>$(AssetTargetFallback);portable-net45+win8+wp8+wpa81;</AssetTargetFallback>
    <RootNamespace>Aqt.CoreOracle</RootNamespace>
    <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects>
    <GenerateBindingRedirectsOutputType>true</GenerateBindingRedirectsOutputType>
    <GenerateRuntimeConfigurationFiles>true</GenerateRuntimeConfigurationFiles>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.12.0" />
    <PackageReference Include="HtmlAgilityPack" Version="1.11.72" />
    <PackageReference Include="Volo.Abp.AspNetCore.TestBase" Version="9.1.0" />
    <ProjectReference Include="..\Aqt.CoreOracle.Application.Tests\Aqt.CoreOracle.Application.Tests.csproj" />
    <ProjectReference Include="..\..\src\Aqt.CoreOracle.Web\Aqt.CoreOracle.Web.csproj" />
    <ProjectReference Include="..\Aqt.CoreOracle.EntityFrameworkCore.Tests\Aqt.CoreOracle.EntityFrameworkCore.Tests.csproj" />
  </ItemGroup>

  <ItemGroup>
    <Content Include="xunit.runner.json">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
      <CopyToPublishDirectory>PreserveNewest</CopyToPublishDirectory>
    </Content>
  </ItemGroup>

  <!-- https://github.com/NuGet/Home/issues/4412 -->
  <Target Name="CopyDepsFiles" AfterTargets="Build" Condition="'$(TargetFramework)'!=''">
    <ItemGroup>
      <DepsFilePaths Include="$([System.IO.Path]::ChangeExtension('%(_ResolvedProjectReferencePaths.FullPath)', '.deps.json'))" />
    </ItemGroup>

    <Copy SourceFiles="%(DepsFilePaths.FullPath)" DestinationFolder="$(OutputPath)" Condition="Exists('%(DepsFilePaths.FullPath)')" />
  </Target>

</Project>
```

## File: test/Aqt.CoreOracle.Web.Tests/CoreOracleWebCollection.cs
```csharp
public class CoreOracleWebCollection : CoreOracleEntityFrameworkCoreCollectionFixtureBase
```

## File: test/Aqt.CoreOracle.Web.Tests/CoreOracleWebTestBase.cs
```csharp
public abstract class CoreOracleWebTestBase : AbpWebApplicationFactoryIntegratedTest<Program>
⋮----
protected virtual async Task<T> GetResponseAsObjectAsync<T>(string url, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
⋮----
return JsonSerializer.Deserialize<T>(strResponse, new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
⋮----
protected virtual async Task<string> GetResponseAsStringAsync(string url, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
⋮----
return await response.Content.ReadAsStringAsync();
⋮----
protected virtual async Task<HttpResponseMessage> GetResponseAsync(string url, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
⋮----
var response = await Client.GetAsync(url);
response.StatusCode.ShouldBe(expectedStatusCode);
```

## File: test/Aqt.CoreOracle.Web.Tests/CoreOracleWebTestModule.cs
```csharp
public class CoreOracleWebTestModule : AbpModule
⋮----
public override void PreConfigureServices(ServiceConfigurationContext context)
⋮----
var builder = new ConfigurationBuilder();
builder.AddJsonFile("appsettings.json", false);
builder.AddJsonFile("appsettings.secrets.json", true);
context.Services.ReplaceConfiguration(builder.Build());
⋮----
builder.PartManager.ApplicationParts.Add(new CompiledRazorAssemblyPart(typeof(CoreOracleWebModule).Assembly));
⋮----
context.Services.GetPreConfigureActions<OpenIddictServerBuilder>().Clear();
⋮----
public override void ConfigureServices(ServiceConfigurationContext context)
⋮----
private static void ConfigureLocalizationServices(IServiceCollection services)
⋮----
var cultures = new List<CultureInfo> { new CultureInfo("en"), new CultureInfo("tr") };
⋮----
options.DefaultRequestCulture = new RequestCulture("en");
⋮----
private static void ConfigureNavigationServices(IServiceCollection services)
⋮----
options.MenuContributors.Add(new CoreOracleMenuContributor());
```

## File: test/Aqt.CoreOracle.Web.Tests/Program.cs
```csharp
var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("Aqt.CoreOracle.Web.csproj");
⋮----
public partial class Program
```

## File: test/Aqt.CoreOracle.Web.Tests/xunit.runner.json
```json
{
  "shadowCopy": false
}
```

## File: Aqt.CoreOracle.sln
```
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.3.32611.2
MinimumVisualStudioVersion = 10.0.40219.1
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "Aqt.CoreOracle.Domain", "src\Aqt.CoreOracle.Domain\Aqt.CoreOracle.Domain.csproj", "{554AD327-6DBA-4F8F-96F8-81CE7A0C863F}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "Aqt.CoreOracle.Application", "src\Aqt.CoreOracle.Application\Aqt.CoreOracle.Application.csproj", "{1A94A50E-06DC-43C1-80B5-B662820EC3EB}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "Aqt.CoreOracle.EntityFrameworkCore", "src\Aqt.CoreOracle.EntityFrameworkCore\Aqt.CoreOracle.EntityFrameworkCore.csproj", "{C956DD76-69C8-4A9C-83EA-D17DF83340FD}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "Aqt.CoreOracle.Web", "src\Aqt.CoreOracle.Web\Aqt.CoreOracle.Web.csproj", "{068855E8-9240-4F1A-910B-CF825794513B}"
EndProject
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "src", "src", "{CA9AC87F-097E-4F15-8393-4BC07735A5B0}"
EndProject
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "test", "test", "{04DBDB01-70F4-4E06-B468-8F87850B22BE}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "Aqt.CoreOracle.Application.Tests", "test\Aqt.CoreOracle.Application.Tests\Aqt.CoreOracle.Application.Tests.csproj", "{50B2631D-129C-47B3-A587-029CCD6099BC}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "Aqt.CoreOracle.Web.Tests", "test\Aqt.CoreOracle.Web.Tests\Aqt.CoreOracle.Web.Tests.csproj", "{5F1B28C6-8D0C-4155-92D0-252F7EA5F674}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "Aqt.CoreOracle.Domain.Shared", "src\Aqt.CoreOracle.Domain.Shared\Aqt.CoreOracle.Domain.Shared.csproj", "{42F719ED-8413-4895-B5B4-5AB56079BC66}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "Aqt.CoreOracle.Application.Contracts", "src\Aqt.CoreOracle.Application.Contracts\Aqt.CoreOracle.Application.Contracts.csproj", "{520659C8-C734-4298-A3DA-B539DB9DFC0B}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "Aqt.CoreOracle.HttpApi", "src\Aqt.CoreOracle.HttpApi\Aqt.CoreOracle.HttpApi.csproj", "{4164BDF7-F527-4E85-9CE6-E3C2D7426A27}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "Aqt.CoreOracle.HttpApi.Client", "src\Aqt.CoreOracle.HttpApi.Client\Aqt.CoreOracle.HttpApi.Client.csproj", "{3B5A0094-670D-4BB1-BFDD-61B88A8773DC}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "Aqt.CoreOracle.EntityFrameworkCore.Tests", "test\Aqt.CoreOracle.EntityFrameworkCore.Tests\Aqt.CoreOracle.EntityFrameworkCore.Tests.csproj", "{1FE30EB9-74A9-47F5-A9F6-7B1FAB672D81}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "Aqt.CoreOracle.TestBase", "test\Aqt.CoreOracle.TestBase\Aqt.CoreOracle.TestBase.csproj", "{91853F21-9CD9-4132-BC29-A7D5D84FFFE7}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "Aqt.CoreOracle.Domain.Tests", "test\Aqt.CoreOracle.Domain.Tests\Aqt.CoreOracle.Domain.Tests.csproj", "{E512F4D9-9375-480F-A2F6-A46509F9D824}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "Aqt.CoreOracle.HttpApi.Client.ConsoleTestApp", "test\Aqt.CoreOracle.HttpApi.Client.ConsoleTestApp\Aqt.CoreOracle.HttpApi.Client.ConsoleTestApp.csproj", "{EF480016-9127-4916-8735-D2466BDBC582}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "Aqt.CoreOracle.DbMigrator", "src\Aqt.CoreOracle.DbMigrator\Aqt.CoreOracle.DbMigrator.csproj", "{70680696-BB1E-4383-BCB2-42C3767171FB}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{554AD327-6DBA-4F8F-96F8-81CE7A0C863F}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{554AD327-6DBA-4F8F-96F8-81CE7A0C863F}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{554AD327-6DBA-4F8F-96F8-81CE7A0C863F}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{554AD327-6DBA-4F8F-96F8-81CE7A0C863F}.Release|Any CPU.Build.0 = Release|Any CPU
		{1A94A50E-06DC-43C1-80B5-B662820EC3EB}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{1A94A50E-06DC-43C1-80B5-B662820EC3EB}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{1A94A50E-06DC-43C1-80B5-B662820EC3EB}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{1A94A50E-06DC-43C1-80B5-B662820EC3EB}.Release|Any CPU.Build.0 = Release|Any CPU
		{C956DD76-69C8-4A9C-83EA-D17DF83340FD}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{C956DD76-69C8-4A9C-83EA-D17DF83340FD}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{C956DD76-69C8-4A9C-83EA-D17DF83340FD}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{C956DD76-69C8-4A9C-83EA-D17DF83340FD}.Release|Any CPU.Build.0 = Release|Any CPU
		{068855E8-9240-4F1A-910B-CF825794513B}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{068855E8-9240-4F1A-910B-CF825794513B}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{068855E8-9240-4F1A-910B-CF825794513B}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{068855E8-9240-4F1A-910B-CF825794513B}.Release|Any CPU.Build.0 = Release|Any CPU
		{50B2631D-129C-47B3-A587-029CCD6099BC}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{50B2631D-129C-47B3-A587-029CCD6099BC}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{50B2631D-129C-47B3-A587-029CCD6099BC}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{50B2631D-129C-47B3-A587-029CCD6099BC}.Release|Any CPU.Build.0 = Release|Any CPU
		{5F1B28C6-8D0C-4155-92D0-252F7EA5F674}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{5F1B28C6-8D0C-4155-92D0-252F7EA5F674}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{5F1B28C6-8D0C-4155-92D0-252F7EA5F674}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{5F1B28C6-8D0C-4155-92D0-252F7EA5F674}.Release|Any CPU.Build.0 = Release|Any CPU
		{42F719ED-8413-4895-B5B4-5AB56079BC66}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{42F719ED-8413-4895-B5B4-5AB56079BC66}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{42F719ED-8413-4895-B5B4-5AB56079BC66}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{42F719ED-8413-4895-B5B4-5AB56079BC66}.Release|Any CPU.Build.0 = Release|Any CPU
		{520659C8-C734-4298-A3DA-B539DB9DFC0B}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{520659C8-C734-4298-A3DA-B539DB9DFC0B}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{520659C8-C734-4298-A3DA-B539DB9DFC0B}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{520659C8-C734-4298-A3DA-B539DB9DFC0B}.Release|Any CPU.Build.0 = Release|Any CPU
		{4164BDF7-F527-4E85-9CE6-E3C2D7426A27}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{4164BDF7-F527-4E85-9CE6-E3C2D7426A27}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{4164BDF7-F527-4E85-9CE6-E3C2D7426A27}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{4164BDF7-F527-4E85-9CE6-E3C2D7426A27}.Release|Any CPU.Build.0 = Release|Any CPU
		{3B5A0094-670D-4BB1-BFDD-61B88A8773DC}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{3B5A0094-670D-4BB1-BFDD-61B88A8773DC}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{3B5A0094-670D-4BB1-BFDD-61B88A8773DC}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{3B5A0094-670D-4BB1-BFDD-61B88A8773DC}.Release|Any CPU.Build.0 = Release|Any CPU
		{1FE30EB9-74A9-47F5-A9F6-7B1FAB672D81}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{1FE30EB9-74A9-47F5-A9F6-7B1FAB672D81}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{1FE30EB9-74A9-47F5-A9F6-7B1FAB672D81}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{1FE30EB9-74A9-47F5-A9F6-7B1FAB672D81}.Release|Any CPU.Build.0 = Release|Any CPU
		{91853F21-9CD9-4132-BC29-A7D5D84FFFE7}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{91853F21-9CD9-4132-BC29-A7D5D84FFFE7}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{91853F21-9CD9-4132-BC29-A7D5D84FFFE7}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{91853F21-9CD9-4132-BC29-A7D5D84FFFE7}.Release|Any CPU.Build.0 = Release|Any CPU
		{E512F4D9-9375-480F-A2F6-A46509F9D824}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{E512F4D9-9375-480F-A2F6-A46509F9D824}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{E512F4D9-9375-480F-A2F6-A46509F9D824}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{E512F4D9-9375-480F-A2F6-A46509F9D824}.Release|Any CPU.Build.0 = Release|Any CPU
		{EF480016-9127-4916-8735-D2466BDBC582}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{EF480016-9127-4916-8735-D2466BDBC582}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{EF480016-9127-4916-8735-D2466BDBC582}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{EF480016-9127-4916-8735-D2466BDBC582}.Release|Any CPU.Build.0 = Release|Any CPU
		{70680696-BB1E-4383-BCB2-42C3767171FB}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{70680696-BB1E-4383-BCB2-42C3767171FB}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{70680696-BB1E-4383-BCB2-42C3767171FB}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{70680696-BB1E-4383-BCB2-42C3767171FB}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(NestedProjects) = preSolution
		{554AD327-6DBA-4F8F-96F8-81CE7A0C863F} = {CA9AC87F-097E-4F15-8393-4BC07735A5B0}
		{1A94A50E-06DC-43C1-80B5-B662820EC3EB} = {CA9AC87F-097E-4F15-8393-4BC07735A5B0}
		{C956DD76-69C8-4A9C-83EA-D17DF83340FD} = {CA9AC87F-097E-4F15-8393-4BC07735A5B0}
		{068855E8-9240-4F1A-910B-CF825794513B} = {CA9AC87F-097E-4F15-8393-4BC07735A5B0}
		{50B2631D-129C-47B3-A587-029CCD6099BC} = {04DBDB01-70F4-4E06-B468-8F87850B22BE}
		{5F1B28C6-8D0C-4155-92D0-252F7EA5F674} = {04DBDB01-70F4-4E06-B468-8F87850B22BE}
		{42F719ED-8413-4895-B5B4-5AB56079BC66} = {CA9AC87F-097E-4F15-8393-4BC07735A5B0}
		{520659C8-C734-4298-A3DA-B539DB9DFC0B} = {CA9AC87F-097E-4F15-8393-4BC07735A5B0}
		{4164BDF7-F527-4E85-9CE6-E3C2D7426A27} = {CA9AC87F-097E-4F15-8393-4BC07735A5B0}
		{3B5A0094-670D-4BB1-BFDD-61B88A8773DC} = {CA9AC87F-097E-4F15-8393-4BC07735A5B0}
		{1FE30EB9-74A9-47F5-A9F6-7B1FAB672D81} = {04DBDB01-70F4-4E06-B468-8F87850B22BE}
		{91853F21-9CD9-4132-BC29-A7D5D84FFFE7} = {04DBDB01-70F4-4E06-B468-8F87850B22BE}
		{E512F4D9-9375-480F-A2F6-A46509F9D824} = {04DBDB01-70F4-4E06-B468-8F87850B22BE}
		{EF480016-9127-4916-8735-D2466BDBC582} = {04DBDB01-70F4-4E06-B468-8F87850B22BE}
		{70680696-BB1E-4383-BCB2-42C3767171FB} = {CA9AC87F-097E-4F15-8393-4BC07735A5B0}
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {28315BFD-90E7-4E14-A2EA-F3D23AF4126F}
	EndGlobalSection
EndGlobal
```

## File: Aqt.CoreOracle.sln.DotSettings
```
<wpf:ResourceDictionary xml:space="preserve" xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" xmlns:s="clr-namespace:System;assembly=mscorlib" xmlns:ss="urn:shemas-jetbrains-com:settings-storage-xaml" xmlns:wpf="http://schemas.microsoft.com/winfx/2006/xaml/presentation">
	<s:Boolean x:Key="/Default/CodeEditing/Intellisense/CodeCompletion/IntelliSenseCompletingCharacters/CSharpCompletingCharacters/UpgradedFromVSSettings/@EntryValue">True</s:Boolean>
	<s:String x:Key="/Default/CodeInspection/Highlighting/InspectionSeverities/=EnforceDoWhileStatementBraces/@EntryIndexedValue">WARNING</s:String>
	<s:String x:Key="/Default/CodeInspection/Highlighting/InspectionSeverities/=EnforceFixedStatementBraces/@EntryIndexedValue">WARNING</s:String>
	<s:String x:Key="/Default/CodeInspection/Highlighting/InspectionSeverities/=EnforceForeachStatementBraces/@EntryIndexedValue">WARNING</s:String>
	<s:String x:Key="/Default/CodeInspection/Highlighting/InspectionSeverities/=EnforceForStatementBraces/@EntryIndexedValue">WARNING</s:String>
	<s:String x:Key="/Default/CodeInspection/Highlighting/InspectionSeverities/=EnforceIfStatementBraces/@EntryIndexedValue">WARNING</s:String>
	<s:String x:Key="/Default/CodeInspection/Highlighting/InspectionSeverities/=EnforceLockStatementBraces/@EntryIndexedValue">WARNING</s:String>
	<s:String x:Key="/Default/CodeInspection/Highlighting/InspectionSeverities/=EnforceUsingStatementBraces/@EntryIndexedValue">WARNING</s:String>
	<s:String x:Key="/Default/CodeInspection/Highlighting/InspectionSeverities/=EnforceWhileStatementBraces/@EntryIndexedValue">WARNING</s:String>
	<s:String x:Key="/Default/CodeStyle/CodeFormatting/CSharpCodeStyle/BRACES_FOR_FOR/@EntryValue">Required</s:String>
	<s:String x:Key="/Default/CodeStyle/CodeFormatting/CSharpCodeStyle/BRACES_FOR_FOREACH/@EntryValue">Required</s:String>
	<s:String x:Key="/Default/CodeStyle/CodeFormatting/CSharpCodeStyle/BRACES_FOR_IFELSE/@EntryValue">Required</s:String>
	<s:String x:Key="/Default/CodeStyle/CodeFormatting/CSharpCodeStyle/BRACES_FOR_WHILE/@EntryValue">Required</s:String>
	<s:Boolean x:Key="/Default/CodeStyle/CodeFormatting/CSharpCodeStyle/BRACES_REDUNDANT/@EntryValue">False</s:Boolean>
	<s:Boolean x:Key="/Default/CodeStyle/Generate/=Implementations/@KeyIndexDefined">True</s:Boolean>
	<s:String x:Key="/Default/CodeStyle/Generate/=Implementations/Options/=Async/@EntryIndexedValue">False</s:String>
	<s:String x:Key="/Default/CodeStyle/Generate/=Implementations/Options/=Mutable/@EntryIndexedValue">False</s:String>
	<s:Boolean x:Key="/Default/CodeStyle/Generate/=Overrides/@KeyIndexDefined">True</s:Boolean>
	<s:String x:Key="/Default/CodeStyle/Generate/=Overrides/Options/=Async/@EntryIndexedValue">False</s:String>
	<s:String x:Key="/Default/CodeStyle/Generate/=Overrides/Options/=Mutable/@EntryIndexedValue">False</s:String>
	<s:String x:Key="/Default/CodeStyle/Naming/CSharpNaming/Abbreviations/=SQL/@EntryIndexedValue">SQL</s:String>
</wpf:ResourceDictionary>
```

## File: NuGet.Config
```
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
  </packageSources>
  <packageSourceMapping>
    <packageSource key="nuget.org">
      <package pattern="*" />
      <package pattern="Microsoft.*" />
      <package pattern="Volo.*" />
    </packageSource>
  </packageSourceMapping>
</configuration>
```
