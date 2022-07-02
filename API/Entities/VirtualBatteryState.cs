namespace API.Entities
{
    public class VirtualBatteryState
    {
        public int Id { get; set; }
        public int BatterySystemId { get; set; }
        public VirtualBatterySystem? BatterySystem { get; set; }

        
        public double RealExportValue { get; set; }
        public double RealImportValue { get; set; }
        public double VirtualExportValue { get; set; }
        public double VirtualImportValue { get; set; }
        public double ChargeLevel { get; set; }
        public long Time { get; set; } = DateTimeOffset.Now.ToUnixTimeSeconds();
    }
}