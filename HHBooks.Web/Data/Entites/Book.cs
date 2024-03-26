using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHBooks.Web.Data.Entites
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        public int AuthorId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required, MaxLength(50), Unicode(false)]
        public string Format { get; set; }

        [Range(1, int.MaxValue)]
        public int NumPages { get; set; }

        [Required, MaxLength(180), Unicode(false)]
        public string Image { get; set; }

        [MaxLength(250)]
        public string? BuyLink { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual Author Author { get; set; }

        public virtual ICollection<GenreBooks> BookGenres { get; set; } = new List<GenreBooks>();   

    }

}
