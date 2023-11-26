using System.ComponentModel.DataAnnotations;
namespace RentaCar.Models
{
    public class Rates
    {
        [Key]
        public int RateId { get; set; }
        public string RateName { get; set; } = string.Empty;
        public int RateTypeId { get; set; } 
        public string RateType { get; set; } = string.Empty; //Day, Week Month
        public string RateDescription { get; set; } = string.Empty;
        public int RateAmount { get; set; }
    }
}
