namespace HumanCapitalManagementApp.Services
{
    using Microsoft.EntityFrameworkCore;

    using Interfaces;
    using ViewModels.LeaveRequest;
    using HumanCapitalManagementApp.Data.Models;
    using HumanCapitalManagementApp.Data;

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

        public async Task<IEnumerable<LeaveRequestViewModel>> AllRequestsForAdmin()
        {
            return await dbContext.LeaveRequests
                .Select(lr => new LeaveRequestViewModel
                {
                    LeaveRequestId = lr.LeaveRequestId,
                    From = lr.StartDate,
                    To = lr.EndDate,
                    Description = lr.Description,
                    VacationOrSickLeave = lr.VacationOrSickLeave,
                    EmployeeId = lr.EmployeeId,
                    Approved = lr.Approved
                })
                .OrderByDescending(d => d.From)
                .ToListAsync();
        }

        public async Task SetApproveAsync(int id)
        {
            LeaveRequest request = await this.dbContext
                .LeaveRequests
                .FirstAsync(r => r.LeaveRequestId == id);

            if (request.Approved == false)
            {
                request.Approved = true;
            }
            else if (request.Approved == true)
            {
                request.Approved = false;
            }

            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteLeaveRequestByIdAsync(int id)
        {
            LeaveRequest request = await this.dbContext
                .LeaveRequests
                .FirstAsync(r => r.LeaveRequestId == id);

            this.dbContext.LeaveRequests.Remove(request);
            await this.dbContext.SaveChangesAsync();
        }

    }
}
