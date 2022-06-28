using API.Interfaces;

namespace API.Services
{
    public class BatteryService : BackgroundService
    {
        private readonly IWorker worker;
 
        public BatteryService(IWorker worker)
        {
            this.worker = worker;
        }
 
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await worker.DoWork(stoppingToken);    
        }
    }
}