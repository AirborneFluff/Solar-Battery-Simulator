using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<bool> UserNameTaken(this UserManager<AppUser> manager, string username)
        {
            var user = await manager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == username.ToUpper());
            if (user == null) return false;
            return true;
        }

        public static async Task<bool> EmailTaken(this UserManager<AppUser> manager, string email)
        {
            var user = await manager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == email.ToUpper());
            if (user == null) return false;
            return true;
        }
    }
}