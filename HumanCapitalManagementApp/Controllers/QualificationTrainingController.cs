﻿namespace HumanCapitalManagementApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System.Net;

    using ViewModels.QualificationTraining;

    public class QualificationTrainingController : BaseController
    {
        private Uri baseAddress = new Uri("http://localhost:5152");
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
    }
}