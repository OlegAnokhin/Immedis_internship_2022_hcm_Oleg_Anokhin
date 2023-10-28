namespace HumanCapitalManagementApp.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    using Models;

    public class HumanCapitalManagementAppDbContext : DbContext
    {
        public HumanCapitalManagementAppDbContext(DbContextOptions<HumanCapitalManagementAppDbContext> options)
            : base(options)
        {
            if (!this.Database.IsRelational())
            {
                this.Database.EnsureCreated();
            }
        }

        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<PreviousPosition> PreviousPositions { get; set; } = null!;
        public DbSet<Position> Positions { get; set; } = null!;
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<QualificationTraining> QualificationsTraining { get; set; }
        public DbSet<PerformanceManagement> PerformanceManagement { get; set; }
        public DbSet<Role> Roles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(HumanCapitalManagementAppDbContext)) ??
                                      Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(configAssembly);

            base.OnModelCreating(builder);
        }
    }
}