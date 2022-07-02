using API.DTOs;
using API.Models;
using API.Services;

namespace API.Entities
{
    public class Geotogether
    {
        private readonly string loginUri = "https://api.geotogether.com/usersservice/v2/login";
        private readonly string deviceIdUri = "https://api.geotogether.com/api/userapi/v2/user/detail-systems?systemDetails=true";
        private readonly string deviceDataUri = "https://api.geotogether.com/api/userapi/system/smets2-live-data/";
        private readonly string periodicDataUri = "https://api.geotogether.com/api/userapi/system/smets2-periodic-data/";
        private readonly HttpClient _http;

        public Geotogether(HttpClient http)
        {
            this._http = http;
        }

        public async Task<GeoLoginResponse?> Login(string email, string password)
        {
            _http.DefaultRequestHeaders.Clear();

            var reqBody = new
            {
                Identity = email,
                Password = password
            };

            var result = await _http.PostAsJsonAsync(loginUri, reqBody)
                .Result.Content.ReadFromJsonAsync<GeoLoginResponse>();
            if (result == null) return null;

            return result;
        }

        public async Task<string?> GetDeviceId(string bearerToken)
        {
            if (TokenService.GetExpiration(bearerToken) <= DateTime.UtcNow) return null;

            _http.DefaultRequestHeaders.Clear();
            _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken}");

            var result = await _http.GetAsync(deviceIdUri);
            
            if (result.IsSuccessStatusCode) 
            {
                var geoResponse = await result.Content.ReadFromJsonAsync<GeoDeviceIdResponse>();
                return geoResponse?.SystemRoles?.FirstOrDefault()?.SystemId;
            }
            return null;
        }

        public async Task<GeoDeviceData?> GetLiveData(string bearerToken, string deviceId)
        {
            if (TokenService.GetExpiration(bearerToken) <= DateTime.UtcNow) return null;

            _http.DefaultRequestHeaders.Clear();
            _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken}");

            var result = await _http.GetFromJsonAsync<GeoDeviceData>(deviceDataUri + deviceId);
            if (result == null) return null;

            return result;
        }

        public async Task<GeoPeriodicData?> GetPeriodicData(string bearerToken, string deviceId)
        {
            if (TokenService.GetExpiration(bearerToken) <= DateTime.UtcNow) return null;

            _http.DefaultRequestHeaders.Clear();
            _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken}");

            var result = await _http.GetFromJsonAsync<GeoPeriodicData>(periodicDataUri + deviceId);
            if (result == null) return null;

            return result;
        }
    }
}