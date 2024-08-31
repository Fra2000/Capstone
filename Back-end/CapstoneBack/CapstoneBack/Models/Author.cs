using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapstoneBack.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [StringLength(1000)]
        public string Bio { get; set; }

        public string ImagePath { get; set; }

        public ICollection<Book> Books { get; set; } 

        public string FullName => $"{FirstName} {LastName}"; 
    }
}
