﻿namespace HumanCapitalManagementApp.Data.Models
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

        [Required]
        public int PositionId { get; set; }

        [ForeignKey(nameof(PositionId))]
        public Position Position { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }

        public List<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

        public List<QualificationTraining> QualificationsTraining { get; set; } = new List<QualificationTraining>();
        
        public List<PerformanceManagement> PerformancesManagement { get; set; } = new List<PerformanceManagement>();

        public List<PreviousPosition> PreviousPositions { get; set; } = new List<PreviousPosition>();
    }
}