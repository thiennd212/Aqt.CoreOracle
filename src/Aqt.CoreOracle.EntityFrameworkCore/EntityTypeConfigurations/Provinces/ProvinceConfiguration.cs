using Aqt.CoreOracle.Domain.Provinces.Entities;
using Aqt.CoreOracle.Domain.Shared;
using Aqt.CoreOracle.Domain.Shared.Provinces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Aqt.CoreOracle.EntityFrameworkCore.EntityTypeConfigurations.Provinces;

public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.ToTable(CoreOracleConsts.DbTablePrefix + "Provinces",
            CoreOracleConsts.DbSchema);
        builder.ConfigureByConvention();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(ProvinceConsts.MaxCodeLength)
            .HasColumnName(nameof(Province.Code));

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(ProvinceConsts.MaxNameLength)
            .HasColumnName(nameof(Province.Name));

        builder.Property(x => x.CountryId)
            .HasColumnName(nameof(Province.CountryId));

        builder.HasOne(x => x.Country)
            .WithMany()
            .HasForeignKey(x => x.CountryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.CountryId, x.Code })
            .IsUnique();

        builder.HasIndex(x => x.Name);
    }
} 