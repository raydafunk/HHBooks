using HHBook.Shared.Dtos;
using HHBook.Shared.Interfaces;
using Microsoft.AspNetCore.Components;

namespace HHBooks.Shared.ComponentsUI.Pages
{
    public partial class Books
    {
        private const int PageSizes = 16;
        [Parameter]
        public int? PageNo { get; set; } = 1;

        [SupplyParameterFromQuery(Name = "genre")]
        public  string? GenreSlug { get; set; }

        private GenreDto[] _genres = [];
        private BookListDto[] _books = [];
        private BookListDto[] _popularbooks = [];
        private int _totalCount = 0;

        private string _heading = "Top Books";

        [Inject]
        private IBooksServices? _booksServices { get; set; }

        protected override async Task OnInitializedAsync()
        {
          var genresTask = _booksServices!.GetGenreAsync(topOnly: true);
          var bookTask = _booksServices.GetBooksAsync(PageNo ?? 1, PageSizes, GenreSlug);
          var popularTask = _booksServices.GetPopulBooksAsync(10, GenreSlug);

            _genres = await genresTask;
             (_books, _totalCount) = await bookTask;
            _popularbooks = await popularTask;

            if (!string.IsNullOrWhiteSpace(GenreSlug))
            {
                var selectedGeners =  _genres.FirstOrDefault( g => g.Slug == GenreSlug );
                if (selectedGeners.Name is not null)
                {
                    _heading = $"{selectedGeners.Name} books";
                }
                else
                {
                    _heading = "Top Books";
                }
            }
        }
    }

}