using Microsoft.AspNetCore.Authorization;
using Aqt.CoreOracle.Permissions;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Aqt.CoreOracle.Web.Pages.Categories.CategoryTypes;

[Authorize(CoreOraclePermissions.CategoryTypes.Default)]
public class IndexModel : CoreOraclePageModel
{
    public void OnGet()
    {
    }
} 