namespace API.DTOs
{
    public class VBSDataQueryParams
    {
        public bool EpochTimestamp { get; set; } = true;
        public bool Csv { get; set; } = true;
        public bool RelativeValues { get; set; } = true;
        public DateTime StartDate { get; set; } = DateTime.Today.ToLocalTime();
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1).ToLocalTime();
    }
}