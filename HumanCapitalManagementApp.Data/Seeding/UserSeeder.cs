namespace HumanCapitalManagementApp.Data.Seeding
{
    using Models;

    class UserSeeder
    {
        internal Employee[] GenerateEmployees()
        {
            ICollection<Employee> employees = new HashSet<Employee>();
            Employee currentEmployee;

            currentEmployee = new Employee
            {
                Id = 1,
                UserName = "admin",
                HashedPassword = "$2a$12$AQDe9jKSadLeCptcFmQrL.8OlWp73tRO3uTPR/mBiio/eFIS9VgYO",
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@admin.bg",
                PhoneNumber = "6666666666",
                IsHired = true,
                HireDate = new DateTime(2023,09,01),
                PositionId = 4,
                Salary = (decimal)5000.00,
                DepartmentId = 2
            };
            employees.Add(currentEmployee);

            return employees.ToArray();
        }
    }
}