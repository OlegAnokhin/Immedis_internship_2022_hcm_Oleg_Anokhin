namespace HumanCapitalManagementApp.Services
{
    using BCrypt.Net;
    using Data;
    using Data.Models;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models.Account;

    public class AccountService : IAccountService
    {
        private readonly HumanCapitalManagementAppDbContext dbContext;

        public AccountService(HumanCapitalManagementAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task RegisterEmployeeAsync(RegisterFormModel model)
        {
                string salt = BCrypt.GenerateSalt(12);
                string hashedPassword = BCrypt.HashPassword(model.Password, salt);

                Employee employee = new Employee
                {
                    UserName = model.UserName,
                    HashedPassword = hashedPassword,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    HireDate = model.HireDate,
                    PositionId = model.PositionId,
                    Salary = model.Salary,
                    DepartmentId = model.DepartmentId,
                    EmployeeInfoId = model.EmployeeInfoId + 1
                };
                await dbContext.Employees.AddAsync(employee);
                await dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistByIdAsync(int id)
        {
            return await this.dbContext.Employees
                .AnyAsync(e => e.Id == id);
        }
    }
}