namespace HumanCapitalManagementApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using Models;

    public class EmployeeInfoEntityConfiguration : IEntityTypeConfiguration<EmployeeInfo>
    {
        public void Configure(EntityTypeBuilder<EmployeeInfo> builder)
        {
            builder
                .HasOne(ei => ei.Employee)
                .WithMany(e => e.EmployeeInfos)
                .HasForeignKey(ei => ei.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(p => p.Salary)
                .HasPrecision(18, 2);
        }
    }
}