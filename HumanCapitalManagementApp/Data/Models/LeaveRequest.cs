namespace HumanCapitalManagementApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.LeaveRequest;

    public class LeaveRequest
    {
        [Key]
        public int LeaveRequestId { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        
        public Employee Employee { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(VacationOrSickLeaveMaxLength)]
        public string VacationOrSickLeave { get; set; } = null!;

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public bool Approved { get; set; }
    }
}