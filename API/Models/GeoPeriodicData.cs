namespace API.Models
{
    public class ActiveTariffList
    {
        public string? CommodityType { get; set; }
        public bool ValueAvailable { get; set; }
        public int NextTariffStartTime { get; set; }
        public double ActiveTariffPrice { get; set; }
        public double NextTariffPrice { get; set; }
        public bool NextPriceAvailable { get; set; }
    }

    public class BillingModes
    {
        public string? BillingMode { get; set; }
        public string? CommodityType { get; set; }
        public bool ValueAvailable { get; set; }
    }

    public class BillToDateList
    {
        public string? CommodityType { get; set; }
        public double BillToDate { get; set; }
        public int ValidUTC { get; set; }
        public int StartUTC { get; set; }
        public int Duration { get; set; }
        public bool ValueAvailable { get; set; }
    }

    public class BudgetRagStatusDetail
    {
        public string? CurrDay { get; set; }
        public string? YesterDay { get; set; }
        public string? CurrWeek { get; set; }
        public string? LastWeek { get; set; }
        public string? CurrMth { get; set; }
        public string? LastMth { get; set; }
        public string? ThisYear { get; set; }
        public bool ValueAvailable { get; set; }
        public string? CommodityType { get; set; }
    }

    public class BudgetSettingDetail
    {
        public bool ValueAvailable { get; set; }
        public double EnergyAmount { get; set; }
        public double CostAmount { get; set; }
        public int BudgetToC { get; set; }
        public string? CommodityType { get; set; }
    }

    public class CurrentCostsElec
    {
        public string? CommodityType { get; set; }
        public string? Duration { get; set; }
        public int Period { get; set; }
        public double CostAmount { get; set; }
        public double EnergyAmount { get; set; }
    }

    public class CurrentCostsGa
    {
        public string? CommodityType { get; set; }
        public string? Duration { get; set; }
        public int Period { get; set; }
        public double CostAmount { get; set; }
        public double EnergyAmount { get; set; }
    }

    public class DaySetPoint
    {
        public int TemperatureSetPoint { get; set; }
        public int TimeOfChange { get; set; }
    }

    public class NightSetPoint
    {
        public int TemperatureSetPoint { get; set; }
        public int TimeOfChange { get; set; }
    }

    public class GeoPeriodicData
    {
        public TotalConsumptionList? ElectricReading { get => TotalConsumptionList?.FirstOrDefault(c => c.CommodityType == "ELECTRICITY"); }


        public int Ttl { get; set; }
        public int LatestUtc { get; set; }
        public string? Id { get; set; }
        public List<TotalConsumptionList>? TotalConsumptionList { get; set; }
        public int TotalConsumptionTimestamp { get; set; }
        public List<SupplyStatusList>? SupplyStatusList { get; set; }
        public int SupplyStatusTimestamp { get; set; }
        public List<BillToDateList>? BillToDateList { get; set; }
        public int BillToDateTimestamp { get; set; }
        public List<ActiveTariffList>? ActiveTariffList { get; set; }
        public int ActiveTariffTimestamp { get; set; }
        public List<CurrentCostsElec>? CurrentCostsElec { get; set; }
        public int CurrentCostsElecTimestamp { get; set; }
        public List<CurrentCostsGa>? CurrentCostsGas { get; set; }
        public int CurrentCostsGasTimestamp { get; set; }
        public object? PrePayDebtList { get; set; }
        public int PrePayDebtTimestamp { get; set; }
        public List<BillingModes>? BillingMode { get; set; }
        public int BillingModeTimestamp { get; set; }
        public List<BudgetRagStatusDetail>? BudgetRagStatusDetails { get; set; }
        public int BudgetRagStatusDetailsTimestamp { get; set; }
        public List<BudgetSettingDetail>? BudgetSettingDetails { get; set; }
        public int BudgetSettingDetailsTimestamp { get; set; }
        public SetPoints? SetPoints { get; set; }
        public List<SeasonalAdjustment>? SeasonalAdjustments { get; set; }
    }

    public class SeasonalAdjustment
    {
        public bool ValueAvailable { get; set; }
        public string? CommodityType { get; set; }
        public bool Adjustment { get; set; }
        public int TimeOfChange { get; set; }
    }

    public class SetPoints
    {
        public DaySetPoint? DaySetPoint { get; set; }
        public NightSetPoint? NightSetPoint { get; set; }
    }

    public class SupplyStatusList
    {
        public string? CommodityType { get; set; }
        public string? SupplyStatus { get; set; }
    }

    public class TotalConsumptionList
    {
        public string? CommodityType { get; set; }
        public int ReadingTime { get; set; }
        public double TotalConsumption { get; set; }
        public bool ValueAvailable { get; set; }
    }
}