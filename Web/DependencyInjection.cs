using Application.Interfaces;
using Application.Repositories;
using Web.GraphQl;

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

            services.AddGraphQLServer()
                    .AddQueryType<Query>()
                    .AddMutationType<Mutation>()
                    .AddFiltering()
                    .AddSorting();

            return services;
        }
    }
}
