namespace HumanCapitalManagementApp.Services.Interfaces
{
    using Data.Models;
    using ViewModels.Employee;

    public interface IEmployeeService
    {
        //Task<IEnumerable<AllEmployeesViewModel>> ListAllEmployeesAsync();
        Task<AllEmployeesFilteredAndPagedServiceModel> AllAsync(AllEmployeesQueryModel model);

        Task<SuccessLoginViewModel> TakeEmployeeByIdAsync(int employeeId);

        Task<EmployeeInfoModel> TakeEmployeeInfoByIdAsync(int employeeId);

        Task<EditEmployeeViewModel> TakeEmployeeForEditByIdAsync(int employeeId);

        Task<bool> ExistByIdAsync(int employeeId);

        Task EditEmployeeByIdAsync(int employeeId, EditEmployeeViewModel model);

        Task SetIsHiredOnTrue(int id);

        Task SoftDeleteEmployeeByIdAsync(int id);
    }
}