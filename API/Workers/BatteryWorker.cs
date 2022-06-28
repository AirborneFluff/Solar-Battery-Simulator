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
            var result = await _geotogether.Login("", ""); // Email and password, get from private json
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
    }
}