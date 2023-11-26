using System.ComponentModel.DataAnnotations;

namespace RentaCar.Models
{
    public class Customers
    {
        [Key]
        public int CustomerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string LicenceNumber { get; set; } = string.Empty;
        public int LicenceExpYear { get; set; }
        public int LicenceExpMonth { get; set; }
    }
}
