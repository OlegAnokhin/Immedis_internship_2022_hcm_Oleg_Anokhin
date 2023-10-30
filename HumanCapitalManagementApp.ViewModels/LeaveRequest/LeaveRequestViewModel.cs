namespace HumanCapitalManagementApp.ViewModels.LeaveRequest
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.LeaveRequest;

    public class LeaveRequestViewModel
    {
        public LeaveRequestViewModel()
        {
            LeaveRequest = new HashSet<LeaveRequestViewModel>();
        }
        public int LeaveRequestId { get; set; }

        [Display(Name = "From date")]
        public DateTime From { get; set; }

        [Display(Name = "To date")]
        public DateTime To { get; set; }

        [Display(Name = "Description")]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Display(Name = "Vacation or sick leave")]
        [MaxLength(VacationOrSickLeaveMaxLength)]
        public string VacationOrSickLeave { get; set; }

        [Display(Name = "EmployeeId")]
        public int EmployeeId { get; set; }

        [Display(Name = "Approved")]
        public bool Approved { get; set; }

        public IEnumerable<LeaveRequestViewModel> LeaveRequest { get; set; }
    }
}