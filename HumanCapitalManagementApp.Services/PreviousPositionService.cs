namespace HumanCapitalManagementApp.Services
{
    using Microsoft.EntityFrameworkCore;

    using Interfaces;
    using ViewModels.PreviousPositions;
    using System.Data;
    using HumanCapitalManagementApp.Data;
    using HumanCapitalManagementApp.Data.Models;

    public class PreviousPositionService : IPreviousPositionService
    {
        private readonly HumanCapitalManagementAppDbContext dbContext;

        public PreviousPositionService(HumanCapitalManagementAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<DetailsPreviousPositionsViewModel> AllPreviousPositionsByIdAsync(int id)
        {
            Employee? employee = await this.dbContext
                .Employees
                .Include(e => e.PreviousPositions)
                .FirstAsync(e => e.Id == id);

            var previousPositions = await dbContext
                .PreviousPositions
                .Where(pp => pp.EmployeeId == id)
                .Select(p => new AllPreviousPositionsViewModel
                {
                    Id = p.Id,
                    Department = p.Department.Name,
                    Position = p.Position.Name, 
                    From = p.From,
                    To = p.To,
                    Salary = p.Salary,
                    EmployeeId = id
                })
                .ToArrayAsync();
            return new DetailsPreviousPositionsViewModel()
            {
                EmployeeId = id,
                PreviousPositions = previousPositions
            };
        }

        public async Task AddPreviousPositionAsync(int id, AddPreviousPositionsFormModel model)
        {
            PreviousPosition position = new PreviousPosition
            {
                DepartmentId = model.DepartmentId,
                PositionId = model.PositionId,
                From = model.From,
                To = model.To,
                Salary = model.Salary,
                EmployeeId = id
            };
            try
            {
                await dbContext.PreviousPositions.AddAsync(position);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new InvalidExpressionException("Unexpected error.");
            }
        }

        public async Task DeletePreviousPositionByIdAsync(int id)
        {
            PreviousPosition previousPosition = await this.dbContext.PreviousPositions
                .FirstAsync(pp => pp.Id == id);

            this.dbContext.PreviousPositions.Remove(previousPosition);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistByIdAsync(int id)
        {
            return await this.dbContext
                .PreviousPositions
                .AnyAsync(pp => pp.Id == id);
        }
    }
}