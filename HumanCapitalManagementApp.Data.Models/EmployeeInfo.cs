namespace HumanCapitalManagementApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class EmployeeInfo
    {
        [Key]
        public int Id { get; set; }

        public string Position { get; set; } = null!;

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public decimal Salary { get; set; }

        public int EmployeeId { get; set; }
    }
}