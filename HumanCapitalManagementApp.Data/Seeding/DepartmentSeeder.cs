namespace HumanCapitalManagementApp.Data.Seeding
{
    using Models;

    class DepartmentSeeder
    {
        internal Department[] GenerateDepartments()
        {
            ICollection<Department> departments = new HashSet<Department>();
            Department currentDepartment;

            currentDepartment = new Department()
            {
                Id = 1,
                Name = "Architect"
            };
            departments.Add(currentDepartment);

            currentDepartment = new Department()
            {
                Id = 2,
                Name = "Developer"
            };
            departments.Add(currentDepartment);

            currentDepartment = new Department()
            {
                Id = 3,
                Name = "UI/UX Designer"
            };
            departments.Add(currentDepartment);

            currentDepartment = new Department()
            {
                Id = 4,
                Name = "Tester"
            };
            departments.Add(currentDepartment);

            return departments.ToArray();
        }
    }
}