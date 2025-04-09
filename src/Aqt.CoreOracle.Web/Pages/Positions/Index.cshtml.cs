using System.Threading.Tasks;
using Aqt.CoreOracle.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace Aqt.CoreOracle.Web.Pages.Positions;

[Authorize(CoreOraclePermissions.OrganizationManagement.Positions.Default)]
public class IndexModel : AbpPageModel
{
    // Define a model to hold permission values
    public PermissionsViewModel Permissions { get; private set; }

    // IPositionAppService sẽ được inject sau này khi cần lấy dữ liệu cho bảng
    // private readonly IPositionAppService _positionAppService;

    // public IndexModel(IPositionAppService positionAppService)
    // {
    //     _positionAppService = positionAppService;
    // }

    public Task OnGetAsync()
    {
        // Assign permission values from C# constants
        Permissions = new PermissionsViewModel
        {
            Create = CoreOraclePermissions.OrganizationManagement.Positions.Create,
            Update = CoreOraclePermissions.OrganizationManagement.Positions.Update,
            Delete = CoreOraclePermissions.OrganizationManagement.Positions.Delete
        };
        return Task.CompletedTask;
    }

    // Logic xử lý xóa sẽ được thêm vào khi cần
    // public async Task<IActionResult> OnPostDeleteAsync(Guid id)
    // {
    //     await CheckPolicyAsync(Permissions.Delete); // Use property here too
    //     await _positionAppService.DeleteAsync(id);
    //     return NoContent();
    // }

    // Inner class for the permissions model
    public class PermissionsViewModel
    {
        public string Create { get; set; }
        public string Update { get; set; }
        public string Delete { get; set; }
    }
} 