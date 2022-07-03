using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using API.DTOs;
using API.Helpers;

namespace API.Entities
{
    public class VirtualBatterySystem
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        [JsonIgnore]
        public AppUser? AppUser { get; set; }
        /// <summary>
        /// Time between storing system states in seconds. Default 5 minutes (300s)
        /// </summary>
        public int LoggingPeriod { get; set; } = 30;
        public double RealExportValue { get; set; }
        public double RealImportValue { get; set; }
        public double VirtualExportValue { get; set; }
        public double VirtualImportValue { get; set; }
        public ICollection<VirtualBatteryState> SystemStates { get; set; } = new Collection<VirtualBatteryState>();

        public VirtualBatteryState? LastState { get => SystemStates?.LastOrDefault(); }
        public VirtualBatteryState CurrentState { get {
                return new VirtualBatteryState
                {
                    RealExportValue = this.RealExportValue,
                    RealImportValue = this.RealImportValue,
                    VirtualExportValue = this.VirtualExportValue,
                    VirtualImportValue = this.VirtualImportValue,
                    ChargeLevel = this.ChargeLevel
                };
            }}
        public double ChargeLevel { get; set; }
        public double ChargePercentage { get => (ChargeLevel - (TotalCapacity - UsableCapacity)) / UsableCapacity; }
        /// <summary>
        /// The rated storage system size, eg. 4800Wh
        /// </summary>
        public double TotalCapacity { get; set; }
        /// <summary>
        /// The usable storage available to use, eg. 4560Wh
        /// </summary>
        public double UsableCapacity { get => TotalCapacity * DepthOfDischarge; }
        /// <summary>
        /// DoD of system - 0.95 => 95%
        /// </summary>
        public double DepthOfDischarge { get; set; }
        /// <summary>
        /// Maximum continuous discharge rate of either batteries or inverter (whichever is lowest) in watts
        /// </summary>
        public double ContinuousDischargeRate { get; set; }
        /// <summary>
        /// Maximum continuous charge rate of either batteries or inverter (whichever is lowest), in watts
        /// </summary>
        public double ContinuousChargeRate { get; set; }
        /// <summary>
        /// Charge efficiency of charger - 0.94 => 94%
        /// </summary>
        public double ChargeEfficiency { get; set; }
        /// <summary>
        /// Discharge efficiency of charger - 0.945 => 94.5%
        /// </summary>
        public double DischargeEfficiency { get; set; }

        /// <summary>
        /// Apply a wattage to the system over time (in seconds). Positive values mean importing from grid. Negative values mean exporting to grid
        /// </summary>
        /// <param name="watts"></param>
        public VirtualBatteryState ApplyPower(double watts, double duration)
        {
            var wattHours = (Math.Abs(watts) * duration) / 3600;

            var virtualImport = GetDischargeRemainder(watts, duration);
            var virtualExport = GetChargeRemainder(watts, duration);
            
            if (watts > 0)
            {
                RealImportValue += wattHours/1000;
                VirtualImportValue += virtualImport/1000;
            }
            if (watts < 0)
            {
                RealExportValue += wattHours/1000;
                VirtualExportValue += virtualExport/1000;
            }

            //if (LastState?.Time + LoggingPeriod <= DateTimeOffset.Now.ToUnixTimeSeconds()) LogCurrentState();
            //else if (LastState == null) LogCurrentState();

            return CurrentState;
        }

        private double GetDischargeRemainder(double watts, double duration)
        {
            if (watts <= 0) return 0;

            var totalWattHours = (Math.Abs(watts) * duration) / 3600; // Total watt hours being currently imported

            watts = Math.Min(ContinuousDischargeRate, Math.Abs(watts)); // Clamp usable watts to discharge rate
            var dischargableWattHours = (Math.Abs(watts) * duration) / 3600; // Total watt hours possible to output

            ChargeLevel = Math.Max(ChargeLevel - dischargableWattHours/DischargeEfficiency, TotalCapacity - UsableCapacity);

            return totalWattHours - dischargableWattHours;
        }

        private double GetChargeRemainder(double watts, double duration)
        {
            if (watts >= 0) return 0;
            var totalWattHours = (Math.Abs(watts) * duration) / 3600; // Total watt hours being currently imported

            watts = Math.Min(ContinuousChargeRate, Math.Abs(watts)); // Clamp usable watts to discharge rate
            var chargableWattHours = (Math.Abs(watts) * duration) / 3600; // Total watt hours possible to output

            ChargeLevel = Math.Min(ChargeLevel + chargableWattHours*ChargeEfficiency, TotalCapacity);

            return totalWattHours - chargableWattHours;
        }

        public void LogCurrentState(int unixLogTime)
        {
            VirtualBatteryState newState = new VirtualBatteryState
                {
                    RealExportValue = this.RealExportValue,
                    RealImportValue = this.RealImportValue,
                    VirtualExportValue = this.VirtualExportValue,
                    VirtualImportValue = this.VirtualImportValue,
                    ChargeLevel = this.ChargeLevel,
                    Time = unixLogTime
                };
            SystemStates.Add(newState);
        }
        public void LogCurrentState()
        {
            SystemStates.Add(CurrentState);
        }
    }

    public static class VirtualBatterySystemExtension
    {
        public static string GetStatesAsCSV(this VirtualBatterySystem system, bool epochTimestamp)
        {
            List<VirtualBatteryState> states = new List<VirtualBatteryState>();
            var firstRealImport = system.SystemStates.First().RealImportValue;
            var firstVirtualImport = system.SystemStates.First().VirtualImportValue;
            var firstRealExport = system.SystemStates.First().RealExportValue;
            var firstVirtualExport = system.SystemStates.First().VirtualExportValue;

            states = system.SystemStates.Select(s => new VirtualBatteryState
            {
                ChargeLevel = s.ChargeLevel,
                RealImportValue = s.RealImportValue - firstRealImport,
                VirtualImportValue = s.VirtualImportValue - firstVirtualImport,
                RealExportValue = s.RealExportValue - firstRealExport,
                VirtualExportValue = s.VirtualExportValue - firstVirtualExport,
                Time = s.Time
            }).ToList();

            return CsvSerializer.SerializeToCsv<VirtualBatteryState>(states, !epochTimestamp);
        }
    }
}