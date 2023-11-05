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

        //[HttpGet]
        //[Authorize(Roles = "Administrator")]
        //public async Task<IActionResult> All([FromQuery] AllEmployeesQueryModel queryModel)
        //{
        //    AllEmployeesFilteredAndPagedServiceModel serviceModel =
        //        await this.employeeService.AllAsync(queryModel);

        //    queryModel.Employees = serviceModel.Employees;
        //    queryModel.TotalEmployees = serviceModel.TotalEmployeesCount;
        //    queryModel.Departments = await this.departmentService.AllDepartmentNamesAsync();
        //    queryModel.Positions = await this.positionService.AllPositionNamesAsync();

        //    return this.View(queryModel);
        //}

        [HttpGet]
        [AllowAnonymous]
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

        //public async Task<IActionResult> Hire(int id)
        //{
        //    bool employeeExist = await this.employeeService
        //        .ExistByIdAsync(id);

        //    if (!employeeExist)
        //    {
        //        TempData["ErrorMessage"] = "Record whit this Id not exist.";
        //        return RedirectToAction("Error", "Home");
        //    }

        //    try
        //    {
        //        await this.employeeService.SetIsHiredOnTrue(id);

        //        string returnUrl = Request.Headers["Referer"].ToString();

        //        if (!string.IsNullOrEmpty(returnUrl))
        //        {
        //            return Redirect(returnUrl);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        TempData["ErrorMessage"] = "An unexpected error occurred";
        //        return RedirectToAction("Error", "Home");
        //    }
        //    return RedirectToAction("SuccessLogin", "Employee", new { EmployeeId = id });
        //}

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + $"APIEmployee/Delete/{id}");

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