namespace HumanCapitalManagementApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using Models;

    public class PreviousPositionEntityConfiguration : IEntityTypeConfiguration<PreviousPosition>
    {
        public void Configure(EntityTypeBuilder<PreviousPosition> builder)
        {
            builder
                .HasOne(ei => ei.Employee)
                .WithMany(e => e.PreviousPositions)
                .HasForeignKey(ei => ei.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(p => p.Salary)
                .HasPrecision(18, 2);
        }
    }
}