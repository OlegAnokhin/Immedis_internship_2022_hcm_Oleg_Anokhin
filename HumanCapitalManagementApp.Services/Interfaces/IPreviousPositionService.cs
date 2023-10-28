namespace HumanCapitalManagementApp.Services.Interfaces
{
    using ViewModels.PreviousPositions;

    public interface IPreviousPositionService
    {
        Task<IEnumerable<AllPreviousPositionsViewModel>> TakeAllPreviousPositionsByIdAsync(int employeeId);

        Task AddPreviousPositionAsync (AddPreviousPositionsFormModel model);
    }
}