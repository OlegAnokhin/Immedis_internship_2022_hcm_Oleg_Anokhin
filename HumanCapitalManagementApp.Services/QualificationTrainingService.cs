namespace HumanCapitalManagementApp.Services
{
    using Microsoft.EntityFrameworkCore;

    using Interfaces;
    using ViewModels.QualificationTraining;
    using HumanCapitalManagementApp.Data;
    using HumanCapitalManagementApp.Data.Models;
    using System.Collections.Generic;

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

        public async Task<AllQualificationTrainingViewModel> GetTrainingByIdAsync(int id)
        {
            return await this.dbContext.QualificationsTraining
                .Where(qt => qt.Id == id)
                .Select(qt => new AllQualificationTrainingViewModel
                {
                    Id = qt.Id,
                    Name = qt.Name,
                    From = qt.From,
                    To = qt.To,
                    Description = qt.Description
                })
                .FirstAsync();
        }

        public async Task<List<AllQualificationTrainingViewModel>> GetJoinedTrainings(int employeeId)
        {
            return await this.dbContext.TrainingParticipants
                .Where(qt => qt.ParticipantId == employeeId)
                .Select(qt => new AllQualificationTrainingViewModel
                {
                    Id = qt.TrainingId,
                    Name = qt.Training.Name,
                    From = qt.Training.From,
                    To = qt.Training.To,
                    Description = qt.Training.Description
                })
                .ToListAsync();
        }

        public async Task JoinToTrainingAsync(int employeeId, AllQualificationTrainingViewModel model)
        {
            if (!await dbContext.TrainingParticipants.AnyAsync(tp =>
                    tp.ParticipantId == employeeId && tp.TrainingId == model.Id))
            {
                await dbContext.TrainingParticipants.AddAsync(new TrainingParticipant
                {
                    ParticipantId = employeeId,
                    TrainingId = model.Id
                });
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task LeaveFromTrainingAsync(int employeeId, AllQualificationTrainingViewModel model)
        {
            var participant = await dbContext.TrainingParticipants
                .FirstOrDefaultAsync(tp => tp.ParticipantId == employeeId && tp.TrainingId == model.Id);
            if (participant != null)
            {
                dbContext.TrainingParticipants.Remove(participant);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}