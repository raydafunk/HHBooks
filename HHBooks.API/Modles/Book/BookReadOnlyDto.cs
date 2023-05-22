namespace HHBooks.API.Modles.Book
{
    public class BookReadOnlyDto : BaseDto
    {
        public string? Title { get; set; }
        public string Image { get; set; } = null!;
        public double? Price { get; set; }
        public int? AuthorId { get; set; }
        public string? AutorName { get; set;}

    }
}
