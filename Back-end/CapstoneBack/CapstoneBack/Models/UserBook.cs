using CapstoneBack.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneBack.Models
{
    public class UserBook
    {
        [Key]
        public int UserBookId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }

        public DateTime PurchaseDate { get; set; }
        public int Quantity { get; set; } = 1;

    }
}
