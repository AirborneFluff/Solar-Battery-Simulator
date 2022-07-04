namespace API.DTOs
{
    public class VBSDataQueryParams
    {
        public bool EpochTimestamp { get; set; } = true;
        public bool Csv { get; set; } = true;
        public bool RelativeValues { get; set; } = true;
        public long StartTime { get; set; } = (long) DateTime.Today.ToUniversalTime().Subtract(DateTime.UnixEpoch).TotalSeconds;
        public long EndTime { get; set; } = (long) DateTime.Today.ToUniversalTime().Subtract(DateTime.UnixEpoch).TotalSeconds + 86400;
    }
}