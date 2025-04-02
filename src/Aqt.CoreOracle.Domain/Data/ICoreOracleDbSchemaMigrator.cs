using System.Threading.Tasks;

namespace Aqt.CoreOracle.Data;

public interface ICoreOracleDbSchemaMigrator
{
    Task MigrateAsync();
}
