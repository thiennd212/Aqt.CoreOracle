using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Aqt.CoreOracle.Data;

/* This is used if database provider does't define
 * ICoreOracleDbSchemaMigrator implementation.
 */
public class NullCoreOracleDbSchemaMigrator : ICoreOracleDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
