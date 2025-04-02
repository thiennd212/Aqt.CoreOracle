using Volo.Abp.Modularity;

namespace Aqt.CoreOracle;

[DependsOn(
    typeof(CoreOracleDomainModule),
    typeof(CoreOracleTestBaseModule)
)]
public class CoreOracleDomainTestModule : AbpModule
{

}
