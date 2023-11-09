namespace HumanCapitalManagementApp.Services.Interfaces
{
    using ViewModels.QualificationTraining;

    public interface IQualificationTrainingService
    {
        Task<IEnumerable<AllQualificationTrainingViewModel>> AllTrainings();

        Task AddTrainingAsync(AllQualificationTrainingViewModel model);

        Task<bool> ExistsByIdAsync(int id);

        Task<AllQualificationTrainingViewModel> DetailsTrainingAsync(int id);

        Task DeleteTrainingAsync(int id);
    }
}