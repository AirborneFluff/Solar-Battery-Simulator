using System.Text;
using API.Entities;
using ServiceStack.Text;

namespace API.Helpers
{
    public static class CsvSerializer
    {
        public static string SerializeToCsv<T>(ICollection<T> list) where T: VirtualBatteryState
        {
            var sb = new StringBuilder();

            sb.Append("\"RealExportValue\"").Append(",");
            sb.Append("\"RealImportValue\"").Append(",");
            sb.Append("\"VirtualExportValue\"").Append(",");
            sb.Append("\"VirtualImportValue\"").Append(",");
            sb.Append("\"ChargeLevel\"").Append(",");
            sb.AppendLine("\"Time\"");


            foreach (var row in list)
            {
                sb.Append(row.RealExportValue).Append(",");
                sb.Append(row.RealImportValue).Append(",");
                sb.Append(row.VirtualExportValue).Append(",");
                sb.Append(row.VirtualImportValue).Append(",");
                sb.Append(row.ChargeLevel).Append(",");
                sb.AppendLine(row.Time.ToString());
            }
            return sb.ToString();
        }
        /*
        
        public double RealExportValue { get; set; }
        public double RealImportValue { get; set; }
        public double VirtualExportValue { get; set; }
        public double VirtualImportValue { get; set; }
        public double ChargeLevel { get; set; }
        public DateTime Time { get; set; } = DateTime.UtcNow;
        */
    }
}