namespace HumanCapitalManagementApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Role
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string NormalizedName { get; set; }
        
        public string ConcurrencyStamp { get; set; }

        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}