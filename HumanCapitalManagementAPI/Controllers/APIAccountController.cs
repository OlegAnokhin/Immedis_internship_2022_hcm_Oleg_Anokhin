namespace HumanCapitalManagementAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using HumanCapitalManagementApp.Models.Account;
    using HumanCapitalManagementApp.Services.Interfaces;

    [Route("[controller]")]
    [ApiController]
    public class APIAccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IPositionService positionService;
        private readonly IDepartmentService departmentService;

        public APIAccountController(IAccountService accountService, IPositionService positionService, IDepartmentService departmentService)
        {
            this.accountService = accountService;
            this.positionService = positionService;
            this.departmentService = departmentService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register()
        {
            var model = new RegisterFormModel();
            try
            {
                model.HireDate = DateTime.Today;
                model.Positions = await this.positionService.AllPositionsAsync();
                model.Departments = await this.departmentService.AllDepartmentsAsync();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterFormModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.HireDate = DateTime.Today;
                    model.Positions = await this.positionService.AllPositionsAsync();
                    model.Departments = await this.departmentService.AllDepartmentsAsync();

                    return BadRequest(ModelState);
                }

                var emplExist = await this.accountService.ExistByUsername(model.UserName);

                if (emplExist)
                {
                    return Conflict("Username already exist.");
                }
                await accountService.RegisterEmployeeAsync(model);

                return Ok("Register success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginFormModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var (identity, token) = await accountService.LoginEmployeeAsync(model);
                var employeeId = await accountService.TakeIdByUsernameAsync(model.UserName);

                return Ok(new {Token = token, id = employeeId});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
    }
}