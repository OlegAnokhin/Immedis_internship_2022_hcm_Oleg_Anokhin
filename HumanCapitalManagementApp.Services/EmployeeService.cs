namespace HumanCapitalManagementApp.Services
{

    using Microsoft.EntityFrameworkCore;
    using BCrypt.Net;

    using Data;
    using Data.Models;
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
                EmployeeId = employeeId,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
        }

        public async Task<EditEmployeeViewModel> TakeEmployeeForEditByIdAsync(int employeeId)
        {
            Employee emp = await dbContext
                .Employees
                .Include(p => p.Position)
                .Include(d => d.Department)
                .FirstAsync(e => e.Id == employeeId);

            return new EditEmployeeViewModel
            {
                UserName = emp.UserName,
                Password = emp.HashedPassword,
                ConfirmPassword = emp.HashedPassword,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Email = emp.Email,
                PhoneNumber = emp.PhoneNumber,
                Salary = emp.Salary,
                From = emp.HireDate,
                To = DateTime.Today,
                PositionId = emp.PositionId,
                DepartmentId = emp.DepartmentId,
                EmployeeId = employeeId
            };
        }

        public async Task<EmployeeInfoModel> TakeEmployeeInfoByIdAsync(int employeeId)
        {
            Employee emp = await dbContext
                .Employees
                .Include(p => p.Position)
                .Include(d => d.Department)
                .FirstAsync(e => e.Id == employeeId);

            return new EmployeeInfoModel
            {
                UserName = emp.UserName,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Email = emp.Email,
                PhoneNumber = emp.PhoneNumber,
                Position = emp.Position.Name,
                Department = emp.Department.Name,
                From = emp.HireDate,
                To = DateTime.UtcNow,
                Salary = emp.Salary,
                EmployeeId = employeeId
            };
        }

        public async Task EditEmployeeByIdAsync(int employeeId, EditEmployeeViewModel model)
        {
            string salt = BCrypt.GenerateSalt(12);
            string hashedPassword = BCrypt.HashPassword(model.Password, salt);

            Employee employee = await this.dbContext
                .Employees
                .FirstAsync(e => e.Id == employeeId);

            employee.UserName = model.UserName;
            employee.HashedPassword = hashedPassword;
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.Email = model.Email;
            employee.PhoneNumber = model.PhoneNumber;
            employee.HireDate = model.From;
            employee.PositionId = model.PositionId;
            employee.DepartmentId = model.DepartmentId;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistByIdAsync(int employeeId)
        {
            return await this.dbContext
                .Employees
                .AnyAsync(e => e.Id == employeeId);
        }

        public async Task SetIsHiredOnTrue(int id)
        {
            Employee employee = await this.dbContext
                .Employees
                .FirstAsync(e => e.Id == id);

            if (employee.IsHired == false)
            {
                employee.IsHired = true;
            }
            else if (employee.IsHired == true)
            {
                employee.IsHired = false;
            }

            await this.dbContext.SaveChangesAsync();
        }
    }
}