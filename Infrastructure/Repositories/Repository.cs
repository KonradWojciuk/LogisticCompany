using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }

            return null;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T?> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }
    }

    public class ShipmentRepository : Repository<Shipment>, IShipmentRepository
    {
        public ShipmentRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Shipment>> GetShipmentsByTruckIdAsync(int truckId)
        {
            return await _context.Shipments
                .Where(s => s.TruckId == truckId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Shipment>> GetShipmentsByWarehouseIdAsync(int warehouseId)
        {
            return await _context.Shipments
                .Where(s => s.WarehouseId == warehouseId)
                .ToListAsync();
        }
    }

    public class TruckRepository : Repository<Truck>, ITruckRepository
    {
        public TruckRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Truck?> GetByLicensePlateAsync(string licensePlate)
        {
            return await Task.FromResult(_context.Trucks.FirstOrDefault(t => t.LicensePlate == licensePlate));
        }

        public async Task<Truck?> GetByDriverCardNumberAsync(string driverCardNumber)
        {
            return await Task.FromResult(_context.Trucks.FirstOrDefault(t => t.DriverCardNumber == driverCardNumber));
        }
    }

    public class WarehouseRepository : Repository<Warehouse>, IWarehouseRepository
    {
        public WarehouseRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Warehouse>> GetByCountryAsync(string country)
        {
            return await _context.Warehouses.Where(w => w.Country == country).ToListAsync();
        }

        public async Task<IEnumerable<Warehouse>> GetByCityAsync(string city)
        {
            return await _context.Warehouses.Where(w => w.City == city).ToListAsync();
        }
    }
}
