﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneBack.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public int NumberOfPages { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string Author { get; set; }

        public DateTime PublicationDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public ICollection<BookGenre> BookGenres { get; set; }
       
        public ICollection<UserBook> UserBooks { get; set; }

        public ICollection<UserBookStatus> UserBookStatuses { get; set; }
    }
}
