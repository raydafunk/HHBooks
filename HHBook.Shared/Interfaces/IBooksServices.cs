
using HHBook.Shared.Dtos;

namespace HHBook.Shared.Interfaces
{
    public interface IBooksServices
    {
        Task<BookDetailsDto> GetBooksAsync(int bookId);
        Task<PagedResult<BookListDto>> GetBooksAsync(int pageNo, int pageSize, string? genreSlug = null);
        Task<PagedResult<BookListDto>> GetBooksByAuthorAsync(int pageNo, int pageSize, string authorSlug);
        Task<GenreDto[]> GetGenreAsync(bool topOnly);
        Task<BookListDto[]> GetPopulBooksAsync(int count, string? genreSlug = null);
        Task<BookListDto[]> GetSimilarBooksAsync(int bookId, int count);
    }
}
