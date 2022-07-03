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
                //options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
                //options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string connStr;
            if (env == "Development")
            {
                connStr = builder.Configuration.GetConnectionString("DefaultConnection");
            }
            else
            {
                // Use connection string provided at runtime by Heroku.
                var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

                // Parse connection URL to connection string for Npgsql
                connUrl = connUrl.Replace("postgres://", string.Empty);
                var pgUserPass = connUrl.Split("@")[0];
                var pgHostPortDb = connUrl.Split("@")[1];
                var pgHostPort = pgHostPortDb.Split("/")[0];
                var pgDb = pgHostPortDb.Split("/")[1];
                var pgUser = pgUserPass.Split(":")[0];
                var pgPass = pgUserPass.Split(":")[1];
                var pgHost = pgHostPort.Split(":")[0];
                var pgPort = pgHostPort.Split(":")[1];

                connStr = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Require;TrustServerCertificate=True";
            }
            options.UseNpgsql(connStr);
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