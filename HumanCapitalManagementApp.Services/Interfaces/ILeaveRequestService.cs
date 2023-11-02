namespace HumanCapitalManagementApp.Services.Interfaces
{
    using ViewModels.LeaveRequest;

    public interface ILeaveRequestService
    {
        Task<DetailsRequestsViewModel> AllLeaveRequestAsync(int id);

        Task AddLeaveRequestAsync(int id, LeaveRequestViewModel model);

        Task<IEnumerable<LeaveRequestViewModel>> AllRequestsForAdmin();

        Task SetApproveAsync(int id);

        Task DeleteLeaveRequestByIdAsync(int id);
    }
}