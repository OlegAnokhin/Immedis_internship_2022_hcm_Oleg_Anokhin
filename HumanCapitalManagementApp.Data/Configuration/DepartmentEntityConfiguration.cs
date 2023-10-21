namespace HumanCapitalManagementApp.Data.Configuration
{

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    using Seeding;

    public class DepartmentEntityConfiguration : IEntityTypeConfiguration<Department>
    {
        private readonly DepartmentSeeder departmentSeeder;

        public DepartmentEntityConfiguration()
        {
            this.departmentSeeder = new DepartmentSeeder();
        }

        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasData(this.departmentSeeder.GenerateDepartments());
        }
    }
}