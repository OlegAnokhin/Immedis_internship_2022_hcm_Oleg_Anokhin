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
                Name = "Architects"
            };
            departments.Add(currentDepartment);

            currentDepartment = new Department()
            {
                Id = 2,
                Name = "Developers"
            };
            departments.Add(currentDepartment);

            currentDepartment = new Department()
            {
                Id = 3,
                Name = "UI/UX Designers"
            };
            departments.Add(currentDepartment);

            currentDepartment = new Department()
            {
                Id = 4,
                Name = "Testers"
            };
            departments.Add(currentDepartment);

            return departments.ToArray();
        }
    }
}