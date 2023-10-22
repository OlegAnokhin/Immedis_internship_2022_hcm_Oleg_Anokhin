namespace HumanCapitalManagementApp.Services.Interfaces
{
    using ViewModels.Employee;

    public interface IDepartmentService
    {
        /// <summary>
        /// Taking all Departments
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AllDepartmentsModel>> AllDepartmentsAsync();

        /// <summary>
        /// Checking if the Department exists
        /// </summary>
        /// <param name="id">Identifire of Department</param>
        /// <returns></returns>
        Task<bool> ExistsByIdAsync(int id);

        /// <summary>
        /// Taking all Departments names
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> AllDepartmentNamesAsync();
    }
}
