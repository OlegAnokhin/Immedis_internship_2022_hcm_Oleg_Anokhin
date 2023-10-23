namespace HumanCapitalManagementApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class EmployeeEntityConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);

            builder
                .HasOne(e => e.Position)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PositionId);

            builder
                .HasMany(e => e.EmployeeInfos)
                .WithOne(ei => ei.Employee)
                .HasForeignKey(ei => ei.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(e => e.HireDate)
                .HasDefaultValue(DateTime.Now);

            builder
                .Property(p => p.Salary)
                .HasPrecision(18, 2);
        }
    }
}