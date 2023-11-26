using System.ComponentModel.DataAnnotations;
namespace RentaCar.Models
{
    public class Roles
    {
        [Key]
        public int RoleId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
