namespace HumanCapitalManagementApp.Services
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using BCrypt.Net;
    using System.Data;
    using System.Text;
    using System.Security.Claims;
    using System.IdentityModel.Tokens.Jwt;

    using Interfaces;
    using Models.Account;
    using HumanCapitalManagementApp.Data;
    using HumanCapitalManagementApp.Data.Models;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.Extensions.Configuration;

    public class AccountService : IAccountService
    {
        private readonly HumanCapitalManagementAppDbContext dbContext;
        public IConfiguration config;


        public AccountService(HumanCapitalManagementAppDbContext dbContext, IConfiguration config)
        {
            this.dbContext = dbContext;
            this.config = config;
        }

        public async Task<bool> ExistByUsername(string username)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(e => e.UserName == username);
            if (employee == null)
            {
                return false;
            }
            return true;
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

        public async Task<(string, Employee)> LoginEmployeeAsync(LoginFormModel model)
        {
            var employee = await dbContext.Employees.SingleOrDefaultAsync(e => e.UserName == model.UserName);

            if (employee != null && VerifyPassword(model.Password, employee.HashedPassword))
            {
                var claims = new List<Claim>();

                if (employee.UserName == "admin")
                {
                    claims = new List<Claim>
                    {
                        new (ClaimTypes.Name, employee.UserName),
                        new (ClaimTypes.NameIdentifier, employee.Id.ToString()),
                        new (ClaimTypes.Role, "Administrator"),
                        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                }
                else
                {
                    claims = new List<Claim>
                    {
                        new (ClaimTypes.Name, employee.UserName),
                        new (ClaimTypes.NameIdentifier, employee.Id.ToString()),
                        new (ClaimTypes.Role, "Employee"),
                        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                }

                JwtSecurityToken token = GenerateJwtToken(claims);

                var jwttoken = new JwtSecurityTokenHandler().WriteToken(token);


                return (jwttoken, employee);
            }
            return (null, null);
        }

        public async Task<int> TakeIdByUsernameAsync(string username)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(e => e.UserName == username);

            if (employee == null)
            {
                return 0;
            }
            return employee.Id;
        }

        private JwtSecurityToken GenerateJwtToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Audience"],
                expires: DateTime.UtcNow.AddMinutes(60),
                claims: claims,
                signingCredentials: signIn);

            return (token);
        }

        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return BCrypt.Verify(inputPassword, hashedPassword);
        }

        [HttpGet]
        private async Task<Employee> GetUser(string username)
        {
            return await dbContext.Employees.FirstOrDefaultAsync(e => e.UserName == username);
        }
    }
}