namespace HumanCapitalManagementApp.Common
{
    public static class EntityValidationConstants
    {
        public static class Employee
        {
            public const int UserNameMinLength = 2;
            public const int UserNameMaxLength = 20;

            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 20;

            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 20;

        }

        public static class Department
        {
            public const int DepartmentNameMinLength = 2;
            public const int DepartmentNameMaxLength = 20;
        }

        public static class Position
        {
            public const int PositionNameMinLength = 2;
            public const int PositionNameMaxLength = 20;
        }


    }
}