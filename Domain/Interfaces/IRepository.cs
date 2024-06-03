using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T?> UpdateAsync(T entity);
        Task<T?> DeleteAsync(int id);
    }

    public interface IShipmentRepository : IRepository<Shipment>
    {
        Task<IEnumerable<Shipment>> GetShipmentsByTruckIdAsync(int truckId);
        Task<IEnumerable<Shipment>> GetShipmentsByWarehouseIdAsync(int warehouseId);
    }

    public interface ITruckRepository : IRepository<Truck>
    {
        Task<Truck?> GetByLicensePlateAsync(string licencePlate);
        Task<Truck?> GetByDriverCardNumberAsync(string driverCardNumber);
    }

    public interface IWarehouseRepository : IRepository<Warehouse>
    {
        Task<IEnumerable<Warehouse>> GetByCountryAsync(string country);
        Task<IEnumerable<Warehouse>> GetByCityAsync(string city);
    }
}
