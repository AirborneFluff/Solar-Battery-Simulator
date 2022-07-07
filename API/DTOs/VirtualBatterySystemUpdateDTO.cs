namespace API.DTOs
{
    public class VirtualBatterySystemUpdateDTO
    {
        public int? LoggingPeriod { get; set; }
        public double? TotalCapacity { get; set; }
        public double? DepthOfDischarge { get; set; }
        public double? ContinuousDischargeRate { get; set; }
        public double? ContinuousChargeRate { get; set; }
        public double? ChargeEfficiency { get; set; }
        public double? DischargeEfficiency { get; set; }
    }
}