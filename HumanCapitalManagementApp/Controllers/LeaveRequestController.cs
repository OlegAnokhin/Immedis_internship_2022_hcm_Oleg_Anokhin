namespace HumanCapitalManagementApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System.Net;

    using ViewModels.LeaveRequest;

    public class LeaveRequestController : BaseController
    {
        private Uri baseAddress = new Uri("https://localhost:7237");
        HttpClient client;

        public LeaveRequestController()
        {
            this.client = new HttpClient();
            this.client.BaseAddress = baseAddress;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AllForAdmin()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + $"APILeaveRequest/All");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<LeaveRequestViewModel> leaveRequests = JsonConvert.DeserializeObject<List<LeaveRequestViewModel>>(json);
                    return View(leaveRequests);
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
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> All(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + $"APILeaveRequest/All/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<DetailsRequestsViewModel>(json);

                    return View(model);
                }
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage); ;
                    return RedirectToAction("Error", "Home");
                }
                if(response.StatusCode == HttpStatusCode.NotFound)
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

        [HttpGet]
        public IActionResult Add()
        {
            LeaveRequestViewModel model = new LeaveRequestViewModel()
            {
                From = DateTime.Today,
                To = DateTime.Today
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id, LeaveRequestViewModel model)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress + $"APILeaveRequest/Add/{id}", model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SuccessLogin", "Employee", new { EmployeeId = id });
                }
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                    return View(model);
                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
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

        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"APILeaveRequest/Approve/{id}", null);

                if (response.IsSuccessStatusCode)
                {
                    string returnUrl = Request.Headers["Referer"].ToString();

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError(string.Empty, errorMessage); ;
                        return RedirectToAction("Error", "Home");
                    }
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"APILeaveRequest/Delete/{id}", null);

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
    }
}