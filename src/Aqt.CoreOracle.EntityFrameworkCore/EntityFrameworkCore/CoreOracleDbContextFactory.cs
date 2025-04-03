using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Aqt.CoreOracle.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class CoreOracleDbContextFactory : IDesignTimeDbContextFactory<CoreOracleDbContext>
{
    public CoreOracleDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        CoreOracleEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<CoreOracleDbContext>()
            .UseOracle(configuration.GetConnectionString("Default"), opt => opt.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion23));
        
        return new CoreOracleDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Aqt.CoreOracle.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
