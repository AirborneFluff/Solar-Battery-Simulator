using API.DTOs;

namespace API.Entities
{
    public class Geotogether
    {
        private readonly string loginUri = "https://api.geotogether.com/usersservice/v2/login";
        private readonly string deviceIdUri = "https://api.geotogether.com/api/userapi/v2/user/detail-systems?systemDetails=true";
        private readonly string deviceDataUri = "https://api.geotogether.com/api/userapi/system/smets2-live-data/";
        private readonly HttpClient _http;
        private string? _bearerToken;
        private string? _deviceId;

        public Geotogether(HttpClient http)
        {
            this._http = http;
        }

        public async Task<GeoLoginResponse?> Login(string email, string password)
        {
            var reqBody = new
            {
                Identity = email,
                Password = password
            };

            var result = await _http.PostAsJsonAsync(loginUri, reqBody)
                .Result.Content.ReadFromJsonAsync<GeoLoginResponse>();
            if (result == null) return null;

            _bearerToken = result.AccessToken;
            _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {_bearerToken}");
            return result;
        }

        public async Task SetDeviceId()
        {
            var result = await _http.GetFromJsonAsync<GeoDeviceIdResponse>(deviceIdUri);
            if (result == null) return;

            _deviceId = result?.SystemRoles?.FirstOrDefault()?.SystemId;
        }

        public async Task<GeoDeviceData?> GetDeviceData()
        {
            var result = await _http.GetFromJsonAsync<GeoDeviceData>(deviceDataUri + _deviceId);
            if (result == null) return null;

            return result;
        }
    }

    public class GeoLoginResponse
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? DisplayName { get; set; }
        public bool Validated { get; set; }
        public string? AccessToken { get; set; }
    }

    public class GeoApiError
    {
        public long Timestamp { get; set; }
        public int Status { get; set; }
        public string? Error { get; set; }
        public string? Message { get; set; }
        public string? Path { get; set; }
    }

    public class GeoDeviceIdResponse
    {
        public List<GeoSystemRole>? SystemRoles { get; set; }
        public List<GeoSystemDetail>? SystemDetails { get; set; }

        public class GeoSystemRole
        {
            public string? Name { get; set; }
            public string? SystemId { get; set; }
            public List<string>? Roles { get; set; }
        }

        public class GeoDevice
        {
            public string? DeviceType { get; set; }
            public int SensorType { get; set; }
            public int NodeId { get; set; }
            public GeoVersionNumber? VersionNumber { get; set; }
            public int PairedTimestamp { get; set; }
            public string? PairingCode { get; set; }
            public bool UpgradeRequired { get; set; }
        }

        public class GeoSystemDetail
        {
            public string? Name { get; set; }
            public List<GeoDevice>? Devices { get; set; }
            public string? SystemId { get; set; }
        }

        public class GeoVersionNumber
        {
            public int Major { get; set; }
            public int Minor { get; set; }
        }
    }

    public class GeoPower
    {
        public string? Type { get; set; }
        public int Watts { get; set; }
        public bool ValueAvailable { get; set; }
    }

    public class GeoDeviceData
    {
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