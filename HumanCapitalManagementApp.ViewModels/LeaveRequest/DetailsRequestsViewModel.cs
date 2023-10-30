namespace HumanCapitalManagementApp.ViewModels.LeaveRequest
{
    public class DetailsRequestsViewModel
    {
        public DetailsRequestsViewModel()
        {
            LeaveRequests = new HashSet<LeaveRequestViewModel>();
        }
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public IEnumerable<LeaveRequestViewModel> LeaveRequests { get; set; }
    }
}