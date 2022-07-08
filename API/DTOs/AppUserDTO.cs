namespace API.DTOs
{
    public class AppUserDTO
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public int? DefaultSystemId { get; set; }
    }
}