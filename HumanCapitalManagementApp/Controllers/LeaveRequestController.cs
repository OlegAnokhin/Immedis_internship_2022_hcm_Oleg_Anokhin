namespace HumanCapitalManagementApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Services.Interfaces;
    using ViewModels.LeaveRequest;
    
    public class LeaveRequestController : BaseController
    {
        private readonly ILeaveRequestService leaveRequestService;

        public LeaveRequestController(ILeaveRequestService leaveRequestService)
        {
            this.leaveRequestService = leaveRequestService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All(int id)
        {
            try
            {
                var model = await this.leaveRequestService
                    .AllLeaveRequestAsync(id);
                return View(model);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Add()
        {
            LeaveRequestViewModel model = new LeaveRequestViewModel()
            {
                From = DateTime.Today,
                To = DateTime.Today
            };
            return View(model);
        }
    }
}
