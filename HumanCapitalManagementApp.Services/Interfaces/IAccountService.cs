namespace HumanCapitalManagementApp.Services.Interfaces
{
    using System.Security.Claims;
    using Models.Account;

    public interface IAccountService
    {
        Task RegisterEmployeeAsync(RegisterFormModel model);

        //Task<ClaimsIdentity> LoginEmployeeAsync(LoginFormModel model);

        Task<(ClaimsIdentity Identity, string Token)> LoginEmployeeAsync(LoginFormModel model);

        Task<int> TakeIdByUsernameAsync(string username);

        Task<bool> ExistByUsername(string username);

    }
}