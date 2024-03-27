
using HHBook.Shared.Dtos;
using HHBooks.Web.Data;
using HHBooks.Web.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HHBooks.Web.Services
{

    public class BooksServices : IBooksServices
    {
        private readonly IDbContextFactory<HHBooksDBContext> _dbContextFactory;

        public BooksServices(IDbContextFactory<HHBooksDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<GenreDto[]> GetGenreAsync(bool topOnly)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var query = context.Genres.AsQueryable();
            if (topOnly)
            {
                query.Where(g => g.IsTopGenere);
            }
            var genres = await query.Select(g => new GenreDto(g.Name, g.Slug))
                               .ToArrayAsync();
            return genres;
        }

        public async Task<PagedResult<BookListDto>> GetBooksAsync(int pageNo, int pageSize, string? genreSlug = null)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var query = context.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(genreSlug))
            {
                query = context.Genres.Where(g => g.Slug == genreSlug)
                                .SelectMany(g => g.GenreBooks)
                                .Select(gb => gb.Book);
            }
            var totalcount = await query.CountAsync();
            var books = await query
                       .OrderByDescending(b => b.Id)
                       .Skip((pageNo - 1) * pageSize)
                       .Take(pageSize)
                       .Select(b => new BookListDto(b.Id, b.Title, b.Image, new AuthorDto(b.Author.Name, b.Author.Slug)))
                       .ToArrayAsync();

            return new PagedResult<BookListDto>(books, totalcount);

        }
        public async Task<BookListDto[]> GetPopulBooksAsync(int count, string? genreSlug = null)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var query = context.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(genreSlug))
            {
                query = context.Genres.Where(g => g.Slug == genreSlug)
                                .SelectMany(g => g.GenreBooks)
                                .Select(gb => gb.Book);
            }

            var books = await query
                              .Select(b => new BookListDto(b.Id, b.Title, b.Image, new AuthorDto(b.Author.Name, b.Author.Slug)))
                              .OrderBy(b => Guid.NewGuid())
                              .Take(count)
                              .ToArrayAsync();

            if (books.Length < count)
            {

                var bookIdalreadyReturn = books.Select(b => b.ID);

                var additonalBooks = await context.Books
                     .Where(b => !bookIdalreadyReturn.Contains(b.Id))
                     .Select(b => new BookListDto(b.Id, b.Title, b.Image, new AuthorDto(b.Author.Name, b.Author.Slug)))
                     .OrderBy(b => Guid.NewGuid())
                     .Take(count - books.Length)
                     .ToArrayAsync();
                books = [.. books, .. additonalBooks];
            }
            return books;
        }
        public async Task<BookDetailsDto> GetBooksAsync(int bookId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var books = await context.Books.Where(b => b.Id == bookId)
                   .Select(b => new BookDetailsDto(b.Id, b.Title, b.Image,
                               new AuthorDto(b.Author.Name, b.Author.Slug), b.NumPages, b.Format, b.Description,
                               b.BookGenres.Select(bg => new GenreDto(bg.Genre.Name, bg.Genre.Slug)).ToArray(),
                               b.BuyLink
                        ))
                   .FirstOrDefaultAsync();
            return books;

        }
        public async Task<BookListDto[]> GetSimilarBooksAsync(int bookId, int count)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var simimalerBooks = await context.GenreBooks.Where(gb => gb.BookId == bookId)
                                  .SelectMany(gb => gb.Genre.GenreBooks)
                                  .Select(gb => gb.Book)
                                  .Where(b => b.Id != bookId)
                                  .Select(b => new BookListDto(b.Id, b.Title, b.Image, new AuthorDto(b.Author.Name, b.Author.Slug)))
                                  .OrderBy(b => Guid.NewGuid())
                                  .Take(count)
                                  .ToArrayAsync();
            return simimalerBooks;
        }
        public async Task<PagedResult<BookListDto>> GetBooksByAuthorAsync(int pageNo, int pageSize, string authorSlug)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var query = context.Books.Where(b => b.Author.Slug == authorSlug);

            var totatcountofPages = await query.CountAsync();

            var books = await query
                        .OrderByDescending(b => b.Id)
                        .Skip((pageNo - 1) * pageSize)
                         .Select(b => new BookListDto(b.Id, b.Title, b.Image, new AuthorDto(b.Author.Name, b.Author.Slug)))
                         .ToArrayAsync();


            return new PagedResult<BookListDto>(books, totatcountofPages);
        }
    }
}
