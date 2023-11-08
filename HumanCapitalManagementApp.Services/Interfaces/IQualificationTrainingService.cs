namespace HumanCapitalManagementApp.Services.Interfaces
{
    using ViewModels.QualificationTraining;

    public interface IQualificationTrainingService
    {
        Task<IEnumerable<AllQualificationTrainingViewModel>> AllTrainigs();
    }
}