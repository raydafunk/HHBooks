﻿namespace HHBooks.API.Modles.Author
{
    public class AutorReadOnlyDto : BaseDto
    {

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Bio { get; set; }
    }
}
