using API.Data;
using API.DTOs;
using API.Entities;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class GeoController : BaseApiController
    {
        private readonly Geotogether _geo;
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        public GeoController(Geotogether geo, UserManager<AppUser> userManager, DataContext context)
        {
            this._context = context;
            this._userManager = userManager;
            this._geo = geo;
        }

        [HttpPost("login")]
        public async Task<ActionResult<GeoLoginResponse?>> GeoLogin(LoginDTO input)
        {
            if (input.Email == null) return BadRequest("Please provide an email");
            if (input.Password == null) return BadRequest("Please provide a password");

            var result = await _geo.Login(input.Email, input.Password);
            if (result == null) return BadRequest("Issue logging into GeoTogether");

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == input.Email.ToUpper());
            if (user == null) return NotFound("Issue finding logged in user");

            user.GeoBearerToken = result.AccessToken;

            if (user.GeoDeviceId == null)
            {
                user.GeoDeviceId = await _geo.GetDeviceId(user.GeoBearerToken);
            }

            if (_context.SaveChanges() > 0) return Ok(result);

            return BadRequest("Couldn't login to GeoTogether");
        }
    }
}