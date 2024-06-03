namespace Domain.Entities
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }
        public string? Region { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Street { get; set; }
        public string? StreetNumber { get; set; }
        public ICollection<Shipment>? Shipment { get; set; }
    }
}
