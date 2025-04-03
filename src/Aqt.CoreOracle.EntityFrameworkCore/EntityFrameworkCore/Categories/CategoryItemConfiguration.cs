using Aqt.CoreOracle.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Aqt.CoreOracle.EntityFrameworkCore.Categories;

public class CategoryItemConfiguration : IEntityTypeConfiguration<CategoryItem>
{
    public void Configure(EntityTypeBuilder<CategoryItem> builder)
    {
        builder.ToTable(CoreOracleConsts.DbTablePrefix + "CategoryItems", CoreOracleConsts.DbSchema);
        
        builder.ConfigureByConvention();

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Value)
            .HasMaxLength(500);

        builder.Property(x => x.Icon)
            .HasMaxLength(100);

        builder.Property(x => x.ExtraProperties)
            .HasMaxLength(2000);

        builder.HasIndex(x => new { x.CategoryTypeId, x.Code })
            .IsUnique();

        builder.HasIndex(x => x.ParentId);

        builder.HasOne(x => x.Parent)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
} 