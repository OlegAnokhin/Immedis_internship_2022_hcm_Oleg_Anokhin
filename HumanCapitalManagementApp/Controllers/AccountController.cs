namespace HumanCapitalManagementApp.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;

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

            var empExist = await this.accountService.ExistByUsername(model.UserName);

            if (empExist)
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                await accountService.RegisterEmployeeAsync(model);
                return RedirectToAction("Login", "Account");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Register", "Account");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var responce = await this.accountService.LoginEmployeeAsync(model);

                if (responce != null)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(responce));
                }

                //var token = await accountService.LoginEmployeeAsync(model);

                //if (token != null)
                //{
                //    var employeeId = await accountService.TakeIdByUsernameAsync(model.UserName);

                //    //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                //    //    new ClaimsPrincipal(responce.Data));

                //    HttpContext.Response.Cookies.Append("AuthToken", token, new CookieOptions
                //    {
                //        Expires = DateTime.Now.AddDays(1)
                //    });

                //    HttpContext.Request.Headers.Add("Authorization", $"Bearer {token}");
                //    return RedirectToAction("SuccessLogin", "Employee", new { EmployeeId = employeeId });
                //}
                var employeeId = await accountService.TakeIdByUsernameAsync(model.UserName);
                return RedirectToAction("SuccessLogin", "Employee", new { EmployeeId = employeeId });
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Invalid username or password";
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public IActionResult Logout()
        {
            return View();
        }
    }
}