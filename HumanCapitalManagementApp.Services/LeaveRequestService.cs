namespace HumanCapitalManagementApp.Services
{
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Interfaces;
    using ViewModels.LeaveRequest;

    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly HumanCapitalManagementAppDbContext dbContext;

        public LeaveRequestService(HumanCapitalManagementAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddLeaveRequestAsync(int id, LeaveRequestViewModel model)
        {
            Employee? employee = await this.dbContext
                .Employees
                .FirstAsync(e => e.Id == id);
            LeaveRequest request = new LeaveRequest
            {
                EmployeeId = id,
                StartDate = model.From,
                EndDate = model.To,
                VacationOrSickLeave = model.VacationOrSickLeave,
                Description = model.Description,
                Approved = model.Approved
            };
            employee.LeaveRequests.Add(request);

            await dbContext.LeaveRequests.AddAsync(request);
            await dbContext.SaveChangesAsync();
        }

        public async Task<DetailsRequestsViewModel> AllLeaveRequestAsync(int id)
        {
            Employee? employee = await this.dbContext
                .Employees
                .Include(e => e.LeaveRequests)
                .FirstAsync(e => e.Id == id);

            var requests = await this.dbContext
                .LeaveRequests
                .Where(l => l.EmployeeId == id)
                .Select(l => new LeaveRequestViewModel
                {
                    LeaveRequestId = l.LeaveRequestId,
                    EmployeeId = id,
                    From = l.StartDate,
                    To = l.EndDate,
                    Description = l.Description,
                    VacationOrSickLeave = l.VacationOrSickLeave,
                    Approved = l.Approved,
                })
                .ToArrayAsync();
            return new DetailsRequestsViewModel()
            {
                EmployeeId = id,
                LeaveRequests = requests
            };
        }
    }
}
