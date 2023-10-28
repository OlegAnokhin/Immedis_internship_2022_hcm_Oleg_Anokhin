namespace HumanCapitalManagementApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Interfaces;
    using ViewModels.PreviousPositions;

    public class PreviousPositionController : BaseController
    {
        private readonly IPreviousPositionService previousPositionService;
        private readonly IPositionService positionService;
        private readonly IDepartmentService departmentService;

        public PreviousPositionController(IPreviousPositionService previousPositionService, 
                                          IPositionService positionService, 
                                          IDepartmentService departmentService)
        {
            this.previousPositionService = previousPositionService;
            this.positionService = positionService;
            this.departmentService = departmentService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> PreviousPositions(int id)
        {
            IEnumerable<AllPreviousPositionsViewModel> info =
                await this.previousPositionService.TakeAllPreviousPositionsByIdAsync(id);

            return View(info);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Add()
        {
            AddPreviousPositionsFormModel model = new AddPreviousPositionsFormModel()
            {
                From = DateTime.Today,
                To = DateTime.Today,
                Positions = await this.positionService.AllPositionsAsync(),
                Departments = await this.departmentService.AllDepartmentsAsync()
            };
            return View(model);
        }
    }
}