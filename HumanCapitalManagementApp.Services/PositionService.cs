namespace HumanCapitalManagementApp.Services
{
    using Microsoft.EntityFrameworkCore;

    using Interfaces;
    using ViewModels.Employee;
    using HumanCapitalManagementApp.Data;

    public class PositionService : IPositionService
    {
        private readonly HumanCapitalManagementAppDbContext dbContext;

        public PositionService(HumanCapitalManagementAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AllPositionsModel>> AllPositionsAsync()
        {
            IEnumerable<AllPositionsModel> allPositions = await this.dbContext
                .Positions
                .AsNoTracking()
                .Select(p => new AllPositionsModel()
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToArrayAsync();
            return allPositions;
        }

        public async Task<IEnumerable<string>> AllPositionNamesAsync()
        {
            IEnumerable<string> allNames = await this.dbContext
                .Positions
                .Select(p => p.Name)
                .ToArrayAsync();
            return allNames;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            bool result = await this.dbContext
                .Positions
                .AnyAsync(p => p.Id == id);
            return result;
        }
    }
}