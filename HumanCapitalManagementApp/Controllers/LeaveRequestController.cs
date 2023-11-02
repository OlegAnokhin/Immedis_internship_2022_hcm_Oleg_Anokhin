namespace HumanCapitalManagementApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Interfaces;
    using ViewModels.LeaveRequest;
    
    public class LeaveRequestController : BaseController
    {
        private readonly ILeaveRequestService leaveRequestService;
        private readonly IEmployeeService employeeService;

        public LeaveRequestController(ILeaveRequestService leaveRequestService, IEmployeeService employeeService)
        {
            this.leaveRequestService = leaveRequestService;
            this.employeeService = employeeService;
        }

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
        public IActionResult Add()
        {
            LeaveRequestViewModel model = new LeaveRequestViewModel()
            {
                From = DateTime.Today,
                To = DateTime.Today
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id, LeaveRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home");
            }

            bool employeeExist = await this.employeeService.ExistByIdAsync(id);
            
            if (!employeeExist)
            {
                TempData["ErrorMessage"] = "Employee not exist";
                return RedirectToAction("Error", "Home");
            }

            try
            {
                await this.leaveRequestService.AddLeaveRequestAsync(id, model);

                return RedirectToAction("SuccessLogin", "Employee", new { EmployeeId = id });

            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Error", "Home");
            }
        }
    }
}