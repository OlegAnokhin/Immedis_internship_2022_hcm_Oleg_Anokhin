namespace HumanCapitalManagementApp.Services.Interfaces
{
    using ViewModels.Employee;

    public interface IEmployeeService
    {
        Task<IEnumerable<AllEmployeesViewModel>> ListAllEmployeesAsync();

        Task<SuccessLoginViewModel> TakeEmployeeByIdAsync(int employeeId);

        Task<EmployeeInfoModel> TakeEmployeeInfoByIdAsync(int employeeId);

        Task<EditEmployeeViewModel> TakeEmployeeForEditByIdAsync(int employeeId);

        Task<bool> ExistByIdAsync(int employeeId);

        Task EditEmployeeByIdAsync(int employeeId, EditEmployeeViewModel model);
    }
}