using System.Text;
using System.Text.Json;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Models;
using API.Services;
using API.Workers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace API.Workers
{
    public class BatteryWorker : IWorker
    {
        private readonly ILogger<BatteryWorker> _logger;
        private readonly Geotogether _geotogether;
        private readonly DataContext _context;
        private readonly GeoLoginDto _loginInfo;

        public BatteryWorker(ILogger<BatteryWorker> logger, Geotogether geotogether, IServiceScopeFactory scopeFactory, IConfiguration config)
        {
            _loginInfo = new GeoLoginDto
            {
                Identity = config.GetValue<string>("GeoSettings:Identity"),
                Password = config.GetValue<string>("GeoSettings:Password")
            };

            this._geotogether = geotogether;
            this._logger = logger;
            this._context = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            if (_context == null)
            {
                _logger.LogWarning("No data context found for worker");
                return;
            }

            if (_loginInfo == null)
            {
                _logger.LogWarning("Couldn't retrieve user GeoLogin information");
                return;
            }

            var user = await _context.Users
                .Include(u => u.VirtualBatterySystems)
                .ThenInclude(v => v.SystemStates)
                .FirstOrDefaultAsync(u => u.NormalizedEmail == _loginInfo.Identity.ToUpper());
            
            if (user == null)
            {
                _logger.LogWarning("Couldn't retrieve user data from database");
                return;
            }

            if (TokenService.Expired(user.GeoBearerToken)) await GeoLogin(user, _loginInfo.Identity, _loginInfo.Password);

            foreach (var vbs in user.VirtualBatterySystems)
            {
                var geoPeriodicData = await GetPeriodicData(user);
                var meterReading = geoPeriodicData?.ElectricReading?.TotalConsumption;
                if (meterReading == null)
                {
                    _logger.LogInformation("Couldn't retrieve meter reading from GeoHome");
                    break;
                }

                vbs.RealImportValue = (double) meterReading;
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                if (TokenService.Expired(user.GeoBearerToken)) await GeoLogin(user, _loginInfo.Identity, _loginInfo.Password);

                var data = await GetLiveData(user);
                if (data == null) break;

                foreach(var vbs in user.VirtualBatterySystems)
                {
                    if (data.Wattage == null) break;

                    vbs.ApplyPower((int)data.Wattage, 3);
                    var sb = new StringBuilder();
                    sb.Append("Battery level: ");
                    sb.Append(string.Format("{0:0.## %}", vbs.ChargePercentage));
                    sb.AppendLine('\t' + string.Format("{0:0.## Wh}", vbs.ChargeLevel));
                    sb.AppendLine("Real meter reading: " + string.Format("{0:0.## Kwh}", vbs.RealImportValue));
                    sb.AppendLine("Virtual meter reading: " + string.Format("{0:0.## Kwh}", vbs.VirtualImportValue));
                    sb.AppendLine("Virtual export reading: " + string.Format("{0:0.## Kwh}", vbs.VirtualExportValue));
                    _logger.LogInformation(sb.ToString());

                    var VBSChanges = _context.ChangeTracker.Entries()
                        .FirstOrDefault(e => e.Entity.GetType() == typeof(VirtualBatteryState));

                    if (VBSChanges != null)
                        _context.SaveChanges();
                }

                await Task.Delay(3000);
            }
        }

        public async Task<GeoDeviceData?> GetLiveData(AppUser user)
        {
            if (user.GeoBearerToken == null || user.GeoDeviceId == null) return null;
            return await _geotogether.GetLiveData(user.GeoBearerToken, user.GeoDeviceId);
        }

        public async Task<GeoPeriodicData?> GetPeriodicData(AppUser user)
        {
            if (user.GeoBearerToken == null || user.GeoDeviceId == null) return null;
            return await _geotogether.GetPeriodicData(user.GeoBearerToken, user.GeoDeviceId);
        }

        public async Task<bool> GeoLogin(AppUser user, string email, string password)
        {
            var loginResult = await _geotogether.Login(email, password);
            if (loginResult == null) return false;

            user.GeoBearerToken = loginResult.AccessToken;
            return true;
        }
    }
}