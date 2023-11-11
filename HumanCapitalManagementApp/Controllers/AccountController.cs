namespace HumanCapitalManagementApp.Controllers
{
    using HumanCapitalManagementApp.Data.Models;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    using System.Net;
    using System.Net.Http;
    using Models.Account;
    using System.Security.Claims;

    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private Uri baseAddress = new Uri("https://localhost:7237");
        HttpClient client;

        public AccountController()
        {
            this.client = new HttpClient();
            this.client.BaseAddress = baseAddress;
        }

        [HttpGet]
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
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress + "APIAccount/Register", model);

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
                return RedirectToAction("Register", "Account");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Register", "Account");
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress + "APIAccount/Login", model);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var responseData = JsonConvert.DeserializeObject<dynamic>(json);

                    string token = responseData.jwttoken;
                    Employee employee = JsonConvert.DeserializeObject<Employee>(responseData.employee.ToString());

                    await SetClaims(token, employee);

                    int employeeId = employee.Id;

                    return RedirectToAction("SuccessLogin", "Employee", new { EmployeeId = employeeId });
                }
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    ModelState.AddModelError(string.Empty, "Username already exist.");
                    return RedirectToAction("Error", "Home");
                }
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage); ;
                    return RedirectToAction("Error", "Home");
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Invalid username or password";
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("Error", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        private async Task SetClaims(string token, Employee user)
        {
            var claims = new List<Claim>();

            if (user.UserName == "admin")
            {
                claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Name, user.UserName),
                    new(ClaimTypes.Authentication, token),
                    new(ClaimTypes.Hash, token),
                    new(ClaimTypes.Role, "Administrator")
                };
            }
            else
            {
                claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Name, user.UserName),
                    new(ClaimTypes.Authentication, token),
                    new(ClaimTypes.Hash, token),
                    new(ClaimTypes.Role, "Employee")

                };
            }
            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var authProp = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(10),
                IsPersistent = true,
                IssuedUtc = DateTimeOffset.UtcNow,
                RedirectUri = Url.Action("Index", "Home")
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProp);
        }
    }
}