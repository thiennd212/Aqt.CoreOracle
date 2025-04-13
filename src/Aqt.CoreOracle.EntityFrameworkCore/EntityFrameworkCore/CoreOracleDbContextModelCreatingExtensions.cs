using Microsoft.EntityFrameworkCore;
using Aqt.CoreOracle.EntityFrameworkCore.EntityTypeConfigurations.Countries;

namespace Aqt.CoreOracle.EntityFrameworkCore;

public static class CoreOracleDbContextModelCreatingExtensions
{
    public static ModelBuilder ApplyConfigurations(this ModelBuilder builder)
    {
        return builder;
    }
} 