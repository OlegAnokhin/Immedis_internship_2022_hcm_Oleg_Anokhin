﻿namespace HumanCapitalManagementApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System.Net;

    using Common;
    using ViewModels.QualificationTraining;

    public class QualificationTrainingController : BaseController
    {
        private Uri baseAddress = new Uri(GeneralAppConstants.baseAddress);
        HttpClient client;

        public QualificationTrainingController()
        {
            this.client = new HttpClient();
            this.client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + $"APIQualificationTraining/All");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<AllQualificationTrainingViewModel> trainings = JsonConvert.DeserializeObject<List<AllQualificationTrainingViewModel>>(json);

                    return View(trainings);
                }
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                    TempData["ErrorMessage"] = errorMessage;
                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                    TempData["ErrorMessage"] = errorMessage;
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
        public async Task<IActionResult> My(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + $"APIQualificationTraining/My/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<AllQualificationTrainingViewModel> trainings = JsonConvert.DeserializeObject<List<AllQualificationTrainingViewModel>>(json);

                    return View(trainings);
                }
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                    TempData["ErrorMessage"] = errorMessage;
                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                    TempData["ErrorMessage"] = errorMessage;
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Error", "Home");
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Add()
        {
            var model = new AllQualificationTrainingViewModel()
            {
                From = DateTime.Today,
                To = DateTime.Today
            };
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Add(AllQualificationTrainingViewModel model)
        {
            try
            {
                HttpResponseMessage response =
                await client.PostAsJsonAsync(client.BaseAddress + $"APIQualificationTraining/Add", model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("All", "QualificationTraining");
                }
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                    TempData["ErrorMessage"] = errorMessage;
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
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + $"APIQualificationTraining/Details/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<AllQualificationTrainingViewModel>(json);

                    return View(model);
                }
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                    TempData["ErrorMessage"] = errorMessage;
                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                    TempData["ErrorMessage"] = errorMessage;
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
                HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"APIQualificationTraining/Delete/{id}", null);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("All", "QualificationTraining");
                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                    TempData["ErrorMessage"] = errorMessage;
                }
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                    TempData["ErrorMessage"] = errorMessage;
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> Join(int id, string additionalParam)
        {
            try
            {
                HttpResponseMessage response =
                    await client.PostAsync(client.BaseAddress + $"APIQualificationTraining/Join/{id}/{additionalParam}", null);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    int employeeId = JsonConvert.DeserializeObject<int>(json);

                    return RedirectToAction("My", "QualificationTraining", new { id = employeeId });
                }
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage); 
                    TempData["ErrorMessage"] = errorMessage;
                }
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                    TempData["ErrorMessage"] = errorMessage;
                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                    TempData["ErrorMessage"] = errorMessage;
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> Leave(int id, string additionalParam)
        {
            try
            {
                HttpResponseMessage response =
                    await client.PostAsync(client.BaseAddress + $"APIQualificationTraining/Leave/{id}/{additionalParam}", null);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    int employeeId = JsonConvert.DeserializeObject<int>(json);

                    return RedirectToAction("My", "QualificationTraining",new { id = employeeId });
                }
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                    TempData["ErrorMessage"] = errorMessage;
                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                    TempData["ErrorMessage"] = errorMessage;
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