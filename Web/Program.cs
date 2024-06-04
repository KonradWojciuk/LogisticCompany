using Infrastructure;
using Web.Middleware;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddInfrastructureService(builder.Configuration);
            builder.Services.AddWebServices();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCustomHeadersMiddleware();
            app.UseAuthorization();

            app.MapControllers();

            app.UseAuthorization();

            app.Run();
        }
    }
}
