using Aqt.CoreOracle.Localization;
using Volo.Abp.Application.Services;

namespace Aqt.CoreOracle;

/* Inherit your application services from this class.
 */
public abstract class CoreOracleAppService : ApplicationService
{
    protected CoreOracleAppService()
    {
        LocalizationResource = typeof(CoreOracleResource);
    }
}
