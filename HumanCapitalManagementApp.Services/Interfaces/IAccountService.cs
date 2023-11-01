namespace HumanCapitalManagementApp.Services.Interfaces
{
    using Models.Account;

    public interface IAccountService
    {
        Task RegisterEmployeeAsync(RegisterFormModel model);

        Task<string> LoginEmployeeAsync(LoginFormModel model);

        Task<int> TakeIdByUsernameAsync(string username);

    }
}