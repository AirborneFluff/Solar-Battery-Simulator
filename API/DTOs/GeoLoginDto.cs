using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class GeoLoginDto
    {
        public string Identity { get; set; }
        public string Password { get; set; }
    }
}