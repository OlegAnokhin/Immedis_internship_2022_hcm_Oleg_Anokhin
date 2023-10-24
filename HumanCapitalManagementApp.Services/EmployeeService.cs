namespace HumanCapitalManagementApp.Services
{
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Interfaces;
    using ViewModels.Employee;

    public class EmployeeService : IEmployeeService
    {
        private readonly HumanCapitalManagementAppDbContext dbContext;

        public EmployeeService(HumanCapitalManagementAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<AllEmployeesViewModel>> ListAllEmployeesAsync()
        {
            IEnumerable<AllEmployeesViewModel> allEmployees = await dbContext
                .Employees
                .Select(e => new AllEmployeesViewModel()
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    IsHired = e.IsHired,
                    HireDate = e.HireDate,
                    Position = e.Position.Name,
                    Department = e.Department.Name
                })
                .ToListAsync();
            return allEmployees;
        }

        public async Task<SuccessLoginViewModel> TakeEmployeeByIdAsync(int employeeId)
        {
            var model = await dbContext
                .Employees
                .FirstAsync(e => e.Id == employeeId);

                return new SuccessLoginViewModel
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
        }
    }
}