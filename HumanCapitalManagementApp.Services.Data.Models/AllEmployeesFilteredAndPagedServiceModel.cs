namespace HumanCapitalManagementApp.Services.Data.Models
{
    using ViewModels.Employee;

    public class AllEmployeesFilteredAndPagedServiceModel
    {
        public AllEmployeesFilteredAndPagedServiceModel()
        {
            this.Employees = new HashSet<AllEmployeesViewModel>();
        }

        public int TotalEmployeesCount { get; set; }
        public IEnumerable<AllEmployeesViewModel> Employees { get; set; }
    }
}