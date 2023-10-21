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

        public static class QualificationTraining
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 20;

            public const int DescriptionMinLength = 2;
            public const int DescriptionMaxLength = 1024;
        }
        
        public static class LeaveRequest
        {
            public const int VacationOrSickLeaveMinLength = 2;
            public const int VacationOrSickLeaveMaxLength = 20;

            public const int DescriptionMinLength = 2;
            public const int DescriptionMaxLength = 1024;
        }
        
        public static class PerformanceManagement
        {
            public const int CompletedТrainingMinLength = 2;
            public const int CompletedТrainingMaxLength = 1024;
            
            public const int ParticipationInProjectsMinLength = 2;
            public const int ParticipationInProjectsMaxLength = 1024;

            public const int ParticipationInTeamBuildingMinLength = 2;
            public const int ParticipationInTeamBuildingMaxLength = 1024;

            public const int DescriptionMinLength = 2;
            public const int DescriptionMaxLength = 1024;
        }
    }
}