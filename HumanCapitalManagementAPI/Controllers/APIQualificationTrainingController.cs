namespace HumanCapitalManagementAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using HumanCapitalManagementApp.ViewModels.QualificationTraining;
    using HumanCapitalManagementApp.Services.Interfaces;

    [Route("[controller]")]
    [ApiController]
    public class APIQualificationTrainingController : ControllerBase
    {
        private readonly IQualificationTrainingService qualificationTrainingService;
        private readonly IAccountService accountService;
        public APIQualificationTrainingController(IQualificationTrainingService qualificationTrainingService, IAccountService accountService)
        {
            this.qualificationTrainingService = qualificationTrainingService;
            this.accountService = accountService;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            try
            {
                var model = await this.qualificationTrainingService.AllTrainings();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("My/{id}")]
        public async Task<IActionResult> My(int id)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound("Invalid identifier");
                }

                var model = await this.qualificationTrainingService.MyTrainings(id);

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }


        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] AllQualificationTrainingViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.From = DateTime.Today;
                    model.To = DateTime.Today;
                    return BadRequest(ModelState);
                }

                await this.qualificationTrainingService.AddTrainingAsync(model);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                bool trainingExists = await this.qualificationTrainingService.ExistsByIdAsync(id);
                
                if (!trainingExists)
                {
                    return NotFound("Invalid identifier");
                }

                var model = await this.qualificationTrainingService.DetailsTrainingAsync(id);

                return Ok(model);
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
                bool trainingExist = await this.qualificationTrainingService
                    .ExistsByIdAsync(id);

                if (!trainingExist)
                {
                    return NotFound("Invalid identifier");
                }
                await this.qualificationTrainingService.DeleteTrainingAsync(id);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Join/{id}/{name}")]
        public async Task<IActionResult> Join(int id, string name)
        {
            var employeeId = 0;
            try
            {
                if (name != null)
                {
                    employeeId = await accountService.TakeIdByUsernameAsync(name);
                }

                if (employeeId == 0)
                {
                    return NotFound("Invalid identifier");
                }

                var trainingToJoin = await qualificationTrainingService.GetTrainingByIdAsync(id);
                var model = await qualificationTrainingService.GetJoinedTrainings(employeeId);

                if (model.Any(m => m.Id == id))
                {
                    return BadRequest("You have already joined");
                }

                await this.qualificationTrainingService.JoinToTrainingAsync(employeeId, trainingToJoin);
                return Ok(employeeId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Leave/{id}/{name}")]
        public async Task<IActionResult> Leave(int id, string name)
        {
            var employeeId = 0;
            try
            {
                var trainingToLeave = await qualificationTrainingService.GetTrainingByIdAsync(id);

                if (trainingToLeave == null)
                {
                    return NotFound("Invalid identifier");
                }

                if (name != null)
                {
                    employeeId = await accountService.TakeIdByUsernameAsync(name);
                }

                if (employeeId == 0)
                {
                    return NotFound("Invalid identifier");
                }

                await qualificationTrainingService.LeaveFromTrainingAsync(employeeId, trainingToLeave);
                return Ok(employeeId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
    }
}