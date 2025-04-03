# Script để generate code từ template
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string]$ModuleName,
    
    [Parameter(Mandatory = $true)]
    [string]$EntityName,
    
    [Parameter(Mandatory = $false)]
    [string]$Description = "",
    
    [Parameter(Mandatory = $false)]
    [switch]$Force,
    
    [Parameter(Mandatory = $false)]
    [switch]$SkipBackup,
    
    [Parameter(Mandatory = $false)]
    [switch]$UpdateDbContext,
    
    [Parameter(Mandatory = $false)]
    [switch]$UpdateMenu,
    
    [Parameter(Mandatory = $false)]
    [switch]$UpdateAutoMapper,
    
    [Parameter(Mandatory = $false)]
    [switch]$CreateMigration,
    
    [Parameter(Mandatory = $false)]
    [switch]$PreviewOnly,
    
    [Parameter(Mandatory = $false)]
    [switch]$ValidateOnly,
    
    [Parameter(Mandatory = $false)]
    [string]$CustomTemplateDir,
    
    [Parameter(Mandatory = $false)]
    [switch]$UpdatePermissions,
    
    [Parameter(Mandatory = $false)]
    [switch]$UpdateNavigation,
    
    [Parameter(Mandatory = $false)]
    [switch]$UpdateSeedData,
    
    [Parameter(Mandatory = $false)]
    [switch]$UpdateDocs,
    
    [Parameter(Mandatory = $false)]
    [string[]]$NavigationProperties,
    
    [Parameter(Mandatory = $false)]
    [hashtable]$SeedData
)

# Biến global để lưu trữ các file đã backup và thay đổi
$script:backupFiles = @{}
$script:changes = @()
$script:ErrorActionPreference = "Stop"

# Các hàm helper
function Convert-ToPascalCase {
    param([string]$text)
    if ([string]::IsNullOrWhiteSpace($text)) {
        throw "Text cannot be empty"
    }
    return (Get-Culture).TextInfo.ToTitleCase($text)
}

function Convert-ToCamelCase {
    param([string]$text)
    $pascal = Convert-ToPascalCase $text
    return $pascal.Substring(0,1).ToLower() + $pascal.Substring(1)
}

function Test-ValidIdentifier {
    param([string]$name)
    return $name -match "^[a-zA-Z][a-zA-Z0-9_]*$"
}

function Add-Change {
    param(
        [string]$type,
        [string]$path,
        [string]$description
    )
    $script:changes += @{
        Type = $type
        Path = $path
        Description = $description
    }
}

function Show-Changes {
    Write-Host "`nProposed changes:" -ForegroundColor Cyan
    foreach ($change in $script:changes) {
        $color = switch ($change.Type) {
            "Create" { "Green" }
            "Modify" { "Yellow" }
            "Delete" { "Red" }
            default { "White" }
        }
        Write-Host "[$($change.Type)] $($change.Path)" -ForegroundColor $color
        Write-Host "  $($change.Description)" -ForegroundColor Gray
    }
}

function Backup-File {
    param([string]$path)
    
    if ($SkipBackup -or $PreviewOnly) {
        return
    }
    
    if (Test-Path $path) {
        $timestamp = Get-Date -Format "yyyyMMddHHmmss"
        $backupPath = "${path}.${timestamp}.bak"
        Copy-Item $path $backupPath
        $script:backupFiles[$path] = $backupPath
        Write-Verbose "Backed up $path to $backupPath"
    }
}

function Restore-Backups {
    if ($PreviewOnly) {
        return
    }
    foreach ($file in $script:backupFiles.Keys) {
        $backupPath = $script:backupFiles[$file]
        if (Test-Path $backupPath) {
            Copy-Item $backupPath $file -Force
            Remove-Item $backupPath
            Write-Verbose "Restored $file from $backupPath"
        }
    }
}

