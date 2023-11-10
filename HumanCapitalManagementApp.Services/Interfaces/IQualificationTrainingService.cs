namespace HumanCapitalManagementApp.Services.Interfaces
{
    using ViewModels.QualificationTraining;

    public interface IQualificationTrainingService
    {
        Task<IEnumerable<AllQualificationTrainingViewModel>> AllTrainings();

        Task<IEnumerable<AllQualificationTrainingViewModel>> MyTrainings(int id);

        Task AddTrainingAsync(AllQualificationTrainingViewModel model);

        Task<bool> ExistsByIdAsync(int id);

        Task<AllQualificationTrainingViewModel> DetailsTrainingAsync(int id);

        Task DeleteTrainingAsync(int id);

        Task<AllQualificationTrainingViewModel> GetTrainingByIdAsync(int id);

        Task<List<AllQualificationTrainingViewModel>> GetJoinedTrainings(int employeeId);

        Task JoinToTrainingAsync(int employeeId, AllQualificationTrainingViewModel model);

        Task LeaveFromTrainingAsync(int employeeId, AllQualificationTrainingViewModel model);
    }
}