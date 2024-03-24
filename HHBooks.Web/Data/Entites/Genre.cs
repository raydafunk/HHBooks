using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HHBooks.Web.Data.Entites
{
    public class Genre
    {
        [Key]
        public short Id { get; set; }

        [Required, MaxLength(50), Unicode(false)]

        public string Name { get; set; }

        [Required, MaxLength(75), Unicode(false)]

        public string Slug { get; set; }

        public bool IsTopGenere { get; set; }

        public ICollection<GenreBooks> GenreBooks { get; set; } = new List<GenreBooks>();

    }

}
