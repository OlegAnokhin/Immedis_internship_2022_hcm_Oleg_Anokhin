namespace HumanCapitalManagementAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using HumanCapitalManagementApp.Services.Interfaces;
    using HumanCapitalManagementApp.ViewModels.LeaveRequest;

    [Route("[controller]")]
    [ApiController]
    public class APILeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestService leaveRequestService;
        private readonly IEmployeeService employeeService;

        public APILeaveRequestController(ILeaveRequestService leaveRequestService, IEmployeeService employeeService)
        {
            this.leaveRequestService = leaveRequestService;
            this.employeeService = employeeService;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            try
            {
                var model = await this.leaveRequestService.AllRequestsForAdmin();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
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

                var model = await this.leaveRequestService.AllLeaveRequestAsync(id);

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Add/{id}")]
        public async Task<IActionResult> Add(int id, [FromBody] LeaveRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.From = DateTime.Now;
                model.To = DateTime.Now;
                return BadRequest(ModelState);
            }

            try
            {
                bool employeeExist = await this.employeeService.ExistByIdAsync(id);

                if (!employeeExist)
                {
                    return NotFound("Invalid identifier");
                }

                await this.leaveRequestService.AddLeaveRequestAsync(id, model);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpPut]
        [Route("Approve/{id}")]
        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                await this.leaveRequestService.SetApproveAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpPut]
        //[Authorize]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.leaveRequestService.DeleteLeaveRequestByIdAsync(id);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
    }
}
