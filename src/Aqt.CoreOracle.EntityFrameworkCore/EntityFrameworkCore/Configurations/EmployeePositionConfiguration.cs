using Aqt.CoreOracle.Domain.OrganizationStructure; // Corrected
using Aqt.CoreOracle.Domain.Positions; // Corrected
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity; // Added for FK

namespace Aqt.CoreOracle.EntityFrameworkCore.Configurations
{
    public class EmployeePositionConfiguration : IEntityTypeConfiguration<EmployeePosition>
    {
        public void Configure(EntityTypeBuilder<EmployeePosition> builder)
        {
            builder.ToTable(CoreOracleConsts.DbTablePrefix + "EmployeePositions", CoreOracleConsts.DbSchema);
            builder.ConfigureByConvention(); //auto configure for the base class props

            // Foreign Keys
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.OrganizationUnit).WithMany().HasForeignKey(x => x.OrganizationUnitId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Position).WithMany().HasForeignKey(x => x.PositionId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            // Properties
            builder.Property(x => x.StartDate).IsRequired();
            // EndDate is nullable by convention

            // Composite Index
            builder.HasIndex(x => new { x.UserId, x.OrganizationUnitId, x.PositionId }).IsUnique();
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.OrganizationUnitId);
            builder.HasIndex(x => x.PositionId);
        }
    }
}