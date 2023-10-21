namespace HumanCapitalManagementApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Position;


    public class Position
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PositionNameMaxLength)]
        public string Name { get; set; } = null!;

        public List<Employee> Employees { get; set; } = null!;
    }
}