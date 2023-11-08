namespace HumanCapitalManagementAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using HumanCapitalManagementApp.Services.Interfaces;
    using HumanCapitalManagementApp.ViewModels.PreviousPositions;

    [Route("[controller]")]
    [ApiController]
    public class APIPreviousPositionController : ControllerBase
    {
        private readonly IPreviousPositionService previousPositionService;
        private readonly IEmployeeService employeeService;
        private readonly IDepartmentService departmentService;
        private readonly IPositionService positionService;

        public APIPreviousPositionController(IPreviousPositionService previousPositionService, IEmployeeService employeeService, IDepartmentService departmentService, IPositionService positionService)
        {
            this.previousPositionService = previousPositionService;
            this.employeeService = employeeService;
            this.departmentService = departmentService;
            this.positionService = positionService;
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

        [HttpGet]
        [Route("Add/{id}")]
        public async Task<IActionResult> Add(int id)
        {
            try
            {
                bool employeeExist = await this.employeeService.ExistByIdAsync(id);

                if (!employeeExist)
                {
                    return NotFound("Invalid identifier");
                }

                var model = new AddPreviousPositionsFormModel
                {
                    EmployeeId = id,
                    From = DateTime.Today,
                    To = DateTime.Today,
                    Positions = await this.positionService.AllPositionsAsync(),
                    Departments = await this.departmentService.AllDepartmentsAsync()
                };
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Add/{id}")]
        public async Task<IActionResult> Add(int id, [FromBody] AddPreviousPositionsFormModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.EmployeeId = id;
                    model.From = DateTime.Today;
                    model.To = DateTime.Today;
                    model.Positions = await this.positionService.AllPositionsAsync();
                    model.Departments = await this.departmentService.AllDepartmentsAsync();
                    return BadRequest(ModelState);
                }

                bool employeeExist = await this.employeeService.ExistByIdAsync(id);

                if (!employeeExist)
                {
                    return NotFound("Invalid identifier");
                }

                await this.previousPositionService.AddPreviousPositionAsync(id, model);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpPut]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool positionExist = await this.previousPositionService
                    .ExistByIdAsync(id);

                if (!positionExist)
                {
                    return NotFound("Invalid identifier");
                }
                await this.previousPositionService.DeletePreviousPositionByIdAsync(id);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
    }
}