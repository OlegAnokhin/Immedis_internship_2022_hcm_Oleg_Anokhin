namespace HumanCapitalManagementApp.ViewModels.PreviousPositions
{
    public class AllPreviousPositionsViewModel
    {
        public int Id { get; set; }

        public string Department { get; set; } = null!;

        public string Position { get; set; } = null!;

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public decimal Salary { get; set; }

        public int EmployeeId { get; set; }
    }
}