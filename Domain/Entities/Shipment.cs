using Domain.Enums;

namespace Domain.Entities
{
    public class Shipment
    {
        public int Id { get; set; }
        public DateTime? LoaudDateTime { get; set; }
        public DateTime? UnLoaudDateTime { get; set; }
        public ShipmentStatus Status { get; set; }
        public int TruckId { get; set; }
        public Truck? Truck { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}
