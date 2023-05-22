using System.ComponentModel.DataAnnotations;

namespace HHBooks.API.Modles.User
{
    public class logiUserDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}