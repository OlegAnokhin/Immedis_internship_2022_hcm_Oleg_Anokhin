namespace HumanCapitalManagementApp.Services.Interfaces
{
    using HumanCapitalManagementApp.Data.Models;

    using Models.Account;

    public interface IAccountService
    {
        Task RegisterEmployeeAsync(RegisterFormModel model);

        Task<(string, Employee)> LoginEmployeeAsync(LoginFormModel model);

        Task<int> TakeIdByUsernameAsync(string username);

        Task<bool> ExistByUsername(string username);

    }
}