namespace HumanCapitalManagementApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Department;


    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DepartmentNameMaxLength)]
        public string Name { get; set; } = null!;

        public List<Employee> Employees { get; set; } = null!;
    }
}