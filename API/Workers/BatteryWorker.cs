using System.Text.Json;
using API.Entities;
using API.Interfaces;
using API.Workers;

namespace API.Workers
{
    public class BatteryWorker : IWorker
    {
        private readonly ILogger<BatteryWorker> _logger;
        private readonly Geotogether _geotogether;

        public BatteryWorker(ILogger<BatteryWorker> logger, Geotogether geotogether)
        {
            this._geotogether = geotogether;
            this._logger = logger;
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            var userData = await System.IO.File.ReadAllTextAsync("geologin.json");
            var loginData = JsonSerializer.Deserialize<GeoLogin>(userData);
            if (loginData == null) return;

            var result = await _geotogether.Login(loginData.Identity, loginData.Password); // Email and password, get from private json
            if (result == null) return;
            
            _logger.LogInformation("Logged in: " + result.Email);

            await _geotogether.SetDeviceId();

            while (!cancellationToken.IsCancellationRequested)
            {
                var geoData = await _geotogether.GetDeviceData();
                _logger.LogInformation(geoData?.Power?.FirstOrDefault(p => p.Type == "ELECTRICITY")?.Watts.ToString());
                await Task.Delay(3000);
            }
        }

        private class GeoLogin
        {
            public string Identity { get; set; }
            public string Password { get; set; }
        }
    }
}