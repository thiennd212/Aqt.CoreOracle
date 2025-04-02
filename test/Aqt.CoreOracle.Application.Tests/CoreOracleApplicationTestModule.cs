using Volo.Abp.Modularity;

namespace Aqt.CoreOracle;

[DependsOn(
    typeof(CoreOracleApplicationModule),
    typeof(CoreOracleDomainTestModule)
)]
public class CoreOracleApplicationTestModule : AbpModule
{

}
