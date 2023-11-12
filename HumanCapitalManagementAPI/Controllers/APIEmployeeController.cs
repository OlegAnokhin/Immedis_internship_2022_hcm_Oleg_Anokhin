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

        [HttpPost]
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
        [Route("SuccessLogin/{employeeId}")]
        public async Task<IActionResult> SuccessLogin(int employeeId)
        {
            try
            {
                if (!await employeeService.ExistByIdAsync(employeeId))
                {
                    return NotFound("Invalid identifier");
                }

                var employeeModel = await this.employeeService.TakeEmployeeByIdAsync(employeeId);

                return Ok(employeeModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("AboutMe/{employeeId}")]
        public async Task<IActionResult> AboutMe(int employeeId)
        {
            try
            {
                if (!await employeeService.ExistByIdAsync(employeeId))
                {
                    return NotFound("Invalid identifier");
                }

                var employeeModel = await this.employeeService.TakeEmployeeInfoByIdAsync(employeeId);

                if (employeeModel == null)
                {
                    return NotFound("Invalid identifier");
                }

                return Ok(employeeModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("Edit/{employeeId}")]
        public async Task<IActionResult> Edit(int employeeId)
        {
            try
            {
                if (!await employeeService.ExistByIdAsync(employeeId))
                {
                    return NotFound("Invalid identifier");
                }
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
        [Route("Edit/{employeeId}")]
        public async Task<IActionResult> Edit(int employeeId, [FromBody] EditEmployeeViewModel model)
        {
            try
            {
                if (!await employeeService.ExistByIdAsync(employeeId))
                {
                    return NotFound("Invalid identifier");
                }
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
        [Route("Delete/{employeeId}")]
        public async Task<IActionResult> Delete(int employeeId)
        {
            try
            {
                if (!await employeeService.ExistByIdAsync(employeeId))
                {
                    return NotFound("Invalid identifier");
                }
                await this.employeeService.SoftDeleteEmployeeByIdAsync(employeeId);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpPut]
        [Route("Hire/{employeeId}")]
        public async Task<IActionResult> Hire(int employeeId)
        {
            try
            {
                if (!await employeeService.ExistByIdAsync(employeeId))
                {
                    return NotFound("Invalid identifier");
                }
                await this.employeeService.SetIsHiredOnTrue(employeeId);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("SalaryInfo/{employeeId}")]
        public async Task<IActionResult> SalaryInfo(int employeeId)
        {
            try
            {
                if (!await employeeService.ExistByIdAsync(employeeId))
                {
                    return NotFound("Invalid identifier");
                }

                var model = await this.employeeService.SalaryInfoByIdAsync(employeeId);

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
    }
}