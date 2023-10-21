namespace HumanCapitalManagementApp.ViewModels.Employee
{
    public class AllEmployeesViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public bool IsHired { get; set; }

        public DateTime HireDate { get; set; }

        public string Position { get; set; }

        public string Department { get; set; }
    }
}