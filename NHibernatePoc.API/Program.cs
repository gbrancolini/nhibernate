using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernatePoc.Core.Domain.Interfaces;
using NHibernatePoc.Core.Services;
using NHibernatePoc.Infraestructure.Data.Configurations;
using NHibernatePoc.Infraestructure.Data.Repositories;

namespace NHibernatePoc.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddNHibernate(
                builder.Configuration.GetConnectionString("DefaultConnection"), 
                builder.Environment.IsDevelopment());

            builder.Services.AddTransient<IInventoryRepository, InventoryRepository>();
            builder.Services.AddTransient<IInventoryService, InventoryService>();
            builder.Services.AddTransient<INotificationService, NotificationService>();
            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddTransient<IOrderRepository, OrderRepository>();
            builder.Services.AddTransient<IPartRepository, PartRepository>();
            builder.Services.AddTransient<IPartService, PartService>();
            builder.Services.AddTransient<IShipmentRepository, ShipmentRepository>();
            builder.Services.AddTransient<IShipmentService, ShipmentService>();
            builder.Services.AddTransient<IWarehouseRepository, WarehouseRepository>();
            builder.Services.AddTransient<IWarehouseService, WarehouseService>();
            builder.Services.AddTransient<IValidationService, ValidationService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
