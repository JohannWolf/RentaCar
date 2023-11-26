using System.ComponentModel.DataAnnotations;
namespace RentaCar.Models
{
    public class RateTypes
    {
        [Key]
        public int RateTypeId { get; set; }
        public string RateType { get; set; } = string.Empty; //Day, Week Month
        public string RateTypeDescription { get; set; } = string.Empty;
    }
}
