﻿using System.ComponentModel.DataAnnotations;

namespace HHBooks.API.Modles.Book
{
    public class UpdateBookDto : BaseDto
    {
        [Required]
        [StringLength(50)]
        public string? Title { get; set; }
        [Required]
        public int? Year { get; set; }
        [Required]
        public string? Isbn { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string? Summary { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public double? Price { get; set; }
    }
}
