namespace HumanCapitalManagementApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class PerformanceManagementEntityConfiguration : IEntityTypeConfiguration<PerformanceManagement>
    {
        public void Configure(EntityTypeBuilder<PerformanceManagement> builder)
        {
            builder
                .HasOne(pm => pm.Employee)
                .WithMany(e => e.PerformancesManagement)
                .HasForeignKey(pm => pm.EmployeeId);
        }
    }
}