namespace HumanCapitalManagementApp.Services
{
    using Microsoft.EntityFrameworkCore;

    using Interfaces;
    using HumanCapitalManagementApp.Data;
    using ViewModels.QualificationTraining;

    public class QualificationTrainingService : IQualificationTrainingService
    {
        private readonly HumanCapitalManagementAppDbContext dbContext;

        public QualificationTrainingService(HumanCapitalManagementAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AllQualificationTrainingViewModel>> AllTrainigs()
        {
            var trainings = await this.dbContext
                .QualificationsTraining
                .AsNoTracking()
                .Select(t => new AllQualificationTrainingViewModel
                {
                    EmployeeId = t.EmployeeId,
                    Name = t.Name,
                    From = t.From,
                    To = t.To,
                    Description = t.Description
                })
                .OrderByDescending(t => t.From)
                .ToListAsync();
            return trainings;
        }
    }
}