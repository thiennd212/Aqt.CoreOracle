using Aqt.CoreOracle.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Aqt.CoreOracle.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class CoreOracleController : AbpControllerBase
{
    protected CoreOracleController()
    {
        LocalizationResource = typeof(CoreOracleResource);
    }
}
