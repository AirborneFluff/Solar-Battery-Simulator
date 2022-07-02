using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string? GeoBearerToken { get; set; }
        public string? GeoDeviceId { get; set; }

        public ICollection<VirtualBatterySystem> VirtualBatterySystems { get; set; } = new Collection<VirtualBatterySystem>();
        public ICollection<AppUserRole> UserRoles { get; set; } = new Collection<AppUserRole>();
    }
}