namespace HumanCapitalManagementApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System.Net;
    using ViewModels.PreviousPositions;

    public class PreviousPositionController : BaseController
    {
        private Uri baseAddress = new Uri("https://localhost:7237");
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
                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage); ;
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
        public async Task<IActionResult> Add(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + $"APIPreviousPosition/Add/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<AddPreviousPositionsFormModel>(json);

                    return View(model);
                }
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage); ;
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id, AddPreviousPositionsFormModel model)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress + $"APIPreviousPosition/Add/{id}", model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SuccessLogin", "Employee", new { EmployeeId = id });
                }
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage); ;
                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
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
                HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"APIPreviousPosition/Delete/{id}", null);

                if (response.IsSuccessStatusCode)
                {
                    string returnUrl = Request.Headers["Referer"].ToString();

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Error", "Home");
        }
    }
}