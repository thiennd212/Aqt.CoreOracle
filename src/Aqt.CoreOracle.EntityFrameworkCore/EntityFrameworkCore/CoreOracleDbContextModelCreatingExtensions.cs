using Microsoft.EntityFrameworkCore;

namespace Aqt.CoreOracle.EntityFrameworkCore;

public static class CoreOracleDbContextModelCreatingExtensions
{
    public static ModelBuilder ApplyConfigurations(this ModelBuilder builder)
    {
        return builder;
    }
} 