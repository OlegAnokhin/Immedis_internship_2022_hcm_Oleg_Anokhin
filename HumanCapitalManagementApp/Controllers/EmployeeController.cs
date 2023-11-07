namespace HumanCapitalManagementApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System.Net;

    using ViewModels.Employee;

    public class EmployeeController : BaseController
    {
        private Uri baseAddress = new Uri("http://localhost:5152");
        HttpClient client;

        public EmployeeController()
        {
            this.client = new HttpClient();
            this.client.BaseAddress = baseAddress;
        }

        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> All([FromQuery] AllEmployeesQueryModel queryModel)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress + $"APIEmployee/All", queryModel);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<AllEmployeesQueryModel>(json);

                return View("All", model);
            }

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, errorMessage); ;
                return View(queryModel);
            }

            return View(queryModel);
        }

        [HttpGet]
        public async Task<IActionResult> SuccessLogin(int employeeId)
        {
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + $"APIEmployee/SuccessLogin/{employeeId}");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<SuccessLoginViewModel>(json);

                return View("SuccessLogin", model);
            }

            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> AboutMe(int id)
        {
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + $"APIEmployee/AboutMe/{id}");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<EmployeeInfoModel>(json);

                return View("AboutMe", model);
            }

            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + $"APIEmployee/Edit/{id}");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<EditEmployeeViewModel>(json);

                return View("Edit", model);
            }

            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditEmployeeViewModel model)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress + $"APIEmployee/Edit/{id}", model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SuccessLogin", "Employee", new { EmployeeId = id });
                }
                
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage); ;
                    return View(model);
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> Hire(int id)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"APIEmployee/Hire/{id}", null);

                if (response.IsSuccessStatusCode)
                {
                    string returnUrl = Request.Headers["Referer"].ToString();

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"APIEmployee/Delete/{id}", null);

                if (response.IsSuccessStatusCode)
                {
                    string returnUrl = Request.Headers["Referer"].ToString();

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Error", "Home");
        }


        [HttpGet]
        public IActionResult QualificationTraining()
        {
            return View();
        }
    }
}