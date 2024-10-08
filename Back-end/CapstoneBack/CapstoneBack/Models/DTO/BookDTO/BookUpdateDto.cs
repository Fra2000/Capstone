﻿using CapstoneBack.Models.DTO.AuthorDTO;
using CapstoneBack.Models.DTO.GenreDTO;

namespace CapstoneBack.Models.DTO.BookDTO
{
    public class BookUpdateDto
    {
        public string? Name { get; set; }
        public int? NumberOfPages { get; set; }
        public string? Description { get; set; }
        public int? AuthorId { get; set; }
        public DateTime? PublicationDate { get; set; }
        public decimal? Price { get; set; }
        public int? AvailableQuantity { get; set; }
        public List<int> GenreIds { get; set; }
    }
}
