namespace HumanCapitalManagementApp.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Employee;

    public class LoginFormModel
    {
        [Required]
        [StringLength(UserNameMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = UserNameMinLength)]
        [Display(Name = "Username")]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;
    }
}