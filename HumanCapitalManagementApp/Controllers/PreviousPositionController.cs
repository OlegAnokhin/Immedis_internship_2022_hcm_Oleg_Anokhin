namespace HumanCapitalManagementApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System.Net;
    using ViewModels.PreviousPositions;

    public class PreviousPositionController : BaseController
    {
        private Uri baseAddress = new Uri("http://localhost:5152");
        HttpClient client;

        public PreviousPositionController()
        {
            this.client = new HttpClient();
            this.client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> All(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + $"APIPreviousPosition/All/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<DetailsPreviousPositionsViewModel>(json);

                    return View(model);
                }
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage); ;
                    return RedirectToAction("Error", "Home");
                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage); ;
                    return RedirectToAction("Error", "Home");
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Error", "Home");
        }

        //[HttpGet]
        //public async Task<IActionResult> Add()
        //{
        //    AddPreviousPositionsFormModel model = new AddPreviousPositionsFormModel()
        //    {
        //        From = DateTime.Today,
        //        To = DateTime.Today,
        //        Positions = await this.positionService.AllPositionsAsync(),
        //        Departments = await this.departmentService.AllDepartmentsAsync()
        //    };
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Add(int id, AddPreviousPositionsFormModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        model.From = DateTime.Today;
        //        model.To = DateTime.Today;
        //        model.Positions = await this.positionService.AllPositionsAsync();
        //        model.Departments = await this.departmentService.AllDepartmentsAsync();

        //        return View(model);
        //    }

        //    try
        //    {
        //        await previousPositionService.AddPreviousPositionAsync(id, model);
        //        return RedirectToAction("SuccessLogin", "Employee", new { EmployeeId = id });
        //    }
        //    catch (Exception)
        //    {
        //        TempData["ErrorMessage"] = "An unexpected error occurred";
        //        return RedirectToAction("SuccessLogin", "Employee", new { EmployeeId = id });
        //    }
        //}

        //public async Task<IActionResult> Delete(int id)
        //{
        //    bool positionExist = await this.previousPositionService
        //        .ExistByIdAsync(id);

        //    if (!positionExist)
        //    {
        //        TempData["ErrorMessage"] = "Record whit this Id not exist.";
        //        return RedirectToAction("Error", "Home");
        //    }

        //    try
        //    {
        //        await this.previousPositionService.DeletePreviousPositionByIdAsync(id);

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
        //    return RedirectToAction("Index", "Home");
        //}
    }
}