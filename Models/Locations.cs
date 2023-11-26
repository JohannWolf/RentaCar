using System.ComponentModel.DataAnnotations;
namespace RentaCar.Models
{
    public class Locations
    {
        [Key]
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
    }
}
