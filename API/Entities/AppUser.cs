using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; } = new Collection<AppUserRole>();
    }
}