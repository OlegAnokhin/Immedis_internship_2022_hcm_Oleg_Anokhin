namespace HumanCapitalManagementApp.Services.Interfaces
{
    using ViewModels.PreviousPositions;

    public interface IPreviousPositionService
    {
        Task<IEnumerable<AllPreviousPositionsViewModel>> TakeAllPreviousPositionsByIdAsync(int employeeId);

        Task AddPreviousPositionAsync (int id, AddPreviousPositionsFormModel model);

        Task DeletePreviousPositionByIdAsync (int id);

        Task<bool> ExistByIdAsync(int id);
    }
}