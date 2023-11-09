namespace HumanCapitalManagementApp.Services
{
    using Microsoft.EntityFrameworkCore;

    using Interfaces;
    using ViewModels.QualificationTraining;
    using HumanCapitalManagementApp.Data;
    using HumanCapitalManagementApp.Data.Models;

    public class QualificationTrainingService : IQualificationTrainingService
    {
        private readonly HumanCapitalManagementAppDbContext dbContext;

        public QualificationTrainingService(HumanCapitalManagementAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AllQualificationTrainingViewModel>> AllTrainings()
        {
            var trainings = await this.dbContext
                .QualificationsTraining
                .AsNoTracking()
                .Select(t => new AllQualificationTrainingViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    From = t.From,
                    To = t.To,
                    Description = t.Description
                })
                .OrderByDescending(t => t.From)
                .ToListAsync();
            return trainings;
        }

        public async Task AddTrainingAsync(AllQualificationTrainingViewModel model)
        {
            QualificationTraining training = new QualificationTraining
            {
                Name = model.Name,
                From = model.From,
                To = model.To,
                Description = model.Description
            };

            await this.dbContext.QualificationsTraining.AddAsync(training);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await this.dbContext.QualificationsTraining
                .AnyAsync(qt => qt.Id == id);
        }

        public async Task<AllQualificationTrainingViewModel> DetailsTrainingAsync(int id)
        {
            QualificationTraining training = await this.dbContext
                .QualificationsTraining
                .FirstAsync(qt => qt.Id == id);

            return new AllQualificationTrainingViewModel
            {
                Id = training.Id,
                Name = training.Name,
                From = training.From,
                To = training.To,
                Description = training.Description
            };
        }

        public async Task DeleteTrainingAsync(int id)
        {
            QualificationTraining training = await this.dbContext
                .QualificationsTraining
                .FirstAsync(qt => qt.Id == id);

            var participants = await this.dbContext.TrainingParticipants
                .Where(p => p.TrainingId == id)
                .ToListAsync();

            if (participants.Any())
            {
                this.dbContext.TrainingParticipants.RemoveRange(participants);
                await this.dbContext.SaveChangesAsync();
            }

            this.dbContext.QualificationsTraining.Remove(training);
            await this.dbContext.SaveChangesAsync();
        }
    }
}