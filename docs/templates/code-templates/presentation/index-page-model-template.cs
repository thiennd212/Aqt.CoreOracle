using Microsoft.AspNetCore.Authorization;
using Aqt.CoreOracle.[ModuleName];

namespace Aqt.CoreOracle.Web.Pages.[ModuleName].[EntityName]s
{
    /// <summary>
    /// Page model for [EntityName]s index page
    /// </summary>
    [Authorize([ModuleName]Permissions.[EntityName]s.Default)]
    public class IndexModel : CoreOraclePageModel
    {
        public void OnGet()
        {
        }
    }
} 