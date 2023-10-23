namespace HumanCapitalManagementApp.Services.Interfaces
{
    using Models.Account;

    public interface IAccountService
    {
        Task RegisterEmployeeAsync(RegisterFormModel model);

        Task<bool> ExistByUsernameAsync(string username);

        Task LoginEmployeeAsync(LoginFormModel model);


    }
}