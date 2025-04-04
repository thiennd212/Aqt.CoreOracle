using Volo.Abp.Modularity;

namespace Aqt.CoreOracle;

[DependsOn(
    typeof(CoreOracleDomainModule),
    typeof(CoreOracleApplicationModule),
    typeof(CoreOracleTestBaseModule)
)]
public class CoreOracleDomainTestModule : AbpModule
{

}
