using Application.Interfaces;
using Domain.Entities;

namespace Web.GraphQl
{
    public class Query
    {
        [UseFiltering]
        [UseSorting]
        public IQueryable<Shipment> GetShipments([Service] IShipmentRepository shipmentRepository) =>
            (IQueryable<Shipment>)shipmentRepository.GetAllAsync();

        [UseFiltering]
        [UseSorting]
        public IQueryable<Truck> GetTrucks([Service] ITruckRepository truckRepository) =>
            (IQueryable<Truck>)truckRepository.GetAllAsync();

        [UseFiltering]
        [UseSorting]
        public IQueryable<Warehouse> GetWarehouses([Service] IWarehouseRepository warehouseRepository) =>
            (IQueryable<Warehouse>)warehouseRepository.GetAllAsync();
    }
}
