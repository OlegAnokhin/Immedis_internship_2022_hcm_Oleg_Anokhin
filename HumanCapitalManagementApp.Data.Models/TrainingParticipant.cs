namespace HumanCapitalManagementApp.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class TrainingParticipant
    {
        public int ParticipantId { get; set; }

        public Employee Participant { get; set; }

        public int TrainingId { get; set; }

        [ForeignKey(nameof(TrainingId))]
        public QualificationTraining Training { get; set; }
    }
}