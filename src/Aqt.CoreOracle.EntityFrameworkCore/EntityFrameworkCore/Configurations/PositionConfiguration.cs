using Aqt.CoreOracle.Domain.Positions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Aqt.CoreOracle.EntityFrameworkCore.Configurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.ToTable(CoreOracleConsts.DbTablePrefix + "Positions", CoreOracleConsts.DbSchema);
            builder.ConfigureByConvention(); //auto configure for the base class props

            // Properties
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(CoreOracleConsts.MaxPositionNameLength);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(CoreOracleConsts.MaxPositionCodeLength);

            builder.Property(x => x.Description).HasMaxLength(CoreOracleConsts.MaxDescriptionLength); // Assuming MaxDescriptionLength exists

            // Indexes
            builder.HasIndex(x => x.Code);
        }
    }
} 