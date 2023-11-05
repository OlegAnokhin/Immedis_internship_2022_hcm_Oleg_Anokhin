namespace HumanCapitalManagementAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using HumanCapitalManagementApp.Services.Data.Models;
    using HumanCapitalManagementApp.Services.Interfaces;
    using HumanCapitalManagementApp.ViewModels.Employee;

    [Route("[controller]")]
    [ApiController]
    public class APIEmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly IDepartmentService departmentService;
        private readonly IPositionService positionService;

        public APIEmployeeController(IEmployeeService employeeService, IDepartmentService departmentService, IPositionService positionService)
        {
            this.employeeService = employeeService;
            this.departmentService = departmentService;
            this.positionService = positionService;
        }

        [HttpPut]
        //[Authorize]
        [Route("All")]
        public async Task<IActionResult> All([FromBody] AllEmployeesQueryModel queryModel)
        {
            try
            {
                AllEmployeesFilteredAndPagedServiceModel serviceModel = await this.employeeService.AllAsync(queryModel);

                queryModel.Employees = serviceModel.Employees;
                queryModel.TotalEmployees = serviceModel.TotalEmployeesCount;
                queryModel.Departments = await this.departmentService.AllDepartmentNamesAsync();
                queryModel.Positions = await this.positionService.AllPositionNamesAsync();

                return Ok(queryModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpGet]
        //[Authorize]
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

        [HttpGet]
        //[Authorize]
        [Route("AboutMe/{employeeId}")]
        public async Task<IActionResult> AboutMe(int employeeId)
        {
            if (employeeId == 0)
            {
                return NotFound("Invalid identifier");
            }

            try
            {
                var employeeModel = await this.employeeService.TakeEmployeeInfoByIdAsync(employeeId);

                return Ok(employeeModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpGet]
        //[Authorize]
        [Route("Edit/{employeeId}")]
        public async Task<IActionResult> Edit(int employeeId)
        {
            bool employeeExist = await this.employeeService.ExistByIdAsync(employeeId);

            if (!employeeExist)
            {
                return NotFound("Invalid identifier");
            }
            try
            {
                var employeeModel = await this.employeeService.TakeEmployeeForEditByIdAsync(employeeId);

                employeeModel.Positions = await this.positionService.AllPositionsAsync();
                employeeModel.Departments = await this.departmentService.AllDepartmentsAsync();

                return Ok(employeeModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpPost]
        //[Authorize]
        [Route("Edit/{employeeId}")]
        public async Task<IActionResult> Edit(int employeeId, [FromBody] EditEmployeeViewModel model)
        {
            bool employeeExist = await this.employeeService.ExistByIdAsync(employeeId);

            if (!employeeExist)
            {
                return NotFound("Invalid identifier");
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Positions = await this.positionService.AllPositionsAsync();
                    model.Departments = await this.departmentService.AllDepartmentsAsync();

                    return BadRequest(ModelState);
                }
                await this.employeeService.EditEmployeeByIdAsync(employeeId, model);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpPut]
        //[Authorize]
        [Route("Delete/{employeeId}")]
        public async Task<IActionResult> Delete(int employeeId)
        {
            bool employeeExist = await this.employeeService.ExistByIdAsync(employeeId);

            if (!employeeExist)
            {
                return NotFound("Invalid identifier");
            }
            try
            {
                await this.employeeService.SoftDeleteEmployeeByIdAsync(employeeId);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpPut]
        //[Authorize]
        [Route("Hire/{employeeId}")]
        public async Task<IActionResult> Hire(int employeeId)
        {
            bool employeeExist = await this.employeeService.ExistByIdAsync(employeeId);

            if (!employeeExist)
            {
                return NotFound("Invalid identifier");
            }
            try
            {
                await this.employeeService.SetIsHiredOnTrue(employeeId);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
    }
}