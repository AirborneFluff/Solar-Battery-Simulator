namespace API.DTOs
{
    public class AppUserDTO
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public string? GeoBearerToken { get; set; }
        public string? GeoDeviceId { get; set; }
    }
}