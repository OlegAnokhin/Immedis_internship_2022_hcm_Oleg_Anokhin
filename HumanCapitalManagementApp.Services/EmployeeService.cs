using HumanCapitalManagementApp.ViewModels.Employee.Enums;

namespace HumanCapitalManagementApp.Services
{
    using Microsoft.EntityFrameworkCore;
    using BCrypt.Net;

    using Data.Models;
    using Interfaces;
    using ViewModels.Employee;
    using HumanCapitalManagementApp.Data;
    using HumanCapitalManagementApp.Data.Models;

    public class EmployeeService : IEmployeeService
    {
        private readonly HumanCapitalManagementAppDbContext dbContext;

        public EmployeeService(HumanCapitalManagementAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //public async Task<IEnumerable<AllEmployeesViewModel>> ListAllEmployeesAsync()
        //{
        //    IEnumerable<AllEmployeesViewModel> allEmployees = await dbContext
        //        .Employees
        //        .Select(e => new AllEmployeesViewModel()
        //        {
        //            Id = e.Id,
        //            FirstName = e.FirstName,
        //            LastName = e.LastName,
        //            IsHired = e.IsHired,
        //            HireDate = e.HireDate,
        //            Position = e.Position.Name,
        //            Department = e.Department.Name
        //        })
        //        .ToListAsync();
        //    return allEmployees;
        //}

        public async Task<AllEmployeesFilteredAndPagedServiceModel> AllAsync(AllEmployeesQueryModel model)
        {
            IQueryable<Employee> employeeQuery = this.dbContext.Employees;

            if (!string.IsNullOrWhiteSpace(model.Department))
            {
                employeeQuery = employeeQuery
                    .Where(e => e.Department.Name == model.Department);
            }

            if (!string.IsNullOrWhiteSpace(model.Position))
            {
                employeeQuery = employeeQuery
                    .Where(e => e.Position.Name == model.Position);
            }

            if (!string.IsNullOrWhiteSpace(model.SearchString))
            {
                string wildCard = $"%{model.SearchString.ToLower()}%";
                employeeQuery = employeeQuery
                    .Where(e => EF.Functions.Like(e.FirstName, wildCard));
            }

            employeeQuery = model.EmployeeSorting switch
            {
                EmployeeSorting.Newest => employeeQuery
                    .OrderByDescending(e => e.HireDate),
                EmployeeSorting.Oldest => employeeQuery
                    .OrderBy(e => e.HireDate),
                _ => employeeQuery
                    .OrderBy(e => e.Id)
            };

            IEnumerable<AllEmployeesViewModel> allEmployees = await employeeQuery
                .Skip((model.CurrentPage - 1) * model.EmployeesPerPage)
                .Take(model.EmployeesPerPage)
                .Select(e => new AllEmployeesViewModel
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    IsHired = e.IsHired,
                    HireDate = e.HireDate,
                    Position = e.Position.Name,
                    Department = e.Department.Name
                })
                .ToArrayAsync();
            return new AllEmployeesFilteredAndPagedServiceModel()
            {
                TotalEmployeesCount = employeeQuery.Count(),
                Employees = allEmployees
            };
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

        public async Task SoftDeleteEmployeeByIdAsync(int id)
        {
            Employee employee = await this.dbContext
                .Employees
                .FirstAsync(e => e.Id == id);

            employee.UserName = "Fired";
            employee.FirstName = "Fired";
            employee.LastName = "Fired";
            employee.IsHired = false;

            await this.dbContext.SaveChangesAsync();
        }
    }
}