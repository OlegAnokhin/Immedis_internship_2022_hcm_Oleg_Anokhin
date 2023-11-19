namespace HumanCapitalManagementApp.ViewModels.Employee
{
    using QualificationTraining;

    public class SalaryInfoModel
    {
        public SalaryInfoModel()
        {
            Trainings = new HashSet<AllQualificationTrainingViewModel>();
        }
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public decimal Salary { get; set; }

        public decimal Bonus { get; set; }

        public decimal TotalSalary { get; set; }

        public int CompleteTrainingsCount { get; set; }

        public TimeSpan TimeWorked { get; set; }

        public IEnumerable<AllQualificationTrainingViewModel> Trainings { get; set; }
    }
}