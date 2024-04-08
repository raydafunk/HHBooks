using HHBook.Shared.Dtos;
using HHBook.Shared.Interfaces;
using Microsoft.AspNetCore.Components;

namespace HHBooks.Shared.ComponentsUI.Pages
{
    
    public partial class BookDetailsComponent
    {
        [Parameter]
        public int BookId { get; set; }

        private BookDetailsDto? _book;
        private BookListDto[] _simialrBook = [];

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Inject]
        private IBooksServices? _booksServices { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (BookId <= 0)
            {
                NavigationManager!.NavigateTo("/");
                return;
            }
            var simalerBookTask = _booksServices!.GetSimilarBooksAsync(BookId, 6);
            _book = await  _booksServices.GetBooksAsync(BookId);
            _simialrBook = await simalerBookTask;

            if(_book is null)
            {
                NavigationManager!.NavigateTo("/");
            }
            
        }
    }
}