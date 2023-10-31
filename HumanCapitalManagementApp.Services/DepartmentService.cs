namespace HumanCapitalManagementApp.Services
{
    using Microsoft.EntityFrameworkCore;

    using Interfaces;
    using ViewModels.Employee;
    using HumanCapitalManagementApp.Data;

    public class DepartmentService : IDepartmentService
    {
        private readonly HumanCapitalManagementAppDbContext dbContext;

        public DepartmentService(HumanCapitalManagementAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AllDepartmentsModel>> AllDepartmentsAsync()
        {
            IEnumerable<AllDepartmentsModel> allDepartments = await this.dbContext
                .Departments
                .AsNoTracking()
                .Select(d => new AllDepartmentsModel()
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .ToArrayAsync();
            return allDepartments;
        }

        public async Task<IEnumerable<string>> AllDepartmentNamesAsync()
        {
            IEnumerable<string> allNames = await this.dbContext
                .Departments
                .Select(d => d.Name)
                .ToArrayAsync();
            return allNames;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            bool result = await this.dbContext
                .Departments
                .AnyAsync(d => d.Id == id);
            return result;
        }
    }
}