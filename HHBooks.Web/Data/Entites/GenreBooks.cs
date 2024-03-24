using System.ComponentModel.DataAnnotations.Schema;

namespace HHBooks.Web.Data.Entites
{
    public class GenreBooks
    {
        public short GenereId { get; set; }
        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        public virtual Book Book { get; set; }

        [ForeignKey(nameof(GenereId))]
        public virtual Genre Genre { get; set; }

    }

}
