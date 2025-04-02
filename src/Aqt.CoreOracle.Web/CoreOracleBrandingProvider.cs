using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using Aqt.CoreOracle.Localization;

namespace Aqt.CoreOracle.Web;

[Dependency(ReplaceServices = true)]
public class CoreOracleBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<CoreOracleResource> _localizer;

    public CoreOracleBrandingProvider(IStringLocalizer<CoreOracleResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