function Update-DbContext {
    param(
        [string]$dbContextPath,
        [string]$entityType,
        [string]$dbSetName
    )
    
    if (-not $UpdateDbContext) {
        return
    }
    
    try {
        $content = Get-Content $dbContextPath -Raw
        if ($content -match "DbSet<$entityType>") {
            Write-Warning "DbSet for $entityType already exists in DbContext"
            return
        }
        
        $insertPoint = $content.LastIndexOf("}")
        $dbSetDefinition = "    public DbSet<$entityType> $dbSetName { get; set; }`r`n"
        
        if ($PreviewOnly) {
            Add-Change -Type "Modify" -Path $dbContextPath -Description "Add DbSet<$entityType> $dbSetName"
            return
        }
        
        $newContent = $content.Insert($insertPoint - 1, $dbSetDefinition)
        Set-Content -Path $dbContextPath -Value $newContent
        Write-Host "Added DbSet<$entityType> to DbContext" -ForegroundColor Green
    }
    catch {
        Write-Error "Failed to update DbContext: $_"
        throw
    }
}

function Update-AutoMapperProfile {
    param(
        [string]$profilePath,
        [string]$entityType
    )
    
    if (-not $UpdateAutoMapper) {
        return
    }
    
    try {
        $content = Get-Content $profilePath -Raw
        if ($content -match "CreateMap<$entityType,") {
            Write-Warning "AutoMapper configuration for $entityType already exists"
            return
        }
        
        $insertPoint = $content.LastIndexOf("}")
        $mapConfig = @"
            CreateMap<$entityType, ${entityType}Dto>();
            CreateMap<Create${entityType}Dto, $entityType>();
            CreateMap<Update${entityType}Dto, $entityType>();
"@
        
        if ($PreviewOnly) {
            Add-Change -Type "Modify" -Path $profilePath -Description "Add AutoMapper configuration for $entityType"
            return
        }
        
        $newContent = $content.Insert($insertPoint - 1, "            " + $mapConfig + "`r`n")
        Set-Content -Path $profilePath -Value $newContent
        Write-Host "Added AutoMapper configuration for $entityType" -ForegroundColor Green
    }
    catch {
        Write-Error "Failed to update AutoMapper profile: $_"
        throw
    }
}

function Update-MenuContributor {
    param(
        [string]$menuContributorPath,
        [string]$moduleName,
        [string]$entityName
    )
    
    if (-not $UpdateMenu) {
        return
    }
    
    try {
        $content = Get-Content $menuContributorPath -Raw
        if ($content -match "$moduleName.$entityName") {
            Write-Warning "Menu item for $moduleName.$entityName already exists"
            return
        }
        
        $insertPoint = $content.LastIndexOf("await ConfigureMainMenuAsync(context);")
        $menuItem = @"
            context.Menu.AddItem(
                new ApplicationMenuItem(
                    "$moduleName.$entityName",
                    l["Menu:$entityName"],
                    "/$moduleName/${entityName}s",
                    icon: "fas fa-list"
                )
            );
"@
        
        if ($PreviewOnly) {
            Add-Change -Type "Modify" -Path $menuContributorPath -Description "Add menu item for $entityName"
            return
        }
        
        $newContent = $content.Insert($insertPoint + 40, "`r`n            " + $menuItem)
        Set-Content -Path $menuContributorPath -Value $newContent
        Write-Host "Added menu item for $entityName" -ForegroundColor Green
    }
    catch {
        Write-Error "Failed to update menu contributor: $_"
        throw
    }
}

