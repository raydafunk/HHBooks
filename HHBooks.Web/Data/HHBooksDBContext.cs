using HHBooks.Web.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace HHBooks.Web.Data
{
    public class HHBooksDBContext : DbContext
    {
        public HHBooksDBContext(DbContextOptions<HHBooksDBContext> options) : base(options)
        {
                
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GenreBooks> GenreBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);   
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenreBooks>()
                .HasKey(gb => new { gb.GenereId, gb.BookId });
        }

    }
}
