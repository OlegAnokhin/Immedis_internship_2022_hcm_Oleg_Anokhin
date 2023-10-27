namespace HumanCapitalManagementApp.ViewModels.Employee
{
    public class EmployeeInfoModel
    {
        public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Position { get; set; } = null!;

        public string Department { get; set; } = null!;

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public decimal Salary { get; set; }

        public int EmployeeId { get; set; }
    }
}