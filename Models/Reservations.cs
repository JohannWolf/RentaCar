using System.ComponentModel.DataAnnotations;
namespace RentaCar.Models
{
    public class Reservations
    {
        [Key]
        public int ReservationId { get; set; }
        public Customers Customer { get; set; }
         public Vehicles Vehicle {  get; set; }
        public ICollection<Rates> Rates { get; set; }
        public DateTime ExpectedPickUpDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public int NumberOfDays { get; set; }
        public Locations ExpectedPickUpLocation { get; set; }
        public Locations ExpectedReturnLocation { get; set; }
        public int ExpectedAmountToPay { get; set; }
        public Users ReservationUser { get; set; }
    }
}
