using System.Collections.ObjectModel;
using System.Text;
using API.Entities;

namespace API.DTOs
{
    public class VirtualBatterySystemDTO
    {
        public int Id { get; set; }
        public int LoggingPeriod { get; set; } = 30;
        public double RealExportValue { get; set; }
        public double RealImportValue { get; set; }
        public double VirtualExportValue { get; set; }
        public double VirtualImportValue { get; set; }
        public ICollection<VirtualBatteryState> SystemStates { get; set; } = new Collection<VirtualBatteryState>();

        public VirtualBatteryState? LastState { get => SystemStates?.LastOrDefault(); }
        public double ChargeLevel { get; set; }
        public string ChargePercentage
        {
            get => string.Format("{0:0.## %}", (ChargeLevel - (TotalCapacity - UsableCapacity)) / UsableCapacity);
        }
        public double TotalCapacity { get; set; }
        public double UsableCapacity { get => TotalCapacity * DepthOfDischarge; }
        public double DepthOfDischarge { get; set; }
        public double ContinuousDischargeRate { get; set; }
        public double ContinuousChargeRate { get; set; }
        public double ChargeEfficiency { get; set; }
        public double DischargeEfficiency { get; set; }
    }
}