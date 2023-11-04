namespace HumanCapitalManagementApp.Controllers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    using System.Net;
    using System.Net.Http;
    using Models.Account;

    public class AccountController : BaseController
    {
        private Uri baseAddress = new Uri("http://localhost:5152");
        HttpClient client;

        //private readonly IAccountService accountService;
        //private readonly IPositionService positionService;
        //private readonly IDepartmentService departmentService;

        public AccountController(/*IAccountService accountService, IPositionService positionService, IDepartmentService departmentService*/)
        {
            this.client = new HttpClient();
            this.client.BaseAddress = baseAddress;

            //this.accountService = accountService;
            //this.positionService = positionService;
            //this.departmentService = departmentService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "APIAccount/Register");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<RegisterFormModel>(json);

                return View("Register", model);
            }

            return StatusCode((int)response.StatusCode, response.ReasonPhrase);

            //RegisterFormModel model = new RegisterFormModel()
            // {
            //това го прави АПИ
            //HireDate = DateTime.Today,
            //Positions = await this.positionService.AllPositionsAsync(),
            //Departments = await this.departmentService.AllDepartmentsAsync()
            //};

            //return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress+ "APIAccount/Register", model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login", "Account");
                }
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    ModelState.AddModelError(string.Empty, "Username already exist.");
                    return View(model);
                }
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage); ;
                    return View(model);
                }
                //това го прави АПИ
                //if (!ModelState.IsValid)
                //{
                //    model.HireDate = DateTime.Today;
                //    model.Positions = await this.positionService.AllPositionsAsync();
                //    model.Departments = await this.departmentService.AllDepartmentsAsync();

                //    return View(model);
                //}

                //var empExist = await this.accountService.ExistByUsername(model.UserName);

                //if (empExist)
                //{
                //    return RedirectToAction("Login", "Account");
                //}

                //serelezirane na model
                //izpra]ane kym api

                //това го прави АПИ
                //await accountService.RegisterEmployeeAsync(model);

                return RedirectToAction("Register", "Account");
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
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress + "APIAccount/Login", model);

                if (response.IsSuccessStatusCode)
                {
                    //response.Content
                    return RedirectToAction("Login", "Account");
                }
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    ModelState.AddModelError(string.Empty, "Username already exist.");
                    return View(model);
                }
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage); ;
                    return View(model);
                }


                //това го прави АПИ
                //var responce = await this.accountService.LoginEmployeeAsync(model);

                //if (responce != null)
                //{
                //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                //        new ClaimsPrincipal(responce));
                //}

                //това е старо
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

                //това го прави АПИ
                //var employeeId = await accountService.TakeIdByUsernameAsync(model.UserName);
                //return RedirectToAction("SuccessLogin", "Employee", new { EmployeeId = employeeId });
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Invalid username or password";
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}