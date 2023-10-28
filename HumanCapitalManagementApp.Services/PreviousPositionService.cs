﻿namespace HumanCapitalManagementApp.Services
{
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Interfaces;
    using ViewModels.PreviousPositions;
    using System.Data;

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
                .Where(pp => pp.EmployeeId == employeeId)
                .Select(p => new AllPreviousPositionsViewModel
                {
                    //Department = p.DepartmentId,
                    //Position = p.PositionId,
                    From = p.From,
                    To = p.To,
                    Salary = p.Salary,
                    EmployeeId = employeeId
                })
                .ToListAsync();
            return allPreviousPositions;
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

    }
}