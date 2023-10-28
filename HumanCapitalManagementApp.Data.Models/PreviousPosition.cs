namespace HumanCapitalManagementApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PreviousPosition
    {
        [Key]
        public int Id { get; set; }

        public string Department { get; set; }

        public string Position { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public decimal Salary { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}