namespace HumanCapitalManagementApp.ViewModels.Employee
{
    using Enums;
    using static Common.GeneralAppConstants;

    public class AllEmployeesQueryModel
    {
        public AllEmployeesQueryModel()
        {
            this.CurrentPage = DefaultPage;
            this.EmployeesPerPage = EntitiesPerPage;

            this.Departments = new HashSet<string>();
            this.Positions = new HashSet<string>();
            this.Employees = new HashSet<AllEmployeesViewModel>();
        }

        /// <summary>
        /// Sorting by Departments
        /// </summary>
        public string? Department { get; set; }
        
        /// <summary>
        /// Sorting by Positions
        /// </summary>
        public string? Position { get; set; }

        /// <summary>
        /// Search by Name
        /// </summary>
        public string? SearchString { get; set;}

        /// <summary>
        /// Ordering employees by date
        /// </summary>
        public EmployeeSorting EmployeeSorting { get; set; }

        public int CurrentPage { get; set; }

        public int EmployeesPerPage { get; set; }

        public int TotalEmployees { get; set; }

        public IEnumerable<string> Departments { get; set; }

        public IEnumerable<string> Positions { get; set; }

        public IEnumerable<AllEmployeesViewModel> Employees { get; set; }
    }
}