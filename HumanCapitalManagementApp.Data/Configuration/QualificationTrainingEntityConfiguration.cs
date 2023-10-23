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
                .HasOne(qt => qt.Employee)
                .WithMany(e => e.QualificationsTraining)
                .HasForeignKey(qt => qt.EmployeeId);
        }
    }
}