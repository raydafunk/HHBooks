
using HHBook.Shared.Dtos;
using HHBooks.Web.Data;
using HHBooks.Web.Data.Entites;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HHBooks.Web.Services
{
    public class BooksServices
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
            var genres = await query.Select(g => new GenreDto (g.Name, g.Slug))
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
                       .Skip((pageNo -1) * pageSize)
                       .Take(pageSize)
                       .Select( b => new BookListDto(b.Id, b.Title,b.Image, new AuthorDto(b.Author.Name, b.Author.Slug)))
                       .ToArrayAsync();

            return new PagedResult<BookListDto>(books, totalcount);
            
        }
        public async Task<BookListDto[]>GetPopulBooksAsync(int count, string? genreSlug = null)
        {
            throw new NotImplementedException();
        }
        public async Task<BookListDto>GetBooksAsync(int bookId)
        {
            throw new NotImplementedException();
        } 
        public async Task<BookListDto[]>GetSimilarBooksAsync(int bookId, int count)
        {
            throw new NotImplementedException();
        } 
        public async Task<PagedResult<BookListDto>> GetBooksByAuthorAsync(int pageNo, int pageSize, string authorSlug)
        {
            throw new NotImplementedException();
        }
    }
}
