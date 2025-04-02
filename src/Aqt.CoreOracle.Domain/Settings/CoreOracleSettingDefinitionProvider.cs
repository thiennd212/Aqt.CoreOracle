using Volo.Abp.Settings;

namespace Aqt.CoreOracle.Settings;

public class CoreOracleSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(CoreOracleSettings.MySetting1));
    }
}
