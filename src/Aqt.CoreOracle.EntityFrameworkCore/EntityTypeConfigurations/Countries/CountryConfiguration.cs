using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Aqt.CoreOracle.Domain.Countries.Entities;
using Aqt.CoreOracle.Domain.Shared.Countries; // Make sure CountryConsts is here
using Volo.Abp.EntityFrameworkCore.Modeling;
using Aqt.CoreOracle.Domain.Shared; // Assuming CoreOracleConsts is here

namespace Aqt.CoreOracle.EntityFrameworkCore.EntityTypeConfigurations.Countries;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        // Use constants for table name and schema if defined
        // Ensure CoreOracleConsts has DbTablePrefix and DbSchema defined
        builder.ToTable(CoreOracleConsts.DbTablePrefix + "Countries", CoreOracleConsts.DbSchema);
        builder.ConfigureByConvention(); // Apply ABP conventions

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(CountryConsts.MaxCodeLength)
            .HasColumnName(nameof(Country.Code)); // Explicit mapping
            
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(CountryConsts.MaxNameLength)
            .HasColumnName(nameof(Country.Name)); // Explicit mapping

        builder.HasIndex(x => x.Code).IsUnique(); // Ensure Code is unique in DB
    }
} 