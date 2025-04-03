using Aqt.CoreOracle.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Aqt.CoreOracle.EntityFrameworkCore.Categories;

public class CategoryTypeConfiguration : IEntityTypeConfiguration<CategoryType>
{
    public void Configure(EntityTypeBuilder<CategoryType> builder)
    {
        builder.ToTable(CoreOracleConsts.DbTablePrefix + "CategoryTypes", CoreOracleConsts.DbSchema);
        
        builder.ConfigureByConvention();

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.HasMany(x => x.Items)
            .WithOne(x => x.CategoryType)
            .HasForeignKey(x => x.CategoryTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
} 