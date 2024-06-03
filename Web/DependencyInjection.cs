using Application.Interfaces;
using Application.Repositories;

namespace Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IShipmentRepository, ShipmentRepository>();
            services.AddScoped<ITruckRepository, TruckRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();

            return services;
        }
    }
}
