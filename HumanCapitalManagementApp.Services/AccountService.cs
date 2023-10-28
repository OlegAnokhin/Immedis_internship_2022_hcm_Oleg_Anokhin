namespace HumanCapitalManagementApp.Services
{
    using Microsoft.EntityFrameworkCore;
    using System.Data;
    using BCrypt.Net;

    using Data;
    using Data.Models;
    using Interfaces;
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
                DepartmentId = model.DepartmentId
            };

            var posId = employee.PositionId;
            if (posId == 1)
            {
                employee.Salary = 2000;
            }
            else if (posId == 2 || posId == 5 || posId == 7)
            {
                employee.Salary = 3000;
            }
            else if (posId == 3)
            {
                employee.Salary = 4000;
            }
            else if (posId == 4 || posId == 6 || posId == 8)
            {
                employee.Salary = 5000;
            }
            try
            {
                await dbContext.Employees.AddAsync(employee);
                await dbContext.SaveChangesAsync();

            }
            catch (Exception)
            {
                throw new InvalidExpressionException("Unexpected error.");
            }
        }

        public async Task<bool> LoginEmployeeAsync(LoginFormModel model)
        {
            var employee = await dbContext.Employees.SingleOrDefaultAsync(e => e.UserName == model.UserName);
            
            if (employee != null && VerifyPassword(model.Password, employee.HashedPassword))
            {
                return true;
            }
            return false;
        }

        public async Task<int> TakeIdByUsernameAsync(string username)
        {
            var employee = await dbContext.Employees.SingleOrDefaultAsync(e => e.UserName == username);

            if (employee == null)
            {
                return 0;
            }
            return employee.Id;
        }

        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return BCrypt.Verify(inputPassword, hashedPassword);
        }
    }
}