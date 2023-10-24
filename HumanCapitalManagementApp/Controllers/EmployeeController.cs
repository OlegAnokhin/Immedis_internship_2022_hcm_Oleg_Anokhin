namespace HumanCapitalManagementApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using ViewModels.Employee;
    using Services.Interfaces;

    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService employeeService;
        private readonly IPositionService positionService;

        public EmployeeController(IEmployeeService employeeService, IPositionService positionService)
        {
            this.employeeService = employeeService;
            this.positionService = positionService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {

            try
            {
                IEnumerable<AllEmployeesViewModel> allEmployees =
                    await this.employeeService.ListAllEmployeesAsync();

                return View(allEmployees);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Error", "Home");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> SuccessLogin(int employeeId)
        {
            var employeeModel = await this.employeeService.TakeEmployeeByIdAsync(employeeId);

            return View(employeeModel);
        }

        [AllowAnonymous]
        public IActionResult AboutMe()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult LeaveRequest()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult PreviousPositions()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult QualificationTraining()
        {
            return View();
        }
    }
}
