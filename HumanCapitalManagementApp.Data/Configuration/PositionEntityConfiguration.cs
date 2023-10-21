namespace HumanCapitalManagementApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using Seeding;
    using Models;

    public class PositionEntityConfiguration : IEntityTypeConfiguration<Position>
    {
        private readonly PositionSeeder positionSeeder;

        public PositionEntityConfiguration()
        {
            this.positionSeeder = new PositionSeeder();
        }

        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasData(this.positionSeeder.GeneratePositions());
        }
    }
}