namespace API.Models
{
    public class GeoLoginResponse
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? DisplayName { get; set; }
        public bool Validated { get; set; }
        public string? AccessToken { get; set; }
    }
}