namespace HumanCapitalManagementApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.EntityValidationConstants.Employee;

    public class Employee
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(UserNameMaxLength)]
        public string UserName { get; set; } = null!;

        [Required]
        public string HashedPassword { get; set; } = null!;

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; } = null!;

        public bool IsHired { get; set; }

        public DateTime HireDate { get; set; }

        public int PositionId { get; set; }

        [Required]
        [ForeignKey(nameof(PositionId))]
        public Position Position { get; set; }

        [Required]
        public decimal Salary { get; set; }

        public int DepartmentId { get; set; }

        [Required]
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }

        public int EmployeeInfoId { get; set; }

        [Required]
        [ForeignKey(nameof(EmployeeInfoId))]
        public EmployeeInfo EmployeeInfo { get; set; }

        public List<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
    }
}