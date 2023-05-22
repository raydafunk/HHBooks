namespace HHBooks.API.Modles.Book
{
    public class BookDetailsDto : BaseDto
    {
        public string? Title { get; set; }
        public int Year { get; set; }
        public string? Isbn { get; set; }
        public string? Summary { get; set; }
        public string Image { get; set; } = null!;
        public double? Price { get; set; }
        public int AuthorId { get; set; }
        public string? AutorName { get; set; }
    }
}
