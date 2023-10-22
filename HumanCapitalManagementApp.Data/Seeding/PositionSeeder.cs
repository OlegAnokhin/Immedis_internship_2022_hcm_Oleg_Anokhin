namespace HumanCapitalManagementApp.Data.Seeding
{
    using Models;

    class PositionSeeder
    {
        internal Position[] GeneratePositions()
        {
            ICollection<Position> positions = new HashSet<Position>();
            Position currentPosition;

            currentPosition = new Position()
            {
                Id = 1,
                Name = "Internal Developer"
            };
            positions.Add(currentPosition);

            currentPosition = new Position()
            {
                Id = 2,
                Name = "Junior Developer"
            };
            positions.Add(currentPosition);

            currentPosition = new Position()
            {
                Id = 3,
                Name = "Mid Developer"
            };
            positions.Add(currentPosition);

            currentPosition = new Position()
            {
                Id = 4,
                Name = "Senior Developer"
            };
            positions.Add(currentPosition);

            currentPosition = new Position()
            {
                Id = 5,
                Name = "Junior Designer"
            };
            positions.Add(currentPosition);

            currentPosition = new Position()
            {
                Id = 6,
                Name = "Senior Designer"
            };
            positions.Add(currentPosition);
            
            currentPosition = new Position()
            {
                Id = 7,
                Name = "Junior Tester"
            };
            positions.Add(currentPosition);

            currentPosition = new Position()
            {
                Id = 8,
                Name = "Senior Tester"
            };
            positions.Add(currentPosition);

            return positions.ToArray();
        }
    }
}