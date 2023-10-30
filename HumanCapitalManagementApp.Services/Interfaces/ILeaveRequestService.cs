namespace HumanCapitalManagementApp.Services.Interfaces
{
    using ViewModels.LeaveRequest;

    public interface ILeaveRequestService
    {
        Task<DetailsRequestsViewModel> AllLeaveRequestAsync(int id);
    }
}