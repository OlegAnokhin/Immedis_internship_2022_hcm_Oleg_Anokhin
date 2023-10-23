namespace HumanCapitalManagementApp.Services
{
    using Microsoft.EntityFrameworkCore;
    using BCrypt.Net;

    using Data;
    using Data.Models;
    using Interfaces;
    using Models.Account;
    //using Microsoft.AspNetCore.Http;

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
                DepartmentId = model.DepartmentId
            };
            try
            {
                await dbContext.Employees.AddAsync(employee);
                await dbContext.SaveChangesAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> ExistByUsernameAsync(string username)
        {
            return await this.dbContext.Employees
                .AnyAsync(e => e.UserName == username);
        }

        public async Task LoginEmployeeAsync(LoginFormModel model)
        {
            Employee employee = await dbContext.Employees.SingleOrDefaultAsync(e => e.UserName == model.UserName);

            if (employee != null && VerifyPassword(model.Password, employee.HashedPassword))
            {
                
                // Потребителският идентификатор се съхранява в сесията
                //Session["UserId"] = user.UserId;

            }
        }

        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return BCrypt.Verify(inputPassword, hashedPassword);
        }
    }
}