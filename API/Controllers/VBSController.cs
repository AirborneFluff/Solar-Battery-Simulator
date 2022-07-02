using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class VBSController : BaseApiController
    {
        private readonly Geotogether _geo;
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public VBSController(Geotogether geo, UserManager<AppUser> userManager, DataContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
            this._userManager = userManager;
            this._geo = geo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VirtualBatterySystemDTO>> GetVirtualBatterySystem(int id)
        {
            var userId = User.GetUserId();
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            var vbs = await _context.VBatterySystems.FirstOrDefaultAsync(u => u.Id == id && u.AppUserId == userId);
            if (vbs == null) return NotFound();

            return _mapper.Map<VirtualBatterySystemDTO>(vbs);
        }

    }
}