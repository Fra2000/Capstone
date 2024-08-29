using CapstoneBack.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneBack.Models
{
    public class UserBookStatus
    {
        [Key]
        public int UserBookStatusId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        public int? CurrentPage { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
