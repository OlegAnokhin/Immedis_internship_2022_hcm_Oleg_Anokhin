//namespace HumanCapitalManagementAPI.Controllers
//{
//    using Microsoft.AspNetCore.Mvc;
//    using Microsoft.EntityFrameworkCore;
//    using Microsoft.IdentityModel.Tokens;
//    using System.Text;
//    using System.Security.Claims;
//    using System.IdentityModel.Tokens.Jwt;

//    using HumanCapitalManagementApp.Data;
//    using HumanCapitalManagementApp.Data.Models;
//    using HumanCapitalManagementApp.Models.Account;

//    [Route("[controller]")]
//    [ApiController]
//    public class TokenController : ControllerBase
//    {
//        public IConfiguration cofig;
//        public readonly HumanCapitalManagementAppDbContext dbContext;

//        public TokenController(IConfiguration cofig, HumanCapitalManagementAppDbContext dbContext)
//        {
//            this.cofig = cofig;
//            this.dbContext = dbContext;
//        }

//        [HttpPost]
//        [Route("Post")]
//        public async Task<IActionResult> Post(LoginFormModel model)
//        {
//            if (model != null && model.UserName != null && model.Password != null)
//            {
//                var user = await GetUser(model.UserName);
//                if (user != null)
//                {
//                    var claims = new[]
//                    {
//                        new Claim(JwtRegisteredClaimNames.Sub, cofig["Jwt:Subject"]),
//                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
//                        new Claim("Id", user.Id.ToString()),
//                        new Claim("UserName", user.UserName),
//                        new Claim("Password", user.HashedPassword)
//                    };

//                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cofig["Jwt:Key"]));
//                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//                    var token = new JwtSecurityToken(
//                        cofig["Jwt:Issuer"],
//                        cofig["Jwt:Audience"],
//                        claims,
//                        expires: DateTime.UtcNow.AddMinutes(60),
//                        signingCredentials: signIn);
//                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
//                }
//                else
//                {
//                    return BadRequest("Invalid credentials");
//                }
//            }
//            else
//            {
//                return BadRequest("Invalid credentials");
//            }
//        }

//        [HttpGet]
//        public async Task<Employee> GetUser(string username)
//        {
//            return await dbContext.Employees.FirstOrDefaultAsync(e => e.UserName == username);
//        }
//    }
//}