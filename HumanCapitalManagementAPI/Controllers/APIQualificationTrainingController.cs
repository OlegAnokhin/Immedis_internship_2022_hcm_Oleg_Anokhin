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
    }
}