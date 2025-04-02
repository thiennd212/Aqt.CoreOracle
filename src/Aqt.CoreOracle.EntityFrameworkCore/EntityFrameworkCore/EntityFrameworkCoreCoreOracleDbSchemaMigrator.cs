using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Aqt.CoreOracle.Data;
using Volo.Abp.DependencyInjection;

namespace Aqt.CoreOracle.EntityFrameworkCore;

public class EntityFrameworkCoreCoreOracleDbSchemaMigrator
    : ICoreOracleDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreCoreOracleDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the CoreOracleDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<CoreOracleDbContext>()
            .Database
            .MigrateAsync();
    }
}
