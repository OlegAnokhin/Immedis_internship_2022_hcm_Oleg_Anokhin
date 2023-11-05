namespace HumanCapitalManagementAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using HumanCapitalManagementApp.Services.Interfaces;

    [Route("[controller]")]
    [ApiController]
    public class APIEmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public APIEmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        [Authorize]
        [Route("SuccessLogin/{employeeId}")]
        public async Task<IActionResult> SuccessLogin(int employeeId)
        {
            if (employeeId == 0)
            {
                return NotFound("Invalid identifier");
            }

            try
            {
                var employeeModel = await this.employeeService.TakeEmployeeByIdAsync(employeeId);

                return Ok(employeeModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

    }
}
