using System;
using System.Collections.Generic;

namespace HHBooks.API.Data;

public partial class Author
{
    public int AuthorsId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Bio { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
