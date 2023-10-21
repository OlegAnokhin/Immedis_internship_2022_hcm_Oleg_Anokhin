namespace HumanCapitalManagementApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.PerformanceManagement;

    public class PerformanceManagement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; } = null!;

        [MaxLength(CompletedТrainingMaxLength)]
        public string CompletedТraining { get; set; }

        [MaxLength(ParticipationInProjectsMaxLength)]
        public string ParticipationInProjects { get; set; }

        [MaxLength(ParticipationInTeamBuildingMaxLength)]
        public string ParticipationInTeamBuilding { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }
    }
}