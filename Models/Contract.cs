using System.ComponentModel.DataAnnotations;

namespace RentaCar.Models
{
    public class Contract
    {
        [Key]
        public int ContractId { get; set; }
        public Reservations Reservation { get; set; }
        public Customers Customer { get; set; }
        public Vehicles Vehicle { get; set; }
        public ICollection<Rates> Rates { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int NumberOfDays { get; set; }
        public Locations PickUpLocation { get; set; }
        public Locations ReturnLocation { get; set; }
        public int AmountToPay { get; set; }
        public Users ContractUser { get; set; }
    }
}
