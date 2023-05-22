using System.ComponentModel.DataAnnotations;

namespace HHBooks.API.Modles.Author
{
    public class CreateAuthorDto 
    {
        [Required]
        [StringLength(50)]
        public string? Firstname { get; set; }

        [Required]
        [StringLength(250)]
        public string? Lastname { get; set; }
        public string? Bio{ get; set; }

    }
}
