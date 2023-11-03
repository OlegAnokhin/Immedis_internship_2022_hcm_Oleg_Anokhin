using HumanCapitalManagementApp.Data.Models;

namespace HumanCapitalManagementAPI.Controllers
{
    using HumanCapitalManagementApp.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IPositionService positionService;
        private readonly IDepartmentService departmentService;

        public AccountController(IAccountService accountService, IPositionService positionService, IDepartmentService departmentService)
        {
            this.accountService = accountService;
            this.positionService = positionService;
            this.departmentService = departmentService;
        }

        
        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AccountController>/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Employee>> Get(string username)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        //Employee employee = await accountService.ExistByUsername(username);
        //    }
        //    return "value";
        //}

        // POST api/<AccountController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
