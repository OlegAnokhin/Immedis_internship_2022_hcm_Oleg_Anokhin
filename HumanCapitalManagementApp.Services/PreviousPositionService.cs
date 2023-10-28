namespace HumanCapitalManagementApp.Services
{
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Interfaces;
    using ViewModels.PreviousPositions;

    public class PreviousPositionService : IPreviousPositionService
    {
        private readonly HumanCapitalManagementAppDbContext dbContext;

        public PreviousPositionService(HumanCapitalManagementAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AllPreviousPositionsViewModel>> TakeAllPreviousPositionsByIdAsync(int employeeId)
        {
            IEnumerable<AllPreviousPositionsViewModel> allPreviousPositions = await dbContext
                .PreviousPositions
                .Include(e => e.Employee)
                .Where(pp => pp.EmployeeId == employeeId)
                .Select(p => new AllPreviousPositionsViewModel
                {
                    Department = p.Employee.Department.Name,
                    Position = p.Employee.Position.Name,
                    From = p.Employee.HireDate,
                    To = p.To,
                    Salary = p.Employee.Salary,
                    EmployeeId = employeeId
                })
                .ToListAsync();
            return allPreviousPositions;
        }

        public async Task AddPreviousPositionAsync(AddPreviousPositionsFormModel model)
        {
            PreviousPosition position = new PreviousPosition
            {
                DepartmentId = model.DepartmentId,
                PositionId = model.PositionId,
                From = model.From,
                To = model.To,
                Salary = model.Salary
            };
        }

    }
}