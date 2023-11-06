namespace HumanCapitalManagementApp.Controllers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json;

    using System.Net;
    using System.Net.Http;
    using Models.Account;

    public class AccountController : BaseController
    {
        private Uri baseAddress = new Uri("http://localhost:5152");
        HttpClient client;

        public AccountController()
        {
            this.client = new HttpClient();
            this.client.BaseAddress = baseAddress;
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
        }

        [HttpPost]
        [AllowAnonymous]
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
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic responseData = JObject.Parse(json);
                    int employeeId = responseData.id;

                    HttpResponseMessage responseToken =
                        await client.PostAsJsonAsync(client.BaseAddress + "Token/Post", model);

                    if (responseToken.IsSuccessStatusCode)
                    {
                        string token = await responseToken.Content.ReadAsStringAsync();
                        Response.Cookies.Append("JWToken", token);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
                    }

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
            Response.Cookies.Delete("JWToken");

            return RedirectToAction("Index", "Home");
        }
    }
}