using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using API.Services;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string? GeoBearerToken { get; set; }
        public string? GeoDeviceId { get; set; }

        public double SolarLat { get; set; }
        public double SolarLon { get; set; }
        public double SolarDec { get; set; }
        public double SolarAz { get; set; }
        public double SolarKwp { get; set; }

        [NotMapped]
        public SolarForecastParams? SolarForecastParams {
            get 
            {
                return new SolarForecastParams
                {
                    lat = SolarLat,
                    lon = SolarLon,
                    dec = SolarDec,
                    az = SolarAz,
                    kwp = SolarKwp
                };
            }
            set
            {
                if (value == null) value = new SolarForecastParams(); // Blank
                SolarLat = value.lat;
                SolarLon = value.lon;
                SolarDec = value.dec;
                SolarAz = value.az;
                SolarKwp = value.kwp;
            }
        }

        public ICollection<VirtualBatterySystem> VirtualBatterySystems { get; set; } = new Collection<VirtualBatterySystem>();
        public ICollection<AppUserRole> UserRoles { get; set; } = new Collection<AppUserRole>();
    }
}