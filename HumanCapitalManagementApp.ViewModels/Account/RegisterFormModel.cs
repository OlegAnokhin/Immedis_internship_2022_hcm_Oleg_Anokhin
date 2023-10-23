namespace HumanCapitalManagementApp.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    using ViewModels.Employee;

    using static Common.EntityValidationConstants.Employee;

    public class RegisterFormModel
    {
        public RegisterFormModel()
        {
            this.Positions = new HashSet<AllPositionsModel>();
            this.Departments = new HashSet<AllDepartmentsModel>();
        }

        [Required]
        [StringLength(UserNameMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = UserNameMinLength)]
        [Display(Name = "UserName")]
        public string UserName { get; set; } = null!;

        [Required]
        [RegularExpression(PasswordRegex, ErrorMessage = "Password must be more than 6 characters and contain at least 1 uppercase letter, 1 lowercase letter and 1 number.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        [StringLength(FirstNameMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = FirstNameMinLength)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = LastNameMinLength)]
        [Display(Name = "LastName")]
        public string LastName { get; set; } = null!;

        [Required]
        [RegularExpression(EmailRegex, ErrorMessage = "Enter a valid Email")]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [Display(Name = "Salary")]
        public decimal Salary { get; set; }

        [Display(Name = "Hire date")]
        public DateTime HireDate { get; set; }

        [Required]
        [Display(Name = "Position")]
        public int PositionId { get; set; }

        [Required] 
        public IEnumerable<AllPositionsModel> Positions { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required]
        public IEnumerable<AllDepartmentsModel> Departments { get; set; }
    }
}