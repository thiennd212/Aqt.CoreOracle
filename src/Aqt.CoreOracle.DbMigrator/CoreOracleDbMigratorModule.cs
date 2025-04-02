using Aqt.CoreOracle.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Aqt.CoreOracle.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CoreOracleEntityFrameworkCoreModule),
    typeof(CoreOracleApplicationContractsModule)
)]
public class CoreOracleDbMigratorModule : AbpModule
{
}
