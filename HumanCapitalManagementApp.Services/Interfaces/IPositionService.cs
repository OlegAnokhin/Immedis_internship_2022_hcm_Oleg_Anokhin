namespace HumanCapitalManagementApp.Services.Interfaces
{
    using ViewModels.Employee;

    public interface IPositionService
    {
        /// <summary>
        /// Taking all positions
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AllPositionsModel>> AllPositionsAsync();

        /// <summary>
        /// Checking if the position exists
        /// </summary>
        /// <param name="id">Identifire of Position</param>
        /// <returns></returns>
        Task<bool> ExistsByIdAsync(int id);

        /// <summary>
        /// Taking all positions names
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> AllPositionNamesAsync();
    }
}