using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Aqt.CoreOracle.Permissions;
using Aqt.CoreOracle.Localization;
using Aqt.CoreOracle.OrganizationUnits;
using Aqt.CoreOracle.Application.Positions;
using Aqt.CoreOracle.Application.Contracts.Positions;
using Volo.Abp.Identity;

namespace Aqt.CoreOracle.Web.Pages.OrganizationStructure;

// Apply authorization using the specific OrganizationUnits permission
[Authorize(CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Default)] // Assuming OrganizationUnits.Default exists
public class IndexModel : AbpPageModel
{
    private readonly ICustomOrganizationUnitAppService _organizationUnitAppService;
    private readonly IPositionAppService _positionAppService;
    private readonly IStringLocalizer<CoreOracleResource> _localizer;

    public string OuCreatePermission { get; }
    public string OuUpdatePermission { get; }
    public string OuDeletePermission { get; }
    public string AssignPositionPermission { get; }

    public IndexModel(
        ICustomOrganizationUnitAppService organizationUnitAppService,
        IPositionAppService positionAppService,
        IStringLocalizer<CoreOracleResource> localizer)
    {
        _organizationUnitAppService = organizationUnitAppService;
        _positionAppService = positionAppService;
        _localizer = localizer;

        // Định nghĩa các permissions - Sử dụng CoreOraclePermissions
        OuCreatePermission = CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Create;
        OuUpdatePermission = CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Update;
        OuDeletePermission = CoreOraclePermissions.OrganizationManagement.OrganizationUnits.Delete;
        AssignPositionPermission = CoreOraclePermissions.OrganizationManagement.AssignPositions;
    }

    public Task OnGetAsync()
    {
        // Không cần xử lý đặc biệt ở đây vì các dữ liệu sẽ được tải bằng AJAX
        return Task.CompletedTask;
    }
} 