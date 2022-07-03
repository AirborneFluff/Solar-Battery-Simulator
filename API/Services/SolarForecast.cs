using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace API.Services
{
    public class SolarForecast
    {
        private readonly string estimateUri = "https://api.forecast.solar/estimate/";
        private readonly HttpClient _http;
        public SolarForecast(HttpClient http)
        {
            this._http = http;
        }

        public async Task<string> GetForecastString(SolarForecastParams solarParams)
        {
            var urlBuilder = new StringBuilder();
            urlBuilder.Append(estimateUri)
                .Append(solarParams.lat).Append('/')
                .Append(solarParams.lon).Append('/')
                .Append(solarParams.dec).Append('/')
                .Append(solarParams.az).Append('/')
                .Append(solarParams.kwp)
                .Append("?time=seconds");
            var sb = urlBuilder.ToString();
            return await _http.GetStringAsync(urlBuilder.ToString());
        }

        public static Dictionary<int, int>? GetWattPeriods(string solarForecast)
        {
            //var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(solarForecast);
            var result = JsonConvert.DeserializeObject(solarForecast);
            if (result == null) return null;
            var values = JToken.FromObject(result)?
                .SelectToken("result")?
                .SelectToken("watts")?
                .Children()
                .ToList();
            if (values == null) return null;

            Dictionary<int, int> wattPeriods = new Dictionary<int, int>();

            foreach (var value in values)
            {
                var str = value.ToString();
                var vals = str.Split(':').Select(s => s.Replace("\"", " ").Trim());
                wattPeriods.Add(int.Parse(vals.First()), -int.Parse(vals.Last()));
            }
            return wattPeriods;
        }

        public class SFWattPeriod
        {
            public int StartTime { get; set; }
            public int EndTime { get; set; }
            public int Duration { get => EndTime - StartTime; }
            public int Watts { get; set; }
        }
    }

    public class SolarForecastParams
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public double dec { get; set; }
        public double az { get; set; }
        public double kwp { get; set; }
    }
}