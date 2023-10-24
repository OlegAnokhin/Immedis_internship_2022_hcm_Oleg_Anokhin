namespace HumanCapitalManagementApp.Services.Interfaces
{
    using Models.Account;

    public interface IAccountService
    {
        Task RegisterEmployeeAsync(RegisterFormModel model);

        Task<bool> LoginEmployeeAsync(LoginFormModel model);

        Task<int> TakeIdByUsernameAsync(string username);

    }
}