namespace Domain.Entities
{
    public class Truck
    {
        public int Id { get; set; }
        public string? DriverFirstName { get; set; }
        public string? DriverLastName { get; set; }
        public string? DriverPhoneNumber { get; set; }
        public string? LicencePlate { get; set; }
        public string? DriverCardNumber { get; set; }
        public ICollection<Shipment>? Shipment { get; set; }
    }
}
