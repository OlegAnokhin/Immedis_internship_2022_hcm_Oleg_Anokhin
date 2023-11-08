namespace HumanCapitalManagementApp.Services.Interfaces
{
    using ViewModels.PreviousPositions;

    public interface IPreviousPositionService
    {
        Task<DetailsPreviousPositionsViewModel> AllPreviousPositionsByIdAsync(int employeeId);

        Task AddPreviousPositionAsync (int id, AddPreviousPositionsFormModel model);

        Task DeletePreviousPositionByIdAsync (int id);

        Task<bool> ExistByIdAsync(int id);
    }
}