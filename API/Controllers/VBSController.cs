using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
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

        [HttpGet("{id}/csv")]
        public async Task<ActionResult<string>> GetCSV(int id)
        {
            var userId = User.GetUserId();
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            var vbs = await _context.VBatterySystems
                .Include(vbs => vbs.SystemStates)
                .FirstOrDefaultAsync(u => u.Id == id && u.AppUserId == userId);
            if (vbs == null) return NotFound("No system found by that Id");
            if (vbs.SystemStates.Count() <= 0) return NotFound("No data has been collected for that system yet");

            return CsvSerializer.SerializeToCsv<VirtualBatteryState>(vbs.SystemStates);
        }

    }
}