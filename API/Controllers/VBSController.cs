using System.Collections.ObjectModel;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceStack.Text;

namespace API.Controllers
{
    [Authorize]
    public class VBSController : BaseApiController
    {
        private readonly Geotogether _geo;
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly SolarForecast _solar;
        public VBSController(Geotogether geo, UserManager<AppUser> userManager, DataContext context, IMapper mapper, SolarForecast solar)
        {
            this._solar = solar;
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

        [HttpPost("{id}/simulate")]
        public async Task<ActionResult> ForceSimulation(int id)
        {
            var userId = User.GetUserId();
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            var vbs = await _context.VBatterySystems.FirstOrDefaultAsync(u => u.Id == id && u.AppUserId == userId);
            if (vbs == null) return NotFound();

            //var forecast = await _solar.GetForecastString(user.SolarForecastParams);
            
            var forecast = await System.IO.File.ReadAllTextAsync("Data/SolarForecast.json");
            var wattPeriods = SolarForecast.GetWattPeriods(forecast);
            if (wattPeriods == null) return BadRequest();

            var simTime = wattPeriods.First().Key;
            var simPower = wattPeriods.First().Value;

            var currentPeriodIndex = 0;
            while (simTime <= wattPeriods.Last().Key)
            {
                vbs.ApplyPower(simPower, 3); // Apply power for 3 seconds
                simTime += 3; // Add 3 seconds to counter
                if (simTime > wattPeriods.ElementAt(currentPeriodIndex + 1).Key) // Check if time period has changed
                {
                    if (currentPeriodIndex + 1 == wattPeriods.Count()) break;
                    currentPeriodIndex += 1; // Increment index counter
                    simPower = wattPeriods.ElementAt(currentPeriodIndex).Value; // Set new power
                }
                if (simTime % 30 == 0) vbs.LogCurrentState(simTime); // Log every 30 seconds
            }

            return Ok(vbs.GetStatesAsCSV(false));
        }

        [HttpGet("{id}/history")]
        public async Task<ActionResult<string>> GetCSV(int id, [FromQuery] VBSDataQueryParams vbsParams)
        {
            var userId = User.GetUserId();
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) BadRequest();

            var vbs = await _context.VBatterySystems.FirstOrDefaultAsync(s => s.Id == id && s.AppUserId == userId);
            if (vbs == null) NotFound();

            var startDate = vbsParams.StartTime;
            var endDate = vbsParams.EndTime;

            var states = await _context.VBatteryStates
                .Where(s => s.Time >= startDate)
                .Where(s => s.Time < endDate)
                .Where(s => s.BatterySystemId == id)
                .ToListAsync();

            if (states == null) return NotFound("No system found by that Id");
            if (states.Count() <= 0) return NotFound("No data has been collected for that system yet");

            if (vbsParams.RelativeValues)
            {
                var firstRealImport = states.First().RealImportValue;
                var firstVirtualImport = states.First().VirtualImportValue;
                var firstRealExport = states.First().RealExportValue;
                var firstVirtualExport = states.First().VirtualExportValue;

                states = states.Select(s => new VirtualBatteryState
                {
                    ChargeLevel = s.ChargeLevel,
                    RealImportValue = (s.RealImportValue - firstRealImport)*1000,
                    VirtualImportValue = (s.VirtualImportValue - firstVirtualImport)*1000,
                    RealExportValue = (s.RealExportValue - firstRealExport)*1000,
                    VirtualExportValue = (s.VirtualExportValue - firstVirtualExport)*1000,
                    Time = s.Time
                }).ToList();
            }

            if (vbsParams.Csv) return Ok(API.Helpers.CsvSerializer
                .SerializeToCsv<VirtualBatteryState>(states, !vbsParams.EpochTimestamp));

            return NotFound("CSV is the only option right now");
        }

        [HttpGet("{id}/history/today")]
        public async Task<ActionResult<string>> GetCSVToday(int id)
        {
            var userId = User.GetUserId();
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) BadRequest();

            var vbs = await _context.VBatterySystems.FirstOrDefaultAsync(s => s.Id == id && s.AppUserId == userId);
            if (vbs == null) NotFound();

            var startDate = DateTime.Today.ToUnixTime();
            var endDate = DateTime.Today.AddDays(1).ToUnixTime();

            var states = await _context.VBatteryStates
                .Where(s => s.Time >= startDate)
                .Where(s => s.Time < endDate)
                .Where(s => s.BatterySystemId == id)
                .ToListAsync();

            if (states == null) return NotFound("No system found by that Id");
            if (states.Count() <= 0) return NotFound("No data has been collected for that system yet");

            var firstRealImport = states.First().RealImportValue;
            var firstVirtualImport = states.First().VirtualImportValue;
            var firstRealExport = states.First().RealExportValue;
            var firstVirtualExport = states.First().VirtualExportValue;

            states = states.Select(s => new VirtualBatteryState
            {
                ChargeLevel = s.ChargeLevel,
                RealImportValue = (s.RealImportValue - firstRealImport)*1000,
                VirtualImportValue = (s.VirtualImportValue - firstVirtualImport)*1000,
                RealExportValue = (s.RealExportValue - firstRealExport)*1000,
                VirtualExportValue = (s.VirtualExportValue - firstVirtualExport)*1000,
                Time = s.Time
            }).ToList();

            return Ok(API.Helpers.CsvSerializer
                .SerializeToCsv<VirtualBatteryState>(states));
        }
    }
}