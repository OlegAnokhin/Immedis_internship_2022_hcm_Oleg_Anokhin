namespace HumanCapitalManagementApp.ViewModels.QualificationTraining
{
    public class AllQualificationTrainingViewModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public string Description { get; set; }
    }
}