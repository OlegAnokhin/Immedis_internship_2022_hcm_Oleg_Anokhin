namespace HumanCapitalManagementApp.ViewModels.Employee.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum EmployeeSorting
    {
        [Display(Name = "Last added")]
        Newest = 0,
        [Display(Name = "First added")]
        Oldest = 1
    }
}