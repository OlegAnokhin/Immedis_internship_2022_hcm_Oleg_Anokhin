namespace HumanCapitalManagementApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    
    using Models.Account;
    using Services.Interfaces;

    public class AccountController : BaseController
    {
        private readonly IAccountService accountService;
        private readonly IPositionService positionService;
        private readonly IDepartmentService departmentService;

        public AccountController(IAccountService accountService, IPositionService positionService, IDepartmentService departmentService)
        {
            this.accountService = accountService;
            this.positionService = positionService;
            this.departmentService = departmentService;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            RegisterFormModel model = new RegisterFormModel()
            {
                HireDate = DateTime.Today,
                Positions = await this.positionService.AllPositionsAsync(),
                Departments = await this.departmentService.AllDepartmentsAsync()
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.HireDate = DateTime.Today;
                model.Positions = await this.positionService.AllPositionsAsync();
                model.Departments = await this.departmentService.AllDepartmentsAsync();

                return View(model);
            }

            try
            {
                await accountService.RegisterEmployeeAsync(model);
                return RedirectToAction("My", "Employee");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Register", "Account");
            }
        }
    }
}