function Test-Templates {
    param(
        [string]$templateDir
    )
    
    $requiredTemplates = @(
        "domain/entity-template.cs",
        "domain/repository-interface-template.cs",
        "infrastructure/repository-template.cs",
        "application/dtos-template.cs",
        "application/app-service-interface-template.cs",
        "application/app-service-template.cs",
        "application/auto-mapper-profile-template.cs",
        "application/permissions-template.cs",
        "presentation/index-page-template.cshtml",
        "presentation/index-page-model-template.cs",
        "presentation/index-js-template.js",
        "presentation/create-modal-template.cshtml",
        "presentation/create-modal-model-template.cs",
        "presentation/edit-modal-template.cshtml",
        "presentation/edit-modal-model-template.cs",
        "localization/localization-template.json"
    )
    
    $missingTemplates = @()
    foreach ($template in $requiredTemplates) {
        $templatePath = Join-Path $templateDir $template
        if (-not (Test-Path $templatePath)) {
            $missingTemplates += $template
        }
    }
    
    if ($missingTemplates.Count -gt 0) {
        throw "Missing required templates:`n$($missingTemplates -join "`n")"
    }
}

function New-Migration {
    param(
        [string]$name
    )
    
    if (-not $CreateMigration) {
        return
    }
    
    try {
        if ($PreviewOnly) {
            Add-Change -Type "Create" -Path "Migrations" -Description "Create new migration: $name"
            return
        }
        
        $migrationName = "${EntityName}_${name}"
        $command = "dotnet ef migrations add $migrationName --project src/Aqt.CoreOracle.EntityFrameworkCore --startup-project src/Aqt.CoreOracle.Web"
        Write-Host "Running: $command" -ForegroundColor Yellow
        Invoke-Expression $command
        
        Write-Host "Migration $migrationName created successfully" -ForegroundColor Green
    }
    catch {
        Write-Error "Failed to create migration: $_"
        throw
    }
}

function Copy-AndReplaceTemplate {
    param(
        [string]$templatePath,
        [string]$targetPath
    )
    
    if (-not (Test-Path $templatePath)) {
        throw "Template file not found: $templatePath"
    }
    
    if ((Test-Path $targetPath) -and -not $Force) {
        throw "Target file already exists: $targetPath. Use -Force to overwrite."
    }
    
    try {
        Backup-File $targetPath
        
        $content = Get-Content $templatePath -Raw
        $content = $content.Replace("[ModuleName]", $ModuleName)
        $content = $content.Replace("[EntityName]", $EntityName)
        $content = $content.Replace("[entityName]", $entityName)
        $content = $content.Replace("[moduleName]", $moduleName)
        $content = $content.Replace("[EntityDescription]", $Description)
        
        if ($PreviewOnly) {
            $action = (Test-Path $targetPath) ? "Modify" : "Create"
            Add-Change -Type $action -Path $targetPath -Description "Generate from template: $templatePath"
            return
        }
        
        # Tạo thư mục cha nếu chưa tồn tại
        $targetDir = Split-Path -Parent $targetPath
        if (-not (Test-Path $targetDir)) {
            New-Item -ItemType Directory -Path $targetDir -Force | Out-Null
        }
        
        Set-Content -Path $targetPath -Value $content -Force
        Write-Verbose "Generated $targetPath"
    }
    catch {
        Write-Error "Failed to generate $targetPath : $_"
        throw
    }
}

function Update-Permissions {
    param(
        [string]$permissionsPath,
        [string]$moduleName,
        [string]$entityName
    )
    
    if (-not $UpdatePermissions) {
        return
    }
    
    try {
        $content = Get-Content $permissionsPath -Raw
        if ($content -match "$moduleName.$entityName") {
            Write-Warning "Permissions for $moduleName.$entityName already exist"
            return
        }
        
        $insertPoint = $content.LastIndexOf("}")
        $permissionDefinitions = @"
            var ${entityName}Permission = ${moduleName}Group.AddPermission(
                name: "$moduleName.$entityName",
                displayName: L["Permission:${entityName}Management"]);

            ${entityName}Permission.AddChild(
                name: "$moduleName.$entityName.Create",
                displayName: L["Permission:${entityName}.Create"]);

            ${entityName}Permission.AddChild(
                name: "$moduleName.$entityName.Edit",
                displayName: L["Permission:${entityName}.Edit"]);

            ${entityName}Permission.AddChild(
                name: "$moduleName.$entityName.Delete",
                displayName: L["Permission:${entityName}.Delete"]);

            ${entityName}Permission.AddChild(
                name: "$moduleName.$entityName.View",
                displayName: L["Permission:${entityName}.View"]);
"@
        
        if ($PreviewOnly) {
            Add-Change -Type "Modify" -Path $permissionsPath -Description "Add permissions for $entityName"
            return
        }
        
        $newContent = $content.Insert($insertPoint - 1, "            " + $permissionDefinitions + "`r`n")
        Set-Content -Path $permissionsPath -Value $newContent
        Write-Host "Added permissions for $entityName" -ForegroundColor Green
    }
    catch {
        Write-Error "Failed to update permissions: $_"
        throw
    }
}

function Update-NavigationProperties {
    param(
        [string]$entityPath,
        [string[]]$properties
    )
    
    if (-not $UpdateNavigation -or -not $properties) {
        return
    }
    
    try {
        $content = Get-Content $entityPath -Raw
        
        $navigationProperties = ""
        foreach ($prop in $properties) {
            $propType = $prop.Split(":")[0]
            $propName = $prop.Split(":")[1]
            
            $navigationProperty = @"
        public virtual $propType $propName { get; set; }
"@
            $navigationProperties += $navigationProperty + "`r`n"
        }
        
        if ($PreviewOnly) {
            Add-Change -Type "Modify" -Path $entityPath -Description "Add navigation properties: $($properties -join ', ')"
            return
        }
        
        $insertPoint = $content.LastIndexOf("}")
        $newContent = $content.Insert($insertPoint - 1, $navigationProperties)
        Set-Content -Path $entityPath -Value $newContent
        Write-Host "Added navigation properties to $EntityName" -ForegroundColor Green
    }
    catch {
        Write-Error "Failed to update navigation properties: $_"
        throw
    }
}

function Update-SeedData {
    param(
        [string]$dataSeederPath,
        [hashtable]$seedData
    )
    
    if (-not $UpdateSeedData -or -not $seedData) {
        return
    }
    
    try {
        $content = Get-Content $dataSeederPath -Raw
        
        $seedMethod = @"
        private async Task Seed${EntityName}Data(IServiceProvider serviceProvider)
        {
            var ${entityName}Repository = serviceProvider.GetRequiredService<IRepository<$EntityName, Guid>>();

            if (await ${entityName}Repository.GetCountAsync() > 0)
            {
                return;
            }

            await ${entityName}Repository.InsertManyAsync(
                new List<$EntityName>
                {
"@
        foreach ($item in $seedData.GetEnumerator()) {
            $seedMethod += @"
                    new $EntityName(
                        id: Guid.NewGuid(),
                        $($item.Value -join ",`r`n                        ")
                    ),
"@
        }
        
        $seedMethod += @"
                },
                autoSave: true
            );
        }
"@
        
        if ($PreviewOnly) {
            Add-Change -Type "Modify" -Path $dataSeederPath -Description "Add seed data for $EntityName"
            return
        }
        
        $insertPoint = $content.LastIndexOf("}")
        $newContent = $content.Insert($insertPoint - 1, $seedMethod)
        Set-Content -Path $dataSeederPath -Value $newContent
        Write-Host "Added seed data for $EntityName" -ForegroundColor Green
    }
    catch {
        Write-Error "Failed to update seed data: $_"
        throw
    }
}

function Update-Documentation {
    param(
        [string]$docsPath,
        [string]$moduleName,
        [string]$entityName,
        [string]$description
    )
    
    if (-not $UpdateDocs) {
        return
    }
    
    try {
        $docContent = @"
# $entityName Management

## Overview

$description

## Features

- List all ${entityName}s with paging, sorting and filtering
- Create new $entityName
- Edit existing $entityName
- Delete $entityName
- View $entityName details

## Permissions

- `$moduleName.$entityName.Create`: Create new ${entityName}s
- `$moduleName.$entityName.Edit`: Edit existing ${entityName}s
- `$moduleName.$entityName.Delete`: Delete ${entityName}s
- `$moduleName.$entityName.View`: View ${entityName}s

## API Endpoints

### GET /api/$moduleName/${entityName}s

Gets a list of ${entityName}s with paging, sorting and filtering support.

### GET /api/$moduleName/${entityName}s/{id}

Gets a specific $entityName by ID.

### POST /api/$moduleName/${entityName}s

Creates a new $entityName.

### PUT /api/$moduleName/${entityName}s/{id}

Updates an existing $entityName.

### DELETE /api/$moduleName/${entityName}s/{id}

Deletes a $entityName.

## Database Schema

\`\`\`sql
CREATE TABLE ${entityName}s (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    CreationTime DATETIME2,
    CreatorId UNIQUEIDENTIFIER,
    LastModificationTime DATETIME2,
    LastModifierId UNIQUEIDENTIFIER,
    IsDeleted BIT,
    DeleterId UNIQUEIDENTIFIER,
    DeletionTime DATETIME2
    -- Add other properties here
);
\`\`\`

## User Interface

The $entityName management UI is located at \`/$moduleName/${entityName}s\`.
"@

        if ($PreviewOnly) {
            Add-Change -Type "Create" -Path "$docsPath/$moduleName/${entityName}s.md" -Description "Create documentation for $entityName"
            return
        }
        
        $docDir = "$docsPath/$moduleName"
        if (-not (Test-Path $docDir)) {
            New-Item -ItemType Directory -Path $docDir -Force | Out-Null
        }
        
        Set-Content -Path "$docDir/${entityName}s.md" -Value $docContent
        Write-Host "Created documentation for $entityName" -ForegroundColor Green
    }
    catch {
        Write-Error "Failed to update documentation: $_"
        throw
    }
}

try {
    # Validate input
    if (-not (Test-ValidIdentifier $ModuleName)) {
        throw "Module name must be a valid C# identifier"
    }
    if (-not (Test-ValidIdentifier $EntityName)) {
        throw "Entity name must be a valid C# identifier"
    }

    $ModuleName = Convert-ToPascalCase $ModuleName
    $EntityName = Convert-ToPascalCase $EntityName
    $entityName = Convert-ToCamelCase $EntityName
    $moduleName = Convert-ToCamelCase $ModuleName

    # Đường dẫn template
    $templateRoot = if ($CustomTemplateDir) { $CustomTemplateDir } else { "docs/templates/code-templates" }
    $projectRoot = "src/Aqt.CoreOracle"

    # Validate templates
    if ($ValidateOnly) {
        Test-Templates $templateRoot
        Write-Host "All required templates are present" -ForegroundColor Green
        return
    }

    # Kiểm tra thư mục template tồn tại
    if (-not (Test-Path $templateRoot)) {
        throw "Template directory not found: $templateRoot"
    }

    # Tạo thư mục
    $directories = @(
        "$projectRoot.Domain/$ModuleName",
        "$projectRoot.Domain.Shared/$ModuleName",
        "$projectRoot.Application/$ModuleName",
        "$projectRoot.Application.Contracts/$ModuleName",
        "$projectRoot.EntityFrameworkCore/$ModuleName",
        "$projectRoot.Web/Pages/$ModuleName/${EntityName}s"
    )

    foreach ($dir in $directories) {
        if (-not (Test-Path $dir)) {
            if ($PreviewOnly) {
                Add-Change -Type "Create" -Path $dir -Description "Create directory"
            } else {
                New-Item -ItemType Directory -Path $dir -Force | Out-Null
                Write-Verbose "Created directory: $dir"
            }
        }
    }

    # Generate Domain Layer
    Copy-AndReplaceTemplate "$templateRoot/domain/entity-template.cs" "$projectRoot.Domain/$ModuleName/$EntityName.cs"
    Copy-AndReplaceTemplate "$templateRoot/domain/repository-interface-template.cs" "$projectRoot.Domain/$ModuleName/I${EntityName}Repository.cs"

    # Generate Infrastructure Layer
    Copy-AndReplaceTemplate "$templateRoot/infrastructure/repository-template.cs" "$projectRoot.EntityFrameworkCore/$ModuleName/EfCore${EntityName}Repository.cs"

    # Generate Application Layer
    Copy-AndReplaceTemplate "$templateRoot/application/dtos-template.cs" "$projectRoot.Application.Contracts/$ModuleName/Dtos/${EntityName}Dtos.cs"
    Copy-AndReplaceTemplate "$templateRoot/application/app-service-interface-template.cs" "$projectRoot.Application.Contracts/$ModuleName/I${EntityName}AppService.cs"
    Copy-AndReplaceTemplate "$templateRoot/application/app-service-template.cs" "$projectRoot.Application/$ModuleName/${EntityName}AppService.cs"
    Copy-AndReplaceTemplate "$templateRoot/application/auto-mapper-profile-template.cs" "$projectRoot.Application/$ModuleName/${EntityName}AutoMapperProfile.cs"
    Copy-AndReplaceTemplate "$templateRoot/application/permissions-template.cs" "$projectRoot.Application.Contracts/$ModuleName/${ModuleName}Permissions.cs"

    # Generate Presentation Layer
    Copy-AndReplaceTemplate "$templateRoot/presentation/index-page-template.cshtml" "$projectRoot.Web/Pages/$ModuleName/${EntityName}s/Index.cshtml"
    Copy-AndReplaceTemplate "$templateRoot/presentation/index-page-model-template.cs" "$projectRoot.Web/Pages/$ModuleName/${EntityName}s/Index.cshtml.cs"
    Copy-AndReplaceTemplate "$templateRoot/presentation/index-js-template.js" "$projectRoot.Web/Pages/$ModuleName/${EntityName}s/index.js"
    Copy-AndReplaceTemplate "$templateRoot/presentation/create-modal-template.cshtml" "$projectRoot.Web/Pages/$ModuleName/${EntityName}s/CreateModal.cshtml"
    Copy-AndReplaceTemplate "$templateRoot/presentation/create-modal-model-template.cs" "$projectRoot.Web/Pages/$ModuleName/${EntityName}s/CreateModal.cshtml.cs"
    Copy-AndReplaceTemplate "$templateRoot/presentation/edit-modal-template.cshtml" "$projectRoot.Web/Pages/$ModuleName/${EntityName}s/EditModal.cshtml"
    Copy-AndReplaceTemplate "$templateRoot/presentation/edit-modal-model-template.cs" "$projectRoot.Web/Pages/$ModuleName/${EntityName}s/EditModal.cshtml.cs"

    # Generate Localization
    $localizationDir = "$projectRoot.Domain.Shared/Localization/CoreOracle/vi.json"
    if (-not (Test-Path $localizationDir)) {
        throw "Localization file not found: $localizationDir"
    }

    if (-not $PreviewOnly) {
        Backup-File $localizationDir
        $localization = Get-Content $localizationDir -Raw | ConvertFrom-Json
        $templateLocalization = Get-Content "$templateRoot/localization/localization-template.json" -Raw | ConvertFrom-Json

        # Merge localization
        foreach ($key in $templateLocalization.texts.PSObject.Properties) {
            $value = $key.Value.Replace("[ModuleName]", $ModuleName).Replace("[EntityName]", $EntityName)
            $localization.texts | Add-Member -MemberType NoteProperty -Name $key.Name.Replace("[ModuleName]", $ModuleName).Replace("[EntityName]", $EntityName) -Value $value -Force
        }

        $localization | ConvertTo-Json -Depth 10 | Set-Content $localizationDir
    } else {
        Add-Change -Type "Modify" -Path $localizationDir -Description "Add localization entries for $EntityName"
    }

    # Update DbContext if requested
    if ($UpdateDbContext) {
        $dbContextPath = "$projectRoot.EntityFrameworkCore/CoreOracleDbContext.cs"
        Update-DbContext -dbContextPath $dbContextPath -entityType $EntityName -dbSetName "${EntityName}s"
    }

    # Update AutoMapper if requested
    if ($UpdateAutoMapper) {
        $profilePath = "$projectRoot.Application/CoreOracleApplicationAutoMapperProfile.cs"
        Update-AutoMapperProfile -profilePath $profilePath -entityType $EntityName
    }

    # Update Menu if requested
    if ($UpdateMenu) {
        $menuContributorPath = "$projectRoot.Web/Menus/CoreOracleMenuContributor.cs"
        Update-MenuContributor -menuContributorPath $menuContributorPath -moduleName $ModuleName -entityName $EntityName
    }

    # Update permissions if requested
    if ($UpdatePermissions) {
        $permissionsPath = "$projectRoot.Application.Contracts/$ModuleName/${ModuleName}PermissionDefinitionProvider.cs"
        Update-Permissions -permissionsPath $permissionsPath -moduleName $ModuleName -entityName $EntityName
    }

    # Update navigation properties if requested
    if ($UpdateNavigation) {
        $entityPath = "$projectRoot.Domain/$ModuleName/$EntityName.cs"
        Update-NavigationProperties -entityPath $entityPath -properties $NavigationProperties
    }

    # Update seed data if requested
    if ($UpdateSeedData) {
        $dataSeederPath = "$projectRoot.Domain/$ModuleName/${ModuleName}DataSeederContributor.cs"
        Update-SeedData -dataSeederPath $dataSeederPath -seedData $SeedData
    }

    # Update documentation if requested
    if ($UpdateDocs) {
        $docsPath = "docs/modules"
        Update-Documentation -docsPath $docsPath -moduleName $ModuleName -entityName $EntityName -description $Description
    }

    # Create migration if requested
    if ($CreateMigration) {
        New-Migration -name "Add${EntityName}Entity"
    }

    if ($PreviewOnly) {
        Show-Changes
        Write-Host "`nThis was a preview. No changes were made." -ForegroundColor Yellow
        return
    }

    Write-Host "Code generation completed successfully!" -ForegroundColor Green
    Write-Host "Generated files:"
    Get-ChildItem -Recurse -Path $directories | Where-Object { !$_.PSIsContainer } | ForEach-Object {
        Write-Host "  - $($_.FullName)" -ForegroundColor Yellow
    }

    Write-Host "`nNext steps:" -ForegroundColor Cyan
    if (-not $UpdateDbContext) {
        Write-Host "1. Add DbSet to CoreOracleDbContext"
    }
    if (-not $UpdateAutoMapper) {
        Write-Host "2. Register AutoMapper Profile in CoreOracleApplicationAutoMapperProfile"
    }
    if (-not $UpdateMenu) {
        Write-Host "3. Add menu item in CoreOracleMenuContributor"
    }
    if (-not $UpdatePermissions) {
        Write-Host "4. Add permissions to PermissionDefinitionProvider"
    }
    if (-not $UpdateNavigation) {
        Write-Host "5. Add navigation properties to entity"
    }
    if (-not $UpdateSeedData) {
        Write-Host "6. Add seed data if needed"
    }
    if (-not $UpdateDocs) {
        Write-Host "7. Add documentation"
    }
    if (-not $CreateMigration) {
        Write-Host "8. Run Add-Migration and Update-Database"
    } else {
        Write-Host "8. Run Update-Database"
    }
    Write-Host "9. Test the new feature"

}
catch {
    Write-Error "An error occurred during code generation: $_"
    Write-Host "Rolling back changes..." -ForegroundColor Yellow
    Restore-Backups
    throw
} 