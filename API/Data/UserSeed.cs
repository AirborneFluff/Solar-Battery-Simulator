using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace API.Data
{
    public class UserSeed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            if (users == null) return;

            var roles = new List<AppRole>
            {
                new AppRole { Name = "Member" },
                new AppRole { Name = "Moderator" },
                new AppRole { Name = "Admin" },
            };

            foreach (var role in roles)
                await roleManager.CreateAsync(role);

            foreach (var user in users)
            {
                user.UserName = user.Email;
                await userManager.CreateAsync(user, "logmein");
                await userManager.AddToRoleAsync(user, "Admin");
                await userManager.AddToRoleAsync(user, "Moderator");
                await userManager.AddToRoleAsync(user, "Member");
            }
        }
    }
}