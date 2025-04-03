using Microsoft.EntityFrameworkCore;
using Aqt.CoreOracle.EntityFrameworkCore.Categories;

namespace Aqt.CoreOracle.EntityFrameworkCore;

public static class CoreOracleDbContextModelCreatingExtensions
{
    public static ModelBuilder ApplyConfigurations(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CategoryTypeConfiguration());
        builder.ApplyConfiguration(new CategoryItemConfiguration());
        return builder;
    }
} 