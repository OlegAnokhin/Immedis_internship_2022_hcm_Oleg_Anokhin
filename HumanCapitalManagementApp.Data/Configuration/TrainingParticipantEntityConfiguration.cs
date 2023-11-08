namespace HumanCapitalManagementApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using Models;

    public class TrainingParticipantEntityConfiguration : IEntityTypeConfiguration<TrainingParticipant>
    {
        public void Configure(EntityTypeBuilder<TrainingParticipant> builder)
        {
            builder
                .HasKey(t => new { t.ParticipantId, t.TrainingId });

            builder
                .HasOne(t => t.Training)
                .WithMany(tp => tp.TrainingParticipants)
                .HasForeignKey(t => t.TrainingId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(tp => tp.Participant)
                .WithMany()
                .HasForeignKey(tp => tp.ParticipantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}