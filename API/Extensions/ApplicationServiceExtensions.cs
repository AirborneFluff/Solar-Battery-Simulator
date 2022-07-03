using API.Data;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using API.Services;
using API.Workers;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static void AddApplicationsServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            builder.Services.AddDbContext<DataContext>(options => {
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddSingleton<SolarForecast>()
                .AddHttpClient();
            builder.Services.AddSingleton<Geotogether>()
                .AddHttpClient();
            builder.Services.AddSingleton<IWorker, BatteryWorker>();
            builder.Services.AddHostedService<BatteryService>();

            return;
        }
    }
}