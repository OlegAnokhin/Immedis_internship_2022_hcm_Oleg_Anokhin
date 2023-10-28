namespace HumanCapitalManagementApp.ViewModels.PreviousPositions
{
    using System.ComponentModel.DataAnnotations;

    using Employee;

    public class AddPreviousPositionsFormModel
    {
        public AddPreviousPositionsFormModel()
        {
            Positions = new HashSet<AllPositionsModel>();
            Departments = new HashSet<AllDepartmentsModel>();
        }

        public int EmployeeId { get; set; }

        [Required]
        [Display(Name = "Salary")]
        public decimal Salary { get; set; }

        [Display(Name = "From date")]
        public DateTime From { get; set; }
        
        [Display(Name = "To date")]
        public DateTime To { get; set; }

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