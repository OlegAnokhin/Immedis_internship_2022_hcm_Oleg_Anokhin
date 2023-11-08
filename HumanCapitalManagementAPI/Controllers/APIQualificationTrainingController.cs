namespace HumanCapitalManagementAPI.Controllers
{
    using HumanCapitalManagementApp.Services.Interfaces;

    using Microsoft.AspNetCore.Mvc;

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
                var model = await this.qualificationTrainingService.AllTrainigs();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

    }
}