namespace HumanCapitalManagementAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using HumanCapitalManagementApp.Services.Interfaces;

    [Route("[controller]")]
    [ApiController]
    public class APIPreviousPositionController : ControllerBase
    {
        private readonly IPreviousPositionService previousPositionService;
        private readonly IEmployeeService employeeService;

        public APIPreviousPositionController(IPreviousPositionService previousPositionService, IEmployeeService employeeService)
        {
            this.previousPositionService = previousPositionService;
            this.employeeService = employeeService;
        }

        [HttpGet]
        [Route("All/{id}")]
        public async Task<IActionResult> All(int id)
        {
            try
            {
                bool employeeExist = await this.employeeService.ExistByIdAsync(id);

                if (!employeeExist)
                {
                    return NotFound("Invalid identifier");
                }

                var model = await this.previousPositionService.AllPreviousPositionsByIdAsync(id);

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
    }
}
