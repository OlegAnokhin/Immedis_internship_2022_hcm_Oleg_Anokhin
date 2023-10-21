namespace HumanCapitalManagementApp.Data.Models
{
    public class UserRole
    {
        
        public int UserId { get; set; }

        public Employee Employee { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}