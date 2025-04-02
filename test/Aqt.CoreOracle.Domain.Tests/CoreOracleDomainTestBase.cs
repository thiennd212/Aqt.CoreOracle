using Volo.Abp.Modularity;

namespace Aqt.CoreOracle;

/* Inherit from this class for your domain layer tests. */
public abstract class CoreOracleDomainTestBase<TStartupModule> : CoreOracleTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
