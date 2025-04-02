using Aqt.CoreOracle.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Aqt.CoreOracle.Web.Pages;

public abstract class CoreOraclePageModel : AbpPageModel
{
    protected CoreOraclePageModel()
    {
        LocalizationResourceType = typeof(CoreOracleResource);
    }
}
