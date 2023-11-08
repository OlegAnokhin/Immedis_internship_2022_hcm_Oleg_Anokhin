namespace HumanCapitalManagementApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.QualificationTraining;

    public class QualificationTraining
    {
        public QualificationTraining()
        {
            TrainingParticipants = new HashSet<TrainingParticipant>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; } = null!;

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public virtual ICollection<TrainingParticipant> TrainingParticipants { get; set; }
    }
}