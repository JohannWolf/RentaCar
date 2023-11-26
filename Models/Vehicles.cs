using System.ComponentModel.DataAnnotations;
namespace RentaCar.Models
{
    public class Vehicles
    {
        [Key]
        public int VehicleId { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Transmission {  get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public int NumberOfSeats { get; set; }
        public string PlateNumber {  get; set; } = string.Empty;
        public string SerialNumber {  get; set; } = string.Empty;
        public string FuelType { get; set; } = string.Empty;//Gas, diesel, hybrid, etc
        public string AVGMPG { get; set; } = string.Empty;
        public string InitialCost { get; set; } = string.Empty;
        public Locations OwnerLocation {  get; set; }
        public Locations CurrentLocation { get; set; }
        public string ParkingSpot {  get; set; } = string.Empty;
    }
}
