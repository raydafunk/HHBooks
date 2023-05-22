using System.ComponentModel.DataAnnotations;

namespace HHBooks.API.Modles.User
{
    public class UserDto : logiUserDto
    {
      
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Role { get; set; }

    }
}