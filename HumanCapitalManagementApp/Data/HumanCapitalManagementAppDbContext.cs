using HumanCapitalManagementApp.Data.Configuration;

namespace HumanCapitalManagementApp.Data
{
    using Microsoft.EntityFrameworkCore;

    using Models;

    public class HumanCapitalManagementAppDbContext : DbContext
    {
        public HumanCapitalManagementAppDbContext(DbContextOptions<HumanCapitalManagementAppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DepartmentEntityConfiguration());

            builder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);

            builder.Entity<Employee>()
                .HasOne(e => e.Position)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PositionId);

            builder.Entity<Employee>()
                .HasOne(e => e.EmployeeInfo)
                .WithOne()
                .HasForeignKey<Employee>(e => e.EmployeeInfoId);

            builder.Entity<QualificationTraining>()
                .HasOne(qt => qt.Employee)
                .WithMany(e => e.QualificationsTraining)
                .HasForeignKey(qt => qt.EmployeeId);

            builder.Entity<Employee>()
                .Property(e => e.HireDate)
                .HasDefaultValueSql("GETDATE()");

            builder.Entity<Employee>()
                .Property(p => p.Salary)
                .HasPrecision(18, 2);
            
            builder.Entity<EmployeeInfo>()
                .Property(p => p.Salary)
                .HasPrecision(18, 2);

            builder.Entity<LeaveRequest>()
                .HasOne(lr => lr.Employee)
                .WithMany(e => e.LeaveRequests)
                .HasForeignKey(lr => lr.EmployeeId);

            builder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.Entity<UserRole>()
                .HasOne(er => er.Employee)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(er => er.UserId);

            builder.Entity<UserRole>()
                .HasOne(er => er.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(er => er.RoleId);

            builder.Entity<PerformanceManagement>()
                .HasOne(pm => pm.Employee)
                .WithMany(e => e.PerformancesManagement)
                .HasForeignKey(pm => pm.EmployeeId);

            base.OnModelCreating(builder);
        }

        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<EmployeeInfo> EmployeesInfo { get; set; } = null!;
        public DbSet<LeaveRequest> LeaveRequests { get; set; } = null;
        public DbSet<Position> Positions { get; set; } = null!;
        public DbSet<QualificationTraining> QualificationsTraining { get; set; }
        public DbSet<PerformanceManagement> PerformanceManagement { get; set; }
        public DbSet<Role> Roles { get; set; } = null!;

    }
}