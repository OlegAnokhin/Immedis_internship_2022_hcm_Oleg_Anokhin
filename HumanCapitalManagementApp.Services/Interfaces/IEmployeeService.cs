namespace HumanCapitalManagementApp.Services.Interfaces
{
    using ViewModels.Employee;

    public interface IEmployeeService
    {
        Task<IEnumerable<AllEmployeesViewModel>> ListAllEmployeesAsync();

        Task<SuccessLoginViewModel> TakeEmployeeByIdAsync(int employeeId);

        Task<EmployeeInfoModel> TakeEmployeeInfoByIdAsync(int employeeId);


    }
}