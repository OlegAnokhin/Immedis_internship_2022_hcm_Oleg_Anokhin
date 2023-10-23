namespace HumanCapitalManagementApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            builder
                .HasOne(er => er.Employee)
                .WithMany()
                .HasForeignKey(er => er.UserId);

            builder
                .HasOne(er => er.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(er => er.RoleId);
        }
    }
}