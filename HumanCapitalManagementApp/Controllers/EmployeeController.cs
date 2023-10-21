namespace HumanCapitalManagementApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using ViewModels.Employee;
    using Services.Interfaces;

    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
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
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Error", "Home");
            }
        }

    }
}
