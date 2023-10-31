using HumanCapitalManagementApp.Services.Data.Models;

namespace HumanCapitalManagementApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Services.Interfaces;
    using ViewModels.Employee;

    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService employeeService;
        private readonly IPositionService positionService;
        private readonly IDepartmentService departmentService;

        public EmployeeController(IEmployeeService employeeService,
                                  IPositionService positionService,
                                  IDepartmentService departmentService)
        {
            this.employeeService = employeeService;
            this.positionService = positionService;
            this.departmentService = departmentService;
        }

        //[AllowAnonymous]
        //public async Task<IActionResult> All()
        //{

        //    try
        //    {
        //        IEnumerable<AllEmployeesViewModel> allEmployees =
        //            await this.employeeService.ListAllEmployeesAsync();

        //        return View(allEmployees);
        //    }
        //    catch (Exception)
        //    {
        //        TempData["ErrorMessage"] = "An unexpected error occurred";
        //        return RedirectToAction("Error", "Home");
        //    }
        //}

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllEmployeesQueryModel queryModel)
        {
            AllEmployeesFilteredAndPagedServiceModel serviceModel =
                await this.employeeService.AllAsync(queryModel);

            queryModel.Employees = serviceModel.Employees;
            queryModel.TotalEmployees = serviceModel.TotalEmployeesCount;
            queryModel.Departments = await this.departmentService.AllDepartmentNamesAsync();
            queryModel.Positions = await this.positionService.AllPositionNamesAsync();

            return this.View(queryModel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> SuccessLogin(int employeeId)
        {
            if (employeeId == 0)
            {
                return RedirectToAction("Error401", "Home");
            }
            var employeeModel = await this.employeeService.TakeEmployeeByIdAsync(employeeId);

            return View(employeeModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> AboutMe(int id)
        {
            var info = await this.employeeService.TakeEmployeeInfoByIdAsync(id);

            return View(info);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Edit(int id)
        {
            bool employeeExist = await this.employeeService
                .ExistByIdAsync(id);

            if (!employeeExist)
            {
                TempData["ErrorMessage"] = "Record whit this Id not exist.";
                return RedirectToAction("Error", "Home");
            }

            EditEmployeeViewModel model = await this.employeeService
                .TakeEmployeeForEditByIdAsync(id);
            model.Positions = await this.positionService.AllPositionsAsync();
            model.Departments = await this.departmentService.AllDepartmentsAsync();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Edit(int id, EditEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Positions = await this.positionService.AllPositionsAsync();
                model.Departments = await this.departmentService.AllDepartmentsAsync();
                return View(model);
            }

            bool employeeExist = await this.employeeService
                .ExistByIdAsync(id);

            if (!employeeExist)
            {
                TempData["ErrorMessage"] = "Record whit this Id not exist.";
                return RedirectToAction("Error", "Home");
            }

            try
            {
                await this.employeeService.EditEmployeeByIdAsync(id, model);
                return RedirectToAction("SuccessLogin", "Employee", new { EmployeeId = id });
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Error", "Home");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> Hire(int id)
        {
            bool employeeExist = await this.employeeService
                .ExistByIdAsync(id);

            if (!employeeExist)
            {
                TempData["ErrorMessage"] = "Record whit this Id not exist.";
                return RedirectToAction("Error", "Home");
            }

            try
            {
                await this.employeeService.SetIsHiredOnTrue(id);

                string returnUrl = Request.Headers["Referer"].ToString();

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("SuccessLogin", "Employee", new { EmployeeId = id });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Delete(int id)
        {
            bool existById = await this.employeeService
                .ExistByIdAsync(id);

            if (!existById)
            {
                TempData["ErrorMessage"] = "Record whit this Id not exist.";
                return RedirectToAction("Error", "Home");
            }

            try
            {
                await this.employeeService.SoftDeleteEmployeeByIdAsync(id);

                string returnUrl = Request.Headers["Referer"].ToString();

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred";
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult QualificationTraining()
        {
            return View();
        }
    }
}
