namespace HumanCapitalManagementApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class QualificationTrainingEntityConfiguration : IEntityTypeConfiguration<QualificationTraining>
    {
        public void Configure(EntityTypeBuilder<QualificationTraining> builder)
        {
            builder
                .Property(t => t.From)
                .HasDefaultValue(DateTime.UtcNow);
            builder
                .Property(t => t.To)
                .HasDefaultValue(DateTime.UtcNow);
        }
    }
}