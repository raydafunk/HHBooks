using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HHBooks.Web.Data.Entites
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(80), Unicode(false)]

        public string Name { get; set; }

        [Required, MaxLength(100), Unicode(false)]
        public string Slug { get; set; }
    }

}
