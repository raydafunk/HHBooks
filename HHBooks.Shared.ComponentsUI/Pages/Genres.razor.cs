using HHBook.Shared.Dtos;
using HHBook.Shared.Interfaces;
using Microsoft.AspNetCore.Components;

namespace HHBooks.Shared.ComponentsUI.Pages
{
    public partial class Genres
    {
        private GenreDto[] _genres = [];

        [Inject]
        private IBooksServices? _booksServices { get; set; }

        protected override async Task OnInitializedAsync()
        {
          _genres = await _booksServices!.GetGenreAsync(topOnly: false);
        }

    }
}