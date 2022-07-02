namespace API.Models
{
    public class GeoDeviceData
    {
        public int? Wattage { get => Power?.FirstOrDefault(p => p.Type == "ELECTRICITY")?.Watts; }
        public int LatestUtc { get; set; }
        public string? Id { get; set; }
        public List<GeoPower>? Power { get; set; }
        public int PowerTimestamp { get; set; }
        public int LocalTime { get; set; }
        public int LocalTimeTimestamp { get; set; }
        public object? CreditStatus { get; set; }
        public int CreditStatusTimestamp { get; set; }
        public object? RemainingCredit { get; set; }
        public int RemainingCreditTimestamp { get; set; }
        public ZigbeeStatus? ZigbeeStatus { get; set; }
        public int ZigbeeStatusTimestamp { get; set; }
        public object? EmergencyCredit { get; set; }
        public int EmergencyCreditTimestamp { get; set; }
        public List<GeoSystemStatus>? SystemStatus { get; set; }
        public int SystemStatusTimestamp { get; set; }
        public double Temperature { get; set; }
        public int TemperatureTimestamp { get; set; }
        public int TTL { get; set; }
        
    }
    public class GeoPower
    {
        public string? Type { get; set; }
        public int Watts { get; set; }
        public bool ValueAvailable { get; set; }
    }    

    public class GeoSystemStatus
    {
        public string? Component { get; set; }
        public string? StatusType { get; set; }
        public string? SystemErrorCode { get; set; }
        public int systemErrorNumber { get; set; }
    }

    public class ZigbeeStatus
    {
        public string? ElectricityClusterStatus { get; set; }
        public string? GasClusterStatus { get; set; }
        public string? HanStatus { get; set; }
        public int NetworkRssi { get; set; }
    }
}