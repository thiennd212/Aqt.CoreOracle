using Volo.Abp.Modularity;

namespace Aqt.CoreOracle;

public abstract class CoreOracleApplicationTestBase<TStartupModule> : CoreOracleTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
