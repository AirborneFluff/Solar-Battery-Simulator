using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDTO
    {
        
        [Required] [EmailAddress] public string? Email { get; set; }
        [Required] public string? FirstName { get; set; }
        [Required] public string? LastName { get; set; }
        [Required] [StringLength(32, MinimumLength = 6)] public string? Password { get; set; }
    }
}