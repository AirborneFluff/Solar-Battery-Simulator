using API.Data;
using API.Helpers;
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

            return;
        }
    }
}