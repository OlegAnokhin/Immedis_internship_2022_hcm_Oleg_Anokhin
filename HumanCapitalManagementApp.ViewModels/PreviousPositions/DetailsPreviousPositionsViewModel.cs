namespace HumanCapitalManagementApp.ViewModels.PreviousPositions
{
    public class DetailsPreviousPositionsViewModel
    {
        public DetailsPreviousPositionsViewModel()
        {
            PreviousPositions = new HashSet<AllPreviousPositionsViewModel>();
        }
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public IEnumerable<AllPreviousPositionsViewModel> PreviousPositions { get; set; }
    }
}