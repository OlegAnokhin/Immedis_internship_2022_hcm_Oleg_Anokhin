namespace HumanCapitalManagementAPI.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;

    using HumanCapitalManagementApp.ViewModels.QualificationTraining;
    using HumanCapitalManagementApp.Services.Interfaces;

    [Route("[controller]")]
    [ApiController]
    public class APIQualificationTrainingController : ControllerBase
    {
        private readonly IQualificationTrainingService qualificationTrainingService;
        //private string GetUserId() => User.FindFirst(ClaimTypes.NameIdentifier).ToString();

        public APIQualificationTrainingController(IQualificationTrainingService qualificationTrainingService)
        {
            this.qualificationTrainingService = qualificationTrainingService;
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
        [Route("Join/{id}")]
        public async Task<IActionResult> Join(int id)
        {
            var employeeId = GetUserId();
            if (employeeId == 0)
            {
                return NotFound("Invalid identifier");
            }
            try
            {
                var trainingToJoin = await qualificationTrainingService.GetTrainingByIdAsync(id);
                var model = await qualificationTrainingService.GetJoinedTrainings(employeeId);

                if (model.Any(m => m.Id == id))
                {
                    return BadRequest("You have already joined");
                }

                await this.qualificationTrainingService.JoinToTrainingAsync(id, trainingToJoin);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Leave/{id}")]
        public async Task<IActionResult> Leave(int id)
        {
            try
            {
                var trainingToLeave = await qualificationTrainingService.GetTrainingByIdAsync(id);

                if (trainingToLeave == null)
                {
                    return NotFound("Invalid identifier");
                }
                var employeeId = GetUserId();
                if (employeeId == 0)
                {
                    return NotFound("Invalid identifier");
                }
                await qualificationTrainingService.LeaveFromTrainingAsync(employeeId, trainingToLeave);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        private int GetUserId()
        {
            var identity = User.Identity as ClaimsIdentity;

            var nameIdentifierClaim = identity?.FindFirst(ClaimTypes.NameIdentifier);

            if (nameIdentifierClaim != null)
            {
                string userId = nameIdentifierClaim.Value;
                int employeeId = int.Parse(userId);
                return employeeId;
            }

            return 0;
        }
    }
}