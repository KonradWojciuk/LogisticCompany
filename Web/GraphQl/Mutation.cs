using Application.Interfaces;
using Domain.Entities;

namespace Web.GraphQl
{
    public class Mutation
    {
        public async Task<Shipment> CreateShipment(Shipment shipment, [Service] IShipmentRepository shipmentRepository)
        {
            await shipmentRepository.AddAsync(shipment);
            return shipment;
        }

        public async Task<Truck> CreateTruck(Truck truck, [Service] ITruckRepository truckRepository)
        {
            await truckRepository.AddAsync(truck);
            return truck;
        }

        public async Task<Warehouse> CreateWarehouse(Warehouse warehouse, [Service] IWarehouseRepository warehouseRepository)
        {
            await warehouseRepository.AddAsync(warehouse);
            return warehouse;
        }

        public async Task<Shipment> UpdateShipment(int id, Shipment shipment, [Service] IShipmentRepository shipmentRepository)
        {
            if (id != shipment.Id)
                throw new Exception("Invalid ID");

            await shipmentRepository.UpdateAsync(shipment);
            return shipment;
        }

        public async Task<Truck> UpdateTruck(int id, Truck truck, [Service] ITruckRepository truckRepository)
        {
            if (id != truck.Id)
                throw new Exception("Invalid ID");

            await truckRepository.UpdateAsync(truck);
            return truck;
        }

        public async Task<Warehouse> UpdateWarehouse(int id, Warehouse warehouse, [Service] IWarehouseRepository warehouseRepository)
        {
            if (id != warehouse.Id)
                throw new Exception("Invalid ID");

            await warehouseRepository.UpdateAsync(warehouse);
            return warehouse;
        }

        public async Task<bool> DeleteShipment(int id, [Service] IShipmentRepository shipmentRepository)
        {
            await shipmentRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> DeleteTruck(int id, [Service] ITruckRepository truckRepository)
        {
            await truckRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> DeleteWarehouse(int id, [Service] IWarehouseRepository warehouseRepository)
        {
            await warehouseRepository.DeleteAsync(id);
            return true;
        }
    }
}